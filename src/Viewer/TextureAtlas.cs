using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace TextureAtlasBuilder
{
    class TextureAtlas
    {
        public struct AtlasEntry
        {
            public uint texID;
            public int x, y, width, height;
        }

        private struct AtlasRect
        {
            public int X, Y, W, H;
        }

        private struct FilledTex
        {
            public AtlasRect rect;
            public uint localID, actualID;
        }

        private struct AreaRow
        {
            public List<AtlasRect> EmptySpaces;
            public List<FilledTex> FilledSpaces;
            public int yPos, width, height;
        }

        private Bitmap atlas;
        public Bitmap Image { get { return atlas; } }
        private List<AtlasEntry> entries = new List<AtlasEntry>();
        public List<AtlasEntry> Entries { get { return entries; } }
        private int area = 0;
        private int squareLen = 0;

        public TextureAtlas(List<Bitmap> textures)
        {
            if (textures.Count > 0)
            {
                for(int i = 0; i < textures.Count; i++)
                    textures[i].Tag = (uint)i; // Needed for stable sorting
                
                // Sort material list by height (and width) in descending order
                textures.Sort((x, y) => {
                    var ret = y.Height.CompareTo(x.Height);
                    if (ret == 0) ret = y.Width.CompareTo(x.Width);
                    if (ret == 0)
                    { // Needed for stable sorting
                        uint xTag = (uint)x.Tag;
                        uint yTag = (uint)y.Tag;
                        ret = xTag.CompareTo(yTag);
                    }
                    return ret;
                });

                area = atlusArea(ref textures);
                squareLen = atlusSideLen(area);
                //Console.WriteLine("Atlus square size: " + squareLen + "x" + squareLen);
                buildAtlas(ref textures, squareLen, squareLen);
            }
        }
        
        public AtlasEntry getEntryFromID(uint texId)
        {
            foreach (AtlasEntry entry in entries)
            {
                if (texId == entry.texID)
                    return entry;
            }
            return entries[0];
        }
        
        private AtlasRect newRect(int x, int y, int w, int h)
        {
            AtlasRect rect = new AtlasRect();
            rect.X = x;
            rect.Y = y;
            rect.W = w;
            rect.H = h;
            return rect;
        }

        private AreaRow newAreaRow(int yPos, int width, int height)
        {
            AreaRow r = new AreaRow();
            r.EmptySpaces = new List<AtlasRect>();
            r.EmptySpaces.Add(newRect(0, yPos, width, height));
            r.FilledSpaces = new List<FilledTex>();
            r.yPos = yPos;
            r.width = width;
            r.height = height;
            return r;
        }

        private AtlasEntry newEntry(uint texID, int x, int y, int width, int height)
        {
            AtlasEntry a = new AtlasEntry();
            a.texID = texID;
            a.x = x;
            a.y = y;
            a.width = width;
            a.height = height;
            return a;
        }

        private void buildAtlas(ref List<Bitmap> textures, int width, int height)
        {
            List<AreaRow> texRows = new List<AreaRow>();
            int curYPos = 0;
            uint texCount = 0;
            foreach (Bitmap bmp in textures)
            {
                if (texCount == 0)
                    texRows.Add(newAreaRow(curYPos, width, bmp.Height));
                
                bool hasBeenSlotted = false;
                while (!hasBeenSlotted)
                {
                    foreach (AreaRow texRow in texRows)
                    {
                        if (hasBeenSlotted) break;
                        int esi = texCanFit(texRow, bmp);
                        if (esi > -1)
                        {
                            AreaRow ar = texRow;
                            Bitmap mr = bmp;
                            insertIntoEmptySpace(ref ar, ref mr, esi, texCount);
                            hasBeenSlotted = true;
                        }
                    }
                    if (!hasBeenSlotted)
                    {
                        curYPos += texRows[texRows.Count - 1].height;
                        texRows.Add(newAreaRow(curYPos, width, bmp.Height));
                    }
                }

                texCount++;
            }
            curYPos += texRows[texRows.Count - 1].height;
            //System.Console.WriteLine("current Y pos: " + curYPos);
            atlas = new Bitmap(width, Math.Min(nextPowerOfTwo(curYPos), height));
            foreach (AreaRow r in texRows)
            {
                //Console.WriteLine("Row number " + (++rNum) + ", RowHeight = "+ r.height);
                foreach (FilledTex ft in r.FilledSpaces)
                {
                    entries.Add(newEntry(ft.actualID, ft.rect.X, ft.rect.Y, ft.rect.W, ft.rect.H));
                    drawOnAtlas(textures[(int)ft.localID], 16, ft.rect.X, ft.rect.Y);
                }
            }
        }

        public void drawOnAtlas(Bitmap bmp, int bpp, int xOffset, int yOffset)
        {
            Graphics g = Graphics.FromImage(atlas);
            g.CompositingMode = CompositingMode.SourceOver;
           // bmp.MakeTransparent();
            g.DrawImage(bmp, new Point(xOffset, yOffset));
        }

        private int texCanFit(AreaRow row, Bitmap bmp)
        {
            int entry = 0;
            foreach (AtlasRect empty in row.EmptySpaces)
            {
                if (bmp.Width <= empty.W && bmp.Height <= empty.H)
                    return entry;
                else
                    entry++;
            }
            return -1;
        }

        private void insertIntoEmptySpace(ref AreaRow row, ref Bitmap bmp, int emptySpaceIndex, uint newLocalID)
        {
            AtlasRect EmptySpaceRect = row.EmptySpaces[emptySpaceIndex];
            FilledTex newTex = new FilledTex();
            newTex.rect = newRect(
                EmptySpaceRect.X,
                EmptySpaceRect.Y,
                bmp.Width,
                bmp.Height
            );

            newTex.localID = newLocalID;
            newTex.actualID = (uint)bmp.Tag;
            /*
            Console.WriteLine("Adding new tex! (X/Y/W/H/LID: " +
                    EmptySpaceRect.X + "/" +
                    EmptySpaceRect.Y + "/" +
                    bmp.Width + "/" +
                    bmp.Height + "/" +
                    newTex.localID.ToString("X2") + ")"
            );
            */
            row.FilledSpaces.Add(newTex);
            row.EmptySpaces.RemoveAt(emptySpaceIndex); // Remove Old empty space;
            /*
            if (EmptySpaceRect.Width == newTex.rect.Width &&
                EmptySpaceRect.Height == newTex.rect.Height)
                return;
            */
            int yd = row.height - (newTex.rect.Y + newTex.rect.H);
            int xd = row.width - (newTex.rect.X + newTex.rect.W);

            if (yd > 0)
            {
                /*
                Console.WriteLine("Adding new empty space below!        (X/Y/W/H: "+
                    EmptySpaceRect.X + "/" +
                    (newTex.rect.Y + newTex.rect.H) + "/" +
                    newTex.rect.W + "/" +
                    yd + ")"
                );
                */
                AtlasRect newSpace = newRect(
                    EmptySpaceRect.X,
                    newTex.rect.Y + newTex.rect.H,
                    newTex.rect.W + (row.width-(EmptySpaceRect.X+newTex.rect.W)),
                    yd
                );

                if (spaceIsFree(row, newSpace))
                    row.EmptySpaces.Add(newSpace);
            }

            if (xd > 0)
            {
                /*
                Console.WriteLine("Adding new empty space to the right! (X/Y/W/H: " +
                    (newTex.rect.X + newTex.rect.W) + "/" +
                    EmptySpaceRect.Y + "/" +
                    xd + "/" +
                    newTex.rect.H + ")"
                );
                */
                AtlasRect newSpace = newRect(
                    newTex.rect.X + newTex.rect.W,
                    EmptySpaceRect.Y,
                    xd,
                    newTex.rect.H
                );

                if (spaceIsFree(row, newSpace))
                    row.EmptySpaces.Add(newSpace);
            }

            row.EmptySpaces.Sort((x, y) => {
                var ret = x.H.CompareTo(y.H);
                if (ret == 0) ret = x.W.CompareTo(y.W);
                return ret;
            });
        }

        private bool testAABB(AtlasRect rect1, AtlasRect rect2)
        {
            return (rect1.X < rect2.X + rect2.W &&
                    rect1.X + rect1.W > rect2.X &&
                    rect1.Y < rect2.Y + rect2.H &&
                    rect1.H + rect1.Y > rect2.Y);
        }

        private bool spaceIsFree(AreaRow row, AtlasRect rect)
        {
            foreach (FilledTex tex in row.FilledSpaces)
                if (testAABB(rect, tex.rect))
                    return false;

            foreach (AtlasRect empty in row.EmptySpaces)
                if (testAABB(rect, empty))
                    return false;

            return true;
        }

        private int nextPowerOfTwo(int v)
        {
            v--;
            v |= v >> 1;
            v |= v >> 2;
            v |= v >> 4;
            v |= v >> 8;
            v |= v >> 16;
            v++;
            return v;
        }

        private int atlusArea(ref List<Bitmap> textures)
        {
            int len = 0;
            foreach (Bitmap bmp in textures)
            {
                len += bmp.Width * bmp.Height;
            }
            return len;
        }

        private int atlusSideLen(int area)
        {
            return nextPowerOfTwo((int)Math.Sqrt(area));
        }

        public void outputToPNG(string filename)
        {
            if (filename != string.Empty && atlas != null)
            {
                atlas.Save(filename, ImageFormat.Png);
            }
        }

    }
}

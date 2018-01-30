using Collada141;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Quad64
{
    class DumpModel
    {
        private static double _scale = 1.0;

        private static param MakeParam(string name, string type)
        {
            param p = new param();
            p.name = name;
            p.type = type;
            return p;
        }

        public static accessor MakePosAccessor(ulong count, string source)
        {
            accessor acc = new accessor();
            acc.stride = 3;
            acc.count = count;
            acc.source = "#" + source;
            acc.param = new param[] {
                MakeParam("X", "float"),
                MakeParam("Y", "float"),
                MakeParam("Z", "float")
            };
            return acc;
        }

        public static accessor MakeTexCoordAccessor(ulong count, string source)
        {
            accessor acc = new accessor();
            acc.stride = 2;
            acc.count = count;
            acc.source = "#" + source;
            acc.param = new param[] {
                MakeParam("S", "float"),
                MakeParam("T", "float")
            };
            return acc;
        }

        public static accessor MakeColorAccessor(ulong count, string source)
        {
            accessor acc = new accessor();
            acc.stride = 3;
            acc.count = count;
            acc.source = "#" + source;
            acc.param = new param[] {
                MakeParam("R", "float"),
                MakeParam("G", "float"),
                MakeParam("B", "float")
            };
            return acc;
        }

        public static source makePositionsSource(ref Model3D mdl)
        {
            source src = new source();
            src.id = "positions_source";
            float_array fa = new float_array();
            fa.id = "positions";

            List<double> values = new List<double>();
            for (int id_num = 0; id_num < mdl.meshes.Count; id_num++)
            {
                int len = mdl.meshes[id_num].vertices.Length;
                for (int i = 0; i < len; i++)
                {
                    values.Add(mdl.meshes[id_num].vertices[i].X * _scale);
                    values.Add(mdl.meshes[id_num].vertices[i].Y * _scale);
                    values.Add(mdl.meshes[id_num].vertices[i].Z * _scale);
                }
            }
            fa.Values = values.ToArray();
            fa.count = (ulong)fa.Values.LongLength;

            src.technique_common = new sourceTechnique_common();
            src.technique_common.accessor = MakePosAccessor(fa.count / 3, fa.id);
            src.Item = fa;
            return src;
        }

        public static source makeTexCoordSource(ref Model3D mdl)
        {

            source src = new source();
            src.id = "texCoord_source";
            float_array fa = new float_array();
            fa.id = "texCoord";

            List<double> values = new List<double>();
            for (int id_num = 0; id_num < mdl.meshes.Count; id_num++)
            {
                bool clampX = mdl.meshes[id_num].texture.TextureParamS == 33071;
                bool clampY = mdl.meshes[id_num].texture.TextureParamT == 33071;
                int len = mdl.meshes[id_num].colors.Length;
                for (int i = 0; i < len; i++)
                {
                    float X = mdl.meshes[id_num].texCoord[i].X;
                    float Y = mdl.meshes[id_num].texCoord[i].Y;
                    if (clampX)
                        X = (X > 1.0f ? 1.0f : (X < 0.0f ? 0.0f : X));
                    if (clampY)
                        Y = (Y > 1.0f ? 1.0f : (Y < 0.0f ? 0.0f : Y));

                    values.Add(X);
                    values.Add(-Y);
                }
            }
            fa.Values = values.ToArray();
            fa.count = (ulong)fa.Values.LongLength;
            
            src.technique_common = new sourceTechnique_common();
            src.technique_common.accessor = MakeTexCoordAccessor(fa.count / 2, fa.id);
            src.Item = fa;
            return src;
        }

        public static source makeColorSource(ref Model3D mdl)
        {
            source src = new source();
            src.id = "color_source";
            float_array fa = new float_array();
            fa.id = "color";

            List<double> values = new List<double>();
            for (int id_num = 0; id_num < mdl.meshes.Count; id_num++)
            {
                int len = mdl.meshes[id_num].colors.Length;
                for (int i = 0; i < len; i++)
                {
                    values.Add(mdl.meshes[id_num].colors[i].X);
                    values.Add(mdl.meshes[id_num].colors[i].Y);
                    values.Add(mdl.meshes[id_num].colors[i].Z);
                }
            }
            fa.Values = values.ToArray();
            fa.count = (ulong)fa.Values.LongLength;

            src.technique_common = new sourceTechnique_common();
            src.technique_common.accessor = MakeColorAccessor(fa.count / 3, fa.id);
            src.Item = fa;
            return src;
        }

        public static InputLocal makeInput(string source, string semantic)
        {
            InputLocal input = new InputLocal();
            input.source = "#" + source;
            input.semantic = semantic;
            return input;
        }

        public static InputLocalOffset makeInputOffset(string source, string semantic, 
            ulong offset, bool setSpecified, ulong set)
        {
            InputLocalOffset input = new InputLocalOffset();
            input.source = "#" + source;
            input.semantic = semantic;
            input.offset = offset;
            input.set = set;
            input.setSpecified = setSpecified;
            return input;
        }

        public static vertices MakeVertices()
        {
            vertices verts = new vertices();
            verts.id = "vertices";
            verts.input = new InputLocal[] { makeInput("positions_source", "POSITION") };
            return verts;
        }

        public static polylist MakePolyList(ulong id_num, uint[] indices, ref uint largest_offset)
        {
            polylist plist = new polylist();
            plist.material = "MaterialInstance_" + id_num;
            plist.input = new InputLocalOffset[] {
                makeInputOffset("vertices", "VERTEX", 0, false, 0),
                makeInputOffset("texCoord_source", "TEXCOORD", 1, true, 0),
                makeInputOffset("color_source", "COLOR", 2, true, 0)
            };
            string p = "", vcount = "";
            long len = indices.LongLength;
            uint largest = largest_offset;
            for (long i = 0; i < len; i++)
            {
                if(i % 3 == 0)
                    vcount += "3 ";
                uint index = indices[i] + largest_offset;
                largest = Math.Max(largest, indices[i] + largest_offset);
                p += index.ToString() + " " + index.ToString() + " " + index.ToString();
                if (i < len - 1)
                    p += " ";
            }
            largest_offset = largest + 1;

            plist.vcount = vcount;
            plist.count = (ulong)(indices.LongLength / 3);
            plist.p = p;
            
            return plist;
        }

        public static library_geometries MakeGeometryLibrary(ref Model3D mdl)
        {
            library_geometries lib_geo = new library_geometries();
            //lib_geo.id = "lib_geo";
            ulong len = (ulong)mdl.builder.TextureImages.Count;
            
            geometry geo = new geometry();

            //for (ulong i = 0; i < len; i++)
            //{
            geo = new geometry();
            geo.id = "geometry";
            mesh m = new mesh();

            m.source = new source[] {
                makePositionsSource(ref mdl),
                makeTexCoordSource(ref mdl),
                makeColorSource(ref mdl)
            };

            m.vertices = MakeVertices();
            List<object> polyLists = new List<object>();
            uint indices_offset = 0;
            for (ulong i = 0; i < len; i++)
            {
                polyLists.Add(MakePolyList(i, mdl.meshes[(int)i].indices, ref indices_offset));
                //indices_offset += (uint)mdl.meshes[(int)i].indices.LongLength;
            }
            m.Items = polyLists.ToArray();
            geo.Item = m;

            lib_geo.geometry = new geometry[] { geo };
            return lib_geo;
        }

        private static image MakeImage(ulong id_num, string modelName)
        {
            image img = new image();
            img.id = "image_"+id_num;
            img.Item = modelName + "/" + id_num + ".png";
            return img;
        }

        private static library_images MakeImagesLibrary(ref Model3D mdl, string modelName)
        {
            library_images lib_img = new library_images();
            int len = mdl.builder.TextureImages.Count;
            
            image[] imgs = new image[len];
            for (ulong i = 0; i < (ulong)imgs.LongLength; i++)
                imgs[i] = MakeImage(i, modelName);
            lib_img.image = imgs;
            return lib_img;
        }
        
        private static common_newparam_type MakeNewParamForEffect(ulong id_num, bool isSampler2D)
        {
            common_newparam_type newparam = new common_newparam_type();
            if (isSampler2D)
            {
                fx_sampler2D_common sampler2D = new fx_sampler2D_common();
                sampler2D.source = "surface_" + id_num;
                newparam.Item = sampler2D;
                newparam.ItemElementName = ItemChoiceType.sampler2D;
                newparam.sid = "sampler2D_" + id_num;
            }
            else
            {
                fx_surface_init_from_common initFrom = new fx_surface_init_from_common();
                fx_surface_common surface = new fx_surface_common();
                initFrom.Value = "image_" + id_num;
                surface.type = fx_surface_type_enum.Item2D;
                surface.init_from = new fx_surface_init_from_common[] {
                    initFrom
                };
                newparam.Item = surface;
                newparam.ItemElementName = ItemChoiceType.surface;
                newparam.sid = "surface_" + id_num;
            }
            return newparam;
        }

        private static effectFx_profile_abstractProfile_COMMON MakeProfileCOMMON(ulong id_num)
        {
            effectFx_profile_abstractProfile_COMMON proCom = new effectFx_profile_abstractProfile_COMMON();
            proCom.technique = new effectFx_profile_abstractProfile_COMMONTechnique();
            effectFx_profile_abstractProfile_COMMONTechniqueLambert lambert = new effectFx_profile_abstractProfile_COMMONTechniqueLambert();
            common_color_or_texture_type diffuse = new common_color_or_texture_type();
            common_color_or_texture_typeTexture tex = new common_color_or_texture_typeTexture();
            tex.texture = "sampler2D_"+ id_num;
            diffuse.Item = tex;
            lambert.diffuse = diffuse;
            proCom.Items = new object[] {
                MakeNewParamForEffect(id_num, false),
                MakeNewParamForEffect(id_num, true)
            };
            proCom.technique.Item = lambert;
            return proCom;
        }

        private static effect MakeEffect(ulong id_num)
        {
            effect eff = new effect();
            eff.id = "effect_" + id_num;
            eff.Items = new effectFx_profile_abstractProfile_COMMON[]
            {
                MakeProfileCOMMON(id_num)
            };
            return eff;
        }

        private static library_effects MakeEffectsLibrary(ref Model3D mdl, string modelName)
        {
            library_effects lib_eff = new library_effects();

            int len = mdl.builder.TextureImages.Count;
            
            effect[] effects = new effect[len];
            for (ulong i = 0; i < (ulong)effects.LongLength; i++)
                effects[i] = MakeEffect(i);

            lib_eff.effect = effects;
            return lib_eff;
        }

        private static material MakeMaterial(ulong id_num)
        {
            material mat = new material();
            mat.name = "Material_" + id_num;
            mat.id = "material_" + id_num;
            instance_effect ieffect = new instance_effect();
            ieffect.url = "#effect_" + id_num;
            mat.instance_effect = ieffect;
            return mat;
        }

        private static library_materials MakeMaterialsLibrary(ref Model3D mdl)
        {
            library_materials lib_mats = new library_materials();

            int len = mdl.builder.TextureImages.Count;
            
            material[] materials = new material[len];
            for (ulong i = 0; i < (ulong)materials.LongLength; i++)
                materials[i] = MakeMaterial(i);

            lib_mats.material = materials;
            return lib_mats;
        }

        private static bind_material MakeBindedMaterial(ref Model3D mdl)
        {
            bind_material bm = new bind_material();
            instance_material[] materials = new instance_material[mdl.builder.TextureImages.Count];
            for (int id_num = 0; id_num < materials.Length; id_num++)
            {
                materials[id_num] = new instance_material();
                materials[id_num].symbol = "MaterialInstance_" + id_num;
                materials[id_num].target = "#material_" + id_num;
            }

            bm.technique_common = materials;
            return bm;
        }

        private static library_visual_scenes MakeVisualScenesLibrary(ref Model3D mdl)
        {
            library_visual_scenes lib_visuals = new library_visual_scenes();
            visual_scene vs = new visual_scene();
            vs.id = "scene";
            node geo_node = new node();
            geo_node.id = "node";
            geo_node.name = "node";
            geo_node.type = NodeType.NODE;
            matrix transform_matrix = new matrix();
            transform_matrix.Values = new double[] {
                1, 0, 0, 0,
                0, 0,-1, 0,
                0, 1, 0, 0,
                0, 0, 0, 1
            };
            geo_node.Items = new object[] {
                transform_matrix
            };
            geo_node.ItemsElementName = new ItemsChoiceType2[] {
                ItemsChoiceType2.matrix
            };
            transform_matrix.sid = "transform";
            int len = mdl.builder.TextureImages.Count;

            instance_geometry geo = new instance_geometry();
            geo.url = "#geometry";
            geo.bind_material = MakeBindedMaterial(ref mdl);
            
            geo_node.instance_geometry = new instance_geometry[] { geo };

            vs.node = new node[] { geo_node };
            lib_visuals.visual_scene = new visual_scene[] { vs };
            return lib_visuals;
        }

        private static void WriteAllTextures(List<Bitmap> textures, string modelName)
        {
            for(int img_index = 0; img_index < textures.Count; img_index++)
            {
                string filepath = Directory.GetCurrentDirectory() + "\\" + modelName + "\\" + img_index + ".png";
                (new FileInfo(filepath)).Directory.Create();
                textures[img_index].Save(filepath, System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        public static void dumpModelToCOLLADA(Model3D mdl, float scale)
        {
            _scale = scale;
            COLLADA model = new COLLADA();
            model.scene = new COLLADAScene();
            model.scene.instance_visual_scene = new InstanceWithExtra();
            model.scene.instance_visual_scene.url = "#scene";
            model.asset = new asset();
            model.asset.unit = new assetUnit();
            model.asset.unit.meter = 1.0;
            model.asset.unit.name = "meter";
            model.asset.up_axis = UpAxisType.Z_UP;

            model.Items = new object[] {
                MakeImagesLibrary(ref mdl, "Test"),
                MakeEffectsLibrary(ref mdl, "Test"),
                MakeMaterialsLibrary(ref mdl),
                MakeGeometryLibrary(ref mdl),
                MakeVisualScenesLibrary(ref mdl)
            };
            WriteAllTextures(mdl.builder.TextureImages, "Test");
            model.Save("Test.dae");
        }
    }
}

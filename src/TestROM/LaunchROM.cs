using Quad64.src.JSON;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Quad64.src.TestROM
{
    class LaunchROM
    {
        public static void setEmulatorPath()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "Select N64 emulator program";
            openFileDialog1.InitialDirectory = "C:\\Program Files (x86)\\";
            openFileDialog1.Filter = "EXE|*.exe|All Files|*";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                Globals.pathToEmulator = openFileDialog1.FileName;
            }
        }

        public static void OpenEmulator()
        {
            bool runProgram = false;
            if (!Globals.pathToEmulator.Equals(""))
            {
                runProgram = true;
            }
            else
            {
                setEmulatorPath();
                if (!Globals.pathToEmulator.Equals(""))
                {
                    runProgram = true;
                }
            }
            if (runProgram)
            {
                Process p = new Process();
                p.StartInfo.FileName = Globals.pathToEmulator;
                p.StartInfo.Arguments = ROM.Instance.Filepath;
                p.StartInfo.UseShellExecute = true;
                p.Start();
            }
            
        }
    }
}

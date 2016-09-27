using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordea_Tunnusluvut_Patcher
{
    class Program
    {
        static void Main(string[] args)
        {
            //            RunProcess(
            //  @"C:\Windows\System32\Notepad.exe", // executable
            //  @"C:\Windows", // working directory
            //  "WindowsUpdate.log" // arguments
            //);

            Console.WriteLine("Coded by Razerman!\r\n");

            if (args.Length < 1)
            {
                Console.WriteLine("Usage: Nordea_Tunnusluvut_Patcher.exe <decompressed_apk_folder>");
                Console.WriteLine("Example: Nordea_Tunnusluvut_Patcher.exe \"nordea_140_nores\"");
                return;
            }

            Stopwatch sw = new Stopwatch();
            sw.Start();
            // get the file attributes for file or directory
            FileAttributes attr = File.GetAttributes(args[0]);

            //detect whether its a directory or file
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory && File.Exists(args[0] + "\\AndroidManifest.xml"))
            {
                PatchRootDetection(args[0]);

                sw.Stop();
                Console.WriteLine("Patched in: {0} ms", sw.ElapsedMilliseconds);
                Console.ReadKey();
            }
            else // TODO: Add option to decompress the APK file
                Console.WriteLine("Not a valid folder!");
        }

        /// <summary>Runs a process and waits for it to exit</summary>
        /// <param name="path">Path of the executable that will be run</param>
        /// <param name="workingDirectory">Working directory for the new process</param>
        /// <param name="arguments">Arguments the process will be started with</param>
        /// <returns>The process' exit code</returns>
        public static int RunProcess(string path, string workingDirectory, string arguments)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.WorkingDirectory = workingDirectory;
            processStartInfo.Arguments = arguments;
            processStartInfo.FileName = path;
            //processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            using (Process runningProcess = Process.Start(processStartInfo))
            {
                runningProcess.WaitForExit();

                return runningProcess.ExitCode;
            }
        }

        private static void PatchRootDetection(string folder)
        {
            Console.WriteLine("Patching Root Detection...");
            string[] foundFiles = FindFiles(folder, "\"7245034DFAD30FD3268D661F26097EB23E313D26270094EF30AA322881E6208EF76DE16BC3220C4D2C4CFDA5C05BD655\"");
            if(foundFiles.Length < 0 || foundFiles.Length > 1)
            {
                Console.WriteLine("Incorrect number of files matches the search query. Should match 1 but found {0} matches!", foundFiles.Length);
                return;
            }
            string[] failed = ReplaceAll(foundFiles[0], "const/4 v0, 0x1", "const/4 v0, 0x0");

            Console.SetCursorPosition(0, Console.CursorTop - 1);
            ClearCurrentConsoleLine();
            Console.WriteLine("Patched Root Detection.");

            if (failed != null && failed.Length > 0)
            {
                Console.WriteLine("Failed count for patch \"PatchRootDetection\": {0}", failed.Length);
                foreach (string fail in failed)
                    Console.WriteLine(fail);
            }
        }

        /// <summary>
        /// Replace every occurance in the folder and subfolders
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="search"></param>
        /// <param name="replace"></param>
        private static string[] ReplaceAll(string folder, string search, string replace)
        {
            string[] files = new string[1];

            // get the file attributes for file or directory
            FileAttributes attr = File.GetAttributes(folder);

            //detect whether its a directory or file
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                files = Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories);
            else
                files[0] = folder;

            List<string> failed_files = new List<string>();
            foreach (string file in files)
            {
                try
                {
                    if (!File.Exists(file))
                        continue;

                    string contents = File.ReadAllText(file);
                    if (contents.Contains(search))
                    {
                        contents = contents.Replace(search, replace);

                        // Make files writable
                        File.SetAttributes(file, FileAttributes.Normal);

                        File.WriteAllText(file, contents);
                    }
                }
                catch (Exception ex)
                {
                    failed_files.Add(String.Format("File: {0} | Failed: {1}", file, ex.Message));
                    Console.WriteLine(ex.Message);
                }
            }
            return (failed_files.ToArray());
        }

        /// <summary>
        /// Find files which contain the search string
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        private static string[] FindFiles(string folder, string search)
        {
            string[] files = new string[1];

            // get the file attributes for file or directory
            FileAttributes attr = File.GetAttributes(folder);

            //detect whether its a directory or file
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                files = Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories);
            else
                files[0] = folder;

            List<string> found_files = new List<string>();
            foreach (string file in files)
            {
                try
                {
                    if (!File.Exists(file))
                        continue;

                    string contents = File.ReadAllText(file);
                    if (contents.Contains(search))
                        found_files.Add(file);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return (found_files.ToArray());
        }

        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}

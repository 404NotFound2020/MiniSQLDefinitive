using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bundler
{
    class Program
    {
        public static string RelPathToSolutionRootFolder = "../../../";
        public static string morePath = "minisql-tester/bin/Release/";
        public static string serverPath = "TCPServer/bin/Release/";
        public static string clientPath= "ClientConsole/bin/Release/";

        public static string RootFolderInZip;

        public static void Main()
        {
            List<string> files = new List<string>();
            List<string> serverFiles = new List<string>();
            List<string> clientFiles = new List<string>();
            string version;
            version = GetVersion(RelPathToSolutionRootFolder + morePath + "minisql-tester.exe");
            RootFolderInZip = "All/";
            string TesterFolder = "PhaseTwoTester/";
            string ServerFolder = "Server/";
            string ClientFolder = "Client/";
            files.Add(RelPathToSolutionRootFolder + morePath + "minisql-tester.exe");
            files.Add(RelPathToSolutionRootFolder + morePath + "ClientConsole.exe");
            files.Add(RelPathToSolutionRootFolder + morePath + "MiniSQL.dll");

            serverFiles.Add(RelPathToSolutionRootFolder + serverPath + "MiniSQL.dll");
            serverFiles.Add(RelPathToSolutionRootFolder + serverPath + "NetworkUtilities.dll");
            serverFiles.Add(RelPathToSolutionRootFolder + serverPath + "TCPServer.exe");

            clientFiles.Add(RelPathToSolutionRootFolder + clientPath+ "NetworkUtilities.dll");
            clientFiles.Add(RelPathToSolutionRootFolder + clientPath + "ClientConsole.exe");
            string outputFile = RelPathToSolutionRootFolder + "404-" + version + ".zip";
            Console.WriteLine("Compressing files");
            Compress(new List<string> { RootFolderInZip + TesterFolder, RootFolderInZip + ServerFolder, RootFolderInZip + ClientFolder }, outputFile, new List<List<string>> {files, serverFiles, clientFiles }, new List<string> { morePath, serverPath, clientPath});
            Console.WriteLine("Finished");
        }

        public static string GetVersion(string file)
        {
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(file);

            return fvi.FileVersion;
        }

        public static void GetDependencies(string inFolder, string module, ref List<string> dependencyList, bool bRecursive = true)
        {
            string depName, modName;

            Assembly assembly = Assembly.LoadFrom(inFolder + module);

            foreach (AssemblyName assemblyName in assembly.GetReferencedAssemblies())
            {
                modName = assemblyName.Name + ".dll";
                depName = inFolder + modName;
                if (System.IO.File.Exists(depName) && !dependencyList.Exists(name => name == depName))
                {
                    dependencyList.Add(depName);
                    if (bRecursive)
                        GetDependencies(inFolder, modName, ref dependencyList, false);
                }
            }
        }

        public static List<string> GetFilesInFolder(string folder, bool bRecursive, string filter = "*.*")
        {
            List<string> files = new List<string>();

            if (bRecursive)
                files.AddRange(Directory.EnumerateFiles(folder, filter, SearchOption.AllDirectories));
            else
                files.AddRange(Directory.EnumerateFiles(folder));
            return files;
        }

        public static void Compress(List<string> dirNames, string outputFilename, List<List<string>> fileList, List<string> otherPath)
        {
            uint numFilesAdded = 0;           
            using (FileStream zipToOpen = new FileStream(outputFilename, FileMode.Create))
            {
                using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
                {
                    List<string> files;
                    for (int i = 0; i < fileList.Count; i++)
                    {
                        files = fileList[i];
                        double totalNumFiles = (double)files.Count;
                        foreach (string file in files)
                        {
                            if (System.IO.File.Exists(file))
                            {
                                archive.CreateEntryFromFile(file, dirNames[i] + file.Substring((RelPathToSolutionRootFolder + otherPath[i]).Length));//a lo chapuzas lo de morepath
                                numFilesAdded++;
                            }
                            else Console.WriteLine("Couldn't find file: {0}", file);

                            Console.Write("\rProgress: {0:F2}%", 100.0 * ((double)numFilesAdded) / totalNumFiles);
                        }
                        Console.WriteLine("\nSaving {0} files in  {1}", numFilesAdded, Path.GetFullPath(outputFilename));
                    }
                }
            }
        }
    }
}

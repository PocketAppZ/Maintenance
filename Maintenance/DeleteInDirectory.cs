using Logger;
using System;
using System.IO;
using static Maintenance.Properties.Settings;

namespace Maintenance
{
    public class DeleteInDirectory
    {
        public static void DeleteSetFiles()
        {
            // Delete Files and Folders in a Directory
            int deleteTemp = 0;
            foreach (string paths in Default.PathFilesToDelete)
            {
                try
                {
                    var filesPath = Environment.ExpandEnvironmentVariables(paths);
                    if (filesPath != "")
                    {
                        try
                        {
                            foreach (string file in Directory.GetFiles(filesPath))
                            {
                                try
                                {
                                    if (!file.Contains(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Temp"))
                                    {
                                        Logging.Info("Deleting file: " + file, "DeleteInDirectory");
                                        File.Delete(file);
                                    }
                                    else
                                    {
                                        deleteTemp++;
                                        if (deleteTemp == 1)
                                        {
                                            Logging.Info("Deleting temp files", "DeleteInDirectory");

                                            DirectoryInfo directory = new DirectoryInfo(Path.GetDirectoryName(file));

                                            DeleteTemp(directory);
                                        }
                                    }
                                }
                                catch (Exception)
                                {
                                    continue;
                                }
                            }
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                        foreach (string directory in Directory.GetDirectories(filesPath))
                        {
                            try
                            {
                                Logging.Info("Deleting directory: " + directory, "DeleteInDirectory");

                                Directory.Delete(directory, true);
                            }
                            catch (Exception)
                            {
                                continue;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }

        private static void DeleteTemp(DirectoryInfo directory)
        {
            foreach (FileInfo file in directory.GetFiles())
            {
                try
                {
                    if (DateTime.UtcNow - file.CreationTimeUtc > TimeSpan.FromHours(6))
                    {
                        file.Delete();
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }
            foreach (DirectoryInfo subDirectory in directory.GetDirectories())
            {
                try
                {
                    subDirectory.Delete(true);
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }
    }
}

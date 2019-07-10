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
            foreach (string paths in Default.PathFilesToDelete)
            {
                try
                {
                    if (Directory.Exists(paths))
                    {
                        var filesPath = Environment.ExpandEnvironmentVariables(paths);

                        // Files
                        foreach (string file in Directory.GetFiles(filesPath))
                        {
                            bool deleted = false;
                            try
                            {
                                File.Delete(file);
                                deleted = true;
                            }
                            catch (Exception ex)
                            {
                                deleted = false;
                                Logging.Error(file + " : " + ex, "DeleteInDirectory");
                                continue;
                            }
                            if (deleted)
                            {
                                Logging.Info("Deleting file: " + file, "DeleteInDirectory");
                            }
                        }

                        // Directories
                        foreach (string directory in Directory.GetDirectories(filesPath))
                        {
                            bool deleted = false;
                            try
                            {
                                Directory.Delete(directory, true);
                                deleted = true;
                            }
                            catch (Exception ex)
                            {
                                deleted = false;
                                Logging.Error(directory + " : " + ex, "DeleteInDirectory");
                                continue;
                            }
                            if (deleted)
                            {
                                Logging.Info("Deleting directory: " + directory, "DeleteInDirectory");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logging.Error(paths + " : " + ex, "DeleteInDirectory");
                    continue;
                }
            }
        }
    }
}

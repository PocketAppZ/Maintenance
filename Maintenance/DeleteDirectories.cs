using Logger;
using System;
using System.IO;
using static Maintenance.Properties.Settings;

namespace Maintenance
{
    public class DeleteDirectories
    {
        public static void DeleteSetPaths()
        {
            // Delete Files
            foreach (string dir in Default.DirectoriesToDelete)
            {
                bool deleted = false;
                if (Directory.Exists(dir))
                {
                    try
                    {
                        Directory.Delete(dir, true);
                        deleted = true;
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            foreach (var subDirectoryPath in Directory.GetDirectories(dir))
                            {
                                var directoryInfo = new DirectoryInfo(subDirectoryPath);
                                foreach (var filePath in directoryInfo.GetFiles())
                                {
                                    var file = new FileInfo(filePath.ToString());
                                    file.Attributes = FileAttributes.Normal;
                                }
                            }

                            Directory.Delete(dir, true);
                        }
                        catch (Exception)
                        {
                            deleted = false;
                            Logging.Error(dir + " : " + ex, "DeleteDirectories");
                            continue;
                        }
                    }
                    if (deleted)
                    {
                        Logging.Info("Deleting directory: " + dir, "DeleteDirectories");
                    }
                }
            }
        }
    }
}

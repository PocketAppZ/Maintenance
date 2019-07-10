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
            foreach (string dir in Default.FilesToDelete)
            {
                bool deleted = false;
                if (Directory.Exists(dir))
                {
                    try
                    {
                        Directory.Delete(dir);
                        deleted = true;
                    }
                    catch (Exception ex)
                    {
                        deleted = false;
                        Logging.Error(dir + " : " + ex, "DeleteDirectories");
                        continue;
                    }
                    if (deleted)
                    {
                        Logging.Info("Deleting file: " + dir, "DeleteFiles");
                    }
                }
            }
        }
    }
}

using Logger;
using System;
using System.IO;
using static Maintenance.Properties.Settings;

namespace Maintenance
{
    public class DeleteInDirectoryOlder
    {
        // Delete Files and Folders in a Directory older than x days
        public static void DeleteSetFiles()
        {
            foreach (string path in Default.PathFilesToDeleteOlder)
            {
                try
                {
                    if (Directory.Exists(path))
                    {
                        int days = Convert.ToInt32(path.Split(',')[0]);
                        var filesPath = Environment.ExpandEnvironmentVariables(path.Split(',')[1].Trim());

                        // Files
                        foreach (string f in Directory.GetFiles(filesPath, "*", SearchOption.AllDirectories))
                        {
                            bool deleted = false;
                            try
                            {
                                FileInfo fi = new FileInfo(f);
                                if (fi.LastWriteTime < DateTime.Now.AddDays(-days))
                                {
                                    File.Delete(f);
                                    deleted = true;
                                }
                            }
                            catch (Exception ex)
                            {
                                deleted = false;
                                Logging.Error(f + " : " + ex, "DeleteInDirectoryOlder");
                                continue;
                            }
                            if (deleted)
                            {
                                Logging.Info("Deleting file: " + f, "DeleteInDirectoryOlder");
                            }
                        }

                        // Directories
                        foreach (string d in Directory.GetDirectories(filesPath))
                        {
                            bool deleted = false;
                            try
                            {
                                DirectoryInfo fi = new DirectoryInfo(d);
                                if (fi.LastWriteTime < DateTime.Now.AddDays(-days))
                                {
                                    Directory.Delete(d, true);
                                    deleted = true;
                                }
                            }
                            catch (Exception ex)
                            {
                                deleted = false;
                                Logging.Error(d + " : " + ex, "DeleteInDirectoryOlder");
                                continue;
                            }
                            if (deleted)
                            {
                                Logging.Info("Deleting file: " + d, "DeleteInDirectoryOlder");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logging.Error(path + " : " + ex, "DeleteInDirectoryOlder");
                    continue;
                }
            }
        }
    }
}

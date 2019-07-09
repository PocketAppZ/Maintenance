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
            // Files to delete
            foreach (string path in Default.PathFilesToDeleteOlder)
            {
                try
                {
                    int days = Convert.ToInt32(path.Split(',')[0]);
                    var filesPath = Environment.ExpandEnvironmentVariables(path.Split(',')[1].Trim());

                    if (filesPath != "")
                    {
                        foreach (string f in Directory.GetFiles(filesPath, "*", SearchOption.AllDirectories))
                        {
                            try
                            {
                                FileInfo fi = new FileInfo(f);
                                if (fi.CreationTime < DateTime.Now.AddDays(-days))
                                {
                                    Logging.Info("Deleting file: " + f, "DeleteInDirectoryOlder");

                                    File.Delete(f);
                                }
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

            // Directories to delete
            foreach (string path in Default.PathFilesToDeleteOlder)
            {
                try
                {
                    int days = Convert.ToInt32(path.Split(',')[0]);
                    var filesPath = Environment.ExpandEnvironmentVariables(path.Split(',')[1].Trim());

                    if (filesPath != "")
                    {
                        foreach (string d in Directory.GetDirectories(filesPath))
                        {
                            try
                            {
                                DirectoryInfo fi = new DirectoryInfo(d);
                                if (fi.CreationTime < DateTime.Now.AddDays(-days))
                                {
                                    Logging.Info("Deleting file: " + d, "DeleteInDirectoryOlder");

                                    Directory.Delete(d, true);
                                }
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
    }
}

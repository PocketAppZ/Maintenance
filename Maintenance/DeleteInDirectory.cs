using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Maintenance
{
    public class DeleteInDirectory
    {
        public static void DeleteSetFiles()
        {
            // Delete Files and Folders in a Directory
            if (ConfigurationManager.GetSection("PathFilesToDelete") as NameValueCollection != null)
            {
                int deleteTemp = 0;
            Retry:;
                foreach (var paths in ConfigurationManager.GetSection("PathFilesToDelete") as NameValueCollection)
                {
                    try
                    {
                        string Variable = (ConfigurationManager.GetSection("PathFilesToDelete") as NameValueCollection).GetValues(paths.ToString()).FirstOrDefault();
                        var filesPath = Environment.ExpandEnvironmentVariables(Variable);
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
                                            Trace.WriteLine(DateTime.Now + "   |     Deleting file: " + file);
                                            File.Delete(file);
                                        }
                                        else
                                        {
                                            deleteTemp++;
                                            if (deleteTemp == 1)
                                            {
                                                Trace.WriteLine(DateTime.Now + "   |     Deleting temp files");

                                                DirectoryInfo directory = new DirectoryInfo(Path.GetDirectoryName(file));

                                                DeleteTemp(directory);
                                            }
                                        }
                                    }
                                    catch (FileNotFoundException)
                                    {
                                        continue;
                                    }
                                    catch (IOException)
                                    {
                                        continue;
                                    }
                                    catch (Exception)
                                    {
                                        foreach (var catcher in ConfigurationManager.GetSection("PathFilesToDeleteCatch") as NameValueCollection)
                                        {
                                            try
                                            {
                                                string catcherVariable = (ConfigurationManager.GetSection("PathFilesToDeleteCatch") as NameValueCollection).GetValues(catcher.ToString()).FirstOrDefault();

                                                var catcherPath = Environment.ExpandEnvironmentVariables(catcherVariable);
                                                if (catcherPath == filesPath)
                                                {
                                                    DialogResult result = MessageBox.Show(filesPath + " cannot be deleted at this time. Please check that a process is not holding it and then try again.", "Maintenance", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Exclamation);
                                                    if (result == DialogResult.Abort)
                                                    {
                                                        Environment.Exit(0);
                                                    }
                                                    else if (result == DialogResult.Retry)
                                                    {
                                                        goto Retry;
                                                    }
                                                    else if (result == DialogResult.Ignore)
                                                    {
                                                        continue;
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
                            catch (Exception)
                            {
                                continue;
                            }
                            foreach (string directory in Directory.GetDirectories(filesPath))
                            {
                                try
                                {
                                    Trace.WriteLine(DateTime.Now + "   |     Deleting directory: " + directory);
                                    Directory.Delete(directory, true);
                                }
                                catch (DirectoryNotFoundException)
                                {
                                    continue;
                                }
                                catch (IOException)
                                {
                                    continue;
                                }
                            }
                        }
                    }
                    catch (DirectoryNotFoundException)
                    {
                        continue;
                    }
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
                catch (IOException) { }
                catch (UnauthorizedAccessException) { }
            }
            foreach (DirectoryInfo subDirectory in directory.GetDirectories())
            {
                try
                {
                    subDirectory.Delete(true);
                }
                catch (IOException) { }
                catch (UnauthorizedAccessException) { }
            }
        }
    }
}

using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Maintenance
{
    public class DeleteInDirectoryOlder
    {
        public static void DeleteSetFiles()
        {
            // Delete Files and Folders in a Directory older than x days
            if (ConfigurationManager.GetSection("PathFilesToDeleteOlder") as NameValueCollection != null)
            {
                foreach (var paths in ConfigurationManager.GetSection("PathFilesToDeleteOlder") as NameValueCollection)
                {
                    try
                    {
                        string Variable = (ConfigurationManager.GetSection("PathFilesToDeleteOlder") as NameValueCollection).GetValues(paths.ToString()).FirstOrDefault();
                        var filesPath = Environment.ExpandEnvironmentVariables(Variable);
                        if (filesPath != "")
                        {
                            // Files to delete
                            foreach (string f in Directory.GetFiles(filesPath, "*", SearchOption.AllDirectories))
                            {
                                if (ConfigurationManager.GetSection("PathFilesToDeleteDays") as NameValueCollection != null)
                                {
                                    foreach (var days in ConfigurationManager.GetSection("PathFilesToDeleteDays") as NameValueCollection)
                                    {
                                        string DaysNumberValue = (ConfigurationManager.GetSection("PathFilesToDeleteDays") as NameValueCollection).GetValues(days.ToString()).FirstOrDefault();
                                        int DaysNumber = Convert.ToInt32(DaysNumberValue);
                                        if (paths.ToString() == days.ToString())
                                        {
                                            try
                                            {
                                                FileInfo fi = new FileInfo(f);
                                                if (fi.CreationTime < DateTime.Now.AddDays(-DaysNumber))
                                                {
                                                    Trace.WriteLine(DateTime.Now + "   |     Deleting file: " + f);
                                                    File.Delete(f);
                                                }
                                            }
                                            catch (UnauthorizedAccessException)
                                            {
                                                continue;
                                            }
                                            catch (FileNotFoundException)
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
                            }
                            foreach (string file in Directory.GetFiles(filesPath))
                            {
                                if (ConfigurationManager.GetSection("PathFilesToDeleteDays") as NameValueCollection != null)
                                {
                                    foreach (var days in ConfigurationManager.GetSection("PathFilesToDeleteDays") as NameValueCollection)
                                    {
                                        string DaysNumberValue = (ConfigurationManager.GetSection("PathFilesToDeleteDays") as NameValueCollection).GetValues(days.ToString()).FirstOrDefault();
                                        int DaysNumber = Convert.ToInt32(DaysNumberValue);
                                        if (paths.ToString() == days.ToString())
                                        {
                                            try
                                            {
                                                FileInfo fi = new FileInfo(file);
                                                if (fi.CreationTime < DateTime.Now.AddDays(-DaysNumber))
                                                {
                                                    Trace.WriteLine(DateTime.Now + "   |     Deleting file: " + file);
                                                    File.Delete(file);
                                                }
                                            }
                                            catch (UnauthorizedAccessException)
                                            {
                                                continue;
                                            }
                                            catch (FileNotFoundException)
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
                            }
                            // Directories to delete
                            foreach (string d in Directory.GetDirectories(filesPath))
                            {
                                if (ConfigurationManager.GetSection("PathFilesToDeleteDays") as NameValueCollection != null)
                                {
                                    foreach (var days in ConfigurationManager.GetSection("PathFilesToDeleteDays") as NameValueCollection)
                                    {
                                        string DaysNumberValue = (ConfigurationManager.GetSection("PathFilesToDeleteDays") as NameValueCollection).GetValues(days.ToString()).FirstOrDefault();
                                        int DaysNumber = Convert.ToInt32(DaysNumberValue);

                                        if (paths.ToString() == days.ToString())
                                        {
                                            try
                                            {
                                                DirectoryInfo fi = new DirectoryInfo(d);
                                                if (fi.CreationTime < DateTime.Now.AddDays(-DaysNumber))
                                                {
                                                    Trace.WriteLine(DateTime.Now + "   |     Deleting file: " + d);
                                                    Directory.Delete(d, true);
                                                }
                                            }
                                            catch (UnauthorizedAccessException)
                                            {
                                                continue;
                                            }
                                            catch (FileNotFoundException)
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
    }
}

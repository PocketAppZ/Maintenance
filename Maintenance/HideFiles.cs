using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Maintenance
{
    public class HideFiles
    {
        public static void SetAsHidden()
        {
            // Hide Files
            if (ConfigurationManager.GetSection("FilesToHide") as NameValueCollection != null)
            {
                foreach (var hide in ConfigurationManager.GetSection("FilesToHide") as NameValueCollection)
                {
                    string filesToHide = (ConfigurationManager.GetSection("FilesToHide") as NameValueCollection).GetValues(hide.ToString()).FirstOrDefault();
                    var Variable = filesToHide;
                    var filePath = Environment.ExpandEnvironmentVariables(Variable);
                    if (filePath != "")
                    {
                        if (File.Exists(filePath))
                        {
                            if ((File.GetAttributes(filePath) & FileAttributes.Hidden) != FileAttributes.Hidden)
                            {
                                FileAttributes attributes = File.GetAttributes(filePath);
                                if (attributes != FileAttributes.Hidden || attributes != FileAttributes.System)
                                {
                                    Trace.WriteLine(DateTime.Now + "   |     Hiding file: " + filePath);
                                    File.SetAttributes(filePath, File.GetAttributes(filePath) | FileAttributes.Hidden);
                                    File.SetAttributes(filePath, File.GetAttributes(filePath) | FileAttributes.System);
                                }
                            }
                        }
                        else if (Directory.Exists(filePath))
                        {
                            if ((File.GetAttributes(filePath) & FileAttributes.Hidden) != FileAttributes.Hidden)
                            {
                                Trace.WriteLine(DateTime.Now + "   |     Hiding directory: " + filePath);
                                FileAttributes attributes = File.GetAttributes(filePath);
                                if (attributes != FileAttributes.Hidden || attributes != FileAttributes.System)
                                {
                                    File.SetAttributes(filePath, File.GetAttributes(filePath) | FileAttributes.Hidden);
                                    File.SetAttributes(filePath, File.GetAttributes(filePath) | FileAttributes.System);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

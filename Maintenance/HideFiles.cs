using Logger;
using System;
using System.IO;
using static Maintenance.Properties.Settings;

namespace Maintenance
{
    public class HideFiles
    {
        public static void SetAsHidden()
        {
            // Hide Files
            foreach (var hideFile in Default.FilesToHide)
            {
                var filePath = Environment.ExpandEnvironmentVariables(hideFile);
                if (filePath != "")
                {
                    if (File.Exists(filePath))
                    {
                        if ((File.GetAttributes(filePath) & FileAttributes.Hidden) != FileAttributes.Hidden)
                        {
                            FileAttributes attributes = File.GetAttributes(filePath);
                            if (attributes != FileAttributes.Hidden || attributes != FileAttributes.System)
                            {
                                Logging.Info("Hiding file: " + filePath, "HideFiles");

                                File.SetAttributes(filePath, File.GetAttributes(filePath) | FileAttributes.Hidden);
                                File.SetAttributes(filePath, File.GetAttributes(filePath) | FileAttributes.System);
                            }
                        }
                    }
                    else if (Directory.Exists(filePath))
                    {
                        if ((File.GetAttributes(filePath) & FileAttributes.Hidden) != FileAttributes.Hidden)
                        {
                            Logging.Info("Hiding directory: " + filePath, "HideFiles");

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

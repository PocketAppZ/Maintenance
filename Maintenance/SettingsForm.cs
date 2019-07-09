using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Windows.Forms;
using static Maintenance.Properties.Settings;

namespace Maintenance
{
    public partial class SettingsForm : Form
    {
        #region Entry Point and Settings

        public SettingsForm()
        {
            InitializeComponent();

            GetSettings();
        }

        private void GetSettings()
        {
            foreach (var item in Default.DirectoriesToDelete)
            {
                DirToDelBox.Items.Add(item);
            }

            foreach (var item in Default.FilesToDelete)
            {
                FilesToDelBox.Items.Add(item);
            }

            foreach (var item in Default.FilesToHide)
            {
                FilesHideBox.Items.Add(item);
            }

            foreach (var item in Default.PathFilesToDelete)
            {
                PathFilesDelBox.Items.Add(item);
            }

            foreach (var item in Default.PathFilesToDeleteOlder)
            {
                PathFilesDelOldBox.Items.Add(item);
            }

            foreach (var item in Default.ServicesToDisable)
            {
                ServicsDisableBox.Items.Add(item);
            }

            foreach (var item in Default.ServicesToManual)
            {
                ServicesManualBox.Items.Add(item);
            }

            foreach (var item in Default.TasksToDisable)
            {
                TasksDisableBox.Items.Add(item);
            }

            LoggingBox.Checked = Default.LoggingEnabled;
            DiskCheckBox.Checked = Default.RunDiskCheckMonthly;

            LoggingBox.CheckedChanged += LoggingBox_CheckedChanged;
            DiskCheckBox.CheckedChanged += DiskCheckBox_CheckedChanged;
        }

        #endregion Entry Point and Settings

        #region CheckBoxes and Help Button

        private void DiskCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (DiskCheckBox.Checked)
            {
                Default.RunDiskCheckMonthly = true;
            }
            else
            {
                Default.RunDiskCheckMonthly = false;
            }

            Default.Save();
        }

        private void LoggingBox_CheckedChanged(object sender, EventArgs e)
        {
            if (LoggingBox.Checked)
            {
                Default.LoggingEnabled = true;
            }
            else
            {
                Default.LoggingEnabled = false;
            }

            Default.Save();
        }

        private void HelpButton_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/xCONFLiCTiONx/Maintenance#help");
        }

        #endregion CheckBoxes and Help Button

        #region Browse Buttons

        private void DirDelBrowse_Click(object sender, EventArgs e)
        {
            DirectoryBrowser(DirToDelBox, Default.DirectoriesToDelete);
        }

        private void FilesDelBrowse_Click(object sender, EventArgs e)
        {
            FileBrowser(FilesToDelBox, Default.FilesToDelete);
        }

        private void FilesHideBrowse_Click(object sender, EventArgs e)
        {
            FileBrowser(FilesHideBox, Default.FilesToHide);
        }

        private void PathFilesDelBrowse_Click(object sender, EventArgs e)
        {
            DirectoryBrowser(PathFilesDelBox, Default.PathFilesToDelete);
        }

        private void PathFilesDelOldBrowse_Click(object sender, EventArgs e)
        {
            DirectoryBrowser(PathFilesDelOldBox, Default.PathFilesToDeleteOlder);
        }

        #endregion Browse Buttons

        #region Remove Buttons

        private void DirDelRemove_Click(object sender, EventArgs e)
        {
            DirToDelBox.Items.Remove(DirToDelBox.SelectedItem);
            foreach (string item in Default.DirectoriesToDelete)
            {
                if (item == DirToDelBox.SelectedItem.ToString())
                {
                    Default.DirectoriesToDelete.Remove(item);

                    break;
                }
            }

            Default.Save();
        }

        private void FilesDelRemove_Click(object sender, EventArgs e)
        {
            FilesToDelBox.Items.Remove(FilesToDelBox.SelectedItem);
            foreach (string item in Default.FilesToDelete)
            {
                if (item == FilesToDelBox.SelectedItem.ToString())
                {
                    Default.FilesToDelete.Remove(item);

                    break;
                }
            }

            Default.Save();
        }

        private void FilesHideRemove_Click(object sender, EventArgs e)
        {
            FilesHideBox.Items.Remove(FilesHideBox.SelectedItem);
            foreach (string item in Default.FilesToHide)
            {
                if (item == FilesHideBox.SelectedItem.ToString())
                {
                    Default.FilesToHide.Remove(item);

                    break;
                }
            }

            Default.Save();
        }

        private void PathFilesDelRemove_Click(object sender, EventArgs e)
        {
            PathFilesDelBox.Items.Remove(PathFilesDelBox.SelectedItem);
            foreach (string item in Default.PathFilesToDelete)
            {
                if (item == PathFilesDelBox.SelectedItem.ToString())
                {
                    Default.PathFilesToDelete.Remove(item);

                    break;
                }
            }

            Default.Save();
        }

        private void PathFilesDelOldRemove_Click(object sender, EventArgs e)
        {
            PathFilesDelOldBox.Items.Remove(PathFilesDelOldBox.SelectedItem);
            foreach (string item in Default.PathFilesToDeleteOlder)
            {
                if (item == PathFilesDelOldBox.SelectedItem.ToString())
                {
                    Default.PathFilesToDeleteOlder.Remove(item);

                    break;
                }
            }

            Default.Save();
        }

        private void ServicesDisableRemove_Click(object sender, EventArgs e)
        {
            ServicsDisableBox.Items.Remove(ServicsDisableBox.SelectedItem);
            foreach (string item in Default.ServicesToDisable)
            {
                if (item == ServicsDisableBox.SelectedItem.ToString())
                {
                    Default.ServicesToDisable.Remove(item);

                    break;
                }
            }
        }

        private void ServicesManualRemove_Click(object sender, EventArgs e)
        {
            ServicesManualBox.Items.Remove(ServicesManualBox.SelectedItem);
            foreach (string item in Default.ServicesToManual)
            {
                if (item == ServicesManualBox.SelectedItem.ToString())
                {
                    Default.ServicesToManual.Remove(item);

                    break;
                }
            }

            Default.Save();
        }

        private void TasksDisableRemove_Click(object sender, EventArgs e)
        {
            TasksDisableBox.Items.Remove(TasksDisableBox.SelectedItem);
            foreach (string item in Default.TasksToDisable)
            {
                if (item == TasksDisableBox.SelectedItem.ToString())
                {
                    Default.TasksToDisable.Remove(item);

                    break;
                }
            }

            Default.Save();
        }

        #endregion Remove Buttons

        #region Services and Tasks Buttons

        private void ServicesDisableButton_Click(object sender, EventArgs e)
        {
            bool ItemExists = false;

            var selected = ServicesTextBox.Text;
            if (selected != null)
            {
                foreach (string item in ServicsDisableBox.Items)
                {
                    if (item == selected)
                    {
                        ItemExists = true;
                        break;
                    }
                }
                if (!ItemExists)
                {
                    ServicsDisableBox.Items.Add(selected);
                    Default.ServicesToDisable.Add(selected);
                }
            }

            Default.Save();
        }

        private void ServicesManualButton_Click(object sender, EventArgs e)
        {
            bool ItemExists = false;

            var selected = ServicesTextBox.Text;
            if (selected != null)
            {
                foreach (string item in ServicesManualBox.Items)
                {
                    if (item == selected)
                    {
                        ItemExists = true;
                        break;
                    }
                }
                if (!ItemExists)
                {
                    ServicesManualBox.Items.Add(selected);
                    Default.ServicesToManual.Add(selected);
                }
            }

            Default.Save();
        }

        private void TasksDisableButton_Click(object sender, EventArgs e)
        {
            bool ItemExists = false;

            var selected = TasksTextBox.Text;
            if (selected != null)
            {
                foreach (string item in TasksDisableBox.Items)
                {
                    if (item == selected)
                    {
                        ItemExists = true;
                        break;
                    }
                }
                if (!ItemExists)
                {
                    TasksDisableBox.Items.Add(selected);
                    Default.TasksToDisable.Add(selected);
                }
            }

            Default.Save();
        }

        #endregion Services and Tasks Buttons

        #region File and Folder Browser Dialogs

        private void DirectoryBrowser(ComboBox comboBox, StringCollection collection)
        {
            using (FolderBrowserDialog browserDialog = new FolderBrowserDialog())
            {
                DialogResult result = browserDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string SelectedPath = browserDialog.SelectedPath;

                    bool ItemExists = false;

                    if (SelectedPath != null)
                    {
                        foreach (string item in comboBox.Items)
                        {
                            if (item == SelectedPath)
                            {
                                ItemExists = true;
                                break;
                            }
                        }
                        if (!ItemExists)
                        {
                            comboBox.Items.Add(SelectedPath);
                            collection.Add(SelectedPath);
                        }
                    }

                    Default.Save();
                }
            }
        }
        private void FileBrowser(ComboBox comboBox, StringCollection collection)
        {
            using (OpenFileDialog browserDialog = new OpenFileDialog())
            {
                DialogResult result = browserDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string SelectedPath = browserDialog.FileName;

                    bool ItemExists = false;

                    if (SelectedPath != null)
                    {
                        foreach (string item in comboBox.Items)
                        {
                            if (item == SelectedPath)
                            {
                                ItemExists = true;
                                break;
                            }
                        }
                        if (!ItemExists)
                        {
                            comboBox.Items.Add(SelectedPath);
                            collection.Add(SelectedPath);
                        }
                    }

                    Default.Save();
                }
            }
        }

        #endregion File and Folder Browser Dialogs
    }
}

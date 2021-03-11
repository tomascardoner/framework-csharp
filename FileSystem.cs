using System;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using IWshRuntimeLibrary;
using Microsoft.Win32;

namespace CardonerSistemas
{
    static class FileSystem
    {
        private static string folderDropbox;
        private static string folderGoogleDrive;
        private static string folderOneDrive;

        #region Path format functions

        internal static string PathAddBackslash(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            path = path.TrimEnd();

            if (PathEndsWithDirectorySeparator(path))
            {
                return path;
            }

            return path + GetDirectorySeparatorUsedInPath(path);
        }

        internal static bool PathEndsWithDirectorySeparator(string path)
        {
            if (path.Length == 0)
            { 
                    return false;
            }

            return path.EndsWith(Path.DirectorySeparatorChar.ToString()) || path.EndsWith(Path.AltDirectorySeparatorChar.ToString());
        }

        internal static string RemoveDirectorySeparatorAtEnd(string path)
        {
            if (PathEndsWithDirectorySeparator(path))
            {
                return RemoveDirectorySeparatorAtEnd(path.Substring(0, path.Length - 1));
            }
            else
            {
                return path;
            }
        }

        internal static char GetDirectorySeparatorUsedInPath(string path)
        {
            if (path.Contains(Path.AltDirectorySeparatorChar.ToString()))
            { 
                return Path.AltDirectorySeparatorChar;
            }

            return Path.DirectorySeparatorChar;
        }

        internal static void ShortcutAddToStartMenu(string filePath, string workingFolder, string subFolderName, string displayName, string iconFilePath = null, int iconFileIndex = 0)
        {
            string startMenuPath = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu);
            string appStartMenuPath;

            if (subFolderName.Length == 0)
            {
                appStartMenuPath = Path.Combine(startMenuPath, "Programs");
            }
            else
            {
                appStartMenuPath = Path.Combine(startMenuPath, "Programs", subFolderName);
                if (!Directory.Exists(appStartMenuPath))
                {
                    Directory.CreateDirectory(appStartMenuPath);
                }
            }
            string shortcutLocation = Path.Combine(appStartMenuPath, displayName + ".lnk");

            if (System.IO.File.Exists(shortcutLocation))
            {
                return;
            }

            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);

            shortcut.WorkingDirectory = workingFolder;
            if (iconFilePath == null)
            {
                shortcut.IconLocation = filePath;
            }
            else
            {
                shortcut.IconLocation = iconFilePath;
            }
            shortcut.TargetPath = filePath;
            shortcut.Save();
        }

        internal static void ShortcutAddToDesktop(string filePath, string workingFolder, string displayName, string iconFilePath = null, int iconFileIndex = 0)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string shortcutLocation = Path.Combine(desktopPath, displayName + ".lnk");

            if (System.IO.File.Exists(shortcutLocation))
            {
                return;
            }

            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);

            shortcut.WorkingDirectory = workingFolder;
            if (iconFilePath == null)
            {
                shortcut.IconLocation = filePath;
            }
            else
            {
                shortcut.IconLocation = iconFilePath;
            }
            shortcut.TargetPath = filePath;
            shortcut.Save();
        }

        #endregion

        #region Process folder name

        private const string FolderTagDropbox = "{Dropbox}";
        private const string FolderTagGoogleDrive = "{GoogleDrive}";
        private const string FolderTagOneDrive = "{OneDrive}";
        private const string FolderTagICloudDrive = "{iCloudDrive}";

        static internal string ProcessFolderName(string folderName)
        {
            string folderNameProcessed = folderName;
            
            if (folderName == null)
            {
                return "";
            }

            // Replace DropBox path
            if (folderName.Contains(FolderTagDropbox))
            {
                string dropboxFolder = "";
                if (GetDropboxPath(ref dropboxFolder))
                {
                    folderNameProcessed = folderName.Replace(FolderTagDropbox, dropboxFolder).Trim();
                }
            }

            // Replace Google Drive path
            if (folderName.Contains(FolderTagGoogleDrive))
            {
                string googleDriveFolder = "";
                if (GetGoogleDrivePath(ref googleDriveFolder))
                {
                    folderNameProcessed = folderName.Replace(FolderTagGoogleDrive, googleDriveFolder).Trim();
                }
            }

            // Replace OneDrive path
            if (folderName.Contains(FolderTagOneDrive))
            {
                string oneDriveFolder = "";
                if (GetOneDrivePath(ref oneDriveFolder))
                {
                    folderNameProcessed = folderName.Replace(FolderTagOneDrive, oneDriveFolder).Trim();
                }
            }

            return folderNameProcessed;
        }

        #endregion

        #region Cloud storage - Dropbox

        private class DropboxConfigInfoRoot
        {
            public DropboxConfigInfoPersonal personal { get; set; }
        }

        private class DropboxConfigInfoPersonal
        {
            public string path { get; set; }
            public long host { get; set; }
            public bool is_team { get; set; }
            public string subscription_type { get; set; }
        }

        internal static bool GetDropboxPath(ref string path)
        {
            if (folderDropbox != null)
            {
                path = folderDropbox;
                return true;
            }

            const string folderName = "Dropbox";
            const string configFilename = "info.json";

            string applicationDatafolder;
            string configFilePath;
            string configFileString;
            DropboxConfigInfoRoot configInfo;

            // Gets the path to the Dropbox config file
            applicationDatafolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            if (applicationDatafolder.Length != 0)
            {
                configFilePath = Path.Combine(applicationDatafolder, folderName, configFilename);

                if (System.IO.File.Exists(configFilePath))
                {
                    try
                    {
                        configFileString = System.IO.File.ReadAllText(configFilePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ha ocurrido un error al leer el archivo de configuraci�n de Dropbox ({configFilePath})\n\nError: {ex.Message}", CardonerSistemas.My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    try
                    {
                        configInfo = JsonSerializer.Deserialize<DropboxConfigInfoRoot>(configFileString);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ha ocurrido un error al interpretar el archivo de configuraci�n de Dropbox ({configFilename})\n\nError: {ex.Message}", CardonerSistemas.My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    if (configInfo.personal != null && configInfo.personal.path != null)
                    {
                        path = configInfo.personal.path;
                        folderDropbox = path;
                        return true;
                    }
                }
            }

            return false;
        }

        #endregion

        #region Cloud storage - Google Drive

        internal static bool GetGoogleDrivePath(ref string path)
        {
            if (folderGoogleDrive != null)
            {
                path = folderGoogleDrive;
                return true;
            }

            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Google\\Drive"))
                {
                    if (key != null)
                    {
                        Object value = key.GetValue("Path");
                        if (value != null)
                        {
                            path = value.ToString();
                            folderGoogleDrive = path;
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ha ocurrido un error al obtener la ubicaci�n de Google Drive desde el Registro de Windows.\n\nError: {ex.Message}", CardonerSistemas.My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        #endregion

        #region Cloud storage - OneDrive

        internal static bool GetOneDrivePath(ref string path)
        {
            if (folderOneDrive != null)
            {
                path = folderOneDrive;
                return true;
            }

            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\OneDrive"))
                {
                    if (key != null)
                    {
                        Object value = key.GetValue("UserFolder");
                        if (value != null)
                        {
                            path = value.ToString();
                            folderOneDrive = path;
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ha ocurrido un error al obtener la ubicaci�n de OneDrive desde el Registro de Windows.\n\nError: {ex.Message}", CardonerSistemas.My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        #endregion

    }
}
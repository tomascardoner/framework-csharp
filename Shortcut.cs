using System;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using IWshRuntimeLibrary;
using Microsoft.Win32;

namespace CardonerSistemas
{
    static class Shortcut
    {
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

    }
}
using System;
using System.Diagnostics;
using System.Reflection;
using System.Security.AccessControl;

namespace CardonerSistemas
{
    internal static class Registry
    {

        #region Declarations

        private static FileVersionInfo fvi;
        private static string applicationConfigurationSubKeyName;

        internal enum Keys
        {
            ClassesRoot,
            CurrentConfig,
            CurrentUser,
            LocalMachine,
            PerformanceData,
            Users
        }

        static Registry()
        {
            fvi = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
            applicationConfigurationSubKeyName = "Software\\" + fvi.CompanyName + "\\" + fvi.ProductName;
        }

        #endregion

        #region Common

        private static Microsoft.Win32.RegistryKey GetBaseKey(Keys key)
        {
            switch (key)
            {
                case Keys.ClassesRoot:
                    return Microsoft.Win32.Registry.ClassesRoot;
                case Keys.CurrentConfig:
                    return Microsoft.Win32.Registry.CurrentConfig;
                case Keys.CurrentUser:
                    return Microsoft.Win32.Registry.CurrentUser;
                case Keys.LocalMachine:
                    return Microsoft.Win32.Registry.LocalMachine;
                case Keys.PerformanceData:
                    return Microsoft.Win32.Registry.PerformanceData;
                case Keys.Users:
                    return Microsoft.Win32.Registry.Users;
                default:
                    return null;
            }
        }

        internal static Microsoft.Win32.RegistryKey OpenSubKey(Keys key, string name, bool createIfNotExist, bool writable)
        {
            Microsoft.Win32.RegistryKey subKey = OpenSubKey(key, name, writable);
            if (subKey == null)
            {
                try
                {
                    return GetBaseKey(key).CreateSubKey(name, Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree);
                }
                catch (Exception ex)
                {
                    CardonerSistemas.Error.ProcessError(ex, "Error al crear la clave en el Registro de Windows.");
                    return null;
                }
            }
            else
            {
                return subKey;
            }
        }

        internal static Microsoft.Win32.RegistryKey OpenSubKey(Keys key, string name, bool writable)
        {
            try
            {
                return GetBaseKey(key).OpenSubKey(name, writable);
            }
            catch (Exception ex)
            {
                CardonerSistemas.Error.ProcessError(ex, "Error al acceder al Registro de Windows.");
                return null;
            }
        }

        #endregion

        #region Load values

        internal static object LoadValue(Keys key, string subKeyName, string name, object defaultValue, bool closeKey, string errorMessage)
        {
            Microsoft.Win32.RegistryKey subKey = OpenSubKey(key, subKeyName, false);
            return  LoadValue(subKey, name, defaultValue, closeKey, errorMessage);
        }

        internal static object LoadValue(Microsoft.Win32.RegistryKey subKey, string name, object defaultValue, bool closeKey, string errorMessage)
        {
            try
            {
                if (subKey == null)
                {
                    return defaultValue;
                }
                else
                {
                    return subKey.GetValue(name, defaultValue);
                }
            }
            catch (Exception ex)
            {
                CardonerSistemas.Error.ProcessError(ex, errorMessage);
                return defaultValue;
            }
            finally
            {
                if (closeKey && subKey != null)
                {
                    subKey.Close();
                }
            }
        }

        internal static object LoadUserValue(string subKeyName, string name, object defaultValue, bool closeKey)
        {
            return LoadValue(Keys.CurrentUser, subKeyName, name, defaultValue, closeKey, "Error al leer el valor desde el Registro de Windows.");
        }

        internal static object LoadUserValueFromApplicationFolder(string subKeyName, string name, object defaultValue, bool closeKey)
        {
            return LoadUserValue(applicationConfigurationSubKeyName + (subKeyName == string.Empty ? string.Empty : "\\" + subKeyName), name, defaultValue, closeKey);
        }

        internal static object LoadMachineValue(string subKeyName, string name, object defaultValue, bool closeKey)
        {
            return LoadValue(Keys.LocalMachine, subKeyName, name, defaultValue, closeKey, "Error al leer el valor desde el Registro de Windows.");
        }
        internal static object LoadMachineValueFromApplicationFolder(string subKeyName, string name, object defaultValue, bool closeKey)
        {
            return LoadMachineValue(applicationConfigurationSubKeyName + (subKeyName == string.Empty ? string.Empty : "\\" + subKeyName), name, defaultValue, closeKey);
        }

        #endregion

        #region Save values

        internal static bool SaveValue(Keys key, string subKeyName, string name, object value, bool closeKey, string errorMessage)
        {
            Microsoft.Win32.RegistryKey subKey = OpenSubKey(key, subKeyName, true, true);
            return SaveValue(subKey, name, value, closeKey, errorMessage);
        }

        internal static bool SaveValue(Microsoft.Win32.RegistryKey subKey, string name, object value, bool closeKey, string errorMessage)
        {
            try
            {
                if (subKey == null)
                {
                    return false;
                }
                else
                {
                    subKey.SetValue(name, value);
                    return true;
                }
            }
            catch (Exception ex)
            {
                CardonerSistemas.Error.ProcessError(ex, errorMessage);
                return false;
            }
            finally
            {
                if (closeKey && subKey != null)
                {
                    subKey.Close();
                }
            }
        }

        internal static bool SaveUserValue(string subKeyName, string name, object value, bool closeKey)
        {
            return SaveValue(Keys.CurrentUser, subKeyName, name, value, closeKey, "Error al guardar el valor en el Registro de Windows.");
        }

        internal static bool SaveUserValueToApplicationFolder(string subKeyName, string name, object defaultValue, bool closeKey)
        {
            return SaveUserValue(applicationConfigurationSubKeyName + (subKeyName == string.Empty ? string.Empty : "\\" + subKeyName), name, defaultValue, closeKey);
        }

        internal static bool SaveMachineValue(string subKeyName, string name, object value, bool closeKey)
        {
            return SaveValue(Keys.LocalMachine, subKeyName, name, value, closeKey, "Error al guardar el valor en el Registro de Windows.");
        }

        internal static bool SaveMachineValueToApplicationFolder(string subKeyName, string name, object defaultValue, bool closeKey)
        {
            return SaveMachineValue(applicationConfigurationSubKeyName + (subKeyName == string.Empty ? string.Empty : "\\" + subKeyName), name, defaultValue, closeKey);
        }

        #endregion

    }
}

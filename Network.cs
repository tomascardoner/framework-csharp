using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CardonerSistemas
{
    static class Network
    {
        public static string FormatMACAddress(string macAddress)
        {
            string pattern = "(.{2})(.{2})(.{2})(.{2})(.{2})(.{2})";
            var replacement = "$1:$2:$3:$4:$5:$6";
            string formattedMACAddress = Regex.Replace(macAddress, pattern, replacement);     
            
            return formattedMACAddress;
        }
    }
}

using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CardonerSistemas
{
    static class Email
    {
        const string EmailValidationRegularExpression = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z_])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

        static private bool invalid = false;

        static public bool IsValidAddress(string address, string regularExpression = EmailValidationRegularExpression)
        {
            if (string.IsNullOrEmpty(address))
            {
                return false;
            }

            // Use IdnMapping class to convert Unicode domain names.
            try
            {
                address = Regex.Replace(address, "(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));
            }
            catch (System.Exception)
            {
                return false;
            }

            if (invalid)
            {
                return false;
            }

            // Return true if address is in valid e-mail format.
            try
            {
                return Regex.IsMatch(address, regularExpression, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (Exception)
            {
                return false;
            }
        }

        static private string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            IdnMapping idn = new IdnMapping();
            string domainName = match.Groups[2].Value;

            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (System.Exception)
            {
                invalid = true;
            }
            return match.Groups[1].Value + domainName;
        }

    }
}

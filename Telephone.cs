using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CardonerSistemas
{
    static class Telephone
    {
        const string PhoneNumberValidationRegularExpression = @"^\+[1-9]\d{1,14}$";

        static private bool invalid = false;

        static public bool IsValidNumber(string phoneNumber, string regularExpression = PhoneNumberValidationRegularExpression)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(phoneNumber, regularExpression, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}

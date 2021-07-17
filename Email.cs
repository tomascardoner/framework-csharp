using MimeKit;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CardonerSistemas
{
    static class Email
    {

        #region Address validation

        const string EmailValidationRegularExpression = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z_])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
        static private bool invalid = false;

        static internal bool IsValidAddress(string address, string regularExpression = EmailValidationRegularExpression)
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

        #endregion

        #region Sending

        static internal MailKit.Security.SecureSocketOptions GetSslOptionsEnumValue(int value)
        {
            if (Enum.IsDefined(typeof(MailKit.Security.SecureSocketOptions), value))
            {
                return (MailKit.Security.SecureSocketOptions)value;
            }
            else
            {
                return MailKit.Security.SecureSocketOptions.None;
            }
        }

        static internal bool Send(EmailConfig config, InternetAddressList recipientsTo, InternetAddressList recipientsCc, InternetAddressList recipientsBcc, string subject, bool isHtml, string body, MimeKit.AttachmentCollection attachments)
        {
            if (recipientsTo.Count == 0 && recipientsCc.Count == 0 && recipientsBcc.Count == 0)
            {
                return false;
            }

            MimeMessage message = new MimeMessage();
            BodyBuilder bodyBuilder = new BodyBuilder();
            MailKit.Net.Smtp.SmtpClient smtp = new MailKit.Net.Smtp.SmtpClient();

            // Set the recipients
            message.From.Add(new MailboxAddress(config.DisplayName, config.Address));
            message.To.AddRange(recipientsTo);
            message.Cc.AddRange(recipientsCc);
            message.Bcc.AddRange(recipientsBcc);

            // Set the properties and establish the Smtp server connection
            smtp.Timeout = config.SmtpTimeout;
            try
            {
                smtp.Connect(config.SmtpServer, config.SmtpPort, GetSslOptionsEnumValue(config.SmtpSslOptions));
            }
            catch (Exception ex)
            {
                Error.ProcessError(ex, "Error al conectar al servidor SMTP.");
                return false;
            }

            string decryptedPassword = string.Empty;
            if (!CardonerSistemas.Encrypt.StringCipher.Encrypt(config.SmtpPassword, Constants.PublicEncryptionPassword, ref decryptedPassword))
            {
                MessageBox.Show("La contraseña de e-mail (SMTP) especificada es incorrecta.", My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            try
            {
                smtp.Authenticate(new System.Net.NetworkCredential(config.SmtpUserName, decryptedPassword));
            }
            catch (Exception)
            {
                MessageBox.Show("Error al iniciar sesión en el servidor SMTP.", My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            // Set the content
            message.Subject = (subject == null ? string.Empty : subject);
            if (isHtml)
            {
                bodyBuilder.HtmlBody = body;
            }
            else
            {
                bodyBuilder.TextBody = body;
            }

            // Set the attachments
            if (attachments != null)
            {
                foreach (MimeEntity attachment in attachments)
                {
                    bodyBuilder.Attachments.Add(attachment);
                }
            }

            // Set the body
            message.Body = bodyBuilder.ToMessageBody();

            // Send the message
            try
            {
                smtp.Send(message);
            }
            catch (Exception)
            {
                MessageBox.Show("Error al enviar el e-mail.", My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            // Closes the Smtp server connection
            try
            {
                smtp.Disconnect(true);
            }
            catch (Exception)
            {
                MessageBox.Show("Error al desconectar al servidor SMTP.", My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            return true;
        }

        static internal bool Send(EmailConfig config, string recipientToDisplayName, string recipientToAddress, string recipientCcDisplayName, string recipientCcAddress, string recipientBccDisplayName, string recipientBccAddress, string subject, bool isHtml, string body, MimeKit.AttachmentCollection attachments)
        {
            InternetAddressList recipientsTo = new InternetAddressList();
            InternetAddressList recipientsCc = new InternetAddressList();
            InternetAddressList recipientsBcc = new InternetAddressList();

            if (!string.IsNullOrWhiteSpace(recipientToAddress))
            {
                recipientsTo.Add(new MailboxAddress(recipientToDisplayName, recipientToAddress));
            }
            if (!string.IsNullOrWhiteSpace(recipientCcAddress))
            {
                recipientsCc.Add(new MailboxAddress(recipientCcDisplayName, recipientCcAddress));
            }
            if (!string.IsNullOrWhiteSpace(recipientBccAddress))
            {
                recipientsBcc.Add(new MailboxAddress(recipientBccDisplayName, recipientBccAddress));
            }

            return Send(config, recipientsTo, recipientsCc, recipientsBcc, subject, isHtml, body, attachments);
        }

        #endregion

    }
}

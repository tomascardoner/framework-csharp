using System;
using System.Security.Cryptography;

namespace CardonerSistemas.Encrypt
{
     public class TripleDES : IDisposable
    {
        private bool disposed = false;
        private TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider();

        private byte[] TruncateHash(string key, int lenght)
        {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();

            // Hash the key.
            byte[] keyBytes = System.Text.Encoding.Unicode.GetBytes(key);
            byte[] hash = sha1.ComputeHash(keyBytes);

            // Truncate or pad the hash.
            Array.Resize(ref hash, lenght);

            return hash;
        }

        public TripleDES(string key)
        {
            // Initialize the crypto provider.
            tripleDes.Key = TruncateHash(key, tripleDes.KeySize / 8);
            tripleDes.IV = TruncateHash("", tripleDes.BlockSize / 8);
        }

        public string Encrypt(string plaintext)
        {
            // Convert the plaintext string to a byte array.
            byte[] plaintextBytes = System.Text.Encoding.Unicode.GetBytes(plaintext);

            // Create the stream.
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            // Create the encoder to write to the stream.
            System.Security.Cryptography.CryptoStream encStream = new CryptoStream(ms, tripleDes.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write);

            // Use the crypto stream to write the byte array to the stream.
            encStream.Write(plaintextBytes, 0, plaintextBytes.Length);
            encStream.FlushFinalBlock();

            // Convert the encrypted stream to a printable string.
            return Convert.ToBase64String(ms.ToArray());
        }

        public bool Decrypt(string encryptedtext, ref string decryptedText)
        {
            if (encryptedtext == null)
            {
                return false;
            }

            if (encryptedtext.Trim() == "")
            {
                decryptedText = "";
                return true;
            }
            else
            {
                try
                {
                    // Convert the encrypted text string to a byte array.
                    byte[] encryptedBytes = Convert.FromBase64String(encryptedtext);

                    // Create the stream.
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    // Create the decoder to write to the stream.
                    System.Security.Cryptography.CryptoStream decStream = new CryptoStream(ms, tripleDes.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write);

                    // Use the crypto stream to write the byte array to the stream.
                    decStream.Write(encryptedBytes, 0, encryptedBytes.Length);
                    decStream.FlushFinalBlock();

                    // Convert the plaintext stream to a string.
                    decryptedText = System.Text.Encoding.Unicode.GetString(ms.ToArray());
                    return true;
                }
                catch (Exception)
                {
                    decryptedText = "";
                    return false;
                }
            }
        }

        // Implement IDisposable.
        // Do not make this method virtual.
        // A derived class should not be able to override this method.
        public void Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the
        // runtime from inside the finalizer and you should not reference
        // other objects. Only unmanaged resources can be disposed.
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    tripleDes.Dispose();
                }

                // Note disposing has been done.
                disposed = true;

            }
        }
    }
}
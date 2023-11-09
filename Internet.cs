using System;
using System.Drawing;
using System.IO;
using System.Net;

namespace CardonerSistemas
{
    internal static class Internet
    {
        internal static bool GetImageFromUrl(string url, ref Image image)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    using (MemoryStream memoryStream = new MemoryStream(webClient.DownloadData(url)))
                    {
                        image = Bitmap.FromStream(memoryStream);
                    }
                }
                return true; 
            }
            catch (Exception ex)
            {
                CardonerSistemas.Error.ProcessError(ex, "Error al obtener la imagen desde internet.");
                return false;
            }
        }
    }
}
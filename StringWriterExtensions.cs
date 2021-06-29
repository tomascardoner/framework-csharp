using System.Text;
using System.IO;

namespace CardonerSistemas.StringWriterExtensions
{
    public sealed class ExtentedStringWriter : StringWriter
    {
        private readonly Encoding stringWriterEncoding;

        public ExtentedStringWriter(StringBuilder builder, Encoding desiredEncoding) : base(builder)
        {
            this.stringWriterEncoding = desiredEncoding;
        }

        public override Encoding Encoding
        {
            get
            {
                return this.stringWriterEncoding;
            }
        }
    }    
}
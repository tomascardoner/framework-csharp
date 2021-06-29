using System;
using System.Xml;
using System.Text;

namespace CardonerSistemas.Soap
{
    /// <summary>
    /// Simple XML manipulation (simil PHP)
    /// </summary>
    class SimpleXMLElement
    {
        private string _nameSpace;
        private string _prefix;
        private string[] _nameSpacesMap;
        private bool _jetty;

        private XmlDocument _document;
        private XmlElement _elements;
        private XmlElement _element;

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="text"></param>
        /// <param name="elements"></param>
        /// <param name="document"></param>
        /// <param name="nameSpace"></param>
        /// <param name="prefix"></param>
        /// <param name="nameSpacesMap">To map our namespace prefix to that given by the client {prefix: received_prefix}</param>
        /// <param name="jetty"></param>
        public SimpleXMLElement(string text = null, XmlElement elements = null, XmlDocument document = null, string nameSpace = null, string prefix = null, string[] nameSpacesMap = null, bool jetty = false)
        {
            _nameSpace = nameSpace;
            _prefix = prefix;
            _nameSpacesMap = nameSpacesMap;
            _jetty = jetty;

            if (text != null)
            {
                try
                {
                    _document = new XmlDocument();
                    _document.PreserveWhitespace = true;
                    _document.LoadXml(text);
                }
                catch (Exception ex)
                {
                    // Log error
                    throw;
                }
                _elements = _document.DocumentElement;
            }
            else
            {
                _elements = elements;
                _document = document;
            }
        }

        /// <summary>
        /// Adds a child tag to a node
        /// </summary>
        /// <param name="name"></param>
        /// <param name="text"></param>
        /// <param name="ns"></param>
        /// <returns></returns>
        public SimpleXMLElement AddChild(string name, string text = null, bool useNameSpace = true)
        {
            XmlElement element;

            if (!useNameSpace | _nameSpace.Length == 0)
            {
                // log.debug($"Adding {name}");
                element = _document.CreateElement(name);
            }
            else
            {
                // log.debug($"Adding {name} ns {_namespace} {namespc}");
                if (_prefix.Length > 0)
                {
                    element = _document.CreateElement(_nameSpace, $"{_prefix}:{name}");
                }
                else
                {
                    element = _document.CreateElement(_nameSpace, name);
                }
            }

            // don't append null tags!
            if (text.Length > 0)
            {
                element.AppendChild(_document.CreateTextNode(text));
            }
            _element.AppendChild(element);

            return new SimpleXMLElement(null, element, _document, _nameSpace, _prefix, _nameSpacesMap, _jetty);
        }

        /// <summary>
        /// Return the XML representation of the document
        /// </summary>
        public string AsXml(ref XmlDocument document)
        {
            using (var stringWriter = new CardonerSistemas.StringWriterExtensions.ExtentedStringWriter(new StringBuilder(), Encoding.UTF8))
            using (var xmlTextWriter = XmlWriter.Create(stringWriter))
            {
                document.WriteTo(xmlTextWriter);
                xmlTextWriter.Flush();
                return stringWriter.GetStringBuilder().ToString();
            }
        }

        /// <summary>
        /// Return the XML representation of the document
        /// </summary>
        /// <param name="pretty">Specifies if keeps the format of the document</param>
        /// <returns></returns>
        public string AsXml(bool pretty = false)
        {
            if (!pretty)
            {
                return AsXml(ref _document);
            }
            else
            {
                XmlDocument document = new XmlDocument();
                document.PreserveWhitespace = false;
                document.LoadXml(AsXml(ref _document));
                return AsXml(ref document);
            }
        }
    }
}
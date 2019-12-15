using Dapper;
using System;
using System.Data;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace DapperMappers.Core.TypeHandlers
{
    public class XmlObjectTypeHandler : SqlMapper.ITypeHandler
    {
        private static readonly XmlWriterSettings XmlWriterSettings = new XmlWriterSettings
        {
            Indent = true,
            OmitXmlDeclaration = true
        };

        public object Parse(Type destinationType, object value)
        {
            if (!typeof(IXmlObjectType).IsAssignableFrom(destinationType))
            {
                throw new ArgumentException(
                    $"'{destinationType}' should implement '{nameof(IXmlObjectType)}' interface.", nameof(destinationType));
            }

            if (value == null || value is DBNull)
            {
                return null;
            }

            object result = DeserializeXml(destinationType, value);
            return result;
        }

        public void SetValue(IDbDataParameter parameter, object value)
        {
            parameter.Value = value == null ? DBNull.Value : SerializeToXml(value);
        }

        private static object SerializeToXml(object value)
        {
            XmlSerializer serializer = new XmlSerializer(value.GetType());

            using (StringWriter stream = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(stream, XmlWriterSettings))
                {
                    serializer.Serialize(writer, value);
                    return stream.ToString();
                }
            }
        }

        private static object DeserializeXml(Type destinationType, object value)
        {
            XmlSerializer serializer = new XmlSerializer(destinationType);
            using (StringReader stringReader = new StringReader((string)value))
            {
                return serializer.Deserialize(stringReader);
            }
        }
    }
}

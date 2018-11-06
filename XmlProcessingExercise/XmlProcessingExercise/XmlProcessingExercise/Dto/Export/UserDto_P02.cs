using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace XmlProcessingExercise.App.Dto.Export
{
    [XmlType("user")]
    public class UserDto_P02
    {
        [XmlAttribute("first-name")]
        public string FirstName { get; set; }

        [XmlAttribute("last-name")]
        public string LastName { get; set; }

        [XmlArray("sold-products")]
        public ProductDto_P02[] ProductsSold { get; set; }
    }
}

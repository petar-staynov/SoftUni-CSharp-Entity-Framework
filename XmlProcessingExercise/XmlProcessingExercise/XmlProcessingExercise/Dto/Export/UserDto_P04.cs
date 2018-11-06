using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace XmlProcessingExercise.App.Dto.Export
{
    [XmlType("user")]
    public class UserDtoP04
    {
        [XmlAttribute("first-name")]
        public string FirstName { get; set; }

        [XmlAttribute("last-name")]
        public string LastName { get; set; }

        [XmlAttribute("age")]
        public int? Age { get; set; }

        [XmlElement("sold-products")]
        public ProductsDtoP04[] SoldProducts { get; set; }
    }
}

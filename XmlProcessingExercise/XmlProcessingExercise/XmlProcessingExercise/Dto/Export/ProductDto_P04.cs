using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace XmlProcessingExercise.App.Dto.Export
{
    [XmlType("product")]
    public class ProductDtoP04
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("price")]
        public decimal Price { get; set; }
    }
}

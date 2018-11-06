using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace XmlProcessingExercise.App.Dto.Export
{
    [XmlType("product")]
    public class ProductDto_P02
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }
    }
}

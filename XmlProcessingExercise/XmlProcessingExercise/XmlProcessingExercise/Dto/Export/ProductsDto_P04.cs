using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace XmlProcessingExercise.App.Dto.Export
{
    [XmlType("sold-products")]
    public class ProductsDtoP04
    {
        [XmlAttribute("count")]
        public int Count { get; set; }

        [XmlElement("product")]
        public ProductDtoP04[] ProductDtoCollection { get; set; }
    }
}
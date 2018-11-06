using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace XmlProcessingExercise.App.Dto
{
    [XmlType("product")]
    public class ProductDto 
    {
        [Required]
        [MinLength(3)]
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }
    }
}

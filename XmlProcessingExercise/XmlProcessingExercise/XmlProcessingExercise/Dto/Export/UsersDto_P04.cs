using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace XmlProcessingExercise.App.Dto.Export
{
    [XmlRoot("users")]
    public class UsersDtoP04
    {
        [XmlAttribute("count")]
        public int Count { get; set; }

        [XmlElement("user")]
        public UserDtoP04[] Users { get; set; }
    }
}

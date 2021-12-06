using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MotorvehicleInspectionSystem.Models.Request
{
    public class NetworkQueryR022
    {
        [XmlElement("hphm")]
        public string Hphm { get; set; }
        [XmlElement("hpzl")]
        public string Hpzl { get; set; }
        [XmlElement ("clsbdh")]
        public string Clsbdh { get; set; }
        [XmlElement("jyjgbh")]
        public string Jyjgbh { get; set; }
        [XmlElement("jylb")]
        public string Ajywlb { get; set; }
        [XmlIgnore ]
        public string Hjywlb { get; set; }
    }
}

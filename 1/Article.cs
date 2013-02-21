using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace t4
{
    public class Article
    {
        [XmlElement("title")]
        public string title { get; set; }

        [XmlElement("description")]
        public string description { get; set; }

        [XmlElement("pubdate")]
        public string pubdate { get; set; }
    }
}

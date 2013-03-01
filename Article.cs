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

namespace CATNewsFinal
{
    public class Article
    {
        [XmlElement("title")]
        public string Title
        {
            get;
            set;
        }
        [XmlElement("description")]
        public string Description
        {
            get;
            set;
        }
        public string Description1
        {
            get;
            set;
        }
        [XmlElement("pubdate")]
        public string Pubdate
        {
            get;
            set;
        }
    }
}

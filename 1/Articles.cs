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
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace t4
{
    [XmlRoot("articles")]
    public class Articles
    {
        [XmlArray("articles")]
        //[XmlArrayItem("link")]
        [XmlArrayItem("item")]
        public ObservableCollection<Article> Collection { get; set; }
    }
}

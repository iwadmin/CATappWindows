using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Xml.Serialization;
using System.Xml.Linq;


namespace CATNewsFinal
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
       
        public MainPage()
        {
            InitializeComponent();
            // is there network connection available
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                MessageBox.Show("No network connection available!");
                return;
            }
            // start loading XML-data
            WebClient downloader = new WebClient();
            Uri uri = new Uri("http://iwpublish.herokuapp.com/api/v1/NewsArticles.xml", UriKind.Absolute);
            downloader.DownloadStringCompleted += new DownloadStringCompletedEventHandler(NewsDownloaded);
            downloader.DownloadStringAsync(uri);
        }
        void NewsDownloaded(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Result == null || e.Error != null)
            {
                MessageBox.Show("There was an error downloading the XML-file!");
            }
            else
            {
                // Deserialize if download succeeds
                XmlSerializer serializer = new XmlSerializer(typeof(Articles));
                XDocument document = XDocument.Parse(e.Result);
                var news = from query in document.Descendants("item")
                           select new Article
                           {
                               Title = (string)query.Element("title"),
                               Description = (string)query.Element("description").Value.ToString().Replace("<![CDATA[", "").Replace("]]>", "").Replace("<p>", "").Replace("</p>", "").Replace("\n", "").Replace("\t", "").Replace("<p style=", "").Replace("text-align: justify; ", "").Replace(">", "").Replace(@"""", "").Substring(0, 20) + "...",
                               Description1 = (string)query.Element("description").Value.ToString().Replace("<![CDATA[", "").Replace("]]>", ""),
                               Pubdate = (string)query.Element("pubdate")

                               //Title = (query.Element("title") == null) ? "" : (string)query.Element("title").Value.ToString().Replace("<![CDATA[", "").Replace("]]>", ""),

                               //Pubdate = (query.Element("pubdate") == null) ? "" : (string)query.Element("pubdate").Value.ToString()
                           };
                newsList.ItemsSource = news;
            }
        }

        private void newsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count>0)
            {
                var app = App.Current as App;
            app.selectedNews=(Article)e.AddedItems[0];
            var targetpage = new Uri("/Detailnews.xaml", UriKind.Relative);
            //reset selection of Listbox
            ((ListBox)sender).SelectedIndex=-1;
            //Change page navigation
            NavigationService.Navigate(targetpage);
            FrameworkElement root=Application.Current.RootVisual as FrameworkElement;
            root.DataContext=app.selectedNews;
            }

         }
     }
}
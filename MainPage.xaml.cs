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
using System.IO;

namespace t4
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

        private void newsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var app = App.Current as App;
            app.selectedNews = (Article)newsList.SelectedItem;
            this.NavigationService.Navigate(new Uri("/Detailnews.xaml", UriKind.Relative));
        }
       void NewsDownloaded(object sender, DownloadStringCompletedEventArgs e)
        {
           /* if (e.Result == null || e.Error != null)
            {
                MessageBox.Show("There was an error downloading the XML-file!");
            }
            else
            {
                // Deserialize if download succeeds
               // XmlSerializer serializer = new XmlSerializer(typeof(Articles));
             //   XDocument document = XDocument.Parse(e.Result);
                // get all the articles
                //Articles arct = (Articles)serializer.Deserialize(document.CreateReader());

                // bind data to ListBox
                //newsList.DataContext= arct;

           */

                using (var reader = new StreamReader(e.Result))
                {

                    string s = reader.ReadToEnd();
                    Stream str = e.Result;
                    str.Position = 0;
                    XDocument xdoc = XDocument.Load(str);

                    var data = from query in xdoc.Descendants("item")
                               select new Article
                               {
                                   title= (string)query.Element("item").Element("title").Value,
                                 //  nickname = (string)query.Element("user_info").Element("nickname"),
                                  // track = (string)query.Element("track"),
                                   //artist = (string)query.Element("artist"),
                               };
                    newsList.ItemsSource = data;
                }

       }
            /*    Stream str = e.Result;
                XDocument document1 = XDocument.Load(str);
                var data = from query in document1.Descendants("item")
                       select new Article
                       {
                           title = (string)query.Element("item").Element("title").Value,
                           description = (string)query.Element("item").Element("description").Value,
                           pubdate= (string)query.Element("item").Element("pubdate").Value
                       };
                  newsList.ItemsSource = data;
                                             */
            
        
        }
    }


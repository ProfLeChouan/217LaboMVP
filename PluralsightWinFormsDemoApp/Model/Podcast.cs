using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace PluralsightWinFormsDemoApp.Model
{
    public class Podcast
    {
        public string SubscriptionUrl { get; set;  }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public List<Episode> Episodes { get; set; }

        public static List<Podcast> Podcasts;

        public static void UpdatePodcast(Podcast podcast)
        {
            var doc = new XmlDocument();
            doc.Load(podcast.SubscriptionUrl);

            XmlElement channel = doc["rss"]["channel"];
            XmlNodeList items = channel.GetElementsByTagName("item");
            podcast.Title = channel["title"].InnerText;
            podcast.Link = channel["link"].InnerText;
            podcast.Description = channel["description"].InnerText;
            if (podcast.Episodes == null) podcast.Episodes = new List<Episode>();
            foreach (XmlNode item in items)
            {
                var guid = item["guid"].InnerText;
                var episode = podcast.Episodes.FirstOrDefault(e => e.Guid == guid);
                if (episode == null)
                {
                    episode = new Episode() { Guid = guid, IsNew = true };
                    episode.Title = item["title"].InnerText;
                    episode.PubDate = item["pubDate"].InnerText;
                    var xmlElement = item["description"];
                    if (xmlElement != null) episode.Description = xmlElement.InnerText;
                    var element = item["link"];
                    if (element != null) episode.Link = element.InnerText;
                    var enclosureElement = item["enclosure"];
                    if (enclosureElement != null) episode.AudioFile = enclosureElement.Attributes["url"].InnerText;
                    podcast.Episodes.Add(episode);
                }
            }
        }

        internal static void ReadPodcasts()
        {
            if (File.Exists("subscriptions.xml"))
            {
                var serializer = new XmlSerializer(typeof(List<Podcast>));
                using (var s = File.OpenRead("subscriptions.xml"))
                {
                    Podcasts = (List<Podcast>)serializer.Deserialize(s);
                }
            }
            else
            {
                var defaultFeeds = new[]
                {
                    "http://hwpod.libsyn.com/rss",
                    "http://feeds.feedburner.com/herdingcode",
                    "http://www.pwop.com/feed.aspx?show=dotnetrocks&amp;filetype=master",
                    "http://feeds.feedburner.com/JesseLibertyYapcast",
                    "http://feeds.feedburner.com/HanselminutesCompleteMP3"
                };
                Podcasts =  defaultFeeds.Select(f => new Podcast() { SubscriptionUrl = f }).ToList();
            }
        }
    }
}
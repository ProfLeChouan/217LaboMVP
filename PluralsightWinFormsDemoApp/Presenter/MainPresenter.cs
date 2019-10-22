using PluralsightWinFormsDemoApp.Model;
using PluralsightWinFormsDemoApp.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace PluralsightWinFormsDemoApp.Presenter
{
    class MainPresenter
    {
        private readonly IMainForm myMainForm;


        public MainPresenter(IMainForm aMainForm)
        {
            myMainForm = aMainForm;
            myMainForm.Load += LoadPodcasts;
            myMainForm.PodcastSelectedIndexChange += LoadPodcastEpisodes;
            myMainForm.EpisodeSelectedIndexChange += DisplayPodcastEpisodeDetails;
            myMainForm.PlayClick += PlayEpisode;
            myMainForm.AddClick += AddPodcast;
            myMainForm.FormClosed += SavePodcasts;
            myMainForm.RemoveClick += RemovePodcast;

        }

        private Episode currentEpisode = null;

        private void RemovePodcast(object sender, EventArgs e)
        {
            var pod = Podcast.Podcasts[myMainForm.PodcastSelectedIndex];
            Podcast.Podcasts.Remove(pod);
            myMainForm.RemoveSelectedPodcast();
        }

        private void SavePodcasts(object sender, FormClosedEventArgs e)
        {
            myMainForm.SaveEpisode(currentEpisode);
            Podcast.SavePodcasts();
        }

        private void AddPodcast(object sender, EventArgs e)
        {
            var url = myMainForm.GetNewPodcastURL();
            if (url != null)
            {
                var pod = new Podcast() { SubscriptionUrl = url };
                Podcast.UpdatePodcast(pod);
                Podcast.Podcasts.Add(pod);
                var index = myMainForm.AddPodcast(pod.Title);
                myMainForm.PodcastSelectedIndex = index;
            }
        }

        private void PlayEpisode(object sender, EventArgs e)
        {
            Process.Start(currentEpisode.AudioFile ?? currentEpisode.Link);
        }

        private void DisplayPodcastEpisodeDetails(object sender, EventArgs e)
        {
            myMainForm.SaveEpisode(currentEpisode);
            currentEpisode = Podcast.Podcasts[myMainForm.PodcastSelectedIndex].Episodes[myMainForm.EpisodeSelectedIndex];
            myMainForm.DisplayEpisode(currentEpisode);
        }

        private void LoadPodcastEpisodes(object sender, EventArgs e)
        {
            myMainForm.ClearEpisodes();
            if (myMainForm.PodcastSelectedIndex == -1) return;
            var pod = Podcast.Podcasts[myMainForm.PodcastSelectedIndex];
            foreach (var episode in pod.Episodes)
            {
                myMainForm.AddEpisode(episode.Title);
            }
        }

        private void LoadPodcasts(object sender, EventArgs e)
        {
            Podcast.ReadPodcasts();

            foreach (var pod in Podcast.Podcasts)
            {
                Podcast.UpdatePodcast(pod);
                myMainForm.AddPodcast(pod.Title);
            }
        }
    }
}

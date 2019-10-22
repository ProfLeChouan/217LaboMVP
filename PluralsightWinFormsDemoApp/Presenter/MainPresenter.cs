using PluralsightWinFormsDemoApp.Model;
using PluralsightWinFormsDemoApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

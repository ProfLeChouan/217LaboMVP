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

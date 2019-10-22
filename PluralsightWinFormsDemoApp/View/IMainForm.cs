using PluralsightWinFormsDemoApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PluralsightWinFormsDemoApp.View
{
    interface IMainForm
    {
        //V1 Display podcasts
        event EventHandler Load;
        //void AddPodcast(string title);

        //V2 Display podcast episodes
        event EventHandler PodcastSelectedIndexChange;
        void AddEpisode(string title);

        //int PodcastSelectedIndex { get; }

        void ClearEpisodes();

        //V3 display episode details
        event EventHandler EpisodeSelectedIndexChange;
        int EpisodeSelectedIndex { get; }
        void DisplayEpisode(Episode anEpisode);
        void SaveEpisode(Episode anEpisode);

        //V4 play click
        event EventHandler PlayClick;

        //V5 add click
        event EventHandler AddClick;
        int PodcastSelectedIndex { get; set; }
        int AddPodcast(string title);

        string GetNewPodcastURL();

        //V6 FormClosed
        event FormClosedEventHandler FormClosed;

        //Bog remove click
        event EventHandler RemoveClick;
        void RemoveSelectedPodcast();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralsightWinFormsDemoApp.View
{
    interface IMainForm
    {  
        //1er scénario ; le chargement du formulaire affiche la lsite de podcast
        event EventHandler Load;
        void AddPodcast(string title);

        //V2 Display podcast episodes
        event EventHandler PodcastSelectedIndexChange;
        void AddEpisode(string title);

        int PodcastSelectedIndex { get; }

        void ClearEpisodes();
    }
}

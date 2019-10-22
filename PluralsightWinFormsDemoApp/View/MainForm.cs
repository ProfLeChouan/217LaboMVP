using PluralsightWinFormsDemoApp.Model;
using System;
using System.Linq;
using System.Windows.Forms;

namespace PluralsightWinFormsDemoApp.View
{
    public partial class MainForm : Form, IMainForm
    {

        public event EventHandler AddClick
        {
            add { button1.Click += value; }
            remove { button1.Click -= value; }
        }

        public event EventHandler PlayClick
        {
            add { button3.Click += value; }
            remove { button3.Click -= value; }
        }

        public event EventHandler EpisodeSelectedIndexChange
        {
            add { listBox2.SelectedIndexChanged += value; }
            remove { listBox2.SelectedIndexChanged -= value; }
        }


        public int EpisodeSelectedIndex => listBox2.SelectedIndex;

        public int PodcastSelectedIndex { get => listBox1.SelectedIndex; set => listBox1.SelectedIndex = value; }

        public event EventHandler PodcastSelectedIndexChange
        {
            add { listBox1.SelectedIndexChanged += value; }
            remove { listBox1.SelectedIndexChanged -= value; }
        }

        public MainForm()
        {
            InitializeComponent();
        }

        public event EventHandler RemoveClick
        {
            add { button2.Click += value; }
            remove { button2.Click -= value; }
        }


        //       public void AddPodcast(string title)
        public int AddPodcast(string title)
        {
            //listBox1.Items.Add(title);
            return listBox1.Items.Add(title);
        }


        public void AddEpisode(string title)
        {
            listBox2.Items.Add(title);
        }


        public void DisplayEpisode(Episode anEpisode)
        {
            textBox1.Text = anEpisode.Title;
            textBox2.Text = anEpisode.PubDate;
            textBox3.Text = anEpisode.Description;
            checkBox1.Checked = anEpisode.IsFavourite;
            anEpisode.IsNew = false;
            numericUpDown1.Value = anEpisode.Rating;
            textBox4.Text = String.Join(",", anEpisode.Tags ?? new string[0]);
            textBox6.Text = anEpisode.Notes ?? "";
        }

        public void SaveEpisode(Episode anEpisode)
        {
            if (anEpisode == null) return;

            anEpisode.Tags = textBox4.Text.Split(new[] { ',' }).Select(s => s.Trim()).ToArray();
            anEpisode.Rating = (int)numericUpDown1.Value;
            anEpisode.IsFavourite = checkBox1.Checked;
            anEpisode.Notes = textBox6.Text;
        }

        public void RemoveSelectedPodcast()
        {
            listBox1.Items.Remove(listBox1.SelectedItem);
            listBox1.SelectedIndex = 0;
        }

        public void ClearEpisodes()
        {
            listBox2.Items.Clear();
        }


        public string GetNewPodcastURL()
        {
            var form = new NewPodcastForm();
            return form.ShowDialog() == DialogResult.OK ? form.PodcastUrl : null;
        }
    }
}

using PluralsightWinFormsDemoApp.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace PluralsightWinFormsDemoApp.View
{
    public partial class MainForm : Form, IMainForm
    {
        private Episode currentEpisode;

        public int PodcastSelectedIndex => listBox1.SelectedIndex;

        public event EventHandler PodcastSelectedIndexChange
        {
            add { listBox1.SelectedIndexChanged += value; }
            remove { listBox1.SelectedIndexChanged -= value; }
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //Podcast.ReadPodcasts();

            //foreach (var pod in Podcast.Podcasts)
            //{
            //    Podcast.UpdatePodcast(pod);
            //    listBox1.Items.Add(pod.Title);
            //}
        }

        public void AddPodcast(string title)
        {
            listBox1.Items.Add(title);
        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //listBox2.Items.Clear();
            //if (listBox1.SelectedIndex == -1) return;
            //var pod = Podcast.Podcasts[listBox1.SelectedIndex];
            //foreach (var episode in pod.Episodes)
            //{
            //    listBox2.Items.Add(episode.Title);
            //}
        }

        public void AddEpisode(string title)
        {
            listBox2.Items.Add(title);
        }

        public void ClearEpisodes()
        {
            listBox2.Items.Clear();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveEpisode();
            currentEpisode = Podcast.Podcasts[listBox1.SelectedIndex].Episodes[listBox2.SelectedIndex];
            textBox1.Text = currentEpisode.Title;
            textBox2.Text = currentEpisode.PubDate;
            textBox3.Text = currentEpisode.Description;
            checkBox1.Checked = currentEpisode.IsFavourite;
            currentEpisode.IsNew = false;
            numericUpDown1.Value = currentEpisode.Rating;
            textBox4.Text = String.Join(",", currentEpisode.Tags ?? new string[0]);
            textBox6.Text = currentEpisode.Notes ?? "";
        }

        private void SaveEpisode()
        {
            if (currentEpisode == null) return;

            currentEpisode.Tags = textBox4.Text.Split(new[] { ',' }).Select(s => s.Trim()).ToArray();
            currentEpisode.Rating = (int)numericUpDown1.Value;
            currentEpisode.IsFavourite = checkBox1.Checked;
            currentEpisode.Notes = textBox6.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process.Start(currentEpisode.AudioFile ?? currentEpisode.Link);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Remove(listBox1.SelectedItem);
            listBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form = new NewPodcastForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                var pod = new Podcast() {SubscriptionUrl = form.PodcastUrl };
                Podcast.UpdatePodcast(pod);
                Podcast.Podcasts.Add(pod);
                var index = listBox1.Items.Add(pod.Title);
                listBox1.SelectedIndex = index;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveEpisode();
            var serializer = new XmlSerializer(Podcast.Podcasts.GetType());
            using (var s = File.Create("subscriptions.xml"))
            {
                serializer.Serialize(s, Podcast.Podcasts);
            }
        }

    }
}

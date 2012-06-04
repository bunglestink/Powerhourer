using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using Powerhourer.BusinessLayer;
using Powerhourer.Entities;

namespace Powerhourer {
    public partial class MainPage : Page {

        public Powerhour CurrentPowerhour { get; set; }

        readonly SongService songService;
        readonly PowerhourService powerhourService;


        public MainPage(SongService SongService)
        {
            InitializeComponent();

            this.songService = SongService;
            this.powerhourService = new PowerhourService(mediaElement);

            SetCurrentPowerhour(new Powerhour());
        }



        void SetCurrentPowerhour(Powerhour Powerhour)
        {
            CurrentPowerhour = Powerhour;
            dgPowerhourView.ItemsSource = Powerhour.SongSamples;
        }


        IEnumerable<SongSample> GetSelectedSongs()
        {
            var songSamples = new List<SongSample>();
            foreach (var song in dgPowerhourView.SelectedItems) {
                songSamples.Add(song as SongSample);
            }
            return songSamples;
        }

        void SelectSongs(IEnumerable<SongSample> SongSamples)
        {
            dgPowerhourView.SelectedItems.Clear();
            foreach (var song in SongSamples) {
                dgPowerhourView.SelectedItems.Add(song);
            }
        }


        private void btnAddSong_Click(object sender, RoutedEventArgs e)
        {
            songService.AddSongs(CurrentPowerhour);
        }

        private void btnRemoveSong_Click(object sender, RoutedEventArgs e)
        {
            var songSamples = GetSelectedSongs();
            songService.RemoveSongs(CurrentPowerhour, songSamples);
        }

        private void btnMoveSongsUp_Click(object sender, RoutedEventArgs e)
        {
            var songSamples = GetSelectedSongs();
            songService.MoveSongsUp(CurrentPowerhour, songSamples);
            SelectSongs(songSamples);
        }

        private void btnMoveSongsDown_Click(object sender, RoutedEventArgs e)
        {
            var songSamples = GetSelectedSongs();
            songService.MoveSongsDown(CurrentPowerhour, songSamples);
            SelectSongs(songSamples);
        }

 
        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            btnAddSong.IsEnabled = false;
            btnRemoveSong.IsEnabled = false;
            btnMoveSongsUp.IsEnabled = false;
            btnMoveSongsDown.IsEnabled = false;

            powerhourService.Play(CurrentPowerhour);
            NavigationService.Navigate(new Uri("/PlayingPage.xaml", UriKind.Relative));
        }
    }
}
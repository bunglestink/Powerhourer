using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using Powerhourer.Entities;

namespace Powerhourer.BusinessLayer {
    public class SongService {
 
        public void AddSongs(Powerhour Powerhour)
        {
            var openFilesDialog = new OpenFileDialog {
                Filter = "MP3 (.mp3)|*.mp3",
                Multiselect = true
            };

            if (openFilesDialog.ShowDialog() == true) {
         
                foreach (var file in openFilesDialog.Files) {
                    
                    var song = new Song(file.FullName);
                    var songSample = new SongSample(song);
                    Powerhour.SongSamples.Add(songSample);
                }
            }
        }


        public void RemoveSongs(Powerhour CurrentPowerhour, IEnumerable<SongSample> songSamples)
        {
            var confirmationResult = MessageBox.Show("Are you sure you wish to remove these songs?", "Confirmation", MessageBoxButton.OKCancel);

            if (confirmationResult == MessageBoxResult.OK) {

                foreach (var song in songSamples) {
                    CurrentPowerhour.SongSamples.Remove(song);
                }
            }
        }



        public void MoveSongsUp(Powerhour CurrentPowerhour, IEnumerable<SongSample> songSamples)
        {
            if (songSamples.Any(s => CurrentPowerhour.SongSamples.First() == s)) {
                MessageBox.Show("Cannot move up when first song is selected.");
                return;
            }

            // TODO: this is a cheat - will fail if a song in more than once
            var indexedSongSamples = songSamples
                .Select(s => new {
                    Song = s,
                    Index = CurrentPowerhour.SongSamples.IndexOf(s)
                })
                .OrderBy(s => s.Index);

            foreach (var indexedSong in indexedSongSamples) {
                CurrentPowerhour.SongSamples.RemoveAt(indexedSong.Index);
                CurrentPowerhour.SongSamples.Insert(indexedSong.Index - 1, indexedSong.Song);
            }
        }



        public void MoveSongsDown(Powerhour CurrentPowerhour, IEnumerable<SongSample> songSamples)
        {
            if (songSamples.Any(s => CurrentPowerhour.SongSamples.Last() == s)) {
                MessageBox.Show("Cannot move down when last song is selected.");
                return;
            }

            // TODO: this is a cheat - will fail if a song in more than once
            var indexedSongSamples = songSamples
                .Select(s => new {
                    Song = s,
                    Index = CurrentPowerhour.SongSamples.IndexOf(s)
                })
                .OrderByDescending(s => s.Index);

            foreach (var indexedSong in indexedSongSamples) {
                CurrentPowerhour.SongSamples.RemoveAt(indexedSong.Index);
                CurrentPowerhour.SongSamples.Insert(indexedSong.Index + 1, indexedSong.Song);
            }
        }
    }
}

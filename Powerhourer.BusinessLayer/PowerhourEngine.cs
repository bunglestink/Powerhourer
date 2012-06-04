using System;
using System.IO;
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
using System.Windows.Threading;

using Powerhourer.Entities;

namespace Powerhourer.BusinessLayer {
    public class PowerhourEngine {

        readonly MediaElement mediaElement;
        readonly DispatcherTimer timer;
        
        Powerhour currentPowerhour;
        int currentSongIndex;
        int songTime;
        FileStream currentFileStream;
        SongSample currentSong { get { return currentPowerhour.SongSamples[currentSongIndex]; } }
        
        public bool IsPlaying { get { return timer.IsEnabled; } }
        public bool IsPaused { get; private set; }


        public PowerhourEngine(MediaElement MediaElement)
        {
            this.mediaElement = MediaElement;
            this.timer = new DispatcherTimer();

            timer.Tick += Tick;
            timer.Interval = new TimeSpan(0, 0, 1);
            
            // the injected media element must exist on the display for MediaOpened to fire
            mediaElement.AutoPlay = false;
            mediaElement.MediaOpened += (o, args) => {
                var player = (MediaElement)o;
                player.Play();
                if (player.CanSeek) {
                    player.Position = new TimeSpan(0, 0, currentSong.Start);
                }
            };
        }


        private void Tick(object o, EventArgs args)
        {
            songTime++;
            if (songTime > currentSong.Duration) {
                
                Next();

                if (currentSong == currentPowerhour.SongSamples.Last()) {
                    timer.Stop();
                }
            }
        }

        private void PlaySong(SongSample currentSong)
        {
            if (currentFileStream != null) {
                currentFileStream.Dispose();
            }

            mediaElement.Stop();
            currentFileStream = new FileStream(currentSong.Song.FilePath, FileMode.Open);
            mediaElement.SetSource(currentFileStream);
            mediaElement.Play();
        }


        private void Next()
        {
            currentSongIndex++;
            songTime = 0;
            PlaySong(currentSong);
        }


        public void Start(Powerhour Powerhour)
        {
            currentPowerhour = Powerhour;
            currentSongIndex = 0;
            songTime = 0;
            PlaySong(currentSong);

            timer.Start();
        }

        public void Pause()
        {
            IsPaused = true;
            timer.Stop();
            mediaElement.Pause();
        }

        public void Resume()
        {
            IsPaused = false;
            mediaElement.Play();
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
            mediaElement.Source = null;
            mediaElement.Stop();
        }
    }
}

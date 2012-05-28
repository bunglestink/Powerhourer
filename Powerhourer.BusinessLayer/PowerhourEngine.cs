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

        public PowerhourEngine() : this(new MediaElement(), new DispatcherTimer())
        {
        }

        public PowerhourEngine(MediaElement MediaElement, DispatcherTimer Timer)
        {
            Timer.Tick += Tick;
            Timer.Interval = new TimeSpan(0, 0, 1);
            MediaElement.AutoPlay = true;

            this.mediaElement = MediaElement;
            this.timer = Timer;
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
        }


        private void Next()
        {
            songTime = 0;
            currentSongIndex++;
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
            timer.Stop();
            mediaElement.Pause();
        }

        public void Resume()
        {
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

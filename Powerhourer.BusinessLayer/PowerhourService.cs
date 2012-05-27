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

using Powerhourer.Entities;

namespace Powerhourer.BusinessLayer {
    public class PowerhourService {

        readonly MediaElement MediaElement;

        public PowerhourService()
        {
            this.MediaElement = new MediaElement();
            MediaElement.MediaOpened += (a, b) => {
                MediaElement.Play();
            };
        }


        public void Play(Powerhour Powerhour)
        {
            var song = Powerhour.SongSamples.First().Song;
            MediaElement.SetSource(new FileStream(song.FilePath, FileMode.Open));
            MediaElement.Position = TimeSpan.Zero;
        }

        public void Pause()
        {
            MediaElement.Pause();
        }

        public void ResumeFromPause()
        {
            MediaElement.Play();
        }

        public void Stop()
        {
            MediaElement.Stop();
        }
    }
}

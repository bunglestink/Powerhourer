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
    public class PowerhourService {

        readonly PowerhourEngine powerhourEngine;

        public PowerhourService(MediaElement MediaElement) : this(new PowerhourEngine(MediaElement))
        {
        }

        public PowerhourService(PowerhourEngine PowerhourEngine)
        {
            this.powerhourEngine = PowerhourEngine;
        }


        public void Play(Powerhour Powerhour)
        {
            powerhourEngine.Start(Powerhour);
        }


        public void Pause()
        {
            if (powerhourEngine.IsPlaying) {
                powerhourEngine.Pause();
            }
        }


        public void Resume()
        {
            if (!powerhourEngine.IsPlaying) {
                powerhourEngine.Resume();
            }
        }


        public void Stop()
        {
            if (powerhourEngine.IsPlaying) {
                powerhourEngine.Stop();
            }
        }
    }
}

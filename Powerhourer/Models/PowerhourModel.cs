using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using Powerhourer.BusinessLayer;
using Powerhourer.Entities;
using System.ComponentModel;

namespace Powerhourer.Models {
    public class PowerhourModel : INotifyPropertyChanged
    {
        readonly SongService songService;
        readonly PowerhourService powerhourService;

        public event PropertyChangedEventHandler PropertyChanged;


        Powerhour powerhour;
        public Powerhour Powerhour {
            get { return powerhour; }
            set {
                powerhour = value;
                OnPropertyChanged("Powerhour");
            }
        }



        public PowerhourModel(SongService songService, PowerhourService powerhourService)
        {

        }


        protected void OnPropertyChanged(string name)
        {
            var handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Powerhourer.Entities
{
    public class Powerhour
    {
        public ObservableCollection<SongSample> SongSamples { get; private set; }

        public Powerhour()
        {
            SongSamples = new ObservableCollection<SongSample>(new List<SongSample>());
        }
    }
}

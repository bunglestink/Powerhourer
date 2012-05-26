using System;
using System.Collections.Generic;

namespace Powerhour.Entities
{
    public class Powerhour
    {
        public IList<SongSample> SongSamples { get; private set; }


        public Powerhour()
        {
            SongSamples = new List<SongSample>();
        }
    }
}

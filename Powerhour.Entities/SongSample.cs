using System;

namespace Powerhour.Entities
{
    public class SongSample
    {
        public Song Song { get; set; }
        public int Start { get; set; }
        public int End { get; set; }

        public SongSample() : this(0, 60)
        {
        }

        public SongSample(int Start, int End)
        {
            this.Start = Start;
            this.End = End;
        }
    }
}

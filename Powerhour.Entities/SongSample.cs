using System;

namespace Powerhourer.Entities
{
    public class SongSample
    {
        public Song Song { get; private set; }
        public int Start { get; set; }
        public int End { get; set; }

        public SongSample(Song Song) : this(Song, 0, 60)
        {
        }

        public SongSample(Song Song, int Start, int End)
        {
            this.Song = Song;
            this.Start = Start;
            this.End = End;
        }
    }
}

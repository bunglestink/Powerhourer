using System;

namespace Powerhourer.Entities
{
    public class Song
    {
        public string FilePath { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }

        public Song(string FilePath)
        {
            this.FilePath = FilePath;
        }
    }
}

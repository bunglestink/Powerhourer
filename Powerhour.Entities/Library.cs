using System;
using System.Collections.Generic;

namespace Powerhour.Entities
{
    public class Library
    {
        readonly IList<Song> Songs { get; private set; }

        public Library()
        {
            Songs = new List<Song>();
        }
    }
}

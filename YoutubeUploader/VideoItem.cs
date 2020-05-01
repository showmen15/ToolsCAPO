using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeUploader
{
    public class VideoItem
    {
        public string Id { get; set; }
        public string Titel { get; set; }

        public VideoItem(string line)
        {
            var split = line.Split('\t');
            Id = split[0];
            Titel = split[1];
        }
    }
}

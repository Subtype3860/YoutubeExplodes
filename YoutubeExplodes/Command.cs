using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExplode.Videos;

namespace YoutubeExplodes
{
    abstract class Command
    {
        public abstract void GetVideoDescription(string url);
        public override async Task GetVideoDescription(string url)
        {
            throw new NotImplementedException();
        }
        public abstract void VideoDownload(string url);            
    }
}

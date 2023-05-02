using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeExplodes
{
    internal class Sender
    {
        Command? _command;
        
        public void SetCommand(Command command) 
        {
            _command = command;
        }

        public void GetVideoDescription(string url)
        {
            _command!.GetVideoDescription(url);
        }
        public void VideoDownload(string url) 
        {
            _command!.VideoDownload(url);
        }

    }
}

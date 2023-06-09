﻿using YoutubeExplode;
using YoutubeExplode.Videos;

namespace YoutubeExplodes
{
    public interface ICommands
    {
        public void GetVideoDescription() { }
        public Task VideoDownload()
        {
            return Task.CompletedTask;
        }
    }
}

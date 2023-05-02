using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;

namespace YoutubeExplodes
{
    internal class YoutubeReceiver : Command
    {
        private string _url;
        public override void GetVideoDescription(string url)
        {
            _url = url;
        }

        private async Task StreamManifest()
        {
            var youtube = new YoutubeClient();
            var videoId = VideoId.Parse(_url);
            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(videoId);
            var streamInfo = streamManifest.GetMuxedStreams().TryGetWithHighestVideoQuality();
            if (streamInfo is null)
            {
                // Available streams vary depending on the video and it's possible
                // there may not be any muxed streams at all.
                // See the readme to learn how to handle adaptive streams.
                Console.Error.WriteLine("В этом видео нет мультиплексированных потоков.");
                return;
            }

            // Download the stream
            var fileName = $"{videoId}.{streamInfo.Container.Name}";

            Console.Write(
                $"Загрузка потока: {streamInfo.VideoQuality.Label} / {streamInfo.Container.Name}... "
            );
                await youtube.Videos.Streams.DownloadAsync(streamInfo, fileName);

            Console.WriteLine("Выполнено");
            Console.WriteLine($"Видео сохранено '{fileName}'");
        }


        public override void VideoDownload(string url)
        {
            throw new NotImplementedException();
        }
    }
}

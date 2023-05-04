using YoutubeExplode;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;

namespace YoutubeExplodes
{
    internal class YoutubeReceiver : ICommands
    {
        private static string _url;
        private static YoutubeClient _youtubeClient;
        public YoutubeReceiver(string url, YoutubeClient youtubeClient)
        {
            _url = url;
            _youtubeClient = youtubeClient;
        }
        public void GetVideoDescription()
        {
            var video = _youtubeClient.Videos.GetAsync(_url);
            var title = video.Result.Title;
            var description = video.Result.Description;
        }

        public static async Task Download()
        {
            var streamManifest = await _youtubeClient.Videos.Streams.GetManifestAsync(_url);
            var streamInfo = streamManifest.GetMuxedStreams().TryGetWithHighestVideoQuality();
            if (streamInfo is null)
            {
                // Available streams vary depending on the video and it's possible
                // there may not be any muxed streams at all.
                // See the readme to learn how to handle adaptive streams.
                Console.Error.WriteLine("This video has no muxed streams.");
                return;
            }

            // Download the stream
            Console.Write(
                $"Downloading stream: {streamInfo.VideoQuality.Label} / {streamInfo.Container.Name}... "
            );

            var fileName = VideoId.Parse(_url);
            using (var progress = new ConsoleProgress())
                await _youtubeClient.Videos.Streams.DownloadAsync(streamInfo, fileName, progress);

            Console.WriteLine("Done");
            Console.WriteLine($"Video saved to '{fileName}'");
        }

        void ICommands.VideoDownload()
        {
            Download();
        }
    }
}

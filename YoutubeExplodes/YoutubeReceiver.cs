using YoutubeExplode;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;

namespace YoutubeExplodes
{
    internal class YoutubeReceiver : ICommands
    {
        private static string? _url;
        private static YoutubeClient? _youtubeClient;
        public YoutubeReceiver(string url, YoutubeClient youtubeClient)
        {
            _url = url;
            _youtubeClient = youtubeClient;
        }
        public void GetVideoDescription()
        {
            var video = _youtubeClient!.Videos.GetAsync(_url!);
            var title = video.Result.Title;
            var description = video.Result.Description;
        }

        public async Task VideoDownload()
        {
            var fileName = VideoId.Parse(_url!);
            var streamManifest = await _youtubeClient!.Videos.Streams.GetManifestAsync(fileName);
            var streamInfo = streamManifest.GetMuxedStreams().TryGetWithHighestVideoQuality();
            if (streamInfo is null)
            {
                Console.Error.WriteLine("В этом видео нет мультиплексированных потоков.");
                return;
            }
            Console.Write(
                $"Загрузка потока: {streamInfo.VideoQuality.Label} / {streamInfo.Container.Name}... "
            );
            
            using (var progress = new ConsoleProgress())
                await _youtubeClient.Videos.Streams.DownloadAsync(streamInfo, fileName, progress);

            Console.WriteLine("Выполнено");
            Console.WriteLine($"Видео '{fileName}' сохранено");
        }
    }
}

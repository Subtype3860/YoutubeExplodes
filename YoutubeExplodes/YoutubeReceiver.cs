using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace YoutubeExplodes
{
    internal class YoutubeReceiver : ICommands
    {
        private string _url;
        private YoutubeClient _youtubeClient;
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

        public async Task Download()
        {
            var streamManifest = await _youtubeClient.Videos.Streams.GetManifestAsync(_url);
            var streamInfo = (MuxedStreamInfo)streamManifest.Mu
        }

        void ICommands.VideoDownload()
        {
            _ = Download();
        }
    }
}

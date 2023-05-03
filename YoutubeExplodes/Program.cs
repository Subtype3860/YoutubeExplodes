using CliWrap;
using YoutubeExplodes;

namespace YoutubeExplode;

public class Program
{
    public static void Main()
    {
        var yotubeClient = new YoutubeClient();
        ICommands comm = new YoutubeReceiver("https://youtu.be/QnavkXE-jio", yotubeClient);
        comm.GetVideoDescription();
        comm.VideoDownload();
    }
}
using CliWrap;
using YoutubeExplodes;

namespace YoutubeExplode;

public class Program
{
    public static async Task Main()
    {
        var yotubeClient = new YoutubeClient();
        Console.WriteLine("Укажите URL видео");
        var url = Console.ReadLine();
        ICommands comm = new YoutubeReceiver(url!, yotubeClient);
        comm.GetVideoDescription();
        await comm.VideoDownload();
    }
}
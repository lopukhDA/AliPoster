using System.Threading;

namespace TelegramBot
{
	class Program
	{
		static void Main(string[] args)
		{
			var bot = new Bot();
			bot.Run();

			Thread.Sleep(int.MaxValue);
		}
	}
}

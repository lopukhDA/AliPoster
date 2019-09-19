using System;
using System.Configuration;
using System.Linq;
using System.Threading;
using EpnParser.EpnApi;
using EpnParser.EpnApi.Entity;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

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

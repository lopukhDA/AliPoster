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
		private static ITelegramBotClient _botClient;
		private static readonly ChatId ChatId = "-1001418836364";

		static void Main(string[] args)
		{
			Parser pareser = new Parser();
			var productsList = pareser.GetTopProduct(); 
			var product = pareser.GetProduct("32953827389");
			//var product = productsList.FirstOrDefault();

			_botClient = new TelegramBotClient(ConfigurationManager.AppSettings.Get("token"));

			var me = _botClient.GetMeAsync().Result;
			Console.WriteLine(
				$"Hello, World! I am user {me.Id} and my name is {me.FirstName}."
			);

			//var myMsg = new Message
			//{
			//	Text = "Trying *all the parameters* of `sendMessage` method",
			//};

			PostPhoto(product);

			_botClient.OnMessage += OnMessage;

			_botClient.StartReceiving();

			Thread.Sleep(int.MaxValue);
		}

		static async void OnMessage(object sender, MessageEventArgs e)
		{
			if (e.Message.Text != null)
			{
				await _botClient.SendTextMessageAsync(
					chatId: ChatId,
					text: e.Message.Text,
					parseMode: ParseMode.Markdown
				);
			}
		}

		public static async void PostMessage(Message message)
		{
			await _botClient.SendTextMessageAsync(
				chatId: ChatId,
				text: message.Text,
				parseMode: ParseMode.Markdown
			);
		}

		public static async void PostPhoto(Message message)
		{
			await _botClient.SendPhotoAsync(
				chatId: ChatId,
				photo: "https://github.com/TelegramBots/book/raw/master/src/docs/photo-ara.jpg",
				caption: message.Text,
				parseMode: ParseMode.Html
			);
		}

		public static async void PostPhoto(Offer product, Message message = null)
		{
			await _botClient.SendPhotoAsync(
				chatId: ChatId,
				photo: product.Picture.AbsoluteUri,
				caption: $"{product.Name}\nPrice: *{product.Price}$*\n{product.Url}",
				parseMode: ParseMode.Markdown
			);
		}
	}
}

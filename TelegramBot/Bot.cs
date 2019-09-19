using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading;
using EpnParser.EpnApi;
using EpnParser.EpnApi.Entity;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramBot
{
	public class Bot
	{
		private static ITelegramBotClient _botClient;
		private static readonly ChatId ChatId = "-1001418836364";

		private readonly Parser _parser = new Parser();

		public void Run()
		{
			_botClient = new TelegramBotClient(ConfigurationManager.AppSettings.Get("token"));

			var me = _botClient.GetMeAsync().Result;
			Console.WriteLine(
				$"Hello, World! I am user {me.Id} and my name is {me.FirstName}."
			);

			//var myMsg = new Message
			//{
			//	Text = "Trying *all the parameters* of `sendMessage` method",
			//};

			_botClient.OnMessage += OnMessage;

			_botClient.StartReceiving();
		}


		static async void OnMessage(object sender, MessageEventArgs e)
		{
			if (e.Message.Text != null)
			{
				if (e.Message.Text.StartsWith('/'))
				{
					Commands.ExecuteCommand(e.Message.Text);
				}
				else
				{
					await _botClient.SendTextMessageAsync(
						chatId: ChatId,
						text: e.Message.Text,
						parseMode: ParseMode.Markdown
					);
				}
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

		public static async void PostMessage(string text)
		{
			await _botClient.SendTextMessageAsync(
				chatId: ChatId,
				text: text,
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

		public static async void PostPhoto(Offer product)
		{
			await _botClient.SendPhotoAsync(
				chatId: ChatId,
				photo: product.Picture.AbsoluteUri,
				caption: $"{product.Name}\nЦена: *{product.Price}$*\n{product.Url}",
				parseMode: ParseMode.Markdown
			);
		}
	}
}

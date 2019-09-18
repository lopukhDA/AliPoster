//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Text;
//using Common.Interfaces;
//using Telegram.Bot;
//using Telegram.Bot.Args;
//using Telegram.Bot.Types;
//using Telegram.Bot.Types.Enums;

//namespace TelegramBot
//{
//	public static class Bot 
//	{
//		public static ITelegramBotClient BotClient;
//		private static readonly ChatId ChatId = "-1001418836364";

//		public static Bot()
//		{
//			BotClient = new TelegramBotClient(ConfigurationManager.AppSettings.Get("token"));
//			var botClient = new TelegramBotClient("YOUR_ACCESS_TOKEN_HERE");

//			BotClient.OnMessage += OnMessage;

//			BotClient.StartReceiving();
//		}

//		public void Run()
//		{
//			BotClient.StartReceiving();
//		}

//		static async void OnMessage(object sender, MessageEventArgs e)
//		{
//			if (e.Message.Text != null)
//			{
//				await BotClient.SendTextMessageAsync(
//					chatId: ChatId,
//					text: e.Message.Text,
//					parseMode: ParseMode.Markdown
//				);
//			}
//		}

//		public static async void PostMessage(Message message)
//		{
//			await BotClient.SendTextMessageAsync(
//				chatId: ChatId,
//				text: message.Text,
//				parseMode: ParseMode.Markdown
//			);
//		}

//		public static async void PostPhoto(Message message)
//		{
//			await BotClient.SendPhotoAsync(
//				chatId: ChatId,
//				photo: "https://github.com/TelegramBots/book/raw/master/src/docs/photo-ara.jpg",
//				caption: message.Text,
//				parseMode: ParseMode.Html
//			);
//		}

//		public void Notify()
//		{
//			throw new NotImplementedException();
//		}
//	}
//}

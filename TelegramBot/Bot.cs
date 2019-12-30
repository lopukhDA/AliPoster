using System;
using System.Configuration;
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

		private readonly Commands _commands = new Commands();
		private readonly PastProductsFile _pastProductsFile = new PastProductsFile();

		public void Run()
		{
			_botClient = new TelegramBotClient(ConfigurationManager.AppSettings.Get("token"));

			var me = _botClient.GetMeAsync().Result;
			Console.WriteLine(
				$"Hello, World! I am user {me.Id} and my name is {me.FirstName}."
			);

			var myMsg = new Message
			{
				Text = "Trying *all the parameters* of `sendMessage` method",
			};

			_botClient.OnMessage += OnMessage;

			_botClient.StartReceiving();
		}


		async void OnMessage(object sender, MessageEventArgs e)
		{
			if (e.Message.Text != null)
			{
				if (e.Message.Text.StartsWith('/'))
				{
					ExecuteCommand(e.Message.Text);
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

		public void ExecuteCommand(string message)
		{
			var command = _commands.StringToCommand(message);
			switch (command)
			{
				case EnumCommands.NotCommand:
					PostMessage(message);
					break;
				case EnumCommands.Url:
					var productUrl = _commands.Url(message);
					PostPhoto(productUrl);
					break;
				case EnumCommands.Id:
					var productId = _commands.Id(message);
					PostPhoto(productId);
					break;
				case EnumCommands.New:
					var productNew = _commands.New();
					PostPhoto(productNew);
					break;
				default:
					PostMessage(message);
					break;
			}
		}

		public async void PostMessage(Message message)
		{
			await _botClient.SendTextMessageAsync(
				chatId: ChatId,
				text: message.Text,
				parseMode: ParseMode.Markdown
			);
		}

		public async void PostMessage(string text)
		{
			if (!string.IsNullOrEmpty(text))
			{
				await _botClient.SendTextMessageAsync(
					chatId: ChatId,
					text: text,
					parseMode: ParseMode.Markdown
				);
			}
		}

		public async void PostPhoto(Message message)
		{
			if (message != null)
			{
				await _botClient.SendPhotoAsync(
					chatId: ChatId,
					photo: "https://github.com/TelegramBots/book/raw/master/src/docs/photo-ara.jpg",
					caption: message.Text,
					parseMode: ParseMode.Html
				);
			}
		}

		public async void PostPhoto(Offer product)
		{
			if (product != null)
			{
				await _botClient.SendPhotoAsync(
					chatId: ChatId,
					photo: product.Picture.AbsoluteUri,
					caption: $"{product.Name}\nЦена: *{product.Price}$*\n{product.Url}",
					parseMode: ParseMode.Markdown
				);

				_pastProductsFile.WriteFile(product.Id);
			}
		}
	}
}

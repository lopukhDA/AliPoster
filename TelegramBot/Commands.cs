using EpnParser.EpnApi;
using EpnParser.EpnApi.Entity;

namespace TelegramBot
{
	public class Commands
	{
		public static EnumCommands StringToCommand(string message)
		{
			if (message.Contains("/productUrl"))
				return EnumCommands.Url;
			if (message.Contains("/productId"))
				return EnumCommands.Id;
			return EnumCommands.NotCommand;
		}

		public static Offer Url(string message)
		{
			var product = Parser.GetProductFromUrl(message.Replace("/productUrl", "").Trim());

			return product;
		}

		public static Offer Id(string message)
		{
			var product = Parser.GetProduct(message.Replace("/productId", "").Trim());

			return product;
		}

		public static void ExecuteCommand(string message)
		{
			var command = StringToCommand(message);
			switch (command)	
			{
				case EnumCommands.NotCommand:
					Bot.PostMessage(message);
					break;
				case EnumCommands.Url:
					var productUrl = Url(message);
					Bot.PostPhoto(productUrl);
					break;
				case EnumCommands.Id:
					var productId = Id(message);
					Bot.PostPhoto(productId);
					break;
				default:
					Bot.PostMessage(message);
					break;
			}
		}
	}
}

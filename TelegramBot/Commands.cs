using EpnParser.EpnApi;
using EpnParser.EpnApi.Entity;

namespace TelegramBot
{
	public class Commands
	{
		private readonly Parser _parser = new Parser();

		public EnumCommands StringToCommand(string message)
		{
			if (message.Contains("/productUrl"))
				return EnumCommands.Url;
			if (message.Contains("/productId"))
				return EnumCommands.Id;
			if (message.Contains("/new"))
				return EnumCommands.New;
			return EnumCommands.NotCommand;
		}

		public Offer Url(string message)
		{
			var product = Parser.GetProductFromUrl(message.Replace("/productUrl", "").Trim());

			return product;
		}

		public Offer Id(string message)
		{
			var product = Parser.GetProduct(message.Replace("/productId", "").Trim());

			return product;
		}

		public Offer New()
		{
			var product = _parser.GetNewProductFromTopList();

			return product;
		}
	}
}

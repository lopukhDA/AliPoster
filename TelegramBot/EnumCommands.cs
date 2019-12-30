using System.ComponentModel;

namespace TelegramBot
{
	public enum EnumCommands
	{
		[Description(null)]
		NotCommand,
		[Description("productUrl")]
		Url,
		[Description("productId")]
		Id,
		[Description("new")]
		New
	} 
}

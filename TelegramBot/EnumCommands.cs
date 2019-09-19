using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TelegramBot
{
	public enum EnumCommands
	{
		[Description(null)]
		NotCommand,
		[Description("productUrl")]
		Url,
		[Description("productId")]
		Id
	} 
}

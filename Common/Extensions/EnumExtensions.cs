using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace Common.Extensions
{
	public static class EnumExtensions
	{
		public static string ToDescriptionString(this Enum val)
		{
			var attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
			return attributes.Length > 0 ? attributes[0].Description : string.Empty;
		}

		public static string GetDescriptionOrValue(this Enum en)
		{
			var type = en.GetType();
			var memInfo = type.GetMember(en.ToString());

			if (memInfo.Length <= 0) return en.ToString();
			var attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

			return attrs.Length > 0 ? ((DescriptionAttribute)attrs[0]).Description : en.ToString();
		}
	}
}

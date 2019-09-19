using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace EpnParser.EpnApi
{
	public class PastProductsFile
	{
		private const string File = "Products.txt";

		public void WriteFile(string productId)
		{
			if (!IsExist(productId))
			{
				System.IO.File.AppendAllText(File, $"{productId}\n");
			}
		}

		public bool IsExist(string productId)
		{
			var productsId = System.IO.File.ReadAllLines(File).ToList();
			return productsId.Contains(productId);
		}

	}
}

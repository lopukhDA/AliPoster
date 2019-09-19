using EpnParser.EpnApi;

namespace EpnParser
{
	class Program
	{
		static void Main(string[] args)
		{
			var parser = new Parser();
			var myOffer = Parser.GetProduct("33002289288");
			var myOffer2 = Parser.GetProductFromUrl("https://ru.aliexpress.com/item/32928738822.html?spm=a2g0v.best.6.5.51df4z2S4z2Syp&scm=1007.17258.143664.0&pvid=8b6d5498-754b-44af-b95a-65445f0732e3");

			var offers = Parser.GetTopProduct();

			PastProductsFile file = new PastProductsFile();

			var x = parser.GetRandomProductOfTopProduct();

			foreach (var offer in offers)
			{
				file.WriteFile(offer.ProductId);
			}

			var exist = file.IsExist("33035297570");
		}
	}
}
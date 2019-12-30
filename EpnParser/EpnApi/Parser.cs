using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using Common.Extensions;
using EpnParser.EpnApi.Entity;

namespace EpnParser.EpnApi
{
	public class Parser
	{
		private const string Url = "http://api.epn.bz/json";
		//private readonly Random _rand = new Random();
		private readonly PastProductsFile _productsFile = new PastProductsFile();

		public static Offer GetProduct(string id)
		{
			var productReq = new RequestEpn
			{
				Requests = new Requests
				{
					Request = new Request
					{
						ActionRequest = ActionRequest.offer_info,
						Lang = Lang.ru,
						Id = id
					}
				}
			};

			var responseObj = ExecuteMethod(productReq);
			var product = responseObj.Results.Request.Offer;
			return product;
		}

		public static List<Offer> GetTopProduct()
		{
			var top = new RequestEpn
			{
				Requests = new Requests
				{
					Request = new Request
					{
						ActionRequest = ActionRequest.top_monthly,
						Lang = Lang.ru
					}
				}
			};
			var responseObj = ExecuteMethod(top);
			var products = responseObj.Results.Request.Offers;
			return products;
		}

		private static Response ExecuteMethod(RequestEpn request)
		{
			var data = request.ToJson();

			string response;
			using (var webClient = new WebClient())
			{
				response = webClient.UploadString(Url, data);
			}

			var responseObj = JsonExtensions.FromJson<Response>(response);
			return responseObj;
		}

		//public Offer GetRandomProductOfTopProduct()
		//{
		//	var products = GetTopProduct();
		//	var itemNumber = _rand.Next(products.Count);
		//	var product = products[itemNumber];

		//	//loop
		//	if (_productsFile.IsExist(product.ProductId))
		//	{
		//		GetRandomProductOfTopProduct();
		//	}

		//	return product;
		//}

		public static Offer GetProductFromUrl(string url)
		{
			var regex = new Regex(@"\/(\d*)\.html", RegexOptions.Compiled);
			var productsId = regex.Matches(url);

			if (productsId.Count == 0) return null;

			var productId = productsId[0].Groups[1].Value;

			return GetProduct(productId);
		}

		public Offer GetNewProductFromTopList()
		{
			var topProducts = GetTopProduct();

			foreach (var product in topProducts)
			{
				if (!_productsFile.IsExist(product.ProductId))
				{
					return product;
				}
			}

			return null;
		}
	}
}
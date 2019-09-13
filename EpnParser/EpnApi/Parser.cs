using System.Collections.Generic;
using System.Net;

namespace EpnParser.EpnApi
{
	public class Parser
	{
		private const string Url = "http://api.epn.bz/json";

		public List<Offer> GetTopProduct()
		{
			RequestEpn top = new RequestEpn()
			{
				Requests = new Requests()
				{
					Request = new Request()
					{
						ActionRequest = ActionRequest.top_monthly,
						Lang = Lang.ru,
					}
				}
			};
			var responseObj = ExecuteMethod(top);
			var products = responseObj.Results.Request.Offers;
			return products;
		}

		public Offer GetProduct(string id)
		{
			var productReq = new RequestEpn()
			{
				Requests = new Requests()
				{
					Request = new Request()
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

		private Response ExecuteMethod(RequestEpn request)
		{
			var data = request.ToJson();

			string response;
			using (var webClient = new WebClient())
			{

				response = webClient.UploadString(Url, data);
			}

			var responseObj = Response.FromJson(response);
			return responseObj;
		}
	}
}
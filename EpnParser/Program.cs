namespace EpnParser
{
	class Program
	{
		static void Main(string[] args)
		{
			Parser parser = new Parser();
			var offer = parser.GetProduct("32953827389");

			var offers = parser.GetTopProduct();
		}
	}
}
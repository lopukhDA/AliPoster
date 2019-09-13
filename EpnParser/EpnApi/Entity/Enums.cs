namespace EpnParser.EpnApi.Entity
{
	public enum ActionRequest
	{
		top_monthly,
		list_categories,
		offer_info,
		search,
		count_for_categories,
		list_currencies
	}

	public enum Currency { USD, UAH, EUR, KZT, BYR, RUR };

	public enum Lang { ru, en };
}
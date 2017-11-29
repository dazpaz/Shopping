using Newtonsoft.Json;

namespace Shopping.Api.Model
{
	public class ApiBasket
	{
		[JsonProperty("userId")]
		public string UserId { get; set; }

		[JsonProperty("item")]
		public ApiPBasketItem[] Items { get; set; }
	}

	public class ApiPBasketItem
	{
		[JsonProperty("productId")]
		public string ProductId { get; set; }

		[JsonProperty("quantity")]
		public int Quantity { get; set; }
	}
}

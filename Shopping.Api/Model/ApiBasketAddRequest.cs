using Newtonsoft.Json;
using System;

namespace Shopping.Api.Model
{
	public class ApiBasketAddRequest
	{
		[JsonProperty("productId")]
		public Guid ProductId { get; set; }

		[JsonProperty("quantity")]
		public int Quantity { get; set; }
	}
}
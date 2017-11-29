﻿using Shopping.ProductCatalog.Model;

namespace Shopping.CheckoutService.Model
{
	public class CheckoutProduct
	{
		public Product Product { get; set; }
		public int Quantity { get; set; }
		public double Price { get; set; }
	}
}

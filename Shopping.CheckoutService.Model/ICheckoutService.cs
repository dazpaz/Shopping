﻿using Microsoft.ServiceFabric.Services.Remoting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopping.CheckoutService.Model
{
	public interface ICheckoutService : IService
	{
		Task<CheckoutSummary> Checkout(string userId);
		Task<IEnumerable<ICheckoutService>> GetOrderHistory(string userId);
	}
}

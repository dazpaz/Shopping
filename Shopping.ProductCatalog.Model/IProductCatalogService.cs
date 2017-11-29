﻿using Microsoft.ServiceFabric.Services.Remoting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopping.ProductCatalog.Model
{
	public interface IProductCatalogService : IService
	{
		Task<IEnumerable<Product>> GetAllProducts();
		Task AddProduct(Product product);
	}
}

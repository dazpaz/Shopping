using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Shopping.Api.Model;
using System;
using System.Threading.Tasks;
using Shopping.ProductCatalog.Model;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Microsoft.ServiceFabric.Services.Client;
using System.Linq;

namespace Shopping.Api.Controllers
{
	[Route("api/[controller]")]
	public class ProductsController : Controller
	{
		private IProductCatalogService CatalogService { get; set; }

		public ProductsController()
		{
			CatalogService = ServiceProxy.Create<IProductCatalogService>(
				new Uri("fabric:/Shopping/Shopping.ProductCatalog"),
				new ServicePartitionKey(0));
		}

		[HttpGet]
		public async Task<IEnumerable<ApiProduct>> Get()
		{
			IEnumerable<Product> allProducts = await CatalogService.GetAllProducts();

			return allProducts.Select(p => new ApiProduct
			{
				Id = p.Id,
				Name = p.Name,
				Description = p.Description,
				Price = p.Price,
				IsAvailable = p.Availability > 0
			});
		}

		[HttpPost]
		public async Task Post([FromBody] ApiProduct product)
		{
			var newProduct = new Product
			{
				Id = Guid.NewGuid(),
				Name = product.Name,
				Description = product.Description,
				Price = product.Price,
				Availability = 100
			};

			await CatalogService.AddProduct(newProduct);
		}
	}
}

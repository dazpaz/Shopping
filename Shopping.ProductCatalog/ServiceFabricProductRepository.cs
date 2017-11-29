using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shopping.ProductCatalog.Model;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Data.Collections;
using System.Threading;

namespace Shopping.ProductCatalog
{
	class ServiceFabricProductRepository : IProductRepository
	{
		private IReliableStateManager StateManager { get; set; }

		public ServiceFabricProductRepository(IReliableStateManager stateManager)
		{
			StateManager = stateManager;
		}

		public async Task AddProduct(Product product)
		{
			var products = await StateManager.GetOrAddAsync<IReliableDictionary<Guid, Product>>("products");

			using (var tx = StateManager.CreateTransaction())
			{
				await products.AddOrUpdateAsync(tx, product.Id, product, (id, value) => product);
				await tx.CommitAsync();
			}
		}

		public async Task<IEnumerable<Product>> GetAllProducts()
		{
			var products = await StateManager.GetOrAddAsync<IReliableDictionary<Guid, Product>>("products");
			var result = new List<Product>();

			using (var tx = StateManager.CreateTransaction())
			{
				var allProducts = await products.CreateEnumerableAsync(tx, EnumerationMode.Unordered);

				using (var enumerator = allProducts.GetAsyncEnumerator())
				{
					while (await enumerator.MoveNextAsync(CancellationToken.None))
					{
						KeyValuePair<Guid, Product> current = enumerator.Current;
						result.Add(current.Value);
					}
				}
			}

			return result;
		}
	}
}

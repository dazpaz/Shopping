using Shopping.ProductCatalog.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopping.ProductCatalog
{
	interface IProductRepository
	{
		Task<IEnumerable<Product>> GetAllProducts();
		Task AddProduct(Product product);
	}
}
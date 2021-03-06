﻿using System;
using System.Collections.Generic;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Shopping.ProductCatalog.Model;

namespace Shopping.ProductCatalog
{
	internal sealed class ProductCatalog : StatefulService, IProductCatalogService
	{
		private IProductRepository Repository { get; set; }

		public ProductCatalog(StatefulServiceContext context)
			: base(context)
		{ }

		protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
		{
			return new[]
			{
				new ServiceReplicaListener(context => this.CreateServiceRemotingListener(context))
			};

		}

		protected override async Task RunAsync(CancellationToken cancellationToken)
		{
			Repository = new ServiceFabricProductRepository(StateManager);

			var product1 = new Product
			{
				Id = Guid.NewGuid(),
				Name = "Lenovo P-50",
				Description = "Nice work Laptop",
				Price = 1300,
				Availability = 10
			};

			var product2 = new Product
			{
				Id = Guid.NewGuid(),
				Name = "Nokia 650",
				Description = "My phone that broke",
				Price = 200,
				Availability = 15
			};

			var product3 = new Product
			{
				Id = Guid.NewGuid(),
				Name = "Office 365",
				Description = "Words, number, and slide shows",
				Price = 50,
				Availability = 28
			};

			await Repository.AddProduct(product1);
			await Repository.AddProduct(product2);
			await Repository.AddProduct(product3);

			IEnumerable<Product> all = await Repository.GetAllProducts();
		}

		public async Task<IEnumerable<Product>> GetAllProducts()
		{
			return await Repository.GetAllProducts();
		}

		public async Task AddProduct(Product product)
		{
			await Repository.AddProduct(product);
		}

		public async Task<Product> GetProduct(Guid productId)
		{
			return await Repository.GetProduct(productId);
		}
	}
}

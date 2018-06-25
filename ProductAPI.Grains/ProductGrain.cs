using System.Collections.Generic;
using System.Threading.Tasks;
using Orleans;
using ProductAPI.GrainInterfaces;
using ProductAPI.Models;

namespace ProductAPI.Grains
{
    public class ProductGrain : Grain, IProductGrain
    {
        private Dictionary<long, Product> _products;
        private long _currentGrainId;

        public override Task OnActivateAsync()
        {
            _currentGrainId = this.GetPrimaryKeyLong();

            _products = new Dictionary<long, Product>();
            _products.Add(1, new Product
            {
                ID = 1,
                Name = "Note 8",
                BrandName = "Samsung",
            });
            _products.Add(2, new Product
            {
                ID = 2,
                Name = "Galaxy S8",
                BrandName = "Samsung",
            });
            _products.Add(3, new Product
            {
                ID = 3,
                Name = "Galaxy S9",
                BrandName = "Samsung",
            });

            return base.OnActivateAsync();
        }

        public async Task<Product> GetProduct()
        {
            Product product = _products[_currentGrainId]; //Some API call or a source for getting product information. E.g: Elasticsearch

            //Divide and conquer.
            var stockGrain = GrainFactory.GetGrain<IStockGrain>(product.ID);
            var priceGrain = GrainFactory.GetGrain<IPriceGrain>(product.ID);

            Task<int> productStockQuantity = stockGrain.GetStockQuantity();
            Task<double> productPriceInfo = priceGrain.GetPriceInfo();

            await Task.WhenAll(productStockQuantity, productPriceInfo);

            product.Stock = productStockQuantity.Result;
            product.Price = productPriceInfo.Result;

            return product;
        }
    }
}
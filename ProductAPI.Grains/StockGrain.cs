using System.Collections.Generic;
using System.Threading.Tasks;
using Orleans;
using ProductAPI.GrainInterfaces;

namespace ProductAPI.Grains
{
    public class StockGrain : Grain, IStockGrain
    {
        private Dictionary<long, int> _stocks;
        private long _currentGrainId;

        public override Task OnActivateAsync()
        {
            _currentGrainId = this.GetPrimaryKeyLong();

            _stocks = new Dictionary<long, int>();
            _stocks.Add(1, 3);
            _stocks.Add(2, 5);
            _stocks.Add(3, 8);

            return base.OnActivateAsync();
        }
        public Task<int> GetStockQuantity()
        {
            int productStock = _stocks[_currentGrainId]; //Some API call or a source for getting stock information.

            return Task.FromResult(productStock);
        }
    }
}
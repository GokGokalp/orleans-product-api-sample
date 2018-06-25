using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orleans;
using ProductAPI.GrainInterfaces;

namespace ProductAPI.Grains
{
    public class PriceGrain : Grain, IPriceGrain
    {
        private Dictionary<long, double> _prices;
        private long _currentGrainId;

        public override Task OnActivateAsync()
        {
            _currentGrainId = this.GetPrimaryKeyLong();

            _prices = new Dictionary<long, double>();
            _prices.Add(1, 50);
            _prices.Add(2, 55);
            _prices.Add(3, 60);

            return base.OnActivateAsync();
        }

        public Task<double> GetPriceInfo()
        {
            double productPrice = _prices[_currentGrainId]; //Some API call or a source for getting price information.

            return Task.FromResult(productPrice);
        }
    }
}
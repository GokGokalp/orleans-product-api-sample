using System.Threading.Tasks;
using Orleans;

namespace ProductAPI.GrainInterfaces
{
    public interface IStockGrain : IGrainWithIntegerKey
    {
        Task<int> GetStockQuantity();
    }
}
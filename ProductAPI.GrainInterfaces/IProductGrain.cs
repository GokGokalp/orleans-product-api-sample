using System.Threading.Tasks;
using Orleans;
using ProductAPI.Models;

namespace ProductAPI.GrainInterfaces
{
    public interface IProductGrain : IGrainWithIntegerKey
    {
        Task<Product> GetProduct();
    }
}
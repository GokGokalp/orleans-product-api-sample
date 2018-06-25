using System.Threading.Tasks;
using Orleans;

namespace ProductAPI.GrainInterfaces
{
    public interface IPriceGrain : IGrainWithIntegerKey
    {
         Task<double> GetPriceInfo();
    }
}
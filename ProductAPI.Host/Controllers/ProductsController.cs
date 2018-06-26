using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.GrainInterfaces;
using Orleans;
using ProductAPI.Models;
using System.Net;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ProductAPI.Host.Controllers
{
    [Route("api/products")]
    public class ProductsController : Controller
    {
        private readonly IClusterClient _clusterClient;

        public ProductsController(IClusterClient clusterClient)
        {
            _clusterClient = clusterClient;
        }

        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Product))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Description = "No product found for requested id.")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var productGrain = _clusterClient.GetGrain<IProductGrain>(id);

            Product product = await productGrain.GetProduct();

            if(product != null)
            {
                return Ok(product);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
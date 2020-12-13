using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhotoBooth.BL.Facades;
using PhotoBooth.BL.Models.Item.Product;
using PhotoBooth.DEMO.API.Filters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PhotoBooth.DEMO.API.Controllers
{
    [QueryFilter]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductFacade _productFacade;

        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<ActionResult<ProductModel>> GetAllProducts()
        {
            var products = await _productFacade.GetAllProductsAsync();
            return Ok(products);
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductModel>> GetProductById(Guid id)
        {
            var product = await _productFacade.GetProductByIdAsync(id);
            if (product != null)
            {
                return Ok(product);
            }
            return BadRequest(id);
        }

        // POST api/<ProductsController>
        [HttpPost]
        public async Task<ActionResult<ProductModel>> AddProduct(ProductModel product)
        {
            var oldId = product.Id;
            product = await _productFacade.AddProductAsync(product);
            if (product.Id == oldId)
            {
                return BadRequest(product);
            }
            return Ok(product);
        }

        [HttpPost("edit")]
        public async Task<ActionResult<ProductModel>> EditProduct(ProductModel product)
        {
            var success = await _productFacade.EditProductAsync(product);
            if (success)
            {
                return Ok(product);
            }
            return BadRequest(product);
        }

        [HttpPost("delete")]
        public async Task<ActionResult<Guid>> EditProduct(Guid id)
        {
            var success = await _productFacade.DeleteProductAsync(id);
            if (success)
            {
                return Ok(id);
            }
            return BadRequest(id);
        }
    }
}

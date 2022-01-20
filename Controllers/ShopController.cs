using DAW.Data;
using DAW.Data.Abstractions;
using DAW.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAW.Controllers
{
    public class ShopController : Controller
    {
        private DAWContext _context;
        private readonly IProductManager _productManager;

        public ShopController(DAWContext context, IProductManager productManager)
        {
            _context = context;
            _productManager = productManager;

        }
        [HttpGet("shop")]
        public IActionResult Shop()
        {
            var results = from p in _context.Products
                          where p.Available.Equals("1")
                          select p;

            return View(results.ToList());
        }
        [Authorize(Roles = "admin")]
        [HttpGet("GetProduct/{id}")]
        public ActionResult<Product> GetProductById(int id)
        {
            var product = _productManager.GetProductById(id);

            if (product == null)
                return BadRequest("Product was not found!");
            return product;
        }

        [HttpGet("GetAllProducts")]
        public ActionResult<List<Product>> GetAllProducts()
        {
            return _productManager.GetAllProducts();
        }

        [HttpPost("CreateProduct")]
        public async Task<ActionResult<Product>> CreateProduct([FromBody]Product new_product)
        {
            if (new_product == null)
                return BadRequest("Data was not send!");
            var prodEntity = await _productManager.CreateProductAsync(new_product);

            if (prodEntity == null)
                return BadRequest("Error while adding the product to DB.");

            return prodEntity;
        }

        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromBody]Product upd_product)
        {
            var updateProduct = await _productManager.UpdateParoductAsync(upd_product);

            if (updateProduct == null)
                return BadRequest("Error while updating item.");

            return Ok(updateProduct);
        }

        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct([FromBody]Product del_product)
        {
            var delProduct = await _productManager.DeleteProduct(del_product);

            if (delProduct == false)
                return BadRequest("Error while deleting item.");

            return Ok();
        }
    }
}

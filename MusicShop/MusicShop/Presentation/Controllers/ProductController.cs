using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicShop.Domain.Model;
using MusicShop.Infrastructure.Data;
using MusicShop.Presentation.Common.DTOs.Category;
using MusicShop.Presentation.Common.DTOs.Product;

namespace MusicShop.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly DataContext db;
        private readonly IMapper mapper;
        public ProductController(DataContext context, IMapper _mapper)
        {
            db = context;
            mapper = _mapper;
        }
        [HttpGet]
        [Route(template: "GetAllByCategory")]
        public async Task<ActionResult> GetProductByCategory(int id)
        {
            var category = await db.Categories.Where(x => x.Id == id).Include(x => x.Product).ToListAsync();
            var productResponse = mapper.Map<List<Category>, List<CategoryResponseByProduct>>(category);
            return Ok(productResponse);
        }
        [HttpGet]
        [Route(template: "GetAll")]
        public async Task<ActionResult> GetAllProduct()
        {
            var products = await db.Products.Include(x => x.Category).ToListAsync();
            var productsResponse = mapper.Map<List<Product>, List<ProductResponse>>(products);
            return Ok(productsResponse);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetProductById(int id)
        {
            var product = await db.Products.Where(x => x.Id == id).Include(x => x.Category).ToListAsync();
            if (product == null)
            {
                return NotFound();
            }
            var productResponse = mapper.Map<List<Product>, List<ProductResponse>>(product);
            return Ok(productResponse);
        }

        [HttpPost]
        [Route(template: "Add")]
        public async Task<ActionResult> Add(ProductRequest product)
        {
            var category = await db.Categories.Where(x => x.Id == product.CategoryId).FirstOrDefaultAsync();
            var responseProduct = mapper.Map<Product>(product);
            if (category == null)
            {
                return NotFound("Not found category");
            }
            responseProduct.Category = category;
            await db.Products.AddAsync(responseProduct);
            await db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Route(template: "Delete")]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await db.Products.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (product == null)
            {
                return NotFound();
            }
            db.Products.Remove(product);
            await db.SaveChangesAsync();
            return Ok();
        }

        [HttpPatch]
        [Route(template: "Update")]
        public async Task<ActionResult> Update(ProductRequestUpdate product)
        {
            if (product == null)
            {
                return BadRequest("Empty field");
            }
            if (!db.Products.Any(x => x.Id == product.Id))
            {
                return NotFound();
            }
            var findProduct = await db.Products.Where(x => x.Id == product.Id).FirstOrDefaultAsync();
            var findCategory = await db.Categories.Where(x => x.Id == product.CategoryId).FirstOrDefaultAsync();
            findProduct.Name = product.Name;
            findProduct.Description = product.Description;
            findProduct.InStock = product.InStock;
            findProduct.Price = product.Price;
            findProduct.Category = findCategory;
            await db.SaveChangesAsync();
            return Ok();
        }
    }
}

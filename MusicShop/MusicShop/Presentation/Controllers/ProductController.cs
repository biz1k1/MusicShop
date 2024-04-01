using AutoMapper;
using MusicShop.Infrastructure.Repository;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicShop.Application.Common.Behavior;
using MusicShop.Domain.Model;
using MusicShop.Infrastructure.Data;
using MusicShop.Presentation.Common.DTOs.Authentication;
using MusicShop.Presentation.Common.DTOs.Category;
using MusicShop.Presentation.Common.DTOs.Product;

namespace MusicShop.Presentation.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;
        private readonly IValidator<ProductRequest> _validator;
        public ProductController(IUnitOfWork unitOfWork, IMapper _mapper, IValidator<ProductRequest> validator)
        {
            _unitOfWork=unitOfWork;
            mapper = _mapper;
            _validator = validator;
        }
        [HttpGet]
        [Route(template: "GetAllByCategory")]
        public async Task<ActionResult> GetProductByCategory(int id)
        {
            var category = await _unitOfWork.Category.FindByCondition(x => x.Id == id).Include(x => x.Product).ToListAsync();
            var productResponse = mapper.Map<List<Category>, List<CategoryResponseByProduct>>(category);
            return Ok(productResponse);
        }
        [HttpGet]
        [Route(template: "GetAll")]
        public async Task<ActionResult> GetAllProduct()
        {
            var products = _unitOfWork.Product.GetAll();
            var productsResponse = mapper.Map<List<Product>, List<ProductResponse>>((List<Product>)products);
            return Ok(productsResponse);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetProductById(int id)
        {
            var product = await _unitOfWork.Product.FindByCondition(x => x.Id == id).Include(x => x.Category).ToListAsync();
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
            ValidationResult validationResult = await _validator.ValidateAsync(product);
            if (!validationResult.IsValid)
            {
                return ValidationProblem(BehaviorExtensions.AddToModelState(validationResult));
            }
            var category = await _unitOfWork.Category.FindByCondition(x => x.Id == product.CategoryId).FirstOrDefaultAsync();
            var responseProduct = mapper.Map<Product>(product);
            if (category == null)
            {
                return NotFound("Not found category");
            }
            responseProduct.Category = category;
            _unitOfWork.Product.Add(responseProduct);
            await _unitOfWork.SaveAsync();
            return Ok();
        }

        [HttpDelete]
        [Route(template: "Delete")]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _unitOfWork.Product.FindByCondition(x => x.Id == id).FirstOrDefaultAsync();
            if (product == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(product);
            await _unitOfWork.SaveAsync();
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
            if (_unitOfWork.Product.FindByCondition(x => x.Id == product.Id)==null)
            {
                return NotFound();
            }
            var findProduct = await _unitOfWork.Product.FindByCondition(x => x.Id == product.Id).FirstOrDefaultAsync();
            var findCategory = await _unitOfWork.Category.FindByCondition(x => x.Id == product.CategoryId).FirstOrDefaultAsync();
            findProduct.Name = product.Name;
            findProduct.Description = product.Description;
            findProduct.InStock = product.InStock;
            findProduct.Price = product.Price;
            findProduct.Category = findCategory;
            _unitOfWork.Product.Update(findProduct);
            await _unitOfWork.SaveAsync();
            return Ok();
        }
    }
}

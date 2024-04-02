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
    //[Authorize]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<ProductRequest> _validator;
        public ProductController(IUnitOfWork unitOfWork, IMapper mapper, IValidator<ProductRequest> validator)
        {
            _unitOfWork=unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }
        [HttpGet]
        [Route(template: "GetProductsByCategory")]
        public async Task<ActionResult> GetProductByCategory(int id)
        {
            var category = await _unitOfWork.Category.FindByCondition(x => x.Id == id).Include(x => x.Product).ToListAsync();
            var productResponse = _mapper.Map<List<Category>, List<CategoryResponseByProduct>>(category);
            return Ok(productResponse);
        }
        [HttpGet]
        [Route(template: "GetAll")]
        public async Task<ActionResult> GetAllProduct()
        {
            var products = _unitOfWork.Product.GetProductsIncludeCategory();
            var productsResponse = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductResponse>>(products);
            return Ok(productsResponse);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetProductById(int id)
        {
            var product = await _unitOfWork.Product.GetProductIncludeCategoryByIdAsync(id);
            if (product == null)
            {
                ModelState.AddModelError("return", "Product not found");
                return ValidationProblem(ModelState);   
            }
            var productResponse = _mapper.Map<ProductResponse>(product);
            return Ok(productResponse);
        }

        [HttpPost]
        [Route(template: "Add")]
        public async Task<ActionResult> Add(ProductRequest product)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(product);
            if (!validationResult.IsValid )
            {
                return ValidationProblem(BehaviorExtensions.AddToModelState(validationResult));
            }
            var category = await _unitOfWork.Category.GetCategoryByIdAsync(product.CategoryId);
            if (category == null)
            {
                ModelState.AddModelError("return", "Category not found");
                return ValidationProblem(ModelState);
            }

            var responseProduct = _mapper.Map<Product>(product);
            responseProduct.Category = category;
            _unitOfWork.Product.Add(responseProduct);
            _unitOfWork.Category.Update(category);
            await _unitOfWork.SaveAsync();
            return Ok();
        }

        [HttpDelete]
        [Route(template: "Delete")]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _unitOfWork.Product.GetProductByIdAsync(id);
            if (product == null)
            {
                ModelState.AddModelError("return", "Product not found");
                return ValidationProblem(ModelState);
            }
            _unitOfWork.Product.Remove(product);
            await _unitOfWork.SaveAsync();
            return Ok();
        }

        [HttpPatch]
        [Route(template: "Update")]
        public async Task<ActionResult> Update(ProductRequestUpdate product)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(product);
            if (!validationResult.IsValid)
            {
                return ValidationProblem(BehaviorExtensions.AddToModelState(validationResult));
            }
            var findProduct = await _unitOfWork.Product.GetProductByIdAsync(product.Id);
            if (findProduct==null)
            {
                ModelState.AddModelError("return", "Product not found");
                return ValidationProblem(ModelState);
            }

            
            var responseProduct = _mapper.Map<Product>(product);
            var findCategory = await _unitOfWork.Category.FindByCondition(x => x.Id == product.CategoryId).FirstOrDefaultAsync();
            responseProduct.Category = findCategory;
            _unitOfWork.Product.Update(responseProduct);
            await _unitOfWork.SaveAsync();
            return Ok();
        }
    }
}

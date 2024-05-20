using AutoMapper;
using MusicShop.Infrastructure.Repository;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicShop.Application.Common.Behavior;
using MusicShop.Presentation.Common.DTOs.Category;
using MusicShop.Presentation.Common.DTOs.Product;
using MusicShop.Domain.Model.Core;
using MusicShop.Application.Common.Errors;


namespace MusicShop.Presentation.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<ProductRequest> _validator;
        public ProductController(
            IUnitOfWork unitOfWork, 
            IMapper mapper, 
            IValidator<ProductRequest> validator)
        {
            _unitOfWork=unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }


        [Authorize(Policy = "Read")]
        [Route(template: "ProductByCategory")]
        [HttpGet]
        public async Task<ActionResult> GetProductByCategory(int id)
        {
            var category = await _unitOfWork.Category.GetCategoryWithProducts(id);

            if (category == null)
            {
                throw new CategoryNotFound();
            }

            var productResponse = _mapper.Map<CategoryEntity, CategoryResponseByProduct>(category);
            return Ok(productResponse);
        }


        [Authorize(Policy = "Read")]
        [HttpGet]
        [Route(template: "GetAll")]
        public async Task<ActionResult> GetAllProduct()
        {
            var products = await _unitOfWork.Product.GetProductsIncludeCategoryAsync();
            var productsResponse = _mapper.Map<IEnumerable<ProductEntity>, IEnumerable<ProductResponse>>(products);
            return Ok(productsResponse);
        }


        [Authorize(Policy = "Read")]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetProductById(int id)
        {
            var product = await _unitOfWork.Product.GetProductIncludeCategoryByIdAsync(id);

            if (product == null)
            {
                throw new ProductNotFound();
            }

            var productResponse = _mapper.Map<ProductResponse>(product);
            return Ok(productResponse);
        }


        [Authorize(Policy = "Create")]
        [HttpPost]
        [Route(template: "Create")]
        public async Task<ActionResult> CreateProduct(ProductRequest product)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(product);

            if (!validationResult.IsValid )
            {
                return ValidationProblem(BehaviorExtensions.AddToModelState(validationResult));
            }

            var category = await _unitOfWork.Category.GetByIdAsync(product.CategoryId);

            if (category == null)
            {
                throw new CategoryNotFound();
            }

            var responseProduct = _mapper.Map<ProductEntity>(product);
            responseProduct.Category = category;

            _unitOfWork.Product.Add(responseProduct);
            _unitOfWork.Category.Update(category);
            await _unitOfWork.SaveAsync();
            return Ok(responseProduct);
        }


        [Authorize(Policy = "Delete")]
        [HttpDelete]
        [Route(template: "Delete")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _unitOfWork.Product.GetByIdAsync(id);

            if (product == null)
            {
                throw new ProductNotFound();
            }

            _unitOfWork.Product.Remove(product);
            await _unitOfWork.SaveAsync();
            return Ok();
        }


        [Authorize(Policy = "Update")]
        [HttpPut]
        [Route(template: "Update")]
        public async Task<ActionResult> UpdateProduct(ProductRequestUpdate product)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(product);

            if (!validationResult.IsValid)
            {
                return ValidationProblem(BehaviorExtensions.AddToModelState(validationResult));
            }

            var productWithCategory = await _unitOfWork.Product.GetProductIncludeCategoryByIdAsync(product.Id);

            if (productWithCategory==null)
            {
                throw new ProductNotFound();
            }
            var findCategory = await _unitOfWork.Category.GetByIdAsync(product.CategoryId);
            if (findCategory==null)
            {
                throw new CategoryNotFound();
            }

            productWithCategory.Category = findCategory;
            _unitOfWork.Product.Update(productWithCategory);
            await _unitOfWork.SaveAsync();
            return Ok();
        }
    }
}

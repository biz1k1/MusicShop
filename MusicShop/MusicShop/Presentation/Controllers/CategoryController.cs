using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MusicShop.Presentation.Common.DTOs.Category;
using FluentValidation;
using MusicShop.Infrastructure.Repository;
using FluentValidation.Results;
using MusicShop.Application.Common.Behavior;
using MusicShop.Application.Services.ServiceHandler;
using MusicShop.Domain.Model.Core;
using Microsoft.AspNetCore.Authorization;


namespace MusicShop.Presentation.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICategoryServicesHandler _services;
        private readonly IValidator<CategoryRequest> _validator;
        public CategoryController(IUnitOfWork repository, IMapper mapper, ICategoryServicesHandler services,
            IValidator<CategoryRequest> validator)
        {
            _unitOfWork = repository;
            _mapper = mapper;
            _services = services;
            _validator = validator;

        }
        [Authorize(Policy = "Read")]
        [Route(template: "GetAll")]
        [HttpGet]
        public async Task<ActionResult> GetCategories()
        {
            var FullTreeCategories = await _services.GetFullTreeCategories();
            var categoriesResponse = _mapper.Map<List<CategoryEntity>, List<CategoryResponse>>((List<CategoryEntity>)FullTreeCategories);
            return Ok(categoriesResponse);
        }
        [Authorize(Policy = "Read")]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetCategoryById(int id)
        {
            var category = await _unitOfWork.Category.GetCategoryWithChildren(id);
            if (category.Count()==0)
            {
                ModelState.AddModelError("return", "Category not found");
                return ValidationProblem(ModelState);
            }
            var categoryResponse = _mapper.Map<List<CategoryEntity>, List<CategoryResponse>>((List<CategoryEntity>)category);

            return Ok(categoryResponse);
        }
        [Authorize(Policy = "Create")]
        [Route(template: "Create")]
        [HttpPost]
        public async Task<ActionResult> AddCategory(CategoryRequest category) {
            ValidationResult validationResult = await _validator.ValidateAsync(category);
            if (!validationResult.IsValid) {
                return ValidationProblem(BehaviorExtensions.AddToModelState(validationResult));
            }
            var responseCategory = _mapper.Map<CategoryEntity>(category);
            var subCategory = await _unitOfWork.Category.GetByIdAsync(category.SubCategoryId);
            if (subCategory != null) {
                subCategory.ChildCategories.Add(responseCategory);
            }
            _unitOfWork.Category.Add(responseCategory);
            await _unitOfWork.SaveAsync();
            return Ok();
        }
        [Authorize(Policy = "Delete")]
        [Route(template: "Delete")]
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var category = await _unitOfWork.Category.GetByIdAsync(id);
            if (category == null)
            {
                ModelState.AddModelError("return", "Category not found");
                return ValidationProblem(ModelState);
            }

            _unitOfWork.Category.Remove(category);
            await _unitOfWork.SaveAsync();
            return Ok();
        }
        [Authorize(Policy = "Update")]
        [Route(template: "Update")]
        [HttpPatch]
        public async Task<ActionResult> Update(CategoryResponseUpdate category)
        {

            var categoryResponse = _mapper.Map<CategoryEntity>(category);
            _unitOfWork.Category.Update(categoryResponse);
            await _unitOfWork.SaveAsync();
            return Ok();
        }

    }
}

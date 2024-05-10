using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MusicShop.Presentation.Common.DTOs.Category;
using FluentValidation;
using MusicShop.Infrastructure.Repository;
using FluentValidation.Results;
using MusicShop.Application.Common.Behavior;
using MusicShop.Application.Services.ServiceHandler;
using MusicShop.Domain.Model.Core;
using Microsoft.AspNetCore.Authorization;
using MusicShop.Application.Common.Errors;


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
        public CategoryController(
            IUnitOfWork repository, 
            IMapper mapper, 
            ICategoryServicesHandler services,
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
            var categoryEntity = await _unitOfWork.Category.GetCategoryWithChildren(id);

            if (categoryEntity==null)
            {
                throw new CategoryNotFound();
            }

            var categoryResponse = _mapper.Map<CategoryEntity, CategoryResponse>(categoryEntity);
            return Ok(categoryResponse);
        }


        [Authorize(Policy = "Create")]
        [Route(template: "Create")]
        [HttpPost]
        public async Task<ActionResult> AddCategory(CategoryRequest categoryRequest) {

            ValidationResult validationResult = await _validator.ValidateAsync(categoryRequest);

            if (!validationResult.IsValid)
            {
                return ValidationProblem(BehaviorExtensions.AddToModelState(validationResult));
            }

            var categoryEntity = _mapper.Map<CategoryEntity>(categoryRequest);
            var parentCategory = await _unitOfWork.Category.GetByIdAsync(categoryRequest.SubCategoryId);

            if (parentCategory != null)
            {
                parentCategory.ChildCategories.Add(categoryEntity);
            }
            _unitOfWork.Category.Add(categoryEntity);
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
                throw new CategoryNotFound();
            }

            _unitOfWork.Category.Remove(category);
            await _unitOfWork.SaveAsync();
            return Ok();
        }


        [Authorize(Policy = "Update")]
        [Route(template: "Update")]
        [HttpPut]
        public async Task<ActionResult> Update(CategoryRequestUpdate categoryRequest)
        {
            var categoryToChange = await _unitOfWork.Category.GetByIdAsync(categoryRequest.CategoryToChangeId);
            var parentCategory = await _unitOfWork.Category.GetByIdAsync(categoryRequest.ParentCategoryId);
            if (categoryToChange == null )
            {
                throw new CategoryNotFound();
            }

            if (parentCategory?.Id == categoryToChange.Id)
            {
                throw new CategoryReference();
            }
            categoryToChange.ParentCategoryId = parentCategory?.Id;
            _unitOfWork.Category.Update(categoryToChange);
            await _unitOfWork.SaveAsync();
            return Ok();
        }

    }
}

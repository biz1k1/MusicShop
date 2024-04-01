using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MusicShop.Domain.Model;
using MusicShop.Presentation.Common.DTOs.Category;
using MusicShop.Application.Services.ServiceHandler;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using MusicShop.Infrastructure.Repository;
using MusicShop.Application.Services.Authentication.Identity;
using FluentValidation.Results;
using MusicShop.Application.Common.Behavior;
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
        public CategoryController(IUnitOfWork repository, IMapper mapper, ICategoryServicesHandler services, IValidator<CategoryRequest> validator)
        {
            _unitOfWork = repository;
            _mapper = mapper;
            _services = services;
            _validator = validator;

        }


        [Route(template: "GetAll")]
        [HttpGet]
        public async Task<ActionResult> GetCategories()
        {
            var FullTreeCategories = _services.GetFullTreeCategories().ToList();
            var categoriesResponse = _mapper.Map<List<Category>, List<CategoryResponse>>(FullTreeCategories);
            return Ok(categoriesResponse);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetCategoryById(int id)
        {
            var category = await _unitOfWork.Category.FindByCondition(x=>x.Id==id).Include(x => x.ChildCategories).ThenInclude(x => x.ChildCategories).ToListAsync();
            if (category == null)
            {
                return BadRequest("Category not found");
            }
            var categoryResponse = _mapper.Map<List<Category>, List<CategoryResponse>>(category);
            return Ok(categoryResponse);
        }

        [Route(template: "Add")]
        [Authorize(Policy = IdentityData.Admin)]
        [HttpPost]
        public async Task<ActionResult> AddCategory(CategoryRequest category) {
            ValidationResult validationResult = await _validator.ValidateAsync(category);
            if (!validationResult.IsValid) {
                return ValidationProblem(BehaviorExtensions.AddToModelState(validationResult));
            }

            var responseCategory = _mapper.Map<Category>(category);
            var subCategory = _unitOfWork.Category.GetCategoryByIdAsync(category.SubCategoryId).;
            if (subCategory != null) {
                subCategory.ChildCategories.Add(responseCategory);
            }
            _unitOfWork.Category.Add(responseCategory);
            await _unitOfWork.SaveAsync();
            return Ok();
        }

        [Route(template: "Delete")]
        [Authorize(Policy = IdentityData.Admin)]
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var category =  await _unitOfWork.Category.FindByCondition(x => x.Id == id).FirstOrDefaultAsync();
            if (category == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove((Category)category);
            await _unitOfWork.SaveAsync();
            return Ok();
        }

        [Route(template: "Update")]
        [Authorize(Policy = IdentityData.Admin)]
        [HttpPatch]
        public async Task<ActionResult> Update(CategoryResponseUpdate category)
        {
            var foundCategory = await _unitOfWork.Category.FindByCondition(x=>x.Id==category.Id).FirstOrDefaultAsync();
            if (foundCategory == null)
            {
                return BadRequest("Category not found");
            }
            var responseCategory = _mapper.Map<Category>(category);
            _unitOfWork.Category.Update(responseCategory);
            //foundCategory.Name = category.Name;
            //foundCategory.ParentCategoryId = category.ParentCategoryId;
            await _unitOfWork.SaveAsync();
            return Ok();
        }

    }
}

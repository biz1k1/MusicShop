using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MusicShop.Infrastructure.Data;
using MusicShop.Domain.Model;
using MusicShop.Presentation.Common.DTOs.Category;
using MusicShop.Application.Services;
using MusicShop.Application.Services.ServiceHandler;
using MusicShop.Application.Common.Behavior;
using FluentValidation;
using MusicShop.Presentation.Common.DTOs.Authentication;
using Azure.Core;
using FluentValidation.Results;
namespace MusicShop.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly DataContext _db;
        private readonly IMapper _mapper;
        private readonly ICategoryServicesHandler _services;
        private readonly IValidator<CategoryRequest> _validator;
        public CategoryController(DataContext context, IMapper mapper, ICategoryServicesHandler services, IValidator<CategoryRequest> validator)
        {
            _db = context;
            _mapper = mapper;
            _services = services;
            _validator = validator;

        }


        [Route(template: "GetAll")]
        [HttpGet]
        public async Task<ActionResult> GetCategories()
        {
            var FullTreeCategories = _services.GetFullTreeCategories();
            var categoriesResponse = _mapper.Map<List<Category>, List<CategoryResponse>>((List<Category>)FullTreeCategories);
            return Ok(categoriesResponse);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetCategoryById(int id)
        {
            var category = _db.Categories.Where(x => x.Id == id).Include(x => x.ChildCategories).ThenInclude(x => x.ChildCategories).ToList();
            if (category == null)
            {
                return BadRequest("Category not found");
            }
            var categoryResponse = _mapper.Map<List<Category>, List<CategoryResponse>>(category);
            return Ok(categoryResponse);
        }
        [Route(template: "Add")]
        [HttpPost]
        public async Task<ActionResult> AddCategory(CategoryRequest category)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(category);
            if (!validationResult.IsValid)
            {
                return ValidationProblem(BehaviorExtensions.AddToModelState(validationResult));
            }
            var responseCategory = _mapper.Map<Category>(category);
            var subCategory = await _db.Categories.FindAsync(category.SubCategoryId);
            if (subCategory != null)
            {
                subCategory.ChildCategories.Add(responseCategory);
            }
            await _db.Categories.AddAsync(responseCategory);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Route(template: "Delete")]
        public async Task<ActionResult> Delete(int id)
        {
            Category category = await _db.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPatch]
        [Route(template: "Update")]
        public async Task<ActionResult> Update(CategoryResponseUpdate category)
        {
            var foundCategory = await _db.Categories.FindAsync(category.Id);
            if (foundCategory == null)
            {
                return BadRequest("Category not found");
            }

            foundCategory.Name = category.Name;
            foundCategory.ParentCategoryId = category.ParentCategoryId;
            await _db.SaveChangesAsync();
            return Ok();
        }

    }
}

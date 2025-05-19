using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiPizushi.Data;
using WebApiPizushi.Data.Entities;
using WebApiPizushi.Interfaces;
using WebApiPizushi.Models.Category;

namespace WebApiPizushi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController(AppDbPizushiContext pizushiContext,
    IMapper mapper, IImageService imageService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> List()
    {
        var model = await mapper.ProjectTo<CategoryItemModel>(pizushiContext.Categories)
            .ToListAsync();

        return Ok(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CategoryCreateModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var repeated = await pizushiContext.Categories.Where(x => x.Name == model.Name).SingleOrDefaultAsync();
        if (repeated != null)
        {
            return BadRequest($"{model.Name} already exists");
        }
        var entity = mapper.Map<CategoryEntity>(model);
        entity.Image = await imageService.SaveImageAsync(model.ImageFile!);
        await pizushiContext.Categories.AddAsync(entity);
        await pizushiContext.SaveChangesAsync();
        return Ok(entity);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Edit(int id, [FromForm] CategoryEditModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var item = await pizushiContext.Categories.Where(x => x.Id == id).SingleOrDefaultAsync();
        if (item == null)
        {
            return BadRequest("No item with such id");
        }

        item.Name = model.Name;
        item.Slug = model.Slug;
        await imageService.DeleteImageAsync(item.Image);
        item.Image = await imageService.SaveImageAsync(model.ImageFile!);

        pizushiContext.Categories.Update(item);
        await pizushiContext.SaveChangesAsync();

        return Ok(item);
    }
}

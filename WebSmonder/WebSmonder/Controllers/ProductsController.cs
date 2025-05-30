﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebSmonder.Data;
using WebSmonder.Models.Helpers;
using WebSmonder.Models.Product;

namespace WebSmonder.Controllers;

public class ProductsController(AppSmonderDbContext context, 
    IMapper mapper) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(ProductSearchViewModel searchModel) //Це будь-який web результат - View - сторінка, Файл, PDF, Excel
    {
        ViewBag.Title = "Продукти";

        searchModel.Categories = await mapper.ProjectTo<SelectItemViewModel>(context.Categories)
            .ToListAsync();

        searchModel.Categories.Insert(0, new SelectItemViewModel
        {
            Id = 0,
            Name = "Оберіть категорію"
        });

        var query = context.Products.AsQueryable();

        if(!string.IsNullOrEmpty(searchModel.Name))
        {
            string textSearch = searchModel.Name.Trim();
            query = query.Where(p => p.Name.ToLower().Contains(textSearch.ToLower()));
        }

        if (searchModel.CategoryId != 0)
            query = query.Where(p => p.CategoryId==searchModel.CategoryId);

        if (!string.IsNullOrEmpty(searchModel.Description))
        {
            string textSearch = searchModel.Description.Trim();
            query = query.Where(p => p.Description.ToLower().Contains(textSearch.ToLower()));
        }

        var model = new ProductListViewModel();

        model.Count = query.Count();

        //Відбір тих елементів, які відображаються на сторінці
        model.Products = mapper.ProjectTo<ProductItemViewModel>(query).ToList();
        model.Search = searchModel;

        return View(model);
    }
}

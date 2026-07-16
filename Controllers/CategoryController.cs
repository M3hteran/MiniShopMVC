using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniShopMVC.Models;
using MiniShopMVC.Services.Interfaces;

namespace MiniShopMVC.Controllers
{
	[Authorize(Roles = "Admin")]
	public class CategoryController : Controller
	{
		private readonly ICategoryService _categoryService;

		public CategoryController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		public async Task<IActionResult> Index()
		{
			var categories = await _categoryService.GetAllAsync();

			return View(categories);
		}

		//--------------------------------------GET----------------------------------------

		public IActionResult Create(int id)
		{
			return View();
		}

		public async Task<IActionResult> Edit(int id)
		{

			var category = await _categoryService.GetByIdAsync(id);

			if (category == null)
			{
				return NotFound();
			}

			return View(category);
		}


		public async Task<IActionResult> Delete(int id)
		{
			var category = await _categoryService.GetByIdAsync(id);
			if (category == null)
			{

				return NotFound();

			}

			return View(category);

		}


		//--------------------------------------Post----------------------------------------

		[HttpPost]
		public async Task<IActionResult> Create(Category category)
		{
			if (!ModelState.IsValid)
			{
				return View(category);
			}

			await _categoryService.AddAsync(category);

			return RedirectToAction("Index");
		}

		[HttpPost]

		public async Task<IActionResult> Edit(Category category)
		{
			if (!ModelState.IsValid)
			{
				return View(category);
			}

			await _categoryService.UpdateAsync(category);

			return RedirectToAction("Index");

		}

		[HttpPost]

		public async Task<IActionResult> Delete(Category category)
		{
			await _categoryService.DeleteAsync(category.Id);
			return RedirectToAction("Index");

		}
	}
}

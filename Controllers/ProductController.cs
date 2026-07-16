using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniShopMVC.Models;
using MiniShopMVC.Services.Interfaces;
using MiniShopMVC.ViewModels;

namespace MiniShopMVC.Controllers
{
	public class ProductController : Controller
	{
		private readonly IProductService _productService;

		private readonly ICategoryService _categoryService;

		public ProductController(IProductService productService, ICategoryService categoryService)
		{
			_productService = productService;
			_categoryService = categoryService;
		}

		//Index Sayfası

		public async Task<IActionResult> Index(string? search, int? categoryId)
		{
			var products = await _productService.GetAllWithCategoryAsync();

			var categories = await _categoryService.GetAllAsync();

			if (!string.IsNullOrWhiteSpace(search))
			{
				products = products.Where(x => x.Name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
				ViewBag.Search = search;
			}

			if (categoryId.HasValue)
			{
				products = products.Where(x => x.CategoryId == categoryId.Value).ToList();
			}

			ViewBag.Search = search;
			ViewBag.CategoryId = categoryId;
			ViewBag.Categories = categories;

			return View(products);
		}



		//------------------------------------------------GET METOTLARI--------------------------------------

		//Create Get Metodu
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Create()
		{
			var categories = await _categoryService.GetAllAsync();
			ViewBag.Categories = categories;
			return View();
		}

		//Edit Get Metodu
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Edit(int id)
		{
			var product = await _productService.GetByIdAsync(id);
			if (product == null)
			{
				return NotFound();
			}

			var categories = await _categoryService.GetAllAsync();

			ViewBag.Categories = categories;

			return View(product);

		}

		//Delete GET Metodu
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Delete(int id)
		{
			var product = await _productService.GetByIdAsync(id);
			if (product == null)
			{

				return NotFound();

			}

			return View(product);

		}
		//--------------------------------------------------SON------------------------------------------

		//Detaylar Sayfası
		public async Task<IActionResult> Details(int id)
		{
			var product = await _productService.GetByIdWithCategoryAsync(id);

			if (product == null)
			{
				return NotFound();
			}

			return View(product);

		}

		//---------------------------------------POST METOTLARI--------------------------------------------
		[HttpPost]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Create(Product product, IFormFile? imageFile)
		{
			ModelState.Remove("Category");

			if (!ModelState.IsValid)
			{
				ViewBag.Categories = await _categoryService.GetAllAsync();
				return View(product);
			}

			if (imageFile != null && imageFile.Length > 0)
			{
				var fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);

				var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "products", fileName);

				using (var stream = new FileStream(filepath, FileMode.Create))
				{
					await imageFile.CopyToAsync(stream);
				}

				product.ImageUrl = "/images/products/" + fileName;
			}

			await _productService.AddAsync(product);

			return RedirectToAction("Index");

		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Edit(Product product, IFormFile? imageFile)
		{
			ModelState.Remove("Category");

			if (!ModelState.IsValid)
			{
				ViewBag.Categories = await _categoryService.GetAllAsync();
				return View(product);
			}

			var extistingProduct=await _productService.GetByIdAsync(product.Id);

			if (extistingProduct == null) 
			{
				return NotFound();
			}

			extistingProduct.Name = product.Name;
			extistingProduct.Description = product.Description;
			extistingProduct.Price = product.Price;
			extistingProduct.Stock = product.Stock;
			extistingProduct.CategoryId = product.CategoryId;

			if (imageFile != null && imageFile.Length > 0)
			{
				var fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);

				var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "products", fileName);

				using var stream = new FileStream(filepath, FileMode.Create);
				
				await imageFile.CopyToAsync(stream);

				extistingProduct.ImageUrl = "/images/products/" + fileName;
			}


			await _productService.UpdateAsync(extistingProduct);

			return RedirectToAction("Index");

		}
		[HttpPost]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Delete(Product product)
		{
			await _productService.DeleteAsync(product.Id);
			return RedirectToAction("Index");
		}

		//-------------------------------------------SON---------------------------------------------------
	}
}

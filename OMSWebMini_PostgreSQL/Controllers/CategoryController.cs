using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using OMSWebMini_PostgreSQL.Models;
using System.Data;

namespace OMSWebMini_PostgreSQL.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private IDbConnection db;

		public CategoryController(IConfiguration configuration)
		{
			db = new NpgsqlConnection(configuration.GetConnectionString("Northwind"));
		}

		[HttpGet]
		[Route("api/[controller]/GetCategories")]
		public async Task<List<Category>> GetCategories()
		{
			var categorylist = await db.QueryAsync<Category>("SELECT * FROM public.categories", new { });
			return categorylist.ToList();
		}

		[HttpGet]
		[Route("api/[controller]/GetCategoryById")]
		public async Task<List<Category>> GetCategoryById(int id)
		{
			var categoryById = await db.QueryAsync<Category>("SELECT * FROM public.categories where category_id=@id", new { id });
			return categoryById.ToList();
		}

		[HttpPost]
		[Route("api/[controller]/PostCategory")]
		public async Task<ActionResult<IEnumerable<Category>>> PostCategory(Category category)
		{
			var postCategory = await db.ExecuteAsync("INSERT INTO public.categories (category_id, category_name, description) VALUES (@CategoryId, @CategoryName, @Description)",
				category);
			return Ok(postCategory);
		}

		[HttpPut]
		[Route("api/[controller]/UpdateCategory")]
		public async Task<IActionResult> UpdateCategory(Category category)
		{
			var updateCategory = await db.ExecuteAsync("UPDATE public.categories SET category_name = @CategoryName, description = @Description WHERE category_id = @CategoryId",
				category);
			return Ok(updateCategory);
		}

		[HttpDelete]
		[Route("api/[controller]/DeleteCategory")]
		public async Task<ActionResult<Category>> DeleteCategory(int id)
		{
			var deleteCategory = await db.ExecuteAsync("DELETE FROM public.categories WHERE category_id = @id", new { id });
			return Ok(deleteCategory);
		}
	}
}

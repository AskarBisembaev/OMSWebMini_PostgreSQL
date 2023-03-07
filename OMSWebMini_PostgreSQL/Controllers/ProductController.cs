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
	public class ProductController : ControllerBase
	{
		private IDbConnection db;

		public ProductController(IConfiguration configuration)
		{
			db = new NpgsqlConnection(configuration.GetConnectionString("Northwind"));
		}

		[HttpGet]
		[Route("api/[controller]/Get")]
		public async Task<List<Product>> Get()
		{
			var categorylist = await db.QueryAsync<Product>("SELECT * FROM public.products", new { });
			return (List<Product>)categorylist;
		}

		[HttpGet]
		[Route("api/[controller]/GetProductsInPriceRange")]
		public async Task<List<Product>> GetProductsInPriceRange(int minPrice, int maxPrice)
		{
			var productList = await db.QueryAsync<Product>("SELECT * FROM public.products " +
				"where unit_price >= @minPrice AND unit_price <= @maxPrice", new { minPrice, maxPrice });
			return (List<Product>)productList;
		}
	}
}

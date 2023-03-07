namespace OMSWebMini_PostgreSQL.Models
{
	public class Product
	{
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public int CategoryId { get; set; }
		public int UnitPrice { get; set; }
		public int Discontinued { get; set; }
	}
}

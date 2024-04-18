using bai3.Models;

namespace bai3.Models
{
	[Serializable]
	public class CartItem
	{
		public Product Product { get; set; }
		public int Quantity { get; set; }
	}
}
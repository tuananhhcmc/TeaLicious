using bai3.Models;

namespace bai3.ViewModels
{
	public class CartViewModel
	{
		public List<Menu> Menus { get; set; }
		public List<Blog> Blogs { get; set; }
		public List<CartItem> CartItems { get; set; }
	}
}
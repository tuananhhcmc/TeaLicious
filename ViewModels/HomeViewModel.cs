using bai3.Models;
namespace bai3.ViewModels
{
    public class HomeViewModel
    {
        public List<Menu> Menus { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<Slider> Sliders { get; set; }
        public List<Product> TeaProds { get; set; }
        public List<Product> CoffeeProds { get; set; }
        public Catology TeaCateProds { get; set; }
        public Catology CoffeeCateProds { get; set; }
    }
}
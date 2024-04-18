using bai3.Models;

namespace bai3.ViewModels
{
    public class UserViewModel
    {
        public List<Menu> Menus { get; set; }
        public List<Blog> Blogs { get; set; }
        public User Register { get; set; }
        public UserViewModel()
        {
            Register = new User();

        }
    }
}

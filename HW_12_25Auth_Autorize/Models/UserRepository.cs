namespace HW_12_25Auth_Autorize.Models
{
    public class UserRepository
    {
        public List<User> Users { get; set; }
        public UserRepository() 
        {
            Users = new List<User>()
            {
              new User() { Id = 1, Email = "sfdhj9pa12@gmail.com", Password = "124125", CreatedAt = DateTime.Now.AddYears(-20), Role = new Role{Name ="Admin" } },
              new User() { Id = 2, Email = "sj9pa12", Password = "12412", CreatedAt = DateTime.Now.AddYears(-15), Role = new Role{Name ="Guest" } },
              new User() { Id = 3, Email = "dhj9pa12", Password = "12125", CreatedAt = DateTime.Now.AddYears(-17), Role = new Role{Name ="Editor" } }
            };
        }
        public void AddUser(User user)
        {
            Users.Add(user);
        }
    }
}


namespace Grocery.Core.Models
{
    public partial class Client : Model
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public Roles Role { get; set; } = Roles.None;
        public Client(int id, string name, string emailAddress, string password, Roles role = Roles.None) : base(id, name)
        {
            EmailAddress=emailAddress;
            Password=password;
            Role = role;
        }
    }
}

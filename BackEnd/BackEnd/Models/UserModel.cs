using System.Diagnostics.CodeAnalysis;

namespace BackEnd.Controllers
{
    [ExcludeFromCodeCoverage]
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
    }
}

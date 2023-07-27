using System.Diagnostics.CodeAnalysis;

namespace WallStreetBetsApp.Server.Controllers
{
    [ExcludeFromCodeCoverage]
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
    }
}

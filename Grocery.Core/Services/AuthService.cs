using Grocery.Core.Helpers;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.Core.Services
{
    public class AuthService : IAuthService
    {
        public IClientService _clientService;
        public Client? CurrentClient { get; set; }
        public AuthService(IClientService clientService)
        {
            _clientService = clientService;
        }
        public Client? Login(string email, string password)
        {
            Client? client = _clientService.Get(email);
            if (client == null) return null;
            if (PasswordHelper.VerifyPassword(password, client.Password))
            {
                SetCurrentClient(client);
                return client;
                }
            return null;
        }
        public void SetCurrentClient(Client? client)
        {
            CurrentClient = client;
        }
    }
}

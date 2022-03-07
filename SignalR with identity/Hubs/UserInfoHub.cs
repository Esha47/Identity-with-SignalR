using Microsoft.AspNetCore.SignalR;
using SignalR_with_identity.Models;
using System.Threading.Tasks;

namespace SignalR_with_identity.Hubs
{
    public class UserInfoHub:Hub
    {
       /* public async Task SendUserInfo(string name)
        {
            await Clients.All.SendAsync("ReceiveInfo", name);
        }*/
        public async Task SendUserInfo(UserApp user)
        {
            await Clients.All.SendAsync("ReceiveMessage", user);
        }
    }
}

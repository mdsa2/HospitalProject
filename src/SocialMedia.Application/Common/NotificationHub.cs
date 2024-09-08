﻿using Microsoft.AspNetCore.SignalR;

namespace SocialMedia.Application.Common
{
    public class NotificationHub:Hub
    {
        public async Task SendNotification(string userId, string message)
        {
 
            await Clients.User(userId).SendAsync("ReceiveNotification", message);
        }
    }
}

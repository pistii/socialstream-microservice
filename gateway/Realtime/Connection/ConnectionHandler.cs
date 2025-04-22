﻿using shared_libraries.Auth;
using shared_libraries.Auth.Helpers;
using shared_libraries.Models;
using gateway.Realtime.Helpers;
using Microsoft.AspNetCore.SignalR;
using shared_libraries.Auth;

namespace gateway.Realtime.Connection
{
    [Authorize]
    public class ConnectionHandler<T> : Hub<T> where T : class
    {
        IMapConnections _connections;
        IJwtUtils _jwtUtils;
        public ConnectionHandler(IJwtUtils utils, IMapConnections mapConnections)
        {
            _jwtUtils = utils;
            _connections = mapConnections;
        }

        public override Task OnConnectedAsync()
        {
            var httpcontext = Context.gethttpcontext();
            if (httpcontext != null)
            {
                var query = httpcontext.Request.Query.GetQueryParameterValue<string>("access_token");
                var userId = _jwtUtils.ValidateJwtToken(query); //Get the userId from token
                if (userId != null)
                {
                    //Add the user to the mapconnections
                    _connections.AddConnection(Context.ConnectionId, (int)userId);
                }
            }
            return base.OnConnectedAsync();
        }

        //TODO: HAndle disconnection https://learn.microsoft.com/en-us/aspnet/core/signalr/hubs?view=aspnetcore-8.0
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = _connections.GetUserById(Context.ConnectionId); //(user?)HttpContext.Items["User"];
            //TODO: át kell adni intefészen keresztül az usernek
            User? user = null; // await _context.user.FirstOrDefaultAsync(p => p.userID == userId);
            if (user != null)
            {
                user.LastOnline = DateTime.Now;
                //await _context.SaveChangesAsync();

                _connections.Remove(Context.ConnectionId);
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}

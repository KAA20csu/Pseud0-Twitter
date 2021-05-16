using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.WebSockets;

namespace Twi
{
    /// <summary>
    /// Сводное описание для Chatter
    /// </summary>
    public class Chatter : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.IsWebSocketRequest)
                context.AcceptWebSocketRequest(WebSocketRequest);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        private static readonly List<WebSocket> Clients = new List<WebSocket>();
        private static readonly ReaderWriterLockSlim Locker = new ReaderWriterLockSlim();
        private async Task WebSocketRequest(AspNetWebSocketContext context)
        {
            var socket = context.WebSocket;

            Locker.EnterWriteLock();
            try
            {
                Clients.Add(socket);
            }
            finally
            {
                Locker.ExitWriteLock();
            }
            while (true)
            {
                var buffer = new ArraySegment<byte>(new byte[1024]);
                var result = await socket.ReceiveAsync(buffer, CancellationToken.None);
                for (int i = 0; i < Clients.Count; i++)
                {
                    WebSocket client = Clients[i];
                    try
                    {
                        if (client.State == WebSocketState.Open)
                        {
                            await client.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                        }
                    }
                    catch (ObjectDisposedException)
                    {
                        Locker.EnterWriteLock();
                        try
                        {
                            Clients.Remove(client);
                            i--;
                        }
                        finally
                        {
                            Locker.ExitWriteLock();
                        }
                    }
                }

            }
        }
    }
}
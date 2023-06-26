using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using OrderService;

namespace GrpcClient.Services
{
    public class OrderServiceClient : ControllerBase
    {
        [HttpGet("/api/orders/grpc")]
        public long GetOrdersByGrpc()
        {
            long start = System.Environment.TickCount;
            var channel = GrpcChannel.ForAddress("https://localhost:5295");
            var client = new OrderCRUD.OrderCRUDClient(channel);
            for (int i = 0; i < 20; i++)
            {
                Orders orders = client.SelectAll(new Empty());
            }
            long end = System.Environment.TickCount;
            return (end - start)/20;
        }


        [HttpGet("/api/orders/rest")]
        public long GetOrdersByRest()
        {
            long start = System.Environment.TickCount;


            long end = System.Environment.TickCount;
            return (end - start) / 20;
        }
    }
}

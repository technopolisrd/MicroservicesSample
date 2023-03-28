using Azure.Messaging.ServiceBus;
using Mango.Services.OrderAPI.Messages;
using Mango.Services.OrderAPI.Models;
using Mango.Services.OrderAPI.Repository;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Mango.Services.OrderAPI.Messaging;

public class AzureServiceBusConsumer : IAzureServiceBusConsumer
{
    private readonly string serviceBusConnectionString;
    private readonly string subscrtiptionCheckOut;
    private readonly string checkoutMessageTopic;
    private readonly OrderRepository _orderRepository;

    private ServiceBusProcessor checkOutProcesor;

    private readonly IConfiguration _configuration;

    public AzureServiceBusConsumer(OrderRepository orderRepository, IConfiguration configuration)
    {
        _orderRepository = orderRepository;
        _configuration = configuration;

        serviceBusConnectionString = _configuration.GetValue<string>("ServiceBusConnectionString");
        checkoutMessageTopic = _configuration.GetValue<string>("CheckoutMessageTopic");
        subscrtiptionCheckOut = _configuration.GetValue<string>("SubscriptionCheckOut");

        var client = new ServiceBusClient(serviceBusConnectionString);

        checkOutProcesor = client.CreateProcessor(checkoutMessageTopic, subscrtiptionCheckOut);
    }

    public async Task Start()
    {
        checkOutProcesor.ProcessMessageAsync += OnCheckOutMessageReceive;
        checkOutProcesor.ProcessErrorAsync += ErrorHandler;
        await checkOutProcesor.StartProcessingAsync();
    }

    public async Task Stop()
    {
        await checkOutProcesor.StopProcessingAsync();
        await checkOutProcesor.DisposeAsync();
    }

    public Task ErrorHandler(ProcessErrorEventArgs args)
    {
        Console.WriteLine(args.Exception.ToString());
        return Task.CompletedTask;
    }

    private async Task OnCheckOutMessageReceive(ProcessMessageEventArgs args)
    {
        var message = args.Message;
        var body = Encoding.UTF8.GetString(message.Body);

        CheckoutHeaderDTO checkoutHeaderDTO = JsonConvert.DeserializeObject<CheckoutHeaderDTO>(body);

        OrderHeader orderHeader = new()
        {
            UserId = checkoutHeaderDTO.UserId,
            FirstName = checkoutHeaderDTO.FirstName,
            LastName = checkoutHeaderDTO.LastName,
            OrderDetails = new List<OrderDetails>(),
            CardNumber = checkoutHeaderDTO.CardNumber,
            CouponCode = checkoutHeaderDTO.CouponCode,
            CVV = checkoutHeaderDTO.CVV,
            DiscountTotal = checkoutHeaderDTO.DiscountTotal,
            Email = checkoutHeaderDTO.Email,
            ExpiryMonthYear = checkoutHeaderDTO.ExpiryMonthYear,
            OrderTime = DateTime.Now,
            OrderTotal = checkoutHeaderDTO.OrderTotal,
            PaymentStatus = false,
            Phone = checkoutHeaderDTO.Phone,
            PickupDateTime = checkoutHeaderDTO.PickupDateTime,
        };
        foreach (var detailList in checkoutHeaderDTO.CartDetails)
        {
            OrderDetails orderDetails = new()
            {
                ProductId = detailList.ProductId,
                ProductName = detailList.Product.Name,
                Price = detailList.Product.Price,
                Count = detailList.Count
            };
            orderHeader.CartTotalItems += detailList.Count;
            orderHeader.OrderDetails.Add(orderDetails);
        }

        await _orderRepository.AddOrder(orderHeader);
    }
}

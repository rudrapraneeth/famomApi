using AutoMapper;
using HomeMade.Api.Filters;
using HomeMade.Api.Models;
using HomeMade.Core.Entities;
using HomeMade.Core.Interfaces;
using HomeMade.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMade.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ServiceFilter(typeof(UsageAttribute))]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IExpoRepository _expoRepository;
        public OrderController(IOrderRepository orderRepository, IExpoRepository expoRepository)
        {
            _orderRepository = orderRepository;
            _expoRepository = expoRepository;
        }

        [HttpGet]
        [Route("GetOrders")]
        public async Task<IActionResult> GetOrders()
        {
            var user = (UserModel)HttpContext.Items["User"];
            var orders = await _orderRepository.GetOrders(user.ApplicationUserId);
            var customerOrders = new List<OrderHistoryModel>();
            orders.ForEach(x =>
            {
                var address = x.Post.Chef.ApplicationUser.UserApartment.FirstOrDefault();
                customerOrders.Add(new OrderHistoryModel
                {
                    SubOrderId = x.SubOrderId,
                    MenuName = x.Post.Menu.Title,
                    Quantity = x.Quantity,
                    QuantityType = x.Post.QuantityType.Name,
                    TotalPrice = x.TotalPrice,
                    Chef = x.Post.Chef.ApplicationUser.UserName,
                    DeliveryTime = x.DeliveryDateTime,
                    OrderDateTime = x.Order.OrderDateTime,
                    DeliveryType = x.DeliveryTypeId,
                    Instructions = x.Instructions,
                    DiscountPrice = x.TotalDiscountPrice,
                    DeliveryCharge = x.Post.DeliveryCharge,
                    Status = x.StatusId,
                    IsRated = x.Rating.Any(),
                    Apartment = $"{address.Block} / {address.ApartmentNumber}"
                });
            });

            return Ok(customerOrders.OrderByDescending(x=>x.OrderDateTime));
        }

        [HttpGet]
        [Route("GetSales")]
        public async Task<IActionResult> GetSales()
        {
            var user = (UserModel)HttpContext.Items["User"];
            var sales = await _orderRepository.GetSales(user.ApplicationUserId);
            var chefSales = new List<OrderHistoryModel>();
            sales.ForEach(x =>
            {
                var address = x.Order.Customer.ApplicationUser.UserApartment.FirstOrDefault();
                chefSales.Add(new OrderHistoryModel
                {
                    SubOrderId = x.SubOrderId,
                    MenuName = x.Post.Menu.Title,
                    Quantity = x.Quantity,
                    TotalPrice = x.TotalPrice,
                    Customer = x.Order.Customer.ApplicationUser.UserName,
                    DeliveryTime = x.DeliveryDateTime,
                    OrderDateTime = x.Order.OrderDateTime,
                    DeliveryType = x.DeliveryTypeId,
                    Instructions = x.Instructions,
                    QuantityType = x.Post.QuantityType.Name,
                    Status = x.StatusId,
                    DiscountPrice = x.TotalDiscountPrice,
                    DeliveryCharge = x.Post.DeliveryCharge,
                    Apartment = $"{address.Block} / {address.ApartmentNumber}"
                });
            });

            return Ok(chefSales.OrderByDescending(x => x.OrderDateTime));
        }

        [HttpPost]
        [Route("PlaceOrder")]
        public async Task<IActionResult> PlaceOrder(OrderModel orderModel)
        {
            var user = (UserModel)HttpContext.Items["User"];
            var customerId = await _orderRepository.GetCustomerId(user.ApplicationUserId);
            var order = new Orders()
            {
                OrderDateTime = DateTime.Now,
                StatusId = (int)Core.Enums.OrderStatus.PLACED,
                TotalCost = orderModel.Total,
            };

            if (customerId > 0)
            {
                order.CustomerId = customerId;
            }

            else
            {
                order.Customer = new Customer() { ApplicationUserId = user.ApplicationUserId };
            }

            var subOrder = new SubOrder()
            {
                DeliveryTypeId = orderModel.DeliveryType,
                DeliveryDateTime = orderModel.DeliveryTime,
                Instructions = string.IsNullOrWhiteSpace(orderModel.Instructions) ? null : orderModel.Instructions,
                PostId = orderModel.PostId,
                Quantity = orderModel.Quantity,
                StatusId = (int)Core.Enums.OrderStatus.PLACED,
                TotalPrice = orderModel.Total,
                TotalDiscountPrice = orderModel.TotalDiscountPrice
        };

            if (orderModel.PromotionId > 0)
            {
                //TODO: Get the promotion Id from 
                var subOrderPrmotion = new SubOrderPromotion() { PromotionId = orderModel.PromotionId };
                subOrder.SubOrderPromotion.Add(subOrderPrmotion);
            }

            order.SubOrder.Add(subOrder);

            order = await _orderRepository.CreateOrder(order, orderModel.PostId, orderModel.Quantity);
            orderModel.SubOrderId = order.SubOrder.FirstOrDefault().SubOrderId;
            await _expoRepository.SendPlaceOrderNotification(orderModel, user.ApplicationUserId);
            return Ok();
        }

        [HttpPost]
        [Route("UpdateStatus")]
        public async Task<ActionResult> UpdateStatus(OrderModel orderModel)
        {
            var user = (UserModel)HttpContext.Items["User"];

            //OrderId is suborderId here
            await _orderRepository.UpdateStatus(orderModel.SubOrderId, orderModel.StatusId);
            switch (orderModel.StatusId)
            {
                case (int)Core.Enums.OrderStatus.CANCELED:
                    await _expoRepository.SendCancelOrderNotification(orderModel.SubOrderId, orderModel.StatusChangeUserTypeId);
                    break;
                case (int)Core.Enums.OrderStatus.READY_FOR_PICKUP:
                case (int)Core.Enums.OrderStatus.OUT_FOR_DELIVERY:
                    await _expoRepository.SendReadyNotification(orderModel);
                    break;
                case (int)Core.Enums.OrderStatus.PENDING_PAYMENT:
                    await _expoRepository.CancelScheduledNotification(orderModel.SubOrderId);
                    break;
                case (int)Core.Enums.OrderStatus.COMPLETE:
                    await _expoRepository.CancelScheduledNotification(orderModel.SubOrderId);
                    await _expoRepository.ScheduleRatingNotification(orderModel,  user.UserName);
                    break;

            }

            return Ok();
        }
    }
}

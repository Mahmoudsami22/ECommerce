using AutoMapper;
using ECommerce.Application.Common;
using ECommerce.Application.Contracts;
using ECommerce.Application.DTO_s.Orders;
using ECommerce.Application.Specifications;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.Orders;
using ECommerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IBasketRepository basketRepository;

        public OrderServices(IMapper mapper,IUnitOfWork unitOfWork,IBasketRepository basketRepository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.basketRepository = basketRepository;
        }
        public async Task<Result<OrderToReturnDto>> CreateOrderAsync(OrderDto orderDto, string email, CancellationToken ct = default)
        {
            //1.Validate Basket Found & Items.
            var basket = await basketRepository.GetBasketAsync(orderDto.BasketId, ct);

            if (basket is null)
                return Result<OrderToReturnDto>.Fail(Error.NotFound("Basket. NotFound", "Basket Is Not Found"));

            if (basket.Items.Count == 0)
                return Result<OrderToReturnDto>.Fail(Error.Validation("Basket. Empty", "Basket Is Empty"));
            //------------------------------------------------------------
            //2.Get Items From Basket Validate As Product
            //then Get the Data from Product => Make it As Order Item
            var productRepo = unitOfWork.GetRepository<Product, int>();
            //Basket Items Ids
            var ProductIds = basket.Items.Select(i => i.Id).ToHashSet();

            var Products = await productRepo.GetAllWithSpecificationsAsync(new ProductWithIdsSpecifications(ProductIds), ct);

            var orderItems = new List<OrderItem>(basket.Items.Count);

            foreach (var item in basket.Items)
            {
                var product = Products.FirstOrDefault(p => p.Id == item.Id);

                if (product is null)
                    return Result<OrderToReturnDto>.Fail(Error.NotFound("Product.NotFound", "Product Is Not Found"));

                orderItems.Add(new OrderItem()
                {
                    Price = product.Price,
                    Quantity = item.Quantity,
                    Product = new ProductItemOrdered()
                    {
                        ProductId = product.Id,
                        PictureUrl = product.PictureUrl,
                        ProductName = product.Name,
                    }
                });

            }
            //-----------------------------------------------------------------------------
            //3.Store Order Address
            var orderAddress = mapper.Map<OrderAddress>(orderDto.ShippingAddress);
            //-----------------------------------------------------------------------------
            //4.Store Delivery Method
            var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDto.DeliveryMethodId, ct);
            if (deliveryMethod is null)
                return Result<OrderToReturnDto>.Fail(Error.NotFound("Delivery.NotFound", "Delivery Method Is Not Found"));
            //-----------------------------------------------------------------------------
            //5.Calcualtions
            var subTotal = orderItems.Sum(i => i.Price * i.Quantity);
            //-----------------------------------------------------------------------------
            //6.Generate Order
            var order = new Order()
            {
                BuyerEmail = email,
                Items = orderItems,
                ShippingAddress = orderAddress,
                DeliveryMethodId = deliveryMethod.Id,
                SubTotal = subTotal,
                DeliveryMethod = deliveryMethod,
            };
            unitOfWork.GetRepository<Order, Guid>().Add(order);
            var result = await unitOfWork.SaveChangesAsync();
            //-----------------------------------------------------------------------------
            //7.Return Order
            if (result <= 0)
                return Result<OrderToReturnDto>.Fail(Error.Failure("Order.Failure", "Order Can not Created"));

            await basketRepository.DeleteBasketAsync(orderDto.BasketId, ct);

            return Result<OrderToReturnDto>.Ok(mapper.Map<OrderToReturnDto>(order));


        }

        public async Task<Result<IReadOnlyList<DeliveryMethodDto>>> GetAllDeliveryMethodsAsync(CancellationToken ct = default)
        {
            var deliveryMethods = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync(ct);

            return Result<IReadOnlyList<DeliveryMethodDto>>.Ok(mapper.Map<IReadOnlyList<DeliveryMethodDto>>(deliveryMethods));
        }

        public async Task<Result<IReadOnlyList<OrderToReturnDto>>> GetAllordersByEmailAsync(string email, CancellationToken ct = default)
        {
            var orders = await unitOfWork.GetRepository<Order, Guid>().GetAllWithSpecificationsAsync(new OrderSpecifications(email));
            return Result<IReadOnlyList<OrderToReturnDto>>.Ok(mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders));
        }

        public async Task<Result<OrderToReturnDto>> GetOrderByIdAndEmailAsync(Guid Id, string email, CancellationToken ct = default)
        {
            var order = await unitOfWork.GetRepository<Order, Guid>().GetByIdWithSpecificationsAsync(new OrderSpecifications(Id, email));

            if (order is null)
                return Result<OrderToReturnDto>.Fail(Error.NotFound("Order.NotFound", "Order Is Not Found"));

            return Result<OrderToReturnDto>.Ok(mapper.Map<OrderToReturnDto>(order));
        }
    }
}

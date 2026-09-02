using AutoMapper;
using ECommerce.Application.DTO_s.Orders;
using ECommerce.Domain.Entities.Orders;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using static ECommerce.Application.Profiles.PictureUrlResolver;

namespace ECommerce.Application.Profiles
{
    public class OrderItemPictureUrlResolver(IOptions<UrlSettings> options) : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly UrlSettings settings = options.Value;

        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.Product.PictureUrl))
                return string.Empty;

            return $"{settings.BaseUrl}/Files/{source.Product.PictureUrl}";
        }
    }
}

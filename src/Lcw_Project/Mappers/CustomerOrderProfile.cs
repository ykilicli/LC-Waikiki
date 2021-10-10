using AutoMapper;
using Lcw_Project.Entities;
using Lcw_Project.ViewModel.CustomerOrderModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lcw_Project.Mappers
{
    public class CustomerOrderProfile : Profile
    {
        public CustomerOrderProfile()
        {
            CreateMap<LcwCustomerOrder, CustomerOrderListResponseModel>()
           .ForMember(x => x.orderId, y => y.MapFrom(x => x.Id))
           .ForMember(x => x.orderDate, y => y.MapFrom(x => x.Date))
           .ForMember(x => x.customerName, y => y.MapFrom(x => x.Customer.CustomerName))
           .ForMember(x => x.customerSurname, y => y.MapFrom(x => x.Customer.CustomerSurname))
           .ForMember(x => x.customerPhone, y => y.MapFrom(x => x.Customer.Phone))
            ;

            CreateMap<LcwCustomerOrder, CustomerOrderResponseModel>()
           .ForMember(x => x.id, y => y.MapFrom(x => x.Id))
           .ForMember(x => x.customer, y => y.MapFrom(x => x.Customer))
           .ForMember(x => x.address, y => y.MapFrom(x => x.Address))
            .ForMember(x => x.orderItems, y => y.MapFrom(x => x.LcwCustomerOrderItems))
           ;

            CreateMap<LcwCustomerOrderItem, CustomerOrderItemResponseModel>()
           .ForMember(x => x.id, y => y.MapFrom(x => x.Id))
           .ForMember(x => x.product, y => y.MapFrom(x => x.Product))
           .ForMember(x => x.price, y => y.MapFrom(x => x.Price))
           .ForMember(x => x.quantity, y => y.MapFrom(x => x.Quantity))
           ;

            CreateMap<CustomerOrderCreateRequestModel, LcwCustomerOrder>()
            .ForMember(x => x.CustomerId, y => y.MapFrom(x => x.customerId))
            .ForMember(x => x.Address, y => y.MapFrom(x => x.address))
            .ForMember(x => x.Date, y => y.MapFrom(x => x.date))
            .ForMember(x => x.LcwCustomerOrderItems, y => y.MapFrom(x => x.orderItems))
            ;

            CreateMap<LcwCustomerOrderItem, CustomerOrderItemCreateRequestModel>()
          .ForMember(x => x.productId, y => y.MapFrom(x => x.ProductId))
          .ForMember(x => x.price, y => y.MapFrom(x => x.Price))
          .ForMember(x => x.quantity, y => y.MapFrom(x => x.Quantity))
          ;
        }
    }
}

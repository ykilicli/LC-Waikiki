using AutoMapper;
using Lcw_Project.Entities;
using Lcw_Project.ViewModel.CustomerModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lcw_Project.Mappers
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<LcwCustomer, CustomerListResponseModel>()
                .ForMember(x => x.id, y => y.MapFrom(x => x.Id))
                .ForMember(x => x.customerName, y => y.MapFrom(x => x.CustomerName))
                .ForMember(x => x.customerSurname, y => y.MapFrom(x => x.CustomerSurname))
                .ForMember(x => x.email, y => y.MapFrom(x => x.Email))
                .ForMember(x => x.phone, y => y.MapFrom(x => x.Phone))
                .ForMember(x => x.orderCount, y => y.MapFrom(x => x.LcwCustomerOrders.Count()))
                ;

            CreateMap<LcwCustomer, CustomerResponseModel>()
              .ForMember(x => x.id, y => y.MapFrom(x => x.Id))
              .ForMember(x => x.customerName, y => y.MapFrom(x => x.CustomerName))
              .ForMember(x => x.customerSurname, y => y.MapFrom(x => x.CustomerSurname))
              .ForMember(x => x.email, y => y.MapFrom(x => x.Email))
              .ForMember(x => x.phone, y => y.MapFrom(x => x.Phone))
              ;


            CreateMap<CustomerCreateRequestModel, LcwCustomer>()
           .ForMember(x => x.CustomerName, y => y.MapFrom(x => x.customerName))
           .ForMember(x => x.CustomerSurname, y => y.MapFrom(x => x.customerSurname))
           .ForMember(x => x.Email, y => y.MapFrom(x => x.email))
           .ForMember(x => x.Phone, y => y.MapFrom(x => x.phone))
           ;

            CreateMap<CustomerUpdateRequestModel, LcwCustomer>()
           .ForMember(x => x.CustomerName, y => y.MapFrom(x => x.customerName))
           .ForMember(x => x.CustomerSurname, y => y.MapFrom(x => x.customerSurname))
           .ForMember(x => x.Phone, y => y.MapFrom(x => x.phone))
           ;
        }
    }
}

using AutoMapper;
using Lcw_Project.Entities; 
using Lcw_Project.ViewModel.ProductModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lcw_Project.Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<LcwProduct, ProductListResponseModel>()
                .ForMember(x => x.id, y => y.MapFrom(x => x.Id))
                .ForMember(x => x.productName, y => y.MapFrom(x => x.ProductName))
                .ForMember(x => x.barcode, y => y.MapFrom(x => x.Barcode))
                .ForMember(x => x.price, y => y.MapFrom(x => x.Price)) 
                ;

            CreateMap<LcwProduct, ProductResponseModel>()
                .ForMember(x => x.id, y => y.MapFrom(x => x.Id))
                .ForMember(x => x.productName, y => y.MapFrom(x => x.ProductName))
                .ForMember(x => x.barcode, y => y.MapFrom(x => x.Barcode))
                .ForMember(x => x.price, y => y.MapFrom(x => x.Price))
              ;


            CreateMap<ProductCreateRequestModel, LcwProduct>()
           .ForMember(x => x.ProductName, y => y.MapFrom(x => x.productName))
           .ForMember(x => x.Barcode, y => y.MapFrom(x => x.barcode))
           .ForMember(x => x.Price, y => y.MapFrom(x => x.price)) 
           ;

            CreateMap<ProductUpdateRequestModel, LcwProduct>()
           .ForMember(x => x.ProductName, y => y.MapFrom(x => x.productName)) 
           .ForMember(x => x.Price, y => y.MapFrom(x => x.price))
           ;
        }
    }
}

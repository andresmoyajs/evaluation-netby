using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using application.Features.Categories.Vms;
using application.Features.Images.Queries.Vms;
using application.Features.Transactions.Vms;
using application.Features.Products.Commands.CreateProduct;
using application.Features.Products.Commands.UpdateProduct;
using application.Features.Products.Queries.Vms;
using domain;

namespace application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductVm>()
                .ForMember(p => p.CategoryName, x => x.MapFrom(a => a.Category!.Name));

            CreateMap<Image, ImageVm>();
            CreateMap<Category, CategoryVm>();
            CreateMap<CreateProductCommand, Product>();
            CreateMap<CreateProductImageCommand, Image>();
            CreateMap<UpdateProductCommand, Product>();


            CreateMap<Transaction, TransactionVm>();
            CreateMap<TransactionItem, TransactionItemVm>();





        }
    }
}

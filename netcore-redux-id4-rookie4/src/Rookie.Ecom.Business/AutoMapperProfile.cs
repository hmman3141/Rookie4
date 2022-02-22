using Rookie.Ecom.Contracts.Dtos;
using Rookie.Ecom.DataAccessor.Entities;

namespace Rookie.Ecom.Business
{
    public class AutoMapperProfile : AutoMapper.Profile
    {
        public AutoMapperProfile()
        {
            FromDataAccessorLayer();
            FromPresentationLayer();
        }

        private void FromPresentationLayer()
        {
            CreateMap<CategoryDto, Category>();

            CreateMap<ProductDto, Product>();

            CreateMap<OrderDto, Order>();

            CreateMap<OrderDetailDto,OrderDetail>();

            CreateMap<RatingDto,Rating>();

            CreateMap<UserDto,User>();

            CreateMap<UserAddressDto,UserAddress>();

            CreateMap<ProductPictureDto, ProductPicture>();
        }

        private void FromDataAccessorLayer()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<Product, ProductDto>();
            CreateMap<ProductPicture, ProductPictureDto>();
            CreateMap<Order,OrderDto>();
            CreateMap<OrderDetail,OrderDetailDto>();
            CreateMap<Rating,RatingDto>();
            CreateMap<User,UserDto>();
            CreateMap<UserAddress,UserAddressDto>();
        }
    }
}

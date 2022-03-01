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
            CreateMap<CategoryDto, Category>()
                .ForMember(d => d.Products, t => t.Ignore());

            CreateMap<ProductDto, Product>()
                .ForMember(d => d.Ratings, t => t.Ignore())
                .ForMember(d => d.ProductPictures, t => t.Ignore());

            CreateMap<OrderDto, Order>()
                .ForMember(d => d.OrderDetails, t => t.Ignore());

            CreateMap<OrderDetailDto,OrderDetail>();

            CreateMap<RatingDto,Rating>();

            CreateMap<UserDto,User>()
                .ForMember(d => d.Ratings, t => t.Ignore())
                .ForMember(d => d.UserAddresses, t => t.Ignore())
                .ForMember(d => d.UserRoles, t => t.Ignore())
                .ForMember(d => d.Orders, t => t.Ignore());

            CreateMap<RoleDto, Role>()
                .ForMember(d => d.UserRoles, t => t.Ignore());

            CreateMap<UserRoleDto, UserRole>();

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
            CreateMap<Role, RoleDto>();
            CreateMap<UserRole, UserRoleDto>();
        }
    }
}

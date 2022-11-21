using AutoMapper;
using Ecommerce.Application.Dto;
using Ecommerce.Application.Handlers.Configuration.Commands;
using Ecommerce.Domain.Models;

namespace Ecommerce.Application.MappingProfile
{
    public class ConfigurationMappingProfile : Profile
    {
        public ConfigurationMappingProfile()
        {
            //GeneralConfiguration
            CreateMap<GeneralConfigurationDto, GeneralConfiguration>().ReverseMap();
            CreateMap<UpdateGeneralConfigurationCommand, GeneralConfiguration>().ReverseMap();

            //SocialProfile
            CreateMap<SocialProfileDto, SocialProfile>().ReverseMap();
            CreateMap<UpdateSocialConfigurationCommand, SocialProfileDto>().ReverseMap();

            //HeaderSlider
            CreateMap<HeaderSliderDto, HeaderSlider>().ReverseMap();

            //FeatureProduct
            CreateMap<FeatureProductConfigurationDto, FeatureProductConfiguration>().ReverseMap();

            //StockConfiguration
            CreateMap<StockConfigurationDto, StockConfiguration>().ReverseMap();
            CreateMap<UpdateStockConfigurationCommand, StockConfigurationDto>().ReverseMap();

            //TopCategories
            CreateMap<TopCategoriesConfiguration, TopCategoriesConfigurationDto>().ReverseMap();

            //SeoConfiguration
            CreateMap<BasicSeoConfiguration, BasicSeoConfigurationDto>().ReverseMap();

            //SMTPConfiguration
            CreateMap<SmtpConfiguration, SmtpConfigurationDto>().ReverseMap();
            CreateMap<UpdateSmtpConfigurationCommand, SmtpConfigurationDto>().ReverseMap();

            //SecurityConfiguration
            CreateMap<SecurityConfiguration, SecurityConfigurationDto>().ReverseMap();
            CreateMap<UpdateSecurityConfigurationCommand, SecurityConfigurationDto>().ReverseMap();

            //AdvancedConfiguration
            CreateMap<AdvancedConfiguration, AdvancedConfigurationDto>().ReverseMap();
            CreateMap<UpdateAdvancedConfigurationCommand, AdvancedConfigurationDto>().ReverseMap();


        }
    }
}

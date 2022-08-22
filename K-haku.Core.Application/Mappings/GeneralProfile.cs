using AutoMapper;
using K_haku.Core.Application.DTOS.Account;
using K_haku.Core.Application.ViewModels;
using K_haku.Core.Application.ViewModels.Cuevana;
using K_haku.Core.Application.ViewModels.ScrapPages;
using K_haku.Core.Application.ViewModels.User;
using K_haku.Core.Domain.Entities;
using K_haku.Core.Domain.Entities.Cuevana;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_haku.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<CuevanaMovies, MovieViewModel>()
                .ReverseMap()
                    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                        .ForMember(dest => dest.LastModifiedby, opt => opt.Ignore())
                            .ForMember(dest => dest.LastModified, opt => opt.Ignore())
                                .ForMember(dest => dest.Created, opt => opt.Ignore());

            CreateMap<CuevanaMovies, CuevanaInfoViewModel>()
                .ForMember(dest => dest.Seen, opt => opt.Ignore())
                .ReverseMap()
                    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                        .ForMember(dest => dest.LastModifiedby, opt => opt.Ignore())
                            .ForMember(dest => dest.LastModified, opt => opt.Ignore())
                                .ForMember(dest => dest.Created, opt => opt.Ignore());

            CreateMap<ScrapPages, ScrapPagesViewModel>()
                .ReverseMap()
                    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                        .ForMember(dest => dest.LastModifiedby, opt => opt.Ignore())
                            .ForMember(dest => dest.LastModified, opt => opt.Ignore())
                                .ForMember(dest => dest.Created, opt => opt.Ignore());

            CreateMap<ScrapPages, ScrapPagesInfoViewModel>()
                .ReverseMap()
                    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                        .ForMember(dest => dest.LastModifiedby, opt => opt.Ignore())
                            .ForMember(dest => dest.LastModified, opt => opt.Ignore())
                                .ForMember(dest => dest.Created, opt => opt.Ignore());

            CreateMap<AuthenticationResponse, UserSaveViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                    .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<AuthenticationRequest, LoginViewModel>()
               .ForMember(x => x.HasError, opt => opt.Ignore())
                    .ForMember(x => x.Error, opt => opt.Ignore())
               .ReverseMap();
            CreateMap<RegisterRequest, UserSaveViewModel>()
               .ForMember(x => x.HasError, opt => opt.Ignore())
                    .ForMember(x => x.Error, opt => opt.Ignore())
               .ReverseMap();

            CreateMap<ForgotPasswordRequest, ForgotPasswordViewModel>()
               .ForMember(x => x.HasError, opt => opt.Ignore())
                    .ForMember(x => x.Error, opt => opt.Ignore())
               .ReverseMap();
            CreateMap<ResetPasswordRequest, ResetPasswordViewModel>()
              .ForMember(x => x.HasError, opt => opt.Ignore())
                    .ForMember(x => x.Error, opt => opt.Ignore())
              .ReverseMap();
        }
    }
}

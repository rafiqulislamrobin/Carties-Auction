﻿using AuctionService.DTOs;
using AuctionService.Entity;
using AutoMapper;

namespace AuctionService.RequestHelpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Auction, AuctionDto>().IncludeMembers(x => x.Item);
            CreateMap<Item, AuctionDto>();
            CreateMap<CreateAuctionDto, Auction>();
            CreateMap<CreateAuctionDto, Auction>()
                .ForMember(d =>d.Item, o =>o.MapFrom(s =>s) );
        }
    }
}
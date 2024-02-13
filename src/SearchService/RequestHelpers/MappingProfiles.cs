using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using SearchService.Models;

namespace SearchService.RequestHelpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Map the AuctionCreated object from Contracts to the Item object in SearchService
            CreateMap<AuctionCreated, Item>();
        }
    }
}
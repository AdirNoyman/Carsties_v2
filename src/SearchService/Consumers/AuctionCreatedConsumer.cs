using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers
{
    public class AuctionCreatedConsumer : IConsumer<AuctionCreated>
    {
        private readonly IMapper _mapper;

        public AuctionCreatedConsumer(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<AuctionCreated> context)
        {
            Console.WriteLine("--> Consuming auction created 🤓: " + context.Message.Id);

            // Get the the item form the consumer and convert it into a Searchservice item
            var item = _mapper.Map<Item>(context.Message);

            // Save the search item to the search service Mongo database
            await item.SaveAsync();
        }
    }
}
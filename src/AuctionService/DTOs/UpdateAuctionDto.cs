using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionService.DTOs
{
    public class UpdateAuctionDto
    {


        public string Make { get; set; }

        public string Model { get; set; }

        // The year the car was manufactured
        public int? Year { get; set; }

        public string Color { get; set; }

        public int? Mileage { get; set; }


    }
}
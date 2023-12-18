using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionService.DTOs
{
    public class UpdateAuctionDto
    {

        [Required]
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        // The year the car was manufactured
        public int Year { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public int Mileage { get; set; }


    }
}
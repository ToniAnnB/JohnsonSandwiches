﻿using JSandwiches.Models.DTO.SpecialFeaturesDTO;
using JSandwiches.Models.DTO.UsersDTO;
using System.ComponentModel.DataAnnotations;

namespace JSandwiches.Models.DTO.OrderDTO
{
    public class RecieptDTO : CreateOrderDTO
    {
        [Required]
        public int Id { get; set; }

        public OrderDTO? Order { get; set; }


        public CustomerDTO? Customer { get; set; }


        public DealSpecificsDTO? DealSpecifics { get; set; }
    }

    public class CreateRecieptDTO
    {
        [Required]
        public string RecieptNumber { get; set; }


        [Required]
        public int OrderID { get; set; }

        [Required]
        public int CustomerID { get; set; }

        public int? DealSpecificsID { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace bookstore.DataTransferObject.DTOs
{
    public class CreateOrderDTO
    {
        [Required]
        public int BookId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Value for {0} must be bigger than 0.")]
        public int Quantity { get; set; }
    }
}

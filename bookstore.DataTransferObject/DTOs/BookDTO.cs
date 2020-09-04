using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace bookstore.DataTransferObject.DTOs
{
    public class BookDTO
    {
        [Required]
        public string BookName { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Publisher { get; set; }

        [Required]
        public DateTime PublishedDate { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Value for {0} must be bigger than 0.")]
        public int Quantity { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Value for {0} must be bigger than 0.")]
        public decimal UnitPrice { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime CreatedAt { get; set; }
    }

    public class UpdateBookDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string BookName { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Publisher { get; set; }

        [Required]
        public DateTime PublishedDate { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Value for {0} must be bigger than 0.")]
        public int Quantity { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Value for {0} must be bigger than 0.")]
        public decimal UnitPrice { get; set; }
    }
}

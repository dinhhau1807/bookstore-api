using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using MicroOrm.Dapper.Repositories.Attributes.LogicalDelete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;
using System.Text;
using System.Text.Json.Serialization;

namespace bookstore.BussinessEnitites.Models
{
    [Table("Books")]
    public class Book
    {
        [Key, Identity]
        [Column(nameof(Id))]
        public int Id { get; set; }

        [Column(nameof(BookName))]
        public string BookName { get; set; }

        [Column(nameof(Author))]
        public string Author { get; set; }

        [Column(nameof(Publisher))]
        public string Publisher { get; set; }

        [Column(nameof(PublishedDate))]
        public DateTime PublishedDate { get; set; }

        [Column(nameof(Quantity))]
        public int Quantity { get; set; }

        [Column(nameof(UnitPrice))]
        public decimal UnitPrice { get; set; }

        [JsonIgnore]
        [Status, Deleted]
        [Column(nameof(IsDeleted))]
        public bool IsDeleted { get; set; }

        [UpdatedAt]
        [Column(nameof(UpdatedAt))]
        public DateTime? UpdatedAt { get; set; }

        [Column(nameof(CreatedAt))]
        public DateTime CreatedAt { get; set; }


        // Join Table
        [LeftJoin("Orders", nameof(Id), nameof(Order.BookId))]
        public List<Order> Orders { get; set; }
    }
}

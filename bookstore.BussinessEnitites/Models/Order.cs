using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text;

namespace bookstore.BussinessEnitites.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key, Identity]
        [Column(nameof(Id))]
        public int Id { get; set; }

        [Key]
        [Column(nameof(UserId))]
        public int UserId { get; set; }

        [Key]
        [Column(nameof(BookId))]
        public int BookId { get; set; }

        [Column(nameof(Quantity))]
        public int Quantity { get; set; }

        [Column(nameof(UnitPrice))]
        public decimal UnitPrice { get; set; }

        [UpdatedAt]
        [Column(nameof(UpdatedAt))]
        public DateTime? UpdatedAt { get; set; }

        [Column(nameof(CreatedAt))]
        public DateTime CreatedAt { get; set; }


        // Join Table
        [LeftJoin("Accounts", nameof(UserId), "Id")]
        public Account User { get; set; }

        [LeftJoin("Books", nameof(BookId), "Id")]
        public Book Book { get; set; }
    }
}

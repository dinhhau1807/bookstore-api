﻿using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using MicroOrm.Dapper.Repositories.Attributes.LogicalDelete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace bookstore.BussinessEnitites.Models
{
    [Table("Users")]
    public class Account
    {
        [Key, Identity]
        [Column(nameof(Id))]
        public int Id { get; set; }

        [Column(nameof(Username))]
        public string Username { get; set; }

        [Column(nameof(HashPassword))]
        public string HashPassword { get; set; }

        [Column(nameof(Email))]
        public string Email { get; set; }

        [Column(nameof(Name))]
        public string Name { get; set; }

        [Column(nameof(Role))]
        public string Role { get; set; }

        [Status, Deleted]
        [Column(nameof(IsDeleted))]
        public bool IsDeleted { get; set; }

        [UpdatedAt]
        [Column(nameof(UpdatedAt))]
        public DateTime? UpdatedAt { get; set; }

        [Column(nameof(CreatedAt))]
        public DateTime CreatedAt { get; set; }


        // Join Table
        [LeftJoin("Orders", nameof(Id), nameof(Order.UserId))]
        public List<Order> Orders { get; set; }
    }
}

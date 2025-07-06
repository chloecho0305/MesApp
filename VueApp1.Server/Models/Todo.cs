﻿using System.ComponentModel.DataAnnotations;
namespace VueApp1.Server.Models
{
    public class Todo
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
}

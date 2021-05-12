using System;
using static System.Guid;

namespace RestApp.Models
{ 
    public class Region
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Alias { get; set; }
    }
}
using Microsoft.AspNetCore.SignalR;

using System.ComponentModel.DataAnnotations.Schema;

namespace EcoMeal.Entities
{
    public class Business
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Address { get; set; }
        public required string ImageURL { get; set; }

        [ForeignKey("BusinessTypeId")]
        public required BusinessType BusinessType { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}

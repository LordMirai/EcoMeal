using Microsoft.AspNetCore.SignalR;

using System.ComponentModel.DataAnnotations.Schema;

namespace EcoMeal.Entities
{
    public class Package
    {
        public Guid Id { get; set; }
        [ForeignKey("BusinessId")]
        public required Business Business { get; set; }

        [ForeignKey("PackageTypeID")]
        public required PackageType PackageType { get; set; }

        public required string Name { get; set; }
        public required string Description { get; set; }
        public float Price { get; set; }
        public float Quantity { get; set; }
        public DateTime PickupTime { get; set; }
        public DateTime ExpiryDate { get; set; }
        public required string ImageURL { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}

using Microsoft.AspNetCore.SignalR;

using System.ComponentModel.DataAnnotations.Schema;

namespace EcoMeal.Entities
{
    public class PackageType
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
    }
}

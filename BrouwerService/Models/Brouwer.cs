using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace BrouwerService.Models
{
    public class Brouwer
    {
        public int Id { get; set; }
        [Required, StringLength(255, MinimumLength = 1)]
        public required string Naam { get; set; }
        [Range(1000, 9999)]
        public int Postcode { get; set; }
        [Required, StringLength(255, MinimumLength = 1)]
        public required string Gemeente { get; set; }

    }
}

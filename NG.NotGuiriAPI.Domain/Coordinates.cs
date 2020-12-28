using System.ComponentModel.DataAnnotations;

namespace NG.NotGuiriAPI.Domain
{
    public class LocationRequest
    {
        [Required]
        [Range(-90.0, 90.0)]
        public double Latitude { get; set; }

        [Required]
        [Range(-90.0, 90.0)]
        public double Longitude { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double Radius { get; set; }
    }
}
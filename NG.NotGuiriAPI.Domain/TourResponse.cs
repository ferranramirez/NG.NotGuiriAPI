using NG.DBManager.Infrastructure.Contracts.Models;
using System;
using System.Collections.Generic;

namespace NG.NotGuiriAPI.Domain
{
    public class TourResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string GeoJson { get; set; }
        public int Duration { get; set; }
        public bool IsPremium { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsActive { get; set; }
        public Guid? ImageId { get; set; }
        public IList<Node> Nodes { get; set; }
        public IList<TourTag> TourTags { get; set; }

        public IList<DealType> DealTypes { get; set; }

        public static explicit operator TourResponse(Tour tour)
        {
            return new TourResponse
            {
                Id = tour.Id,
                Name = tour.Name,
                Description = tour.Description,
                GeoJson = tour.GeoJson,
                Duration = tour.Duration,
                IsPremium = tour.IsPremium,
                IsFeatured = tour.IsFeatured,
                ImageId = tour.ImageId,
                Nodes = tour.Nodes,
                TourTags = tour.TourTags
            };
        }
    }
}

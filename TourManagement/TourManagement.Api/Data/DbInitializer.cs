using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourManagement.Api.Models;

namespace TourManagement.Api.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Tours.Any())
            {
                return;
            }

            var tours = new Tour[]
            {
                new Tour
                {
                    TourId = new Guid("c7ba6add-09c4-45f8-8dd0-eaca221e5d93"),
                    Title = "Villains World Tour",
                    Description = "The Villains World Tour is a concert tour in support of the band's seventh studio album, Villains.",
                    StartDate = new DateTimeOffset(2017,6,22,0,0,0, new TimeSpan()),
                    EndDate = new DateTimeOffset(2018,3,18,0,0,0, new TimeSpan()),
                    EstimatedProfits = 2500000,
                    CreatedBy = "system",
                    CreatedOn = DateTime.UtcNow,
                },
                new Tour
                {
                    TourId = new Guid("f67ba678-b6e0-4307-afd9-e804c23b3cd3"),
                    Title = "Skeleton Tree European Tour",
                    Description = "Nick Cave and The Bad Seeds have announced an 8-week European tour kicking off in the UK at Bournemouth’s International Centre on 24th September. The tour will be the first time European audiences can experience live songs from new album Skeleton Tree alongside other Nick Cave & The Bad Seeds classics.  The touring line up features Nick Cave, Warren Ellis, Martyn Casey, Thomas Wydler, Jim Sclavunos, Conway Savage, George Vjestica and Larry Mullins.",
                    StartDate = new DateTimeOffset(2017,9,24,0,0,0, new TimeSpan()),
                    EndDate = new DateTimeOffset(2017,11,20,0,0,0, new TimeSpan()),
                    EstimatedProfits = 1200000,
                    CreatedBy = "system",
                    CreatedOn = DateTime.UtcNow,
                }
            };

            foreach(Tour tour in tours)
            {
                context.Tours.Add(tour);
            }
            context.SaveChanges();
        }
    }
}

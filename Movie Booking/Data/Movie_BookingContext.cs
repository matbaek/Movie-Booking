using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Movie_Booking.Models
{
    public class Movie_BookingContext : DbContext
    {
        public Movie_BookingContext (DbContextOptions<Movie_BookingContext> options)
            : base(options)
        {
        }

        public DbSet<Movie_Booking.Models.Movie> Movie { get; set; }
    }
}

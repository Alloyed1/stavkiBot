using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace stavkiBot.Models
{
    public class ApplicationContext: DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
			: base(options)
		{

		}
       public DbSet<Game> Games { get; set; }
        public DbSet<UsersBot> Users { get; set; }
    }
}

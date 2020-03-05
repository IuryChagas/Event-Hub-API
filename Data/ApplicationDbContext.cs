using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Event_Hub_API.Data
{
    public class ApplicationDbContext : DbContext
    {
     public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
     {

     }   
    }
}
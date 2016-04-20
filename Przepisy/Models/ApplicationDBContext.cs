using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Przepisy.Models
{
    public class ApplicationDBContext :DbContext 
    {
        public DbSet<ReceipModel> Receipts { get; set; }
    }
}
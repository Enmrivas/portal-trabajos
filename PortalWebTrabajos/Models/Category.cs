using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalWebTrabajos.Models
{
    public class Category
    {
        [Key]
        public int CatID { get; set; }

        public string Categoria { get; set; }
    }
}
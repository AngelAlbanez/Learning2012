using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class Carrito
    {

        [Key]
        public int RecordId { get; set; }
        public string CartID { get; set; }
        public int AlbumId { get; set; }
        public int Count { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual Movies Movies { get; set; }


    }
}
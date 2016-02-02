using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BakerWebApp.Models
{
    public class Registro
    {
        [Key]
        public int RegistroId { get; set; }

        //public int UsuarioId { get; set; }
        public int ClienteId { get; set; }
        public string UsuarioId { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name ="Fecha")]
        public DateTime FechaRegistro { get; set; }

        [DataType(DataType.MultilineText)]
        public string Nota { get; set; }

        [DataType(DataType.Currency)]
        public decimal Monto { get; set; }

        public bool Pagado { get; set; }

        [ScaffoldColumn(false)]
        [DataType(DataType.DateTime)]
        public DateTime FechaCreacion { get; set; }

        [ScaffoldColumn(false)]
        [DataType(DataType.DateTime)]
        public DateTime FechaUpdate { get; set; }

        public virtual ApplicationUser Usuario { get; set; }
        public virtual Cliente Cliente { get; set; }

        public Registro()
        {
            FechaCreacion = DateTime.Now;
            Monto = 0m;
            Pagado = false;
        }
    }
}

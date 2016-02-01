using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BakerWebApp.Models
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }

        //public int UsuarioId { get; set; }

        [DataType(DataType.Text)]
        public string Nombre { get; set; }

        [DataType(DataType.MultilineText)]
        public string Nota { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [Display(Name ="Teléfono")]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string Telefono { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Correo { get; set; }

        [ScaffoldColumn(false)]
        [DataType(DataType.DateTime)]
        public DateTime FechaCreacion { get; set; }

        [ScaffoldColumn(false)]
        [DataType(DataType.DateTime)]
        public DateTime FechaUpdate { get; set; }

        public string UsuarioId { get; set; }

        public virtual ApplicationUser Usuario { get; set; }

        public Cliente()
        {
            FechaCreacion = DateTime.Now;
        }
    }
}

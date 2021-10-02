using System;
using System.ComponentModel.DataAnnotations;

namespace VAP_NetCoreApp.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Perfil")]
        [Required(ErrorMessage = "El campo '{0}' es requerido")]
        public int RoleID { get; set; }

        [Display(Name = "Correo electronico")]
        [Required(ErrorMessage = "El campo '{0}' es requerido")]
        [MaxLength(50, ErrorMessage = "La longitud maxima del campo '{0}' es de {1}")]
        [EmailAddress(ErrorMessage = "El campo '{0}' tiene que ser un correo electronico valido")]
        public string Email { get; set; }

        [Display(Name = "Edad")]
        [Required(ErrorMessage = "El campo '{0}' es requerido")]
        public int? Age { get; set; }

        [Display(Name = "Esta activo?")]
        public bool IsActive { get; set; }

        [Display(Name = "Fecha de registro")]
        public DateTimeOffset RecordDate { get; set; }

        [Display(Name = "Balance actual")]
        [Required(ErrorMessage = "El campo '{0}' es requerido")]
        [Range(minimum: 0, maximum: 1000, ErrorMessage = "El valor para '{0}' tiene que estar entre {1} y {2}")]
        public decimal Balance { get; set; }

    }
}

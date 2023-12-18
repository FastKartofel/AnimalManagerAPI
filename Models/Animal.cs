using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PatientCareAPI.Models
{
    public class Animal
    {
        [Key]
        [Required]
        public int  _idAnimal { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(200)]
        [AllowNull]
        public string Description { get; set; }

        [MaxLength(200)]
        public string Category { get; set; }

        [MaxLength(200)]
        public string Area { get; set; }


    }
}

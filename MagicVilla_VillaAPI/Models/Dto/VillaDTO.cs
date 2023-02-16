using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaAPI.Models.Dto
{
    public class VillaDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(15)]
        public string Name { get; set; }

        public string Location { get; set; }

        public float sqft { get; set; }


    }
}

using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaAPI.Models.Dto
{
    public class VillaNoUpdateDTO
    {
        [Required]

        public int VillaNo { get; set; }

        public string specialDetails { get; set; }

    }
}

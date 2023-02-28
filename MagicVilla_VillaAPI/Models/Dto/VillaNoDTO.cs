using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaAPI.Models.Dto
{
    public class VillaNoDTO
    {
        [Required ]
        public int VillaNo { get; set; }

        public string specialDetails { get; set; }

        
    }
}

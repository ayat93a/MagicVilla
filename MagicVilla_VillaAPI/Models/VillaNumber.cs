﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVilla_VillaAPI.Models
{
    public class VillaNumber
    {
        [Key ]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VillaNo { get; set; }

        public string specialDetails { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatesDate { get; set; }
    }
}

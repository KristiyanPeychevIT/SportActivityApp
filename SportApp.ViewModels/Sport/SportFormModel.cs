using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SportApp.ViewModels.Sport
{
    public class SportFormModel
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string Duration { get; set; } = null!;

        [Required]
        public int TimesPerWeek { get; set; }
    }
}

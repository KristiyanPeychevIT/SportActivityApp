using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportApp.ViewModels.Sport
{
    public class SportAllViewModel
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Duration { get; set; } = null!;
        public int TimesPerWeek { get; set; }
        public string User { get; set; } = null!;
    }
}

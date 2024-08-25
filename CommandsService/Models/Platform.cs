using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CommandsService.Models
{
    public class Platform
    {
        [Key]
        [Required]
        public int Id { get; set; }

        // Id from external service
        [Required]
        public int ExternalID { get; set; }

        // name from external service
        [Required]
        public string Name { get; set; } = string.Empty;

        public ICollection<Command> Commands { get; set; } = new List<Command>();
    }
}
using System.ComponentModel.DataAnnotations;

namespace DrikusApp.Models
{
    public class MessageForm
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Message { get; set; }
    }

}
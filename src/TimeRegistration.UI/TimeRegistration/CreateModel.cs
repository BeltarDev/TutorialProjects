using System.ComponentModel.DataAnnotations;

namespace TimeRegistration.UI.TimeRegistration;

public class CreateModel
{
    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Description { get; set; }
}
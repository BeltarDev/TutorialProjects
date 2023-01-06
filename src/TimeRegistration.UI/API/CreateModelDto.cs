using System.ComponentModel.DataAnnotations;

namespace TimeRegistration.UI.API;

public class CreateModelDto
{
    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Description { get; set; }
}

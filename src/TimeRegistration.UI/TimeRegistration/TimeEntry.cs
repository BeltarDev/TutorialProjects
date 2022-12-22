using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace TimeRegistration.UI.TimeRegistration;

public class TimeEntry
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public DateTime StartTime { get; set; }
}

using System.ComponentModel.DataAnnotations;
namespace Assignment2_TripLogApp.Models;

public class TripLog
{
    [Key]
    public int TripId { get; set; }
    
    [Required(ErrorMessage = "Please enter a trip location")]
    public string Destination { get; set; }

    public string? Accommodation { get; set; }
    
    [Required(ErrorMessage = "Please enter a start date")]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }
    
    [Required(ErrorMessage = "Please enter an end date")]
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }
    
    [Required]
    [Phone(ErrorMessage = "Please enter a valid phone number")]
    [Display(Name = "Phone Number")]
    public string AccommodationPhoneNumber { get; set; }
    
    [Required]
    [EmailAddress(ErrorMessage = "Please enter a valid email address")]
    [Display(Name = "Email Address")]
    public string AccommodationEmailAddress { get; set; }
    
    public string? ToDo1 { get; set; }
    public string? ToDo2 { get; set; }
    public string? ToDo3 { get; set; }
}
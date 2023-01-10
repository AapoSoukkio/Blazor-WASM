using System.ComponentModel.DataAnnotations;

namespace PeopleLib;

public class Address
{
    public int Id { get; set; }    

    [Required]
    public string StreetAddress { get; set; }

    [Required]
    public int PostalNumber { get; set; }

    [Required]
    public string  PostalAddress { get; set; }
}

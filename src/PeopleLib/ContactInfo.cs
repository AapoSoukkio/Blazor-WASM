using System.ComponentModel.DataAnnotations;

namespace PeopleLib;

public class ContactInfo
{
    public int Id { get; set; }
    public int PersonId { get; set; }
    public int AddressId { get; set; }

    /// <summary>
    /// Possible extra info about the contact information
    /// </summary>
    public string? Info { get; set; }

}

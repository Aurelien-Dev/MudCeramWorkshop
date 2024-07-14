using MudCeramWorkshop.Data.Domain.Models.Identity;
using MudCeramWorkshop.Data.Domain.Models.MainDomain;
using System.ComponentModel.DataAnnotations;

namespace MudCeramWorkshop.Data.Domain.Models.WorkshopDomaine;

/// <summary>
/// Represents a workshop in the system.
/// </summary>
public class Workshop
{
    /// <summary>
    /// The unique identifier of the workshop.
    /// </summary>
    [Required]
    public int Id { get; set; }


    /// <summary>
    /// The name of the workshop.
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// The culture of the workshop (e.g. "fr" for French).
    /// </summary>
    [Required]
    public string Culture { get; set; }

    /// <summary>
    /// The logo of the workshop, if any.
    /// </summary>
    public string? Logo { get; set; }

    /// <summary>
    /// The products offered by the workshop.
    /// </summary>
    public ICollection<Product> Products { get; set; } = new List<Product>();

    /// <summary>
    /// The parameters for the workshop.
    /// </summary>
    public ICollection<WorkshopParameter> WorkshopParameters { get; set; } = new List<WorkshopParameter>();

    public ApplicationUser? ApplicationUser { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Workshop"/> class.
    /// </summary>
    /// <param name="name">The name of the workshop.</param>
    public Workshop(string name)
    {
        Name = name;
        Culture = "fr";
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Workshop"/> class with the specified parameters and default french culture.
    /// </summary>
    /// <param name="name">The name of the workshop.</param>
    /// <param name="logo">The logo of the workshop, if any.</param>
    public Workshop(string name, string culture)
    {
        Name = name;
        Culture = culture;
    }
}
﻿using MudCeramWorkshop.Data.Domain.Models.MainDomain.Enums;
using MudCeramWorkshop.Data.Domain.CustomDataAnotation;

namespace MudCeramWorkshop.Data.Domain.Models.MainDomain;

/// <summary>
/// Represents an image instruction for a product.
/// </summary>
public class ImageInstruction
{
    /// <summary>
    /// Gets or sets the ID of the image instruction.
    /// </summary>
    [CeramRequired]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the ID of the product associated with the image instruction.
    /// </summary>
    [CeramRequired]
    public int IdProduct { get; set; }

    /// <summary>
    /// Gets or sets the URL of the image file.
    /// </summary>
    [CeramRequired]
    public string UrlMedium { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the URL of the image file.
    /// </summary>
    [CeramRequired]
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the comment associated with the image instruction.
    /// </summary>
    public string Comment { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the location of the image file.
    /// </summary>
    /// <remarks>
    /// This property is mainly used to specify the deletion link for the file.
    /// </remarks>
    public EnumLocation FileLocation { get; set; }

    /// <summary>
    /// Indicates whether this is the product favorite image
    /// </summary>
    public bool IsFavoriteImage { get; set; }

    /// <summary>
    /// Gets or sets the product associated with the image instruction.
    /// </summary>
    public Product? ProductAssociate { get; set; }
}
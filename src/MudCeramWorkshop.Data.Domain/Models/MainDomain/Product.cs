﻿using MudCeramWorkshop.Data.Domain.Models.MainDomain.Enums;
using MudCeramWorkshop.Data.Domain.Models.WorkshopDomaine;
using MudCeramWorkshop.Data.Domain.CustomDataAnotation;
using System.Xml.Serialization;

namespace MudCeramWorkshop.Data.Domain.Models.MainDomain;


public class Product
{
    /// <summary>
    /// Gets or sets the ID of the product.
    /// </summary>
    [CeramRequired]
    public int? Id { get; set; }

    /// <summary>
    /// Gets or sets the ID of the workshop where the product is made.
    /// </summary>
    [CeramRequired]
    public int IdWorkshop { get; set; }

    /// <summary>
    /// Gets or sets the reference code of the product.
    /// </summary>
    [CeramRequired]
    public string Reference { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the product.
    /// </summary>
    [CeramRequired]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the product.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the height of the product.
    /// </summary>
    public double? Height { get; set; }

    /// <summary>
    /// Gets or sets the top diameter of the product.
    /// </summary>
    public double? TopDiameter { get; set; }

    /// <summary>
    /// Gets or sets the bottom diameter of the product.
    /// </summary>
    public double? BottomDiameter { get; set; }

    /// <summary>
    /// Gets or sets the shrinking coefficient of the product.
    /// </summary>
    public double? ShrinkingCoeficient { get; set; }

    /// <summary>
    /// Gets or sets the design instruction of the product.
    /// </summary>
    public string? DesignInstruction { get; set; }

    /// <summary>
    /// Gets or sets the glazing instruction of the product.
    /// </summary>
    public string? GlazingInstruction { get; set; }

    /// <summary>
    /// Gets or sets the price of the product.
    /// </summary>
    public double? Price { get; set; }

    /// <summary>
    /// Gets or sets the status of the product.
    /// </summary>
    public EnumProductStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the image instructions associated with the product.
    /// </summary>
    public ICollection<ImageInstruction> ImageInstructions { get; set; } = new List<ImageInstruction>();

    /// <summary>
    /// Gets or sets the materials associated with the product.
    /// </summary>
    public ICollection<ProductMaterial> ProductMaterial { get; set; } = new List<ProductMaterial>();

    /// <summary>
    /// Gets or sets the firings associated with the product.
    /// </summary>
    public ICollection<ProductFiring> ProductFiring { get; set; } = new List<ProductFiring>();

    /// <summary>
    /// Gets or sets the workshop where the product is made.
    /// </summary>
    public Workshop Workshop { get; set; } = default!;


    /// <summary>
    /// Initializes a new instance of the <see cref="Product"/> class with the specified name and reference.
    /// </summary>
    public Product(int idWorkshop, string name, string reference)
    {
        IdWorkshop = idWorkshop;
        Name = name;
        Reference = reference;
    }

    /// <summary>
    /// Gets or sets the image instructions associated with the product.
    /// </summary>
    [XmlIgnore]
    public ImageInstruction? FavoriteImage
    {
        get => ImageInstructions.FirstOrDefault(i => i.IsFavoriteImage);
    }
    
    public Product GetClone()
    {
        //Both Cloned and Existing Object Point to the Same Memory Location of the Address Object
        return (Product)this.MemberwiseClone();
    }
}
using MudCeramWorkshop.Data.Domain.Models.MainDomain.Enums;
using MudCeramWorkshop.Data.Domain.CustomDataAnotation;

namespace MudCeramWorkshop.Data.Domain.Models.MainDomain;

/// <summary>
/// Represents a material used in the production process.
/// </summary>
public class Material
{
    /// <summary>
    /// Gets or sets the ID of the material.
    /// </summary>
    [CeramRequired]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the reference code of the material.
    /// </summary>
    [CeramRequired]
    public string Reference { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the material.
    /// </summary>
    [CeramRequired]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether the material is homemade.
    /// </summary>
    public bool? IsHomeMade { get; set; } = false;

    /// <summary>
    /// Gets or sets the cost of the material.
    /// </summary>
    public double Cost { get; set; }

    /// <summary>
    /// Gets or sets a comment associated with the material.
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    /// Gets or sets a link associated with the material.
    /// </summary>
    public string? Link { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the material.
    /// </summary>
    public double Quantity { get; set; } = 1;

    /// <summary>
    /// Gets or sets the unit of measurement for the material.
    /// </summary>
    public EnumMaterialUnite UniteMesure { get; set; } = default;

    /// <summary>
    /// Gets or sets the type of material.
    /// </summary>
    public EnumMaterialType Type { get; set; } = default;

    /// <summary>
    /// Gets or sets the products associated with the material.
    /// </summary>
    public ICollection<ProductMaterial> ProductMaterial { get; set; } = new List<ProductMaterial>();

    /// <summary>
    /// Initializes a new instance of the <see cref="Material"/> class.
    /// </summary>
    public Material(string reference, string name)
    {
        Reference = reference;
        Name = name;
    }

    /// <summary>
    /// Gets the quantity of the material in unified units (e.g. grams or liters).
    /// </summary>
    public double UnifiedQuantity
    {
        get
        {
            switch (UniteMesure)
            {
                case EnumMaterialUnite.Kg:
                    return Quantity * 1000;
                case EnumMaterialUnite.L:
                    return Quantity * 1000;
                case EnumMaterialUnite.Unit:
                    return Quantity;
                default:
                    throw new InvalidOperationException();
            }
        }
    }

    public string StringSearch()
    {
        return $"{Reference}{Name}{Cost}{Comment}{UniteMesure}";
    }
}
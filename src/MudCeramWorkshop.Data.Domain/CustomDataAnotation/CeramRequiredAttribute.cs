using System.ComponentModel.DataAnnotations;

namespace MudCeramWorkshop.Data.Domain.CustomDataAnotation;

/// <summary>
/// Represents a custom validation attribute that requires a property to be non-null or non-empty.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class CeramRequiredAttribute : RequiredAttribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CeramRequiredAttribute"/> class.
    /// </summary>
    public CeramRequiredAttribute() : base()
    {
        // Set the error message to a localized string resource
        ErrorMessageResourceName = nameof(Resources.FormErrors.FieldRequired);
        ErrorMessageResourceType = typeof(Resources.FormErrors);
    }
}
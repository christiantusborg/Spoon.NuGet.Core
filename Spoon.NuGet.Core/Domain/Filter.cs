﻿#nullable enable

namespace Spoon.NuGet.Core.Domain;

/// <summary>
/// The filter.
/// </summary>
public class Filter
{
    /// <summary>
    /// Gets or sets the property name.
    /// </summary>
    /// <value>The name of the property.</value>
    public string PropertyName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the operation.
    /// </summary>
    /// <value>The operation.</value>
    public Operation Operation { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    /// <value>The value.</value>
    public object? Value { get; set; }
}
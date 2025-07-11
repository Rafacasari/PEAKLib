using System;
using System.Collections.Generic;
using UnityEngine;

namespace PEAKLib.Core.UnityEditor;

/// <summary>
/// A <see cref="ScriptableObject"/> representation of a <see cref="ModDefinition"/>.
/// </summary>
[CreateAssetMenu(fileName = "ModDefinition", menuName = "PEAKLib/ModDefinition", order = 0)]
public class UnityModDefinition : ScriptableObject, IModDefinitionResolvable
{
    [SerializeField]
    private string modId = "";

    [SerializeField]
    private string modName = "";

    [SerializeField]
    private string modVersion = "";

    /// <exception cref="FormatException"></exception>
    /// <inheritdoc/>
    public ModDefinition Resolve()
    {
        ThrowHelper.ThrowIfFieldNullOrWhiteSpace(modId);
        ThrowHelper.ThrowIfFieldNullOrWhiteSpace(modName);
        ThrowHelper.ThrowIfFieldNullOrWhiteSpace(modVersion);

        Version version;
        try
        {
            version = new Version(modVersion);
        }
        catch (FormatException ex)
        {
            throw new FormatException(
                $"Version of the mod is not in a valid format!\n{ex.Message}",
                ex
            );
        }

        return ModDefinition.GetOrCreate(modId, modName, version);
    }
}

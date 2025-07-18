using System;
using System.Diagnostics.CodeAnalysis;
using PEAKLib.Core;
using UnityEngine;

namespace PEAKLib.Items;

/// <summary>
/// An <see cref="ItemComponent"/> to use with PEAKLib.
/// </summary>
public abstract class ModItemComponent : ItemComponent
{
    /// <summary>
    /// The <see cref="ModDefinition"/> which owns this item.
    /// </summary>
    public ModDefinition Mod
    {
        get
        {
            if (_mod is not null)
                return _mod;

            if (!ModDefinition.TryGetMod(_modId, out _mod))
            {
                throw new NullReferenceException(
                    $"{nameof(ModItemComponent)}'s {nameof(Mod)} property was null"
                        + " meaning it wasn't initialized by PEAKLib!"
                );
            }

            return _mod;
        }
    }

    private ModDefinition? _mod;

    [HideInInspector, SerializeField]
    private string _modId = null!;

    /// <summary>
    /// Used by PEAKLib to initialize this <see cref="ModItemComponent"/>
    /// during <see cref="ItemContent"/> Registration.
    /// </summary>
    /// <param name="mod">The <see cref="ModDefinition"/> who registered this item.</param>
    internal void InitializeModItem(ModDefinition mod)
    {
        _modId = ThrowHelper.ThrowIfArgumentNull(mod).Id;
    }

    /// <summary>
    /// Tries to get mod item data for this item.
    /// </summary>
    /// <inheritdoc cref="CustomItemData.TryGetModItemDataFromJson{T}(ItemComponent, ModDefinition, out T)"/>
    public bool TryGetModItemDataFromJson<T>([MaybeNullWhen(false)] out T data) =>
        CustomItemData.TryGetModItemDataFromJson(this, Mod, out data);

    /// <summary>
    /// Tries to get raw mod item data for this item.
    /// </summary>
    /// <param name="rawData">The data as a byte array or null.</param>
    /// <returns>Whether or not data was found.</returns>
    public bool TryGetRawModItemData([NotNullWhen(true)] out byte[]? rawData) =>
        CustomItemData.TryGetRawModItemData(this, Mod, out rawData);

    /// <summary>
    /// Sets mod item data for this item.
    /// </summary>
    /// <inheritdoc cref="CustomItemData.SetModItemDataFromJson(ItemComponent, ModDefinition, object?)"/>
    public void SetModItemDataFromJson(object? data) =>
        CustomItemData.SetModItemDataFromJson(this, Mod, data);

    /// <summary>
    /// Sets raw mod item data for this item.
    /// </summary>
    /// <param name="rawData">The data to set as a byte array.</param>
    public void SetRawModItemData(byte[] rawData) =>
        CustomItemData.SetRawModItemData(this, Mod, rawData);
}

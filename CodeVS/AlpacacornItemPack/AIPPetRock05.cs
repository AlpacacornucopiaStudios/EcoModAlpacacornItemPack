namespace Eco.Mods.TechTree
{
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Eco.Core.Items;
using Eco.Gameplay.Blocks;
using Eco.Gameplay.Components;
using Eco.Gameplay.Components.Auth;
using Eco.Gameplay.DynamicValues;
using Eco.Gameplay.Economy;
using Eco.Gameplay.Housing;
using Eco.Gameplay.Interactions;
using Eco.Gameplay.Items;
using Eco.Gameplay.Modules;
using Eco.Gameplay.Minimap;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Occupancy;
using Eco.Gameplay.Players;
using Eco.Gameplay.Property;
using Eco.Gameplay.Skills;
using Eco.Gameplay.Systems;
using Eco.Gameplay.Systems.TextLinks;
using Eco.Shared;
using Eco.Shared.Math;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using Eco.Shared.Utils;
using Eco.Shared.View;
using Eco.Shared.Items;
using Eco.Shared.Networking;
using Eco.World.Blocks;
using Eco.Gameplay.Housing.PropertyValues;
using Eco.Gameplay.Civics.Objects;
using Eco.Gameplay.Settlements;
using Eco.Gameplay.Systems.NewTooltip;
using Eco.Core.Controller;
using Eco.Core.Utils;
using Eco.Gameplay.Components.Storage;
using Eco.Gameplay.Items.Recipes;
using Eco.Stats;
using System.Runtime.Versioning;
using Eco.Mods.Organisms;
using Eco.Simulation.WorldLayers;
//Object////////////////////////////////////////////////////////////////
[Serialized]
[RequireComponent(typeof(PropertyAuthComponent))]
[RequireComponent(typeof(HousingComponent))]
[RequireComponent(typeof(OccupancyRequirementComponent))]
[RequireComponent(typeof(ForSaleComponent))]
[RequireComponent(typeof(RoomRequirementsComponent))]
[Tag("Usable")]
[Ecopedia("Housing Objects", "Decoration", subPageName: "Pet Rock 05 Item")]
[SupportedOSPlatform("windows7.0")]
public partial class AIPPetRock05Object : WorldObject, IRepresentsItem
{
public virtual Type RepresentedItemType => typeof(AIPPetRock05Item);
public override LocString DisplayName => Localizer.DoStr("Pet Rock 05");
public override TableTextureMode TableTexture => TableTextureMode.Stone;
protected override void Initialize()
{
this.ModsPreInitialize();
this.GetComponent<HousingComponent>().HomeValue = AIPPetRock05Item.homeValue;
this.ModsPostInitialize();
}
static AIPPetRock05Object()
{
var occupancies = new List<BlockOccupancy>();
//Vector3(x, y, z):  x is left/right, y is up/down, z is forward/backward
// back   Shorthand for writing Vector3(0, 0, -1).
// down    Shorthand for writing Vector3(0, -1, 0).
// forward Shorthand for writing Vector3(0, 0, 1).
// left    Shorthand for writing Vector3(-1, 0, 0).
for (int x = 0; x <= 0; x++)
for (int y = 0; y <= 0; y++)
for (int z = 0; z <= 0; z++)
occupancies.Add(new BlockOccupancy(new Vector3i(x, y, z)));
WorldObject.AddOccupancy<AIPPetRock05Object>(occupancies);
}
/// <summary>Hook for mods to customize WorldObject before initialization. You can change housing values here.</summary>
partial void ModsPreInitialize();
/// <summary>Hook for mods to customize WorldObject after initialization.</summary>
partial void ModsPostInitialize();
}
//Item////////////////////////////////////////////////////////////////
[Serialized]
[SupportedOSPlatform("windows7.0")]
[LocDisplayName("Pet Rock 05")]
[LocDescription("A Pet Rock carefully painted by a skilled artisan. It exudes personality.")]
[Ecopedia("Housing Objects", "Decoration", createAsSubPage: true)]
[Tag("Housing")]
[Weight(100)]
[Tag(nameof(SurfaceTags.CanBeOnRug))]
public partial class AIPPetRock05Item : WorldObjectItem<AIPPetRock05Object>
{
protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));
public override HomeFurnishingValue HomeValue => homeValue;
public static readonly HomeFurnishingValue homeValue = new HomeFurnishingValue()
{
ObjectName = typeof(AIPPetRock05Object).UILink(),
Category = HousingConfig.GetRoomCategory("Decoration"),
BaseValue = 2,
TypeForRoomLimit = Localizer.DoStr("Decoration"),
DiminishingReturnMultiplier = 0.01f
};
}
//Recipe////////////////////////////////////////////////////////////////
[RequiresSkill(typeof(PaintingSkill), 2)]
[Ecopedia("Housing Objects", "Decoration", subPageName: "Pet Rock 05 Item")]
[SupportedOSPlatform("windows7.0")]
public partial class AIPPetRock05Recipe : Recipe
{
public AIPPetRock05Recipe()
{
this.Init(
name: "AIPPetRock05", //noloc
displayName: Localizer.DoStr("Pet Rock 05"),
ingredients: new List<IngredientElement>
{
            new IngredientElement(typeof(OilItem), 5, true),
            new IngredientElement(typeof(OilPaintItem), 9, true),
            new IngredientElement("Rock",30, true),
},
items: new List<CraftingElement>{new CraftingElement<AIPPetRock05Item>(),});
this.ModsPreInitialize();
this.ModsPostInitialize();
CraftingComponent.AddTagProduct(tableType: typeof(PaintMixerObject), typeof(AIPPetRock01Recipe), this);
}
/// <summary>Hook for mods to customize RecipeFamily before initialization. You can change recipes, xp, labor, time here.</summary>
partial void ModsPreInitialize();
/// <summary>Hook for mods to customize RecipeFamily after initialization, but before registration. You can change skill requirements here.</summary>
partial void ModsPostInitialize();
};
};
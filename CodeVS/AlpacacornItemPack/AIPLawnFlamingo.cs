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
[Ecopedia("Housing Objects", "Decoration", subPageName: "Lawn Flamingo Item")]
[SupportedOSPlatform("windows7.0")]
public partial class AIPLawnFlamingoObject : WorldObject, IRepresentsItem
{
public virtual Type RepresentedItemType => typeof(AIPLawnFlamingoItem);
public override LocString DisplayName => Localizer.DoStr("Lawn Flamingo");
public override TableTextureMode TableTexture => TableTextureMode.Stone;
protected override void Initialize()
{
this.ModsPreInitialize();
this.GetComponent<HousingComponent>().HomeValue = AIPLawnFlamingoItem.homeValue;
this.ModsPostInitialize();
}
static AIPLawnFlamingoObject()
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
WorldObject.AddOccupancy<AIPLawnFlamingoObject>(occupancies);
}
/// <summary>Hook for mods to customize WorldObject before initialization. You can change housing values here.</summary>
partial void ModsPreInitialize();
/// <summary>Hook for mods to customize WorldObject after initialization.</summary>
partial void ModsPostInitialize();
}
//Item////////////////////////////////////////////////////////////////
[Serialized]
[SupportedOSPlatform("windows7.0")]
[LocDisplayName("Lawn Flamingo")]
[LocDescription("A timeless fashionable icon, this flamingo wants to be your friend.")]
[Ecopedia("Housing Objects", "Decoration", createAsSubPage: true)]
[Tag("Housing")]
[Weight(100)]
[Tag(nameof(SurfaceTags.CanBeOnRug))]
public partial class AIPLawnFlamingoItem : WorldObjectItem<AIPLawnFlamingoObject>
{
protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));
public override HomeFurnishingValue HomeValue => homeValue;
public static readonly HomeFurnishingValue homeValue = new HomeFurnishingValue()
{
ObjectName = typeof(AIPLawnFlamingoObject).UILink(),
Category = HousingConfig.GetRoomCategory("Decoration"),
BaseValue = 2,
TypeForRoomLimit = Localizer.DoStr("Decoration"),
DiminishingReturnMultiplier = 0.01f
};
}
//Recipe////////////////////////////////////////////////////////////////
[RequiresSkill(typeof(PaintingSkill), 2)]
[Ecopedia("Housing Objects", "Decoration", subPageName: "Lawn Flamingo Item")]
[SupportedOSPlatform("windows7.0")]
public partial class AIPLawnFlamingoRecipe : RecipeFamily
{
public AIPLawnFlamingoRecipe()
{
var recipe = new Recipe();
recipe.Init(
name: "AIPLawnFlamingo", //noloc
displayName: Localizer.DoStr("Lawn Flamingo"),
ingredients: new List<IngredientElement>
{
            new IngredientElement(typeof(MagentaPowderItem), 30, typeof(TailoringSkill), typeof(TailoringLavishResourcesTalent)),
            new IngredientElement(typeof(AIPGlassEyeBeadItem), 2, true),
            new IngredientElement(typeof(BoardItem), 5, true),
            new IngredientElement(typeof(OilPaintItem), 5, true),
},
items: new List<CraftingElement>{new CraftingElement<AIPLawnFlamingoItem>(),});
this.Recipes = new List<Recipe> { recipe };
this.ExperienceOnCraft = 1;
// Defines the amount of labor required and the required skill to add labor
this.LaborInCalories = CreateLaborInCaloriesValue(100, typeof(TailoringSkill));
// Defines our crafting time for the recipe
this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(PaintingSkill), start: 2f, skillType: typeof(PaintingSkill), typeof(PaintingFocusedSpeedTalent), typeof(PaintingParallelSpeedTalent));
// Perform pre/post initialization for user mods and initialize our recipe instance with the display name "Lawn Flamingo"
this.ModsPreInitialize();
this.Initialize(displayText: Localizer.DoStr("Lawn Flamingo"), recipeType: typeof(AIPLawnFlamingoRecipe));
this.ModsPostInitialize();
// Register our RecipeFamily instance with the crafting system so it can be crafted.
CraftingComponent.AddRecipe(tableType: typeof(PaintMixerObject), recipeFamily: this);
}
/// <summary>Hook for mods to customize RecipeFamily before initialization. You can change recipes, xp, labor, time here.</summary>
partial void ModsPreInitialize();
/// <summary>Hook for mods to customize RecipeFamily after initialization, but before registration. You can change skill requirements here.</summary>
partial void ModsPostInitialize();
};
};
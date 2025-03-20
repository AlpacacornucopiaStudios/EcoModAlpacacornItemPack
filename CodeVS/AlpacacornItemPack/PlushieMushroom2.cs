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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Plushie Mushroom 2 Item")]
    [SupportedOSPlatform("windows7.0")]

    public partial class PlushieMushroom2Object : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(PlushieMushroom2Item);
        public override LocString DisplayName => Localizer.DoStr("Plushie Mushroom 2");
        public override TableTextureMode TableTexture => TableTextureMode.Stone;

        protected override void Initialize()
        {
            this.ModsPreInitialize();
            this.GetComponent<HousingComponent>().HomeValue = PlushieMushroom2Item.homeValue;
            this.ModsPostInitialize();
        }

        static PlushieMushroom2Object()
        {
            WorldObject.AddOccupancy<PlushieMushroom2Object>(new List<BlockOccupancy>(){
//Vector3(x, y, z): x is left/right, y is up/down, z is forward/backward
            // back   Shorthand for writing Vector3(0, 0, -1).
            // down    Shorthand for writing Vector3(0, -1, 0).
            // forward Shorthand for writing Vector3(0, 0, 1).
            // left    Shorthand for writing Vector3(-1, 0, 0).

             //new BlockOccupancy(new Vector3i(0, 1, 0)),
             //new BlockOccupancy(new Vector3i(0, 1, -1)),
             new BlockOccupancy(new Vector3i(0, 0, 0)),
             //new BlockOccupancy(new Vector3i(0, 0, -1))
            });
        }

        /// <summary>Hook for mods to customize WorldObject before initialization. You can change housing values here.</summary>
        partial void ModsPreInitialize();
        /// <summary>Hook for mods to customize WorldObject after initialization.</summary>
        partial void ModsPostInitialize();
    }

    //Item////////////////////////////////////////////////////////////////

    [Serialized]
    [SupportedOSPlatform("windows7.0")]
    [LocDisplayName("Plushie Mushroom 2")]
    [LocDescription("A huggable plushie Mushroom stuffed with cotton.")]
    [Ecopedia("Housing Objects", "Decoration", createAsSubPage: true)]
    [Tag("Housing")]
    [Tag("Plushie")]
    [Weight(100)]
    [Tag(nameof(SurfaceTags.CanBeOnRug))]
    public partial class PlushieMushroom2Item : WorldObjectItem<PlushieMushroom2Object>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));
        public override HomeFurnishingValue HomeValue => homeValue;
        public static readonly HomeFurnishingValue homeValue = new HomeFurnishingValue()
        {
            ObjectName = typeof(PlushieMushroom2Object).UILink(),
            Category = HousingConfig.GetRoomCategory("Decoration"),
            BaseValue = 2,
            TypeForRoomLimit = Localizer.DoStr("Decoration"),
            DiminishingReturnMultiplier = 0.01f
        };
    }


    //Recipe////////////////////////////////////////////////////////////////

    [RequiresSkill(typeof(TailoringSkill), 5)]
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Plushie Mushroom 2 Item")]
    [SupportedOSPlatform("windows7.0")]
    public partial class PlushieMushroom2Recipe : RecipeFamily
    {
        public PlushieMushroom2Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "PlushieMushroom2", //noloc
                displayName: Localizer.DoStr("Plushie Mushroom 2"),

            ingredients: new List<IngredientElement>
            {
                new IngredientElement(typeof(CottonFabricItem), 10, typeof(TailoringSkill), typeof(TailoringLavishResourcesTalent)),
                new IngredientElement(typeof(CottonLintItem), 20, typeof(TailoringSkill), typeof(TailoringLavishResourcesTalent)),
                new IngredientElement(typeof(CottonThreadItem), 5, typeof(TailoringSkill), typeof(TailoringLavishResourcesTalent)),
                new IngredientElement(typeof(CharcoalPowderItem), 20, typeof(TailoringSkill), typeof(TailoringLavishResourcesTalent)),
                new IngredientElement(typeof(MagentaPowderItem), 10, typeof(TailoringSkill), typeof(TailoringLavishResourcesTalent)),
                new IngredientElement(typeof(WhitePowderItem), 20, typeof(TailoringSkill), typeof(TailoringLavishResourcesTalent))
            },
            items: new List<CraftingElement>
            {
                new CraftingElement<PlushieMushroom2Item>(),
            });

            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 3;
            // Defines the amount of labor required and the required skill to add labor
            this.LaborInCalories = CreateLaborInCaloriesValue(100, typeof(TailoringSkill));
            // Defines our crafting time for the recipe
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(TailoringSkill), start: 2f, skillType: typeof(TailoringSkill), typeof(TailoringFocusedSpeedTalent), typeof(TailoringParallelSpeedTalent));


            // Perform pre/post initialization for user mods and initialize our recipe instance with the display name "Plushie Mushroom 2"
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Plushie Mushroom 2"), recipeType: typeof(PlushieMushroom2Recipe));
            this.ModsPostInitialize();
            // Register our RecipeFamily instance with the crafting system so it can be crafted.
            CraftingComponent.AddRecipe(tableType: typeof(TailoringTableObject), recipeFamily: this);

        }
        /// <summary>Hook for mods to customize RecipeFamily before initialization. You can change recipes, xp, labor, time here.</summary>
        partial void ModsPreInitialize();
        /// <summary>Hook for mods to customize RecipeFamily after initialization, but before registration. You can change skill requirements here.</summary>
        partial void ModsPostInitialize();
    };
};
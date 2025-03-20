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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Traffic Barricade A-Frame Item")]
    [SupportedOSPlatform("windows7.0")]

    public partial class AIPTrafficBarricadeAFrame01Object : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(AIPTrafficBarricadeAFrame01Item);
        public override LocString DisplayName => Localizer.DoStr("Traffic Barricade A-Frame");
        public override TableTextureMode TableTexture => TableTextureMode.Stone;

        protected override void Initialize()
        {
            this.ModsPreInitialize();
            this.GetComponent<HousingComponent>().HomeValue = AIPTrafficBarricadeAFrame01Item.homeValue;
            this.ModsPostInitialize();
        }

        static AIPTrafficBarricadeAFrame01Object()
        {
            var occupancies = new List<BlockOccupancy>();

            //Vector3(x, y, z):  x is left/right, y is up/down, z is forward/backward
            // back   Shorthand for writing Vector3(0, 0, -1).
            // down    Shorthand for writing Vector3(0, -1, 0).
            // forward Shorthand for writing Vector3(0, 0, 1).
            // left    Shorthand for writing Vector3(-1, 0, 0).

            for (int x = -1; x <= 1; x++)
                for (int y = 0; y <= 1; y++)
                    for (int z = 0; z <= 0; z++)
                        occupancies.Add(new BlockOccupancy(new Vector3i(x, y, z)));
            WorldObject.AddOccupancy<AIPTrafficBarricadeAFrame01Object>(occupancies);
        }

        /// <summary>Hook for mods to customize WorldObject before initialization. You can change housing values here.</summary>
        partial void ModsPreInitialize();
        /// <summary>Hook for mods to customize WorldObject after initialization.</summary>
        partial void ModsPostInitialize();
    }

    //Item////////////////////////////////////////////////////////////////

    [Serialized]
    [SupportedOSPlatform("windows7.0")]
    [LocDisplayName("Traffic Barricade A-Frame")]
    [LocDescription("Traffic Barricade A-Frame: promoting a culture of safety. Friends don't let friends fall into pits when doing roadwork.")]
    [Ecopedia("Crafted Objects", "Community", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(100)]
    [Tag(nameof(SurfaceTags.CanBeOnRug))]
    public partial class AIPTrafficBarricadeAFrame01Item : WorldObjectItem<AIPTrafficBarricadeAFrame01Object>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));
        public override HomeFurnishingValue HomeValue => homeValue;
        public static readonly HomeFurnishingValue homeValue = new HomeFurnishingValue()
        {
            ObjectName = typeof(AIPTrafficBarricadeAFrame01Object).UILink(),
            Category = HousingConfig.GetRoomCategory("Decoration"),
            BaseValue = 1,
            TypeForRoomLimit = Localizer.DoStr("Decoration"),
            DiminishingReturnMultiplier = 0.01f
        };
    }


    //Recipe////////////////////////////////////////////////////////////////

    [Ecopedia("Housing Objects", "Decoration", subPageName: "Traffic Barricade A-Frame Item")]
    [SupportedOSPlatform("windows7.0")]
    public partial class AIPTrafficBarricadeAFrame01Recipe : RecipeFamily
    {
        public AIPTrafficBarricadeAFrame01Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "AIPTrafficBarricadeAFrame01", //noloc
                displayName: Localizer.DoStr("Traffic Barricade A-Frame"),

            ingredients: new List<IngredientElement>
            {
                    new IngredientElement(typeof(BoardItem), 4 ,typeof(Skill)), //noloc
                    new IngredientElement(typeof(IronOxideItem), 1, typeof(Skill)),
                    new IngredientElement(typeof(YellowPowderItem), 1, typeof(Skill))
            },
            items: new List<CraftingElement>
            {
                new CraftingElement<AIPTrafficBarricadeAFrame01Item>(),
            });

            this.Recipes = new List<Recipe> { recipe };
            // Defines the amount of labor required and the required skill to add labor
            this.LaborInCalories = CreateLaborInCaloriesValue(15);

            // Defines our crafting time for the recipe
            this.CraftMinutes = CreateCraftTimeValue(2);

            // Perform pre/post initialization for user mods and initialize our recipe instance with the display name "Traffic Barricade A-Frame"
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Traffic Barricade A-Frame"), recipeType: typeof(AIPTrafficBarricadeAFrame01Recipe));
            this.ModsPostInitialize();
            // Register our RecipeFamily instance with the crafting system so it can be crafted.
            CraftingComponent.AddRecipe(tableType: typeof(WorkbenchObject), recipeFamily: this);

        }
        /// <summary>Hook for mods to customize RecipeFamily before initialization. You can change recipes, xp, labor, time here.</summary>
        partial void ModsPreInitialize();
        /// <summary>Hook for mods to customize RecipeFamily after initialization, but before registration. You can change skill requirements here.</summary>
        partial void ModsPostInitialize();
    };
};
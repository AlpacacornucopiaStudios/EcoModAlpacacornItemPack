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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Plushie Bear with Pink Bowtie Item")]
    [SupportedOSPlatform("windows7.0")]

    public partial class AIPPlushieBearBowtiePinkObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(AIPPlushieBearBowtiePinkItem);
        public override LocString DisplayName => Localizer.DoStr("Plushie Bear with Pink Bowtie");
        public override TableTextureMode TableTexture => TableTextureMode.Stone;

        protected override void Initialize()
        {
            this.ModsPreInitialize();
            this.GetComponent<HousingComponent>().HomeValue = AIPPlushieBearBowtiePinkItem.homeValue;
            this.ModsPostInitialize();
        }

        static AIPPlushieBearBowtiePinkObject()
        {
            WorldObject.AddOccupancy<AIPPlushieBearBowtiePinkObject>(new List<BlockOccupancy>(){
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
    [LocDisplayName("Plushie Bear with Pink Bowtie")]
    [LocDescription("A huggable Plushie Bear stuffed with cotton wearing a Pink Bowtie. The bear appears to be wiggling and wants to be held.")]
    [Ecopedia("Housing Objects", "Decoration", createAsSubPage: true)]
    [Tag("Housing")]
    [Tag("Plushie")]
    [Weight(100)]
    [Tag(nameof(SurfaceTags.CanBeOnRug))]
    public partial class AIPPlushieBearBowtiePinkItem : WorldObjectItem<AIPPlushieBearBowtiePinkObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));
        public override HomeFurnishingValue HomeValue => homeValue;
        public static readonly HomeFurnishingValue homeValue = new HomeFurnishingValue()
        {
            ObjectName = typeof(AIPPlushieBearBowtiePinkObject).UILink(),
            Category = HousingConfig.GetRoomCategory("Decoration"),
            BaseValue = 2.5f,
            TypeForRoomLimit = Localizer.DoStr("Decoration"),
            DiminishingReturnMultiplier = 0.01f
        };
    }


    //Recipe////////////////////////////////////////////////////////////////

    [RequiresSkill(typeof(TailoringSkill), 1)]
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Plushie Bear with Pink Bowtie Item")]
    [SupportedOSPlatform("windows7.0")]
    public partial class AIPPlushieBearBowtiePinkRecipe : Recipe
    {
        public AIPPlushieBearBowtiePinkRecipe()
        {
            this.Init(
                name: "AIPPlushieBearBowtiePink", //noloc
                displayName: Localizer.DoStr("Plushie Bear with Pink Bowtie"),

            ingredients: new List<IngredientElement>
            {
                new IngredientElement(typeof(IronOxideItem), 10, typeof(TailoringSkill), typeof(TailoringLavishResourcesTalent)),
                new IngredientElement(typeof(WhitePowderItem), 10, typeof(TailoringSkill), typeof(TailoringLavishResourcesTalent)),
                //new IngredientElement(typeof(BluePowderItem), 20, typeof(TailoringSkill), typeof(TailoringLavishResourcesTalent)),
                new IngredientElement(typeof(AIPPlushieBearItem), 1, true)
            },
            items: new List<CraftingElement>
            {
                new CraftingElement<AIPPlushieBearBowtiePinkItem>(),
            });

            // Perform pre/post initialization for user mods and initialize our recipe instance with the display name "Plushie Bear with Pink Bowtie"
            this.ModsPostInitialize();
            // Register our RecipeFamily instance with the crafting system so it can be crafted.
            CraftingComponent.AddTagProduct(tableType: typeof(TailoringTableObject), typeof(AIPPlushieBearRecipe), this);

        }
        /// <summary>Hook for mods to customize RecipeFamily before initialization. You can change recipes, xp, labor, time here.</summary>
        partial void ModsPreInitialize();
        /// <summary>Hook for mods to customize RecipeFamily after initialization, but before registration. You can change skill requirements here.</summary>
        partial void ModsPostInitialize();
    };
};
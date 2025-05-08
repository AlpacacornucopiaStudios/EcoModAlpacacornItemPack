namespace Eco.Mods.TechTree
{
    using Eco.Core.Items;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.Housing;
    using Eco.Gameplay.Housing.PropertyValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Occupancy;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.NewTooltip;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Items;
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Shared.Serialization;
    using System;
    using System.Collections.Generic;
    using System.Runtime.Versioning;
    //Object////////////////////////////////////////////////////////////////
    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [Tag("Usable")]
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Plushie AvocadoPair Item")]
    [SupportedOSPlatform("windows7.0")]
    public partial class AIPPlushieAvocadoPairObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(AIPPlushieAvocadoPairItem);
        public override LocString DisplayName => Localizer.DoStr("Plushie AvocadoPair");
        public override TableTextureMode TableTexture => TableTextureMode.Stone;
        protected override void Initialize()
        {
            this.ModsPreInitialize();
            this.GetComponent<HousingComponent>().HomeValue = AIPPlushieAvocadoPairItem.homeValue;
            this.ModsPostInitialize();
        }
        static AIPPlushieAvocadoPairObject()
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
            WorldObject.AddOccupancy<AIPPlushieAvocadoPairObject>(occupancies);
        }
        /// <summary>Hook for mods to customize WorldObject before initialization. You can change housing values here.</summary>
        partial void ModsPreInitialize();
        /// <summary>Hook for mods to customize WorldObject after initialization.</summary>
        partial void ModsPostInitialize();
    }
    //Item////////////////////////////////////////////////////////////////
    [Serialized]
    [SupportedOSPlatform("windows7.0")]
    [LocDisplayName("Plushie AvocadoPair")]
    [LocDescription("A pair of huggable Plushie Avocados stuffed with cotton. They are holding hands.")]
    [Ecopedia("Housing Objects", "Decoration", createAsSubPage: true)]
    [Tag("Housing")]
    [Tag("Plushie")]
    [Weight(100)]
    [Tag(nameof(SurfaceTags.CanBeOnRug))]
    public partial class AIPPlushieAvocadoPairItem : WorldObjectItem<AIPPlushieAvocadoPairObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));
        public override HomeFurnishingValue HomeValue => homeValue;
        public static readonly HomeFurnishingValue homeValue = new HomeFurnishingValue()
        {
            ObjectName = typeof(AIPPlushieAvocadoPairObject).UILink(),
            Category = HousingConfig.GetRoomCategory("Decoration"),
            BaseValue = 3,
            TypeForRoomLimit = Localizer.DoStr("Decoration"),
            DiminishingReturnMultiplier = 0.01f
        };
    }
    //Recipe////////////////////////////////////////////////////////////////
    [RequiresSkill(typeof(TailoringSkill), 4)]
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Plushie AvocadoPair Item")]
    [SupportedOSPlatform("windows7.0")]
    public partial class AIPPlushieAvocadoPairRecipe : Recipe
    {
        public AIPPlushieAvocadoPairRecipe()
        {
            this.Init(
            name: "AIPPlushieAvocadoPair", //noloc
            displayName: Localizer.DoStr("Plushie AvocadoPair"),
            ingredients: new List<IngredientElement>
            {
            new IngredientElement(typeof(AIPGlassEyeBeadItem), 0, true),
            new IngredientElement(typeof(AIPPlushieAvocadoHalf1Item), 1, true),
            new IngredientElement(typeof( AIPPlushieAvocadoHalf2Item), 1, true),
            },
            items: new List<CraftingElement> { new CraftingElement<AIPPlushieAvocadoPairItem>(), });
            this.ModsPreInitialize();
            this.ModsPostInitialize();
            CraftingComponent.AddTagProduct(tableType: typeof(TailoringTableObject), typeof(AIPPlushieAvocadoHalf1Recipe), this);
        }
        /// <summary>Hook for mods to customize RecipeFamily before initialization. You can change recipes, xp, labor, time here.</summary>
        partial void ModsPreInitialize();
        /// <summary>Hook for mods to customize RecipeFamily after initialization, but before registration. You can change skill requirements here.</summary>
        partial void ModsPostInitialize();
    };
};
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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Plushie Avocado Half 2 Item")]
    [SupportedOSPlatform("windows7.0")]
    public partial class AIPPlushieAvocadoHalf2Object : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(AIPPlushieAvocadoHalf2Item);
        public override LocString DisplayName => Localizer.DoStr("Plushie Avocado Half 2");
        public override TableTextureMode TableTexture => TableTextureMode.Stone;
        protected override void Initialize()
        {
            this.ModsPreInitialize();
            this.GetComponent<HousingComponent>().HomeValue = AIPPlushieAvocadoHalf2Item.homeValue;
            this.ModsPostInitialize();
        }
        static AIPPlushieAvocadoHalf2Object()
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
            WorldObject.AddOccupancy<AIPPlushieAvocadoHalf2Object>(occupancies);
        }
        /// <summary>Hook for mods to customize WorldObject before initialization. You can change housing values here.</summary>
        partial void ModsPreInitialize();
        /// <summary>Hook for mods to customize WorldObject after initialization.</summary>
        partial void ModsPostInitialize();
    }
    //Item////////////////////////////////////////////////////////////////
    [Serialized]
    [SupportedOSPlatform("windows7.0")]
    [LocDisplayName("Plushie Avocado Half 2")]
    [LocDescription("A huggable Plushie Avocado stuffed with cotton. It is looking for its friend Plushie Avocado Half 1.")]
    [Ecopedia("Housing Objects", "Decoration", createAsSubPage: true)]
    [Tag("Housing")]
    [Tag("Plushie")]
    [Weight(100)]
    [Tag(nameof(SurfaceTags.CanBeOnRug))]
    public partial class AIPPlushieAvocadoHalf2Item : WorldObjectItem<AIPPlushieAvocadoHalf2Object>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));
        public override HomeFurnishingValue HomeValue => homeValue;
        public static readonly HomeFurnishingValue homeValue = new HomeFurnishingValue()
        {
            ObjectName = typeof(AIPPlushieAvocadoHalf2Object).UILink(),
            Category = HousingConfig.GetRoomCategory("Decoration"),
            BaseValue = 2,
            TypeForRoomLimit = Localizer.DoStr("Decoration"),
            DiminishingReturnMultiplier = 0.01f
        };
    }
    //Recipe////////////////////////////////////////////////////////////////
    [RequiresSkill(typeof(TailoringSkill), 2)]
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Plushie Avocado Half 2 Item")]
    [SupportedOSPlatform("windows7.0")]
    public partial class AIPPlushieAvocadoHalf2Recipe : Recipe
    {
        public AIPPlushieAvocadoHalf2Recipe()
        {
            this.Init(
            name: "AIPPlushieAvocadoHalf2", //noloc
            displayName: Localizer.DoStr("Plushie Avocado Half 2"),
            ingredients: new List<IngredientElement>
            {
            new IngredientElement(typeof(CottonFabricItem), 4, typeof(TailoringSkill), typeof(TailoringLavishResourcesTalent)),
            new IngredientElement(typeof(CottonLintItem), 20, typeof(TailoringSkill), typeof(TailoringLavishResourcesTalent)),
            new IngredientElement(typeof(CottonThreadItem), 4, typeof(TailoringSkill), typeof(TailoringLavishResourcesTalent)),
            new IngredientElement(typeof(BluePowderItem), 20, typeof(TailoringSkill), typeof(TailoringLavishResourcesTalent)),
            new IngredientElement(typeof(AIPGlassEyeBeadItem), 2, true),
            },
            items: new List<CraftingElement> { new CraftingElement<AIPPlushieAvocadoHalf2Item>(), });
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
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
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Pet Rock 07 Item")]
    [SupportedOSPlatform("windows7.0")]
    public partial class AIPPetRock07Object : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(AIPPetRock07Item);
        public override LocString DisplayName => Localizer.DoStr("Pet Rock 07");
        public override TableTextureMode TableTexture => TableTextureMode.Stone;
        protected override void Initialize()
        {
            this.ModsPreInitialize();
            this.GetComponent<HousingComponent>().HomeValue = AIPPetRock07Item.homeValue;
            this.ModsPostInitialize();
        }
        static AIPPetRock07Object()
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
            WorldObject.AddOccupancy<AIPPetRock07Object>(occupancies);
        }
        /// <summary>Hook for mods to customize WorldObject before initialization. You can change housing values here.</summary>
        partial void ModsPreInitialize();
        /// <summary>Hook for mods to customize WorldObject after initialization.</summary>
        partial void ModsPostInitialize();
    }
    //Item////////////////////////////////////////////////////////////////
    [Serialized]
    [SupportedOSPlatform("windows7.0")]
    [LocDisplayName("Pet Rock 07")]
    [LocDescription("A Pet Rock carefully painted by a skilled artisan. It exudes personality.")]
    [Ecopedia("Housing Objects", "Decoration", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(100)]
    [Tag(nameof(SurfaceTags.CanBeOnRug))]
    public partial class AIPPetRock07Item : WorldObjectItem<AIPPetRock07Object>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));
        public override HomeFurnishingValue HomeValue => homeValue;
        public static readonly HomeFurnishingValue homeValue = new HomeFurnishingValue()
        {
            ObjectName = typeof(AIPPetRock07Object).UILink(),
            Category = HousingConfig.GetRoomCategory("Decoration"),
            BaseValue = 2,
            TypeForRoomLimit = Localizer.DoStr("Decoration"),
            DiminishingReturnMultiplier = 0.01f
        };
    }
    //Recipe////////////////////////////////////////////////////////////////
    [RequiresSkill(typeof(PaintingSkill), 2)]
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Pet Rock 07 Item")]
    [SupportedOSPlatform("windows7.0")]
    public partial class AIPPetRock07Recipe : Recipe
    {
        public AIPPetRock07Recipe()
        {
            this.Init(
            name: "AIPPetRock07", //noloc
            displayName: Localizer.DoStr("Pet Rock 07"),
            ingredients: new List<IngredientElement>
            {
            new IngredientElement(typeof(OilItem), 5, true),
            new IngredientElement(typeof(OilPaintItem), 11, true),
            new IngredientElement("Rock",30, true),
            },
            items: new List<CraftingElement> { new CraftingElement<AIPPetRock07Item>(), });
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
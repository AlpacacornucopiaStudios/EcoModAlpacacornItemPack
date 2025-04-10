namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Settlements;
    using Eco.Gameplay.Systems;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Core.Items;
    using Eco.World;
    using Eco.World.Blocks;
    using Eco.Gameplay.Pipes;
    using Eco.Core.Controller;
    using Eco.Gameplay.Items.Recipes;
    using System.Runtime.Versioning;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.Housing;
    using Eco.Gameplay.Occupancy;
    using Eco.Shared.Items;
    using Eco.Shared.Math;
    using Eco.Gameplay.Housing.PropertyValues;
    using Eco.Core.Serialization;

    //Object////////////////////////////////////////////////////////////////

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [Tag("Usable")]
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Jade Egg Item")]
    [SupportedOSPlatform("windows7.0")]

    public partial class AIPJadeEggObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(AIPJadeEggItem);
        public override LocString DisplayName => Localizer.DoStr("Jade Egg Item");
        public override TableTextureMode TableTexture => TableTextureMode.Stone;

        protected override void Initialize()
        {
            this.ModsPreInitialize();
            this.GetComponent<HousingComponent>().HomeValue = AIPJadeEggItem.homeValue;
            this.ModsPostInitialize();
        }

        static AIPJadeEggObject()
        {
            WorldObject.AddOccupancy<AIPJadeEggObject>(new List<BlockOccupancy>(){
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
    [LocDisplayName("Jade Egg")]
    [LocDescription("A wooden egg painted by a skilled artisan to look like a Jade Egg. Please use responsibly and contact a doctor if you have any unexpected side effects.")]
    [Ecopedia("Housing Objects", "Decoration", createAsSubPage: true)]
    [Tag("Housing")]
    [Weight(100)]
    [Tag(nameof(SurfaceTags.CanBeOnRug))]
    public partial class AIPJadeEggItem : WorldObjectItem<AIPJadeEggObject>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));
        public override HomeFurnishingValue HomeValue => homeValue;
        public static readonly HomeFurnishingValue homeValue = new HomeFurnishingValue()
        {
            ObjectName = typeof(AIPJadeEggObject).UILink(),
            Category = HousingConfig.GetRoomCategory("Decoration"),
            BaseValue = 1,
            TypeForRoomLimit = Localizer.DoStr("Decoration"),
            DiminishingReturnMultiplier = 0.01f
        };
    }

    /// <summary>
    /// <para>Server side recipe definition for "JadeEgg".</para>
    /// <para>More information about RecipeFamily objects can be found at https://docs.play.eco/api/server/eco.gameplay/Eco.Gameplay.Items.RecipeFamily.html</para>

    [SupportedOSPlatform("windows7.0")]
    [RequiresSkill(typeof(PaintingSkill), 1)]
    [ForceCreateView]
    [Ecopedia("Items", "Products", subPageName: "Jade Egg Item")]
    public partial class JadeEggRecipe : Recipe
    {
        public JadeEggRecipe()
        {
            this.Init(
                name: "JadeEgg",  //noloc
                displayName: Localizer.DoStr("Jade Egg"),

                // Defines the ingredients needed to craft this recipe. An ingredient items takes the following inputs
                // type of the item, the amount of the item, the skill required, and the talent used.
                ingredients: new List<IngredientElement>
                {
                new IngredientElement(typeof(AIPWoodenEggItem), 1, typeof(PaintingSkill), typeof(PaintingLavishResourcesTalent)),
                new IngredientElement(typeof(OilPaintItem), 1, typeof(PaintingSkill), typeof(PaintingLavishResourcesTalent)),
                new IngredientElement(typeof(CopperHydroxideItem), 5, typeof(PaintingSkill), typeof(PaintingLavishResourcesTalent)),
                },

                // Define our recipe output items.
                // For every output item there needs to be one CraftingElement entry with the type of the final item and the amount
                // to create.
                items: new List<CraftingElement>
                {
                    new CraftingElement<AIPJadeEggItem>()
                });
            // Perform post initialization steps for user mods and initialize our recipe instance as a tag product with the crafting system
            this.ModsPostInitialize();
            CraftingComponent.AddTagProduct(typeof(PaintMixerObject), typeof(AIPPaintedEggRecipe), this);
        }


        /// <summary>Hook for mods to customize RecipeFamily after initialization, but before registration. You can change skill requirements here.</summary>
        partial void ModsPostInitialize();
    }
}
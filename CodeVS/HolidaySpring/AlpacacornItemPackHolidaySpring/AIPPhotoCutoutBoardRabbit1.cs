using Eco.Gameplay.Items.Recipes;
using Eco.Mods.TechTree;

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
    [Tag("Usable")]
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Photo Cutout Board Rabbit 1 Item")]
    [SupportedOSPlatform("windows7.0")]

    public partial class AIPPhotoCutoutBoardRabbit1Object : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(AIPPhotoCutoutBoardRabbit1Item);
        public override LocString DisplayName => Localizer.DoStr("Photo Cutout Board Rabbit 1");
        public override TableTextureMode TableTexture => TableTextureMode.Stone;

        protected override void Initialize()
        {
            this.ModsPreInitialize();
            this.GetComponent<HousingComponent>().HomeValue = AIPPhotoCutoutBoardRabbit1Item.homeValue;
            this.ModsPostInitialize();
        }

        static AIPPhotoCutoutBoardRabbit1Object()
        {
            var occupancies = new List<BlockOccupancy>();
            //Vector3(x, y, z): x is left/right, y is up/down, z is forward/backward
            for (int x = -1; x <= 1; x++)
                for (int y = 0; y <= 2; y++)
                    for (int z = 0; z <= 0; z++)
                        occupancies.Add(new BlockOccupancy(new Vector3i(x, y, z)));
            WorldObject.AddOccupancy<AIPPhotoCutoutBoardRabbit1Object>(occupancies);



        }

        /// <summary>Hook for mods to customize WorldObject before initialization. You can change housing values here.</summary>
        partial void ModsPreInitialize();
        /// <summary>Hook for mods to customize WorldObject after initialization.</summary>
        partial void ModsPostInitialize();
    }

    //Item////////////////////////////////////////////////////////////////

    [Serialized]
    [SupportedOSPlatform("windows7.0")]
    [LocDisplayName("Photo Cutout Board Rabbit 1")]
    [LocDescription("A Photo Cutout Board, sometimes called a face-in-hole, face in the hole board, or photo stand-in, is notable as native English speakers really have no idea what to call them but merely say something like \"go to that board with the hole cut out for your face\". Makes you wonder.")]
    [Ecopedia("Housing Objects", "Decoration", createAsSubPage: true)]
    [Tag("Housing")]
    [Tag("Plushie")]
    [Weight(100)]
    [Tag(nameof(SurfaceTags.CanBeOnRug))]
    public partial class AIPPhotoCutoutBoardRabbit1Item : WorldObjectItem<AIPPhotoCutoutBoardRabbit1Object>
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));
        public override HomeFurnishingValue HomeValue => homeValue;
        public static readonly HomeFurnishingValue homeValue = new HomeFurnishingValue()
        {
            ObjectName = typeof(AIPPhotoCutoutBoardRabbit1Object).UILink(),
            Category = HousingConfig.GetRoomCategory("Decoration"),
            BaseValue = 3,
            TypeForRoomLimit = Localizer.DoStr("Decoration"),
            DiminishingReturnMultiplier = 0.01f
        };
    }


    //Recipe////////////////////////////////////////////////////////////////

    [RequiresSkill(typeof(PaintingSkill), 3)]
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Photo Cutout Board Rabbit 1 Item")]
    [SupportedOSPlatform("windows7.0")]
    public partial class AIPPhotoCutoutBoardRabbit1Recipe : RecipeFamily
    {
        public AIPPhotoCutoutBoardRabbit1Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "AIPPhotoCutoutBoardRabbit1", //noloc
                displayName: Localizer.DoStr("Photo Cutout Board Rabbit 1"),

            ingredients: new List<IngredientElement>
            {
                new IngredientElement(typeof(BoardItem), 10, typeof(PaintingSkill), typeof(PaintingLavishResourcesTalent)),
                new IngredientElement(typeof(OilPaintItem), 5, typeof(PaintingSkill), typeof(PaintingLavishResourcesTalent)),
                new IngredientElement(typeof(BluePowderItem), 5, typeof(PaintingSkill), typeof(PaintingLavishResourcesTalent)),
                new IngredientElement(typeof(YellowPowderItem), 5, typeof(PaintingSkill), typeof(PaintingLavishResourcesTalent)),
                new IngredientElement(typeof(WhitePowderItem), 5, typeof(PaintingSkill), typeof(PaintingLavishResourcesTalent)),
                new IngredientElement(typeof(CopperHydroxideItem), 5, typeof(PaintingSkill), typeof(PaintingLavishResourcesTalent)),
                new IngredientElement(typeof(IronOxideItem), 5, typeof(PaintingSkill), typeof(PaintingLavishResourcesTalent)),
                new IngredientElement(typeof(CharcoalPowderItem), 5, typeof(PaintingSkill), typeof(PaintingLavishResourcesTalent)),
            },
            items: new List<CraftingElement>
            {
                new CraftingElement<AIPPhotoCutoutBoardRabbit1Item>(),
            });

            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 2;
            // Defines the amount of labor required and the required skill to add labor
            this.LaborInCalories = CreateLaborInCaloriesValue(100, typeof(PaintingSkill));
            // Defines our crafting time for the recipe
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(PaintingSkill), start: 2f, skillType: typeof(PaintingSkill), typeof(PaintingFocusedSpeedTalent), typeof(PaintingParallelSpeedTalent));


            // Perform pre/post initialization for user mods and initialize our recipe instance with the display name "Photo Cutout Board Rabbit 1"
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Photo Cutout Board Rabbit 1"), recipeType: typeof(AIPPhotoCutoutBoardRabbit1Recipe));
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
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


    //Recipe////////////////////////////////////////////////////////////////
    [Serialized]
    [RequiresSkill(typeof(LoggingSkill), 1)]
    [Ecopedia("Housing Objects", "Decoration", subPageName: "Wooden Egg Item")]
    [SupportedOSPlatform("windows7.0")]
    public partial class AIPWoodenEggRecipe2 : RecipeFamily
    {
        public AIPWoodenEggRecipe2()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "AIPWoodenEgg2", //noloc
                displayName: Localizer.DoStr("Wooden Egg"),

            ingredients: new List<IngredientElement>
            {
                new IngredientElement(typeof(BoardItem), 1, typeof(LoggingSkill))
            },
            items: new List<CraftingElement>
            {
                new CraftingElement<AIPWoodenEggItem>(),
            });

            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 0.6f;
            // Defines the amount of labor required and the required skill to add labor
            this.LaborInCalories = CreateLaborInCaloriesValue(25, typeof(LoggingSkill));
            // Defines our crafting time for the recipe
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(LoggingSkill), start: 0.4f, skillType: typeof(LoggingSkill));


            // Perform pre/post initialization for user mods and initialize our recipe instance with the display name "Wooden Egg"
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Wooden Egg"), recipeType: typeof(AIPWoodenEggRecipe2));
            this.ModsPostInitialize();
            // Register our RecipeFamily instance with the crafting system so it can be crafted.
            CraftingComponent.AddRecipe(tableType: typeof(CarpentryTableObject), recipeFamily: this);

        }
        /// <summary>Hook for mods to customize RecipeFamily before initialization. You can change recipes, xp, labor, time here.</summary>
        partial void ModsPreInitialize();
        /// <summary>Hook for mods to customize RecipeFamily after initialization, but before registration. You can change skill requirements here.</summary>
        partial void ModsPostInitialize();
    };
};
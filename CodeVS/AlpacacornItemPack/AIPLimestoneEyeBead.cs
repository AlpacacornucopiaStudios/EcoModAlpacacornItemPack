namespace Eco.Mods.TechTree
{
    using Eco.Core.Items;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using System.Collections.Generic;
    using System.Runtime.Versioning;


    //Item////////////////////////////////////////////////////////////////

    [Serialized]
    [SupportedOSPlatform("windows7.0")]
    [LocDisplayName("Limestone Eye Bead")]
    [LocDescription("A piece of limestone carefully carved by your local mason to serve as the base for a colorful eyeball perfect for a huggable plushies.")]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    [Weight(50)]
    public partial class AIPLimestoneEyeBeadItem : Item
    {
    }
    //Recipe////////////////////////////////////////////////////////////////

    [RequiresSkill(typeof(MasonrySkill), 1)]
    [Ecopedia("Items", "Products", subPageName: "Limestone Eye Bead Item")]
    [SupportedOSPlatform("windows7.0")]
    public partial class AIPLimestoneEyeBeadRecipe : RecipeFamily
    {
        public AIPLimestoneEyeBeadRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "AIPLimestoneEyeBead", //noloc
                displayName: Localizer.DoStr("Limestone Eye Bead"),

            ingredients: new List<IngredientElement>
            {
                new IngredientElement(typeof(LimestoneItem), 4, typeof(MasonrySkill), typeof(MasonryLavishResourcesTalent)),
            },
            items: new List<CraftingElement>
            {
                new CraftingElement<AIPLimestoneEyeBeadItem>(2),
            });

            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 1;
            // Defines the amount of labor required and the required skill to add labor
            this.LaborInCalories = CreateLaborInCaloriesValue(50, typeof(MasonrySkill));
            // Defines our crafting time for the recipe
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(MasonrySkill), start: 1.0f, skillType: typeof(MasonrySkill), typeof(MasonryFocusedSpeedTalent), typeof(MasonryParallelSpeedTalent));


            // Perform pre/post initialization for user mods and initialize our recipe instance with the display name "Limestone Eye Bead"
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Limestone Eye Bead"), recipeType: typeof(AIPLimestoneEyeBeadRecipe));
            this.ModsPostInitialize();
            // Register our RecipeFamily instance with the crafting system so it can be crafted.
            CraftingComponent.AddRecipe(tableType: typeof(MasonryTableObject), recipeFamily: this);

        }
        /// <summary>Hook for mods to customize RecipeFamily before initialization. You can change recipes, xp, labor, time here.</summary>
        partial void ModsPreInitialize();
        /// <summary>Hook for mods to customize RecipeFamily after initialization, but before registration. You can change skill requirements here.</summary>
        partial void ModsPostInitialize();
    };
};
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
    [LocDisplayName("Glass Eye Bead")]
    [LocDescription("A black glass bead carefully polished by your local glassmaker to look like an eyeball fit for only the most huggable of plushies.")]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    [Weight(50)]
    public partial class AIPGlassEyeBeadItem : Item
    {
    }
    //Recipe////////////////////////////////////////////////////////////////

    [RequiresSkill(typeof(GlassworkingSkill), 1)]
    [Ecopedia("Items", "Products", subPageName: "Glass Eye Bead Item")]
    [SupportedOSPlatform("windows7.0")]
    public partial class AIPGlassEyeBeadRecipe : RecipeFamily
    {
        public AIPGlassEyeBeadRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "AIPGlassEyeBead", //noloc
                displayName: Localizer.DoStr("Glass Eye Bead"),

            ingredients: new List<IngredientElement>
            {
                new IngredientElement(typeof(GlassItem), 1, typeof(GlassworkingSkill), typeof(GlassworkingLavishResourcesTalent)),
                new IngredientElement(typeof(BlackPowderItem), 5, typeof(GlassworkingSkill), typeof(GlassworkingLavishResourcesTalent)),
            },
            items: new List<CraftingElement>
            {
                new CraftingElement<AIPGlassEyeBeadItem>(4),
            });

            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 1;
            // Defines the amount of labor required and the required skill to add labor
            this.LaborInCalories = CreateLaborInCaloriesValue(50, typeof(GlassworkingSkill));
            // Defines our crafting time for the recipe
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(GlassworkingSkill), start: 1.0f, skillType: typeof(GlassworkingSkill), typeof(GlassworkingFocusedSpeedTalent), typeof(GlassworkingParallelSpeedTalent));


            // Perform pre/post initialization for user mods and initialize our recipe instance with the display name "Glass Eye Bead"
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Glass Eye Bead"), recipeType: typeof(AIPGlassEyeBeadRecipe));
            this.ModsPostInitialize();
            // Register our RecipeFamily instance with the crafting system so it can be crafted.
            CraftingComponent.AddRecipe(tableType: typeof(GlassworksObject), recipeFamily: this);

        }
        /// <summary>Hook for mods to customize RecipeFamily before initialization. You can change recipes, xp, labor, time here.</summary>
        partial void ModsPreInitialize();
        /// <summary>Hook for mods to customize RecipeFamily after initialization, but before registration. You can change skill requirements here.</summary>
        partial void ModsPostInitialize();
    };
};
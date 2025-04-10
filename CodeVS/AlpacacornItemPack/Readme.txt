Last updated for Eco 0.11.1.10 beta release-847

To install, just place the mod folder AlpacacornItemPack in your server's Mods -> UserCode folder and restart your server.

Feel free to update the .cs files for balancing purposes (let me know if you think something is way off and I can adjust it, as well).
Let me know on mod.io if you have any further plushie requests or balancing suggestions!

Almost all models are original & made by me in Blender with textures created using Adobe Substance Painter. 
No AI was used in the creation of this item pack.

Attributions:
	Mesh: "3D Origami crane" (https://skfb.ly/m5lih20ba) by JuanG3D is licensed under Creative Commons Attribution (http://creativecommons.org/licenses/by/4.0/). Crane texture has been changed.

Update 2.0 2025, April 8
	Significantly rebalanced all plushies:
		Added new Glass Eye Bead and Limestone Eye Bead item recipes crafted by Glassworkers & Masons respectively
		Plushies now use fewer tailor parts to give other specialties a larger share of the profit
		Made colored powder totals a bit more consistent, compensating more expensive recipes with a little bit more decoration bonus
		Adjusted some calorie requirements, crafting times, and xp bonuses
	Internally renamed all .cs files to include the prefix AIP for Alpacacorn Item Pack 
		(I meant to do the same for each class to prevent the wild possibility of an item conflict 
		with a similarly named item in another modpackbut that would delete the items from any server that has the pack already installed, so
		I'll just hope there are no other mods that will conflict making identical plushies)
	Adjusted minimum required Tailor Level and Painter level to spread out recipes a bit more evenly.
	Modified Traffic Cone Material to be less shiny
	Modified Paper Crane Material to be less shiny
	Added new original plushies:
		Lawn Flamingo
		Plushie Avocado Half 1, 2, and Avocado Pair
		Plushie Bear, Plushie Bear Bowtie Blue and Pink, and Plushie Bear Hairbow Blue and Pink
		Plushie Polar Bear and Panda Bear variants for the same above Bear recipes
		Plushie Polar Bear Recently Fed variant craftable by the butcher using a deer carcass and Plushie Polar Bear
		Plushie Chick
		Plushie Cow
		Plushie Dragon
		Plushie Giraffe
		Plushie Hippo
		Plushie Landshark
		Plushie Monkey
		Plushie Otter
		Plushie Platypus
		Plushie Rainfrog
		Plushie Slug
		Plushie Snail
		Plushie Strawberry
	Added wiggle animation to all Plushie Bears, Polar Bears, and Panda Bears
	Added image for Plushie tag
	Combined Pet Rocks into one recipe category
Update 1.1.3 2025, March 20
	Added Paper Crane (see attribution for mesh)
	Added Paper Mache Pear
	Added Plushie Alpacacorn
	Added Traffic Cone
	Added Traffic Barricade A-Frame
	Corrected Counterfeit Bee Crown material to lack the seal of authenticity
	Correct comments in all code about xyz vector 3 directions

Update 1.1.2 2025, March 19
	Fix materials to use GPU rendering
	Fix materials to use double sided global illumination
	Added Large Plushie Squirrel (3x3)

Update 1.1.1 2025, March 18
	Fixed shader on Pet Rocks to curved, so they no longer float on the horizon.

Update 1.1.0 2025, March 18
	Added PetRock01 through PetRock07 all using the same texture set, craftable by Painters with rocks, oil, and oil paint
	Adjusted all plushies to use more colored powders

Update 1.0.13 2025, March 15
	Misc Updates:
		Fix typo in Queen Bee recipes
		Rename Bee Queens to Queen Bees
		Added the word Counterfeit to the Counterfeit Queen Bee recipe to make it more obvious

Rebalancing Update:
	Reduced amount of cotton fabric needed from 20 to 10 for most plushies
	Spread out the tailoring level at which tailors can craft each plushie from 1 to 6:
		Level 1: Alligator, Banana, LeatherAlligator
		Level 2: Habanero, Squid, Squirrel, SquirrelBaby
		Level 3: Bee, Elephant, Hedgehog
		Level 4: Counterfeit Bee Queen, Pink Elephant
		Level 5: Mushroom 1 and 2, Precious Plushie Squid, and Precious Plushie Squid for Goths
		Level 6: Bee Queen
	Reduced the base plushie decoration bonus back down to 2
	Increased the diminishing returns back to 99% for all plushies
	Added glass as a required ingredient for both precious squids
	The following recipes now require an advanced tailoring table:
		Plushie Bee Queen
		Plushie Pink Elephant
		Precious Plushie Squid for Goths
	Reduced yellow powder cost from counterfeit bee queen from 60 to 20 as originally intended


Update 1.0.12 2025, March 14
	Rebalanced recipes, reducing the amount of cotton lint needed from 40 to 20 for all plushies except the Plushie Squirrel Baby
	Increased the amount of experience gain from recipes from 2x to 3x to be more similar to the Large Rug recipe.
	Increased base value of plushies to 4 to be more similar to the Large Rug recipe.
	Decreased the DiminishingReturnMultiplier of all plushies from 99% to 50% to be more similar to the Large Rug recipe.
	Increased the base value of the Plushie Queen Bee (authentic) to 5 to stand out from the other recipes.
	Modified all meshes to change from the Standard Specular or Metallic shader to the Curved/Standard shader.

Update 1.0.11 2025, March 13
	Added Plushie Mushroom 1
	Added Plushie Mushroom 2

Update 1.0.10 2025, March 13
	Removed Stack Size limitation from all items

Update 1.0.9 2025, March 13
	Fixed float error in PlushieSquirrelBaby.cs, changing value from .4 to 0.4f;

Update 1.0.8 2025, March 12
	Adjusted z axis of meshes slightly to reduce clipping into the ground (most notably for the Plushie Squirrel Baby)

Update 1.0.7 2025, March 12
	Adjusted the scale of all meshes (adjusted in Blender and reimported into Blender using adjusted settings) so objects weren't scaled by 100+ in Unity.
	Added Plushie Squirrel Baby upon being inspired by an accidentally small squirrel model through the aforementioned rescaling process.
	Bug: Plushie Bee Queen Counterfeit object may need to be picked up and replaced if the model shows up as an untextured cube.

Update 1.0.6 2025, March 11
	Added Plushie Squirrel

Update 1.0.5 2025, March 10
	Updated Plushie Pink Elephant recipe ingredients to include more colored powders.

Update 1.0.4 2025, March 10
	Added Plushie Elephant
	Added Plushie Pink Elephant

Update 1.0.3 2025, March 9
	Added Plushie Hedgehog
	Renamed Mesh_Crown to Mesh_PlushieCrown
	Renamed Mesh_Banana to Mesh_PlushieBanana

Update 1.0.2 - 2025, March 7
	Rotated Plushie Banana Mesh 180 degrees to face same direction as the other plushies by default
	Added Plushie Squid
	Added Precious Plushie Squid (holds a green gem).
	Added Precious Plushie Squid For Goth (is a modified model resembling a Vampire Squid holding a red gem, accomodating various tastes in fashion sense).

Update 1.0.1 - 2025, March 6
	Improved Plushie Bee model to make it smooter, still keeping it under 1k vertices.
	Added Plushie Queen Bee (uses gold bars).
	Added Counterfeit Plushie Queen Bee (uses yellow powder and gives a smaller decoration bonus than the gold bar variant).

Update 1.0.0 - 2025, March 5
	Added Plushie Habanero
	Added Plushie Alligator
	Added Plushie Leather Alligator
	Added Plushie Bee
	Added Plushie Banana
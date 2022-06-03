__________________________________________________________________________________________

Package "Procedural Generation Grid"
Version 1.1.2.1 (Beta)

Made by FImpossible Creations - Filip Moeglich
https://www.FilipMoeglich.pl
FImpossibleGames@Gmail.com or Filip.Moeglich@Gmail.com

__________________________________________________________________________________________

Youtube: https://www.youtube.com/channel/UCDvDWSr6MAu1Qy9vX4w8jkw
Facebook: https://www.facebook.com/FImpossibleGames
Twitter (@FimpossibleC): https://twitter.com/FImpossibleC
Discord: https://discord.gg/rdD2Yu

__________________________________________________________________________________________

Package Contests:

Procedural Generation Grid - Demo Scenes.unitypackage
Package contains example scenes, scripts and assets showing how Procedural Generation Grid can be used.

Procedural Generation Grid - Assembly Definitions.unitypackage (Supported since Unity 2017)
Assembly Definition files to speed up compilation time of your project (Fimpossible Directories will have no influence on compilation time of whole project)

__________________________________________________________________________________________

Known issues: 

Some Unity versions, around 2019.4.1 have bug which is causing FieldSetup presets to break
when trying to rename it's file or duplicate it.


If you use Unity 2018.2 or lower DEMO prefabs might be broken.
Unfortunatelly Unity does not support back compatibility for prefabs on this versions.
Since examples database is so big (258 prefabs) I can't afford another two weeks of making example assets,
so please consider using Unity 2019.4+ for demo scenes as learning version.


When Unity opens with some glitched DPI setup then Grid Painter "Paint" button and some other
GUI is not visible, I wasn't able to fix it yet. 
If you have this issue, you an fix it by clicking right mouse button 
on running Unity App Icon -> Properties -> Compatibility -> Button on the bottom about changing DPI
-> toggle on the bottom about scaling dpi -> then select System -> Restart Unity Editor -> Now all should work!
(above solution is windows only)
__________________________________________________________________________________________

Description:

- Procedural Generation Grid is a system which uses grids to define fields and paths for procedural generation algorithms.

- System is designed to be flexible and able to generate very various and complex objects/places.

- It can be used to generate procedurally whole game levels at runtime or for editor use to quickly paint levels for speed up level design process.
 
- System is using something like visual scripting, so you don’t need to know how to code (but you can still take benefits out of it) you just need to plan logics for your needs.

- Procedural Generation Grid is right now in Beta Stage, so many features may change, new features will come and some small elements are not fully finished but developmental stage is completed enough to be functional.
 
- Package is providing additional packages for randomly placing smaller objects in physical space using Unity’s collision system (Object’s Stamper and Pipe Generator)

__________________________________________________________________________________________


Beta Version 1.1.2.1:
- Fixed Sub Spawner Node Display
- Fixed handling lists in Rule Nodes in unity 2020+
- Added nav mesh bake basing on static meshes option for demo component (only editor - place in code for custom implementation for builds)
- Fixed Scaling option for Stamp Emitter (going with ScaleAxis.x to negative value triggers uniform scaling now)
- 'Colorize' toggle should disable displaying cells numbers on scene gizmo - thats because unity 2020 scene text labels are bugged

Beta Version 1.1.2:
- Fixing some GUI errors which was appearing when viewing "Spawn Rules" for a first time
Some of the GUI errors started to appear in newest Unity Versions and as for now their sources are unknown but maybe next Unity versions will fix it.
- Added some additional utility GUI buttons for FieldSetup fields
- Added possibility to read cell instructions placed on grid
- New Node: "Get Restricted Rotation" to read rotation out of cell command placed in cell
- Important Change for "Check Neighbour" rule, "D" toggle which is enabling/disabling using restricted direction setted by cell command, in old versions of PGG it was used for placing doors, if you're using "Check Cell Neighbour" rule for placing doors, you need to toggle the "D" field (tooltip will be displayed if you enter on the field)


Beta Version 1.1.1:
- Beginner mode switch button for Field Designer Window, with just most important views to prepare FieldSetups.
- Draft workflow replaced with "New" button, it creates FieldSetup file in some directory, if you feel you can move FieldSetup to some custom directory you can use "Move" button, whe "Generate New" button functionality replaced by "+" button.

- Some new unity inspector fixes
- Some smaller fixes in build plan algorithm


Beta Version 1.1.0:

- New work-in-progress components with implemented new way of generating objects, making possible
instantiating just changed cells, async computing rules logics and instantiating game objects in coroutines.
This component are not yet supporting all features of the old components, but in some updates it will be fulfilled.
- New 3 example scenes: Playmode grid painting, Infinite run, 2.5D/3D platformer level
- Cell selector have option to display choosed cells in different perspectives, which can be helpful for setting rules based on vertical logics or 2D
- New Component "Pose Stamp" for random but partially defined random position/rotation setup
- Some GUI and Gizmos updates/fixes
- New Node: 'Helper Pivot Correction' helpful if you're using walls with pivot in corner, with this node you can adjust it to the center to make furniture placement easier!
- Node Update: Doorway node now can be debugged for correcting measure origins
- Now "Empty" prefab spawns will be visible in scene view gizmos as green sphere
- "Empty" and some debugging gizmos now can be displayed using Grid Painter when enabling "Draw Grid with Details" Debug draw mode (grid needs to be re-generated to display gizmos)
- You can choose to inherit or no rotations of previous cell spawns under spawners advanced settings (toggle on the right of "Cell Check Mode")
- Now new added rules will be hidden inside file, so unfolding preset files contents will not display them, if needed then it can be exposed with inspector window of Modificators Pack
- If you want to keep project more clean for searching field modificators, you can use Modificators Pack buttons for hiding/exposing their visibility, or using new button visible on header of Field Modificator ("A/B" button, package needs to be inside FieldSetup / ModPack in order to see this button)
- Clicking on prefab thumbnails in the FieldModificator window will switch to spawner which is spawning the prefab
- On the Unity 2021+ the injection list in build plan wasn't working - fixed

Variables Features (After 1.0.5):
- Spawner inspector window is drawing global rules with possibility to not use global rules on selected spawner
- ModificatorsPack can have added rules which can be triggered on all spawners inside pack / ignored for selective spawners
- ModificatorsPack can contain variables which can be used by nodes in the Package
- FieldSetup have option to display FieldVariables by type
- When hitting variable GUI dot there are new options like "Add Variable"
- Grid Painter component now have many options for handling variables, in next versions it will be available for every generator component
- Some smaller fixes

Beta Version 1.0.5:

- New Feature: Cell Selector Window which allows to freely select multiple cells to be executed on cells around, implemented by few other nodes (listed below)

- New Node: 'Operations/Stack Spawner' which works like Multi Objects Stamper without need of creating preset files etc. (needs some more testing)
- New Node: 'Operations/Pipe Spawner' which requires PipeGenerator preset to spawn objects with PipeGenerator algorithm
- New Node: 'Quick Solutions/Call Rules Logics Of' it will call nodes of other FieldModificator's spawner
- New Node: 'Transforming/Utilities/Prefab Offset' and 'Rotate On Repeat' first for correcting origin of spawned objects using default position inside prefab file and second for rotating object when using "Repeat" of Spawner

- Node Update: Floor Placer has possibility to advanced customize auto-placement
- Node Update: Most of the nodes under '/Cells' gets possibility to influence / check multiple cells with cell selector (new button opens selector window)
- Node Update: Check Cell Neightbours can have access to further cells than 9 around using cells selector (button opens selector window)

- Field Designer Window Update: Buttons for export Variant/Copy of FieldModificator are removed, this events can be triggered by hitting right mouse button on '[0]', '[1]' buttons on the left
- Field Designer Window Update: '+' button now adds automatically new FieldModificator without need to hitting 'IN' button after that, empty FieldModificator field still can be added with '+ []' button

- ModificatorsPack now have option to reset/set custom seed for generating (can be setted through inspector window)
- ModificatorsPack now have option to mass add additional tags to all spawners during generating time

- Grid Painter Update: If there is need to display more than 8 Field Commands they're bookmarked
- Grid Painter Update: More compact inspector window with "More Options" foldout
- Grid Painter Update: 2D paint toggle to draw cells in XY axis
- Grid Painter Update: Possibility to change FieldSetup variables for generation time (under More Options foldout)
- Grid Painter Update: Possibility to ignore selective FieldModificators out of FieldSetup (under More Options foldout)

- Shape Designer Window now can be used, for now only Facility Generator/Simple Dungeon Generator is supporting custom shapes
Shape designer window will have much more features in future updates. (like manual adding cell data / using rules to generate shapes and restrictions with relation to other grids in world)

- Create/GameObject Unity Editor menu now have new bookmark "/Generators" for quick creating game object with PGG components inside
- Some upgrades and fixes in PGG core


Beta Version 1.0.4:
- New Node + New Component: Align to Ground for placing object on ground accordingly to ground angle, very useful for placing on terrain
- New Node + New Component: Flatten Terrain for aligning Unity Terrain ground level under spawned objects
- New Node: Acquire Spawn: Mostly used for allowing /Modelling nodes to be executed without need of being added to spawner with spawning object, can be used after all spawns with "Empty" spawn
- New Node: Replace with Random Prefab: Which can be used instead of spawner's prefabs list
- New Node: Set Material Property: To change some material property on spawned object
- New Node: If World Position: Working in the same way like "Grid Position" but using world space position instead of local grid space positioning
- Node Update: Logic If Block: Added possibility to use "AND" "OR" conditions and breaking or triggering events mode
- Added buttons in spawner prefab list to automatically generate prefabs out of Model files and automatically adding colliders if not present in prefab (if you see button and press it when prefab already have some collider then no collider will be added, some prefab info just need to be updated)
- Improved some algorithms for Grid Painter component


Beta Version 1.0.3:
- New feature for copying/pasting FiledModificators by using right mouse button on "[0]" "[1]" buttons in FieldDesigner window in FieldModificators lists
- Changing Cell Size in Field Designer window should result in updated preview on the scene view
- Added "Generate" button on scene view gui for Grid Painter
- Added feature for working with "Draft" Field Setup in FieldDesigner window and exporting to file when ready
- Added option to "Copy All Properties" and "Paste All Properties" when hitting right mouse button on any rule header title (be careful, it needs to be tested to make sure everything works right)
- Added searchbox window for adding rules when using Unity 2019.4+
- Added "IF Logic Block" node to call events if some conditions are met inside node
- Added "Scale Range" node for more controll over objects random scaling
- Fixed "Get / Move-Scale-Rotate" nodes GetCoordinates logics
- Fixed "Fill Rectangle" feature not filling completely area for MiniCityGenerator
- Fixed some "GetCoordinates" logics using "Cell Size" unit size and added "Run on repetition" parameter (check tooltip in unity)
- Found and fixed some issues in AnalyzeCell node


Beta Version 1.0.2:
- New Generators Feature: Now generated rooms/areas can be outlined with any field setup using "Outline Fill" option in the "Additional Options" tab
- New Generators Feature: Now generated area can be covered with any field setup using "Fill Rectangle" option in the "Additional Options" tab
Above features are NOT available for the BuildingPlanGenerator (it's old component which isn't using newest framework of PGG)

- New Node: "Transforming/Noise/" - Perlin Noise Offset and - Rotation/Scale Perlin Noise Offset
- New Node: "Modelling/" - Set Random Mesh Material

- Some node moved to the different bookmark (node: IfRotated moved to /Placement)
- Some nodes moved to Transforming/Legacy bookmark (nodes: WorldOffset, DirectOffset, Rotate, Scale)
Their features can be replaced by single node "Move-Rotate-Scale" or "Get Position-Rotation-Scale"

- Changed default core asset rules for ClearWall modificator
- Some nodes will take a bit less space in the inpsector window

Fixes:
- Fixed BuildingPlanGenerator corridor doorway command to choose right command from the field setup
- Fixed some issue with generating doorways with BuildingPlanGenerator


Beta Version 1.0.1:
- DPI Fix for painting with Field Designer Window
- In Door Placer node added option to measure remove distance with object's bounds
- More Options under "Window->Fimpossible Creations->Level Design" and added presets scheme to the QuickStart.pdf file
- New Node "Duplicate Spawns"
- Added PGG Starter Window displayed only once when importing package, then it can be displayed by going to "Window->Fimpossible Creations->Level Design->Display PGG Startup Window"
- Now build plan shows arrows to change oder of FieldSetups, order will have influence on FacilityGenerator algorithm if disabling "Shuffle Plan Order"
- Updated and modified some "Checker" framework methods for Generators like FacilityGenerator/SimpleDungeonGenerator
! Previously generated levels with this generators can look different now even when using the same seed!


Beta Version 1.0.0.1:
- DPI fix for Grid Painter Component
- Added extensions to Vector2Int to support old Unity Versions like 2017.4
- Added links to PGG tutorials and manual in Window/Fimpossible...
- Fixed SetMaterial rule to interate through all renderers
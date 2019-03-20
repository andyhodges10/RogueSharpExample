using RLNET;

namespace RogueSharpExample.Core
{
    class Colors
    {
        public static RLColor Background = RLColor.Black;
        public static RLColor BackgroundFov1 = new RLColor(36, 40, 45);//Swatch.SecondaryDarker; // (51, 56, 64)
        public static RLColor BackgroundFov2 = new RLColor(9, 11, 14);//Swatch.SecondaryDarkest; // (31, 38, 47)
        public static RLColor LowLevelFloor = new RLColor(17, 30, 11); //Swatch.DbVegetation; // (52, 101, 36)
        public static RLColor Floor = new RLColor(22, 19, 14); // Swatch.AlternateDarkest; // (71, 62, 45)
        public static RLColor IceFloor = new RLColor(15, 21, 26); // (49, 69, 84)
        public static RLColor CaveFloor = new RLColor(16, 15, 14);
        public static RLColor HellFloor = new RLColor(29, 11, 10);
        public static RLColor FloorBackgroundFov = Swatch.DbDark; // (20, 12, 28)
        public static RLColor LowLevelFloorFov = new RLColor(142, 221, 57); //Swatch.DbGrass; // (109, 170, 44)
        public static RLColor FloorFov = new RLColor(167, 157, 139); //Swatch.Alternate; // (129, 121, 107)
        public static RLColor IceFloorFov = new RLColor(85, 119, 168); // (66, 92, 129)
        public static RLColor CaveFloorFov = new RLColor(85, 80, 75);
        public static RLColor HellFloorFov = new RLColor(128, 55, 52);

        public static RLColor LowLevelWall = new RLColor(24, 26, 28); // (80, 85, 92);
        public static RLColor Wall = new RLColor(23, 26, 29); //Swatch.Secondary; // (72, 77, 85)
        public static RLColor IceWall = new RLColor(13, 20, 25);
        public static RLColor CaveWall = new RLColor(17, 15, 14);
        public static RLColor HellWall = new RLColor(30, 11, 10);
        public static RLColor WallBackgroundFov = Swatch.SecondaryDarker;
        public static RLColor LowLevelWallFov = Swatch.DbStone; //(117, 113, 97)
        public static RLColor WallFov = new RLColor(121, 127, 137); //Swatch.SecondaryLighter; // (93, 97, 105)
        public static RLColor IceWallFov = new RLColor(88, 104, 124); // (68, 80, 95)
        public static RLColor CaveWallFov = new RLColor(82, 76, 66); // (97, 67, 40)
        public static RLColor HellWallFov = new RLColor(130, 64, 60);

        public static RLColor Door = new RLColor(165, 76, 4); // Swatch.ComplimentLighter;
        public static RLColor DoorBackgroundFov = new RLColor(41, 19, 1); // Swatch.ComplimentDarker;
        public static RLColor DoorFov = Swatch.ComplimentLightest;
        public static RLColor Stairs = Swatch.Secondary;
        public static RLColor StairsFOV = Swatch.DbStone; // old: Swatch.SecondaryLighter

        public static RLColor TextHeading = RLColor.White; // old: Swatch.DbLight
        public static RLColor InventoryHeading = Swatch.DbLight;
        public static RLColor Text = Swatch.DbLight;
        public static RLColor Player = Swatch.DbLight;
        public static RLColor Gold = Swatch.DbSun;
        public static RLColor NPC = Swatch.DbSkin;
        public static RLColor Healing = RLColor.LightGreen;

        public static RLColor LichenColor = new RLColor(102, 205, 170);
        public static RLColor JackalColor = RLColor.LightGray;
        public static RLColor RatColor = Swatch.DbBrightWood;
        public static RLColor KoboldColor = Swatch.DbBrightWood;
        public static RLColor GoblinColor = RLColor.Green;
        public static RLColor GnollColor = Swatch.DbBrightWood;
        public static RLColor MimicColor = RLColor.Yellow;
        public static RLColor WolfColor = RLColor.LightGray;
        public static RLColor ViperColor = RLColor.Green;
        public static RLColor OozeColor = new RLColor(102, 205, 170);
        public static RLColor SlimeColor = Swatch.DbDeepWater;
        public static RLColor OrcColor = RLColor.Green;
        public static RLColor LizardmanColor = RLColor.LightGreen;
        public static RLColor DragonColor = RLColor.Green;

        public static RLColor HPBacking = new RLColor(75, 5, 5);
        public static RLColor MPBacking = new RLColor(5, 5, 75);
        public static RLColor PoisonBacking = new RLColor(5, 75, 5);
    }

    class Swatch
    {
        // http://paletton.com/#uid=73d0u0k5qgb2NnT41jT74c8bJ8X

        public static RLColor PrimaryLightest = new RLColor(110, 121, 119);
        public static RLColor PrimaryLighter = new RLColor(88, 100, 98);
        public static RLColor Primary = new RLColor(68, 82, 79);
        public static RLColor PrimaryDarker = new RLColor(48, 61, 59);
        public static RLColor PrimaryDarkest = new RLColor(29, 45, 42);

        public static RLColor SecondaryLightest = new RLColor(116, 120, 126);
        public static RLColor SecondaryLighter = new RLColor(93, 97, 105);
        public static RLColor Secondary = new RLColor(72, 77, 85);
        public static RLColor SecondaryDarker = new RLColor(51, 56, 64);
        public static RLColor SecondaryDarkest = new RLColor(31, 38, 47);

        public static RLColor AlternateLightest = new RLColor(190, 184, 174);
        public static RLColor AlternateLighter = new RLColor(158, 151, 138);
        public static RLColor Alternate = new RLColor(129, 121, 107);
        public static RLColor AlternateDarker = new RLColor(97, 89, 75);
        public static RLColor AlternateDarkest = new RLColor(71, 62, 45);

        public static RLColor ComplimentLightest = new RLColor(190, 180, 174);
        public static RLColor ComplimentLighter = new RLColor(158, 147, 138);
        public static RLColor Compliment = new RLColor(129, 116, 107);
        public static RLColor ComplimentDarker = new RLColor(97, 84, 75);
        public static RLColor ComplimentDarkest = new RLColor(71, 56, 45);

        // http://pixeljoint.com/forum/forum_posts.asp?TID=12795

        public static RLColor DbDark = new RLColor(20, 12, 28);
        public static RLColor DbOldBlood = new RLColor(68, 36, 52);
        public static RLColor DbDeepWater = new RLColor(48, 52, 109);
        public static RLColor DbOldStone = new RLColor(78, 74, 78);
        public static RLColor DbWood = new RLColor(133, 76, 48);
        public static RLColor DbVegetation = new RLColor(52, 101, 36);
        public static RLColor DbBlood = new RLColor(208, 70, 72);
        public static RLColor DbStone = new RLColor(117, 113, 97);
        public static RLColor DbWater = new RLColor(89, 125, 206);
        public static RLColor DbBrightWood = new RLColor(210, 125, 44);
        public static RLColor DbMetal = new RLColor(133, 149, 161);
        public static RLColor DbGrass = new RLColor(109, 170, 44);
        public static RLColor DbSkin = new RLColor(210, 170, 153);
        public static RLColor DbSky = new RLColor(109, 194, 202);
        public static RLColor DbSun = new RLColor(218, 212, 94);
        public static RLColor DbLight = new RLColor(222, 238, 214);
    }
}

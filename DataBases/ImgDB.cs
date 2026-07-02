using Avalonia.Media.Imaging;
using ApoFisher.Helpers;
using System.Collections.Generic;

namespace ApoFisher.DataBases;

public class ImgDB
{
    public static readonly Dictionary<string, Bitmap> Icons = new Dictionary<string, Bitmap>()
    {
        //                Logos
        {   "CraftingLogo"          ,       GetIcon(4,4)                                        },
        {   "SkillsLogo"            ,       GetIcon(7,4)                                        },
        {   "InventoryLogo"         ,       GetIcon(9, 8)                                       },
        {   "GlossaryLogo"          ,       GetIcon(8,13)                                       },
        {   "TraverseLogo"          ,       GetIcon(12, 13)                                     },
        //           Weapons / Tools    
        {   "Wooden Sword"          ,       GetIcon(0, 5)                                       },
        {   "Iron Sword"            ,       GetIcon(1, 5)                                       },
        {   "Obsidian Sword"        ,       GetIcon(2, 5)                                       },
        {   "Buckler"               ,       GetIcon(0, 6)                                       },
        {   "Wooden Shield"         ,       GetIcon(1, 6)                                       },
        {   "Iron Shield"           ,       GetIcon(2, 6)                                       },
        {   "Bow"                   ,       GetIcon(3, 6)                                       },
        {   "Crossbow"              ,       GetIcon(4, 6)                                       },
        {   "Slingshot"             ,       GetIcon(5, 6)                                       },
        {   "Knife"                 ,       BitmapHelper.Load("Images/FishermansKnife.png")     },
        {   "Fishing Rod"           ,       BitmapHelper.Load("Images/FishingRod0.png")         },
        //                Armor
        {   "Helmet"                ,       BitmapHelper.Load("Images/Helmet.png")              },
        {   "Chestplate"            ,       BitmapHelper.Load("Images/Chestplate.png")          },
        {   "GauntletL"             ,       BitmapHelper.Load("Images/GauntletL.png")           },
        {   "GauntletR"             ,       BitmapHelper.Load("Images/GauntletR.png")           },
        {   "Boots"                 ,       BitmapHelper.Load("Images/Boots.png")               },
        //                Food
        {   "Apple"                 ,       GetIcon(0, 14)                                      },
        {   "Banana"                ,       GetIcon(1, 14)                                      },
        {   "Pear"                  ,       GetIcon(2, 14)                                      },
        {   "Lemon"                 ,       GetIcon(3, 14)                                      },
        {   "Strawberry"            ,       GetIcon(4, 14)                                      },
        {   "Grapes"                ,       GetIcon(5, 14)                                      },
        {   "Carrot"                ,       GetIcon(6, 14)                                      },
        {   "Meat"                  ,       GetIcon(0, 15)                                      },
        {   "Steak"                 ,       GetIcon(1, 15)                                      },
        {   "Fish"                  ,       GetIcon(4, 15)                                      },
        {   "Egg"                   ,       GetIcon(6, 15)                                      },
        {   "Cheese"                ,       GetIcon(7, 15)                                      },
        {   "Milk"                  ,       GetIcon(8, 15)                                      },
        {   "Honey"                 ,       GetIcon(9, 15)                                      },
        {   "Sugar"                 ,       GetIcon(10, 15)                                     },
        //             Materials    
        {   "Wood"                  ,       GetIcon(0, 17)                                      },
        {   "Stone"                 ,       GetIcon(1, 17)                                      },
        {   "Gold"                  ,       GetIcon(3, 17)                                      },
        {   "Cotton"                ,       GetIcon(5, 17)                                      },
        {   "Wool"                  ,       GetIcon(6, 17)                                      },
        {   "Fabric"                ,       GetIcon(7, 17)                                      },
        {   "Leather"               ,       GetIcon(8, 17)                                      },
        {   "Tooth"                 ,       GetIcon(9, 17)                                      },
        //          Map Elements    
        {   "RoomS"                 ,       BitmapHelper.Load("Images/Room0_2.png")             },
        {   "RoomL"                 ,       BitmapHelper.Load("Images/RoomL_1.png")             },
        {   "RoomF"                 ,       BitmapHelper.Load("Images/RoomF_0.png")             },
        {   "RoomR"                 ,       BitmapHelper.Load("Images/RoomL_-1.png")            },
        {   "Room0"                 ,       BitmapHelper.Load("Images/Room0_0.png")             },
        //          UI Elements
        {   "Logo"                  ,       BitmapHelper.Load("Images/Logo.png")                },
        {   "SettingsLogo"          ,       BitmapHelper.Load("Images/Settings.png")            }, // Duplicate
        {   "ArrowR"                ,       BitmapHelper.Load("Images/Arrow_Right.png")         },
        {   "ArrowL"                ,       BitmapHelper.Load("Images/Arrow_Left.png")          },
        {   "ArrowU"                ,       BitmapHelper.Load("Images/Arrow_Up.png")            },
        {   "ArrowD"                ,       BitmapHelper.Load("Images/Arrow_Down.png")          },
        {   "Expand"                ,       BitmapHelper.Load("Images/ExpandIcon.png")          },
        {   "Collapse"              ,       BitmapHelper.Load("Images/ShrinkIcon.png")          },
        {   "Settings"              ,       BitmapHelper.Load("Images/Settings.png")            }, // Duplicate
        {   "EmptySlot"             ,       BitmapHelper.Load("Images/EmptySlot.png")           },
        {   "HelmetPH"              ,       BitmapHelper.Load("Images/HelmetPH.png")            },
        {   "ChestplatePH"          ,       BitmapHelper.Load("Images/ChestplatePH.png")        },
        {   "BootsPH"               ,       BitmapHelper.Load("Images/BootsPH.png")             },
        {   "GauntletLPH"           ,       BitmapHelper.Load("Images/GauntletLPH.png")         },
        {   "GauntletRPH"           ,       BitmapHelper.Load("Images/GauntletRPH.png")         },
        {   "Menu"                  ,       BitmapHelper.Load("Images/FishMenu.png")            },
    };

    private static Bitmap GetIcon(int column, int row) 
    {
        return BitmapHelper.LoadBitmapFromSpriteSheet("Images/IconsSpriteSheet.png", 32, column, row);
    }
    public static Bitmap Get(string name) 
    {
        return Icons[name];
    }
}
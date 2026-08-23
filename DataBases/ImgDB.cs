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
        {   "SettingsLogo"          ,       BitmapHelper.Load("Images/Settings.png")            },
        //                 Fish    
        {   "Bulbo"                 ,       BitmapHelper.Load("Images/Bulbo.png")               },
        {   "Bob"                   ,       BitmapHelper.Load("Images/Bob.png")                 },
        {   "Gary"                  ,       BitmapHelper.Load("Images/Gary.png")                },
        {   "Jelly Fish"            ,       BitmapHelper.Load("Images/Jelly Fish.png")          },
        {   "Seeker"                ,       BitmapHelper.Load("Images/Seeker.png")              },
        {   "Trapgea"               ,       BitmapHelper.Load("Images/Trapgea.png")             },
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

        //          UI Elements
        {   "Expand"                ,       BitmapHelper.Load("Images/ExpandIcon.png")          },
        {   "Collapse"              ,       BitmapHelper.Load("Images/ShrinkIcon.png")          },
        {   "InventorySlot"         ,       BitmapHelper.Load("Images/InventorySlot.png")       },
        {   "Menu"                  ,       BitmapHelper.Load("Images/FishMenu.png")            },
        {   "Woods32"               ,       BitmapHelper.Load("Images/Woods32.png")             },
        {   "Woods48"               ,       BitmapHelper.Load("Images/Woods48.png")             },
        {   "Woods64"               ,       BitmapHelper.Load("Images/Woods64.png")             },
        {   "Lake32"                ,       BitmapHelper.Load("Images/Lake32.png")              },
        {   "Lake48"                ,       BitmapHelper.Load("Images/Lake48.png")              },
        {   "Lake64"                ,       BitmapHelper.Load("Images/Lake64.png")              },
        {   "Village32"             ,       BitmapHelper.Load("Images/Village32.png")           },
        {   "Village48"             ,       BitmapHelper.Load("Images/Village48.png")           },
        {   "Village64"             ,       BitmapHelper.Load("Images/Village64.png")           },
        {   "SelectedTile32"        ,       BitmapHelper.Load("Images/SelectedTile32.png")      },
        {   "SelectedTile48"        ,       BitmapHelper.Load("Images/SelectedTile48.png")      },
        {   "SelectedTile64"        ,       BitmapHelper.Load("Images/SelectedTile64.png")      },
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
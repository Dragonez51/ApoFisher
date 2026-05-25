using System.Collections.ObjectModel;
using ApoFisher.DataStructures;

namespace ApoFisher.DataBases;

public class GlossaryDB
{
    public static ObservableCollection<GlossaryCategory> Glossary = new()
    {
        new GlossaryCategory("Weapons", new ObservableCollection<GlossaryEntry>()
        {
            new (   "Wooden Sword"      ,  "This is a wooden sword that was found by your bunker."      ),
            new (   "Iron Sword"        ,  "This is an iron sword that is craftable by your bunker."    ),
            new (   "Obsidian Sword"    ,  "This is an obsidian sword that is craftable by your bunker."),
            new (   "Bow"               ,  "This is a bow that is craftable by your bunker."            ),
            new (   "Crossbow"          ,  "This is a crossbow that is craftable by your bunker."       ),
            new (   "Slingshot"         ,  "This is a slinghot that is craftable by your bunker."       ),
            new (   "Buckler"           ,  "This is a shield that is craftable by your bunker."         ),
            new (   "Wooden Shield"     ,  "This is a shield that is craftable by your bunker."         ),
            new (   "Iron Shield"       ,  "This is a shield that is craftable by your bunker."         ),
        }),
        new GlossaryCategory("Food", new ObservableCollection<GlossaryEntry>()
        {
            new(    "Apple"             , "This is a common food source that grows on trees."           ),
            new(    "Banana"            , "This is a common food source that grows on trees."           ),
            new(    "Pear"              , "This is a common food source that grows on trees."           ),
            new(    "Lemon"             , "This is a rare food source that grows on trees."             ),
            new(    "Carrot"            , "This is a rare food source that grows in soil."              ),
            new(    "Strawberry"        , "This is an epic food source that grows on bushes."           ),
            new(    "Grapes"            , "This is an epic food source that grows on bushes."           ),
            new(    "Meat"              , "This is an expensive food source that is gathered from animals. It has to be prepared by a fireplace to be edible."),
            new(    "Steak"             , "This is an expensive food source that is gathered from animals. It has to be prepared by a fireplace to be edible."),
            new(    "Fish"              , "This is a rare food source that is gathered from fishing."   ),
            new(    "Egg"               , "This is an epic ingredient that is collected from animals."  ),
            new(    "Cheese"            , "This is an epic ingredient and food source that is collected from animals."),
            new(    "Milk"              , "This is an epic ingredient an food source that is collected from animals."),
            new(    "Honey"             , "This is an epic ingredient that is harvested from bees."     ),
            new(    "Sugar"             , "This is an epic ingredient that is harvested from sugarcane's."),
        }),
        new GlossaryCategory("Materials", new ObservableCollection<GlossaryEntry>()
        {
            new(    "Wood"              , "This is a common material found in the woods."               ),
            new(    "Stone"             , "This is a common material found in caves."                   ),
            new(    "Gold"              , "This is a rare material found in caves."                     ),
            new(    "Cotton"            , "This is a rare material found in woods."                     ),
            new(    "Wool"              , "This is a rare material gathered form animals like sheep."   ),
            new(    "Fabric"            , "This is a craftable material."                               ),
            new(    "Leather"           , "This is an epic material gathered from rare animals like bears."),
            new(    "Tooth"             , "This is an epic material gathered from rare animals like sabre."),
        }),
    };
    
    public static void ToggleVisibility(string categoryName)
    {
        GetCategory(categoryName).ToggleVisibiliy();
    }

    private static GlossaryCategory GetCategory(string categoryName)
    {
        switch (categoryName)
        {
            case "Weapons":
                return Glossary[0];
            case "Food":
                return Glossary[1];
            case "Materials":
                return Glossary[2];
            default:
                throw new System.Exception("[GlossaryDB]=>GetCategory("+categoryName+") || categoryName is null");
        }
    }

    public static GlossaryEntry GetEntry(string categoryName, string itemTitle)
    {
        GlossaryCategory category = GetCategory(categoryName);
        foreach(GlossaryEntry e in category.Entries)
        {
            if(e.Title == itemTitle) return e;
        }
        throw new System.Exception("[GlossaryDB]=>GetEntry("+categoryName+", "+itemTitle+") || no such element!");
    }
}
using Avalonia.Media.Imaging;
using ApoFisher.DataBases;

namespace ApoFisher.DataStructures;

public class InventoryItemTemplate
{
    public string ItemName { get; private set; }
    public int MaxQuantity { get; private set; }
    public Bitmap Icon { get =>  ImgDB.Get(ItemName); }
    
    public int LeftPosition { get; private set; }
    
    public InventoryItemTemplate(string itemName, int maxQuantity)
    {
        ItemName = itemName;
        MaxQuantity = maxQuantity;
        
        countMaxQuantityZeros();
    }

    private void countMaxQuantityZeros() //idk what is this code bruh
    {
        int temp = MaxQuantity;
        int count = 0;
        while (temp != 0)
        {
            temp /= 10;
            count++;
        }
        
        LeftPosition = 70 - (count * 10);
    }
}
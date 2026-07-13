using ApoFisher.ViewModels;

namespace ApoFisher.DataStructures;

public class Item
{
    private string _name;
    private ItemShape _itemShape;

    public Item(string name)
    {
        this._name = name;
        _itemShape = new ItemShape(name);
        MainViewModel.Player.GetInventory().AddItem(this);
    }

    public ItemShape GetItemShape() => _itemShape;
    public string Getname() => _name;
}
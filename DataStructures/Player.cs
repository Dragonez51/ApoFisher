namespace ApoFisher.DataStructures;

public class Player : Entity 
{
    private PlayerInventory Inventory;

    public Player() : base() { Inventory = new PlayerInventory(20); }
    public Player(int inventoryCapacity) : base() { Inventory = new PlayerInventory(inventoryCapacity); }
    public Player(int strength, int dexterity, int intelligence, int maxHealth) : base(strength, dexterity, intelligence, maxHealth) { Inventory = new PlayerInventory(); }
    public Player(int strength, int dexterity, int intelligence, int maxHealth, int inventoryCapacity) : base(strength, dexterity, intelligence, maxHealth) { Inventory = new PlayerInventory(inventoryCapacity); }

    public PlayerInventory GetInventory() => Inventory;
}
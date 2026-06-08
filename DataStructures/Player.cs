namespace ApoFisher.DataStructures;

public class Player : Entity 
{
    private PlayerInventory Inventory;
    private int _hp = 0;
    private int _stamina = 0;

    public Player() : base() { Inventory = new PlayerInventory(20); initialize(); }
    public Player(int inventoryCapacity) : base() { Inventory = new PlayerInventory(inventoryCapacity); initialize(); }
    public Player(int strength, int dexterity, int intelligence, int maxHealth, int maxStamina) : base(strength, dexterity, intelligence, maxHealth, maxStamina) { Inventory = new PlayerInventory(); initialize(); }
    public Player(int strength, int dexterity, int intelligence, int maxHealth, int maxStamina, int inventoryCapacity) : base(strength, dexterity, intelligence, maxHealth, maxStamina) { Inventory = new PlayerInventory(inventoryCapacity); initialize(); }

    private void initialize()
    {
        // _hp = GetStatistics().GetMaxHP();
        _hp = 50;
        _stamina = GetStatistics().GetMaxStamina();
    }

    public PlayerInventory GetInventory() => Inventory;
    public int GetHP() => _hp;
    public int GetMaxHP() => GetStatistics().GetMaxHP();
    public int GetStamina() => _stamina;
    public int GetMaxStamina() => GetStatistics().GetMaxStamina();
}
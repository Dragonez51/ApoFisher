namespace ApoFisher.DataStructures;

public class Player
{
    private Inventory Inventory;
    private int _hp = 0;
    private int _stamina = 0;

    private Statistics statistics = new Statistics();

    public Player() { Inventory = new Inventory(); initialize(); }
    public Player(int inventoryCapacity) { Inventory = new Inventory(inventoryCapacity); initialize(); }
    public Player(int inventoryWidth, int inventoryHeight) { Inventory = new Inventory(inventoryWidth, inventoryHeight); initialize(); }
    public Player(int strength, int dexterity, int intelligence, int maxHealth, int maxStamina){ Inventory = new Inventory(); initialize(); }
    public Player(int strength, int dexterity, int intelligence, int maxHealth, int maxStamina, int inventoryCapacity) { Inventory = new Inventory(inventoryCapacity); initialize(); }

    // TODO: Clear the statistics slop.
    
    private void initialize()
    {
        // statistics = new Statistics();
        _hp = 50;
        _stamina = statistics.GetMaxStamina();
    }

    public Inventory GetInventory() => Inventory;
    public int GetHP() => _hp;
    public int GetMaxHP() => statistics.GetMaxHP();
    public int GetStamina() => _stamina;
    public int GetMaxStamina() => statistics.GetMaxStamina();
}
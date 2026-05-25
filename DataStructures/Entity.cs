namespace ApoFisher.DataStructures;

public class Entity 
{
    private Statistics Statistics;
    private int AttackPower;
    public Entity() 
    {
        Statistics = new Statistics();
        AttackPower = 3;
    }

    public Entity(int strength, int dexterity, int intelligence, int maxHealth)
    {
        Statistics = new Statistics(strength, dexterity, intelligence, maxHealth);
        AttackPower = 3;
    }

    public Statistics getStats() => this.Statistics;
    public int getAttackPower() => AttackPower;
}
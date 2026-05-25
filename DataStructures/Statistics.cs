namespace ApoFisher.DataStructures;

public class Statistics 
{
    public int Strength;
    public int Dexterity;
    public int Intelligence;
    public int MaxHealth;

    public Statistics() 
    {
        Strength = 1;
        Dexterity = 1;
        Intelligence = 1;
        MaxHealth = 100;
    }

    public Statistics(int strength, int dexterity, int intelligence, int maxHealth) 
    {
        Strength = strength;
        Dexterity = dexterity;
        Intelligence = intelligence;
        MaxHealth = maxHealth;
    }
}
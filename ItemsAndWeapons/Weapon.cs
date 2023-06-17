using NewGame.Interface;

namespace NewGame;

public class Weapon : IItems
{
    public string Name { get; }
    public string Type { get; }
    public double Damage { get; }


    public Weapon(string name, string type, double damage)
    {
        Name = name;
        Type = type;
        Damage = damage;
    }
    public static Weapon WeaponPool()
    {
        string[] names = { "Rusty Sword", "Iron Sword", "Basic Staff", "Wizard Staff", "Rusty Knife", "Iron Dagger" };
        string[] types = { "Melee", "Melee", "Magic", "Magic", "Deceit", "Deceit" };
        int[] damage = { 2, 3, 2, 3, 2, 3 };
        var random = new Random().Next(0, names.Length);

        return new Weapon(names[random], types[random], damage[random]);
    }
    public static Weapon RareWeaponPool()
    {
        string[] names = { "Steel Sword", "Ebony Sword", "Dragon Staff", "Staff of Cosmos", "Steel Dagger", "Ebony Dagger" };
        string[] types = { "Melee", "Melee", "Magic", "Magic", "Deceit", "Deceit" };
        int[] damage = { 4, 6, 4, 6, 4, 6 };
        var random = new Random().Next(0, names.Length);

        return new Weapon(names[random], types[random], damage[random]);
    }
}
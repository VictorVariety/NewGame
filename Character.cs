using NewGame.Interface;
using NewGame.Scene;
using static NewGame.Inventory;

namespace NewGame;

public class Character
{
    public string Name { get; private set; }
    public string Class { get; private set; }
    public int Hp { get; set; }
    public int MaxHp { get; private set; }
    public int Level { get; private set; }
    public int Xp { get; set; }
    public int MaxXp { get; private set; }
    public Weapon? Hand { get; set; }
    public Inventory Inventory { get; private set; }



    public Character(string name, string type)
    {
        Name = name;
        Class = type;
        Level = 1;
        MaxHp = 50;
        Hp = 50;
        MaxXp = 50;
        Xp = 0;
        Inventory = new Inventory();

        Hand = type switch
        {
            "Warrior" => new Weapon("Training Stick", "Melee", 1),
            "Mage" => new Weapon("Makeshift Wand", "Magic", 1),
            "Rogue" => new Weapon("Chopstick", "Deceit", 1),
            _ => Hand
        };
    }

    public void LevelUp()
    {
        var excessXp = Xp - MaxXp;
        Level += 1;
        MaxHp += 10;
        Hp = MaxHp;
        Xp = 0 + excessXp;
        MaxXp += 10;
        Console.Clear();
        Console.WriteLine($"You reached level {Level}! Max HP increased by 10, to {MaxHp}");
        AnyButton();
    }

    public string ShowCharacterStats()
    {
        var title = $"{Name} the {Class}";
        var titlePadLength = (int)Math.Round((40 - title.Length) / 2.0);
        var titleBar = title.PadLeft(titlePadLength + title.Length, ' ');

        var level = $"Level: {Level}";
        var levelPadLength = (int)Math.Round((40 - level.Length) / 2.0);
        var levelBar = level.PadLeft(levelPadLength + level.Length, ' ');

        var weapon = $"Equipped with a {Hand.Name}";
        var weaponPadLength = (int)Math.Round((40 - weapon.Length) / 2.0);
        var weaponBar = weapon.PadLeft(weaponPadLength + weapon.Length, ' ');

        var hpLine = $"HP: {Hp}/{MaxHp}".PadRight(15, ' ');
        var hpBar = $"|{GetProgressBar(Hp, MaxHp)}|".PadLeft(25, ' ');

        var xpLine = $"XP: {Xp}/{MaxXp}".PadRight(15, ' ');
        var xpBar = $"|{GetProgressBar(Xp, MaxXp)}|".PadLeft(25, ' ');

        var s =
                    $"----------------------------------------\n" + 
                    $"{titleBar}\n" + 
                    $"{levelBar}\n" +
                    $"{weaponBar}\n " +
                    $"\n" +
                    $"{hpLine + hpBar}\n" +
                    $"{xpLine + xpBar}\n" +
                    $"----------------------------------------";
        return s;

    }


    public void ConsumeItem(int index)
    {
        var item = Inventory.Bag[index];
        var consumable = (Item)item;
        switch (consumable.Effect)
        {
            case "Experience":
            {
                Console.Clear();
                Console.WriteLine($"You {consumable.FindVerb(consumable)} it and gain {consumable.Potency}XP");
                AnyButton();

                Xp += consumable.Potency;
                if (Xp >= MaxXp) LevelUp();
                break;
            }
            case "Healing":
            {
                var healthGained = MaxHp - Hp < consumable.Potency ? MaxHp - Hp : consumable.Potency;

                Console.Clear();
                Console.WriteLine($"You {consumable.FindVerb(consumable)} it and gain {healthGained}HP");
                AnyButton();

                Hp += consumable.Potency;
                if (Hp > MaxHp) Hp = MaxHp;
                break;
            }
        }
        Inventory.Bag.Remove(item);
    }

    public bool IsSpaceInInventory(int index)
    {
        if (Inventory.Bag.Count != Inventory.BagSize) return true;

        Console.Clear();
        Console.WriteLine("You have no space left in your inventory");
        AnyButton();
        return false;
    }
    public void EquipWeapon(int index)
    {
        Console.Clear();

        if (Inventory.Bag[index] is not Weapon weapon) return;

        Console.WriteLine($"You equipped {weapon.Name} and put {Hand.Name} in your bag.");
        AnyButton();

        var currentWeapon = Hand;
        Hand = weapon;
        Inventory.Bag.RemoveAt(index);
        Inventory.Bag.Add(currentWeapon);

    }

    public void RemoveItem(int index)
    {
        Console.Clear();
        Console.WriteLine($"You dropped {Inventory.Bag[index].Name}.");
        AnyButton();
        Inventory.Bag.Remove(Inventory.Bag[index]);
    }

    private static void AnyButton()
    {
        Console.WriteLine();
        Console.WriteLine("Any button to continue..");
        Console.ReadKey();
        Console.Clear();
    }

    private string GetProgressBar(int current, int max)
    {
        var percentage = (int)((double)current / max * 100);
        var filledBlocks = percentage / 5;
        var emptyBlocks = 20 - filledBlocks;

        var progressBar = new string('█', filledBlocks);
        progressBar += new string(' ', emptyBlocks);

        return progressBar;
    }

}
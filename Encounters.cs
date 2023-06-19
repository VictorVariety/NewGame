using System;

namespace NewGame;

public class Encounters
{
    public string Name { get; private set; }
    public string EncounterText { get; private set; }
    public Enemy? Enemy { get; private set; }
    public Item? Item { get; private set; }
    public Weapon? Weapon { get; private set; }

    public Encounters()
    {
        
    }
    public Encounters(string name, string encounter, Enemy? enemy, Item item, Weapon? weapon)
    {
        Name = name;
        EncounterText = encounter;
        Enemy = enemy;
        Item = item;
        Weapon = weapon;
    }

    public Encounters Forest(int level)
    {
        var encounter = string.Empty;
        Enemy? enemy = null;
        Item? item = null;
        Weapon? weapon = null;
        var random = new Random();
        var rnd = random.Next(0, 3);

        switch (rnd)
        {
            case 0:
                encounter = "It's beautiful here, would be a perfect place to pick berries,\n" +
                            "but there is a noise.";
                enemy = random.Next(0, 2) == 1
                    ? Enemy.GenerateGiantSpider(level)
                    : Enemy.GenerateBear(level);
                item = new Random().Next(0, 2) == 0
                    ? Item.GenerateEpicLoot()
                    : Item.GenerateLoot();
                break;

            case 1:
                encounter = "It's getting dark, and something is watching you..";
                enemy = random.Next(0, 2) == 1
                    ? Enemy.GenerateGhost(level)
                    : Enemy.GenerateShade(level);
                break;

            case 2:
                encounter = "You find an empty camp, and of course you loot it.";
                item = Item.GenerateLoot();
                if (random.Next(0, 2) == 1) weapon = Weapon.WeaponPool(); ;
                break;
        }

        return new Encounters("the forest", encounter, enemy, item, weapon);
    }

    public Encounters OldBattlefield(int level)
    {
        var encounter = string.Empty;
        Enemy? enemy = null;
        Item? item = null;
        Weapon? weapon = null;
        var rnd = new Random().Next(0, 3);
        switch (rnd)
        {
            case 0:
                encounter = "All the dead bodies here probably attracted some ghouls, \n" +
                            "but there are weapons laying around so you risk it.";
                enemy = Enemy.GenerateGhoul(level);
                weapon = Weapon.WeaponPool();
                break;
            case 1:
                encounter = "It's getting dark, and something is watching you..";
                enemy = Enemy.GenerateGhost(level);
                if (new Random().Next(0, 2) == 1) item = Item.GenerateLoot();
                break;
            case 2:
                encounter = "You find what looks like the remnants of a commander tent, \n" +
                            "there might be something valuable here.";
                item = Item.GenerateLoot();
                weapon = Weapon.WeaponPool();
                break;
        }

        return new Encounters("the old battlefield", encounter, enemy, item, weapon);
    }
}
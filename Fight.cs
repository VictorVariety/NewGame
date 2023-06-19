using NewGame.Scene;

namespace NewGame;

public class Fight
{
    private static bool _ongoing = true;
    private static bool _fledFight;


    public void Combat(Character you, Enemy opponent)
    {
        _ongoing = true;
        _fledFight = false;
        while (_ongoing)
        {
            ShowFightState(you, opponent);
            ShowMoves();
            MakeAMove(you, opponent);

            if (_fledFight)
            {
                FleeFight();
                continue;
            }

            if (opponent.Hp > 0) continue;
            _ongoing = false;
            PostFight(you, opponent);
        }
    }

    private void FleeFight()
    {
        Console.Clear();
        Console.WriteLine("You flee the scene.");
        AnyButton();
        _ongoing = false;
    }


    private void ShowFightState(Character player, Enemy opponent)
    {
        Console.Clear();
        var you = player.Name.PadRight(30, ' ');
        var yourHp = ($"Hp: {player.Hp}/{player.MaxHp}").PadRight(15, ' ');
        var yourLvl = ("Lvl " + player.Level).PadRight(15, ' ');
        var enemy = opponent.Name.PadLeft(30, ' ');
        var opponentHp = ($"Hp: {opponent.Hp}/{opponent.MaxHp}").PadLeft(15, ' ');
        var opponentLvl = ("Lvl " + opponent.Level).PadLeft(15, ' ');
        Console.WriteLine($"------------------------------------------------------------\n" + //60 length
                          $"{you}{enemy}\n" +
                          $"\n" +
                          $"{yourHp}{yourLvl}{opponentLvl}{opponentHp}\n" +
                          $"------------------------------------------------------------");



    }

    private void ShowMoves()
    {
        Console.WriteLine($"\n" +
                          $"1. Attack\n" +
                          $"2. Run\n");
    }

    private void PostFight(Character you, Enemy opponent)
    {
        var xp = (int)Math.Floor(opponent.Level * 5 +
                                 opponent.MaxHp * 2 / 4 *
                                 opponent.Toughness * opponent.Strength);
        you.Xp += xp;
        Console.Clear();
        Console.WriteLine($"You defeated the {opponent.Name.ToLower()}\n" +
                          $"Gained {xp} Exp");
        AnyButton();

        if (you.Xp >= you.MaxXp) you.LevelUp();
    }


    private void MakeAMove(Character you, Enemy opponent)
    {
        var choice = Menu.GetNumFromUser(2); switch (choice)
        {
            case 1:
                DealDamage(you, opponent);
                if (you.Hp < 1) Game.GameOver();
                break;
            case 2:
                _fledFight = true;
                break;
        }
    }

    private void DealDamage(Character you, Enemy opponent)
    {
        var playerDamage = PlayerDamage(you, opponent);
        var enemyDamage = EnemyDamage(you, opponent);
        opponent.Hp -= playerDamage;
        you.Hp -= enemyDamage;

        Console.Clear();
        ShowFightState(you, opponent);
        Console.WriteLine($"You dealt {playerDamage} to the {opponent.Name.ToLower()}.\n" +
                          $"While the {opponent.Name.ToLower()} dealt {enemyDamage} to you.");
        AnyButton();
    }

    private int EnemyDamage(Character you, Enemy opponent)
    {
        var random = new Random();
        var randomDamage = random.Next(1, 3 + opponent.Level);
        var dealDamage = randomDamage + (opponent.Level * opponent.Strength * EnemyEffectiveness(you, opponent));
        return (int)Math.Round(dealDamage);
    }

    private int PlayerDamage(Character you, Enemy opponent)
    {
        var random = new Random();
        var randomDamage = random.Next(1, 3 + you.Level);
        var weapon = you.Hand;
        var damage = weapon.Damage - opponent.Toughness;
        if (damage < 1) damage = 1;
        var dealDamage = randomDamage + you.Level + (damage * Proficiency(you, weapon) * Effectiveness(weapon, opponent));
        return (int)Math.Round(dealDamage);
    }

    private double Proficiency(Character you, Weapon weapon)
    {
        var type = you.Class switch
        {
            "Warrior" => "Melee",
            "Mage" => "Magic",
            "Rogue" => "Deceit",
            _ => throw new ArgumentOutOfRangeException()
        };
        if (type == "Melee" & weapon.Type == "Melee" ||
            type == "Magic" & weapon.Type == "Magic" ||
            type == "Deceit" & weapon.Type == "Deceit")
        {
            return 2;
        }
        else return 1;
    }
    private double Effectiveness(Weapon weapon, Enemy opponent)
    {
        double effectiveness = 1;
        if (weapon.Type == "Melee" & opponent.Type == "Magic" ||
            weapon.Type == "Magic" & opponent.Type == "Deceit" ||
            weapon.Type == "Deceit" & opponent.Type == "Melee")
        {
            effectiveness = 1.5;
        }
        if (weapon.Type == "Melee" & opponent.Type == "Deceit" ||
            weapon.Type == "Magic" & opponent.Type == "Melee" ||
            weapon.Type == "Deceit" & opponent.Type == "Magic")
        {
            effectiveness = 0.75;
        }

        return effectiveness;
    }
    private double EnemyEffectiveness(Character you, Enemy opponent)
    {
        double effectiveness = 1;
        if (you.Class == "Warrior" & opponent.Type == "Deceit" ||
            you.Class == "Mage" & opponent.Type == "Melee" ||
            you.Class == "Rogue" & opponent.Type == "Magic")
        {
            effectiveness = 1.5;
        }
        if (you.Class == "Warrior" & opponent.Type == "Magic" ||
            you.Class == "Mage" & opponent.Type == "Deceit" ||
            you.Class == "Rogue" & opponent.Type == "Melee")
        {
            effectiveness = 0.75;
        }

        return effectiveness;
    }

    public static void AnyButton()
    {
        Console.WriteLine();
        Console.WriteLine("Press any button to continue..");
        Console.ReadKey();
        Console.Clear();
    }

}
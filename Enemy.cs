namespace NewGame;

public class Enemy
{
    public string Name { get; }
    public string Type { get; }
    public int Level { get; }
    public int Hp { get; set; }
    public int MaxHp { get; }
    public double Strength { get; }
    public double Toughness { get; }

    public Enemy(string name, string type, int hp, int level, double strengthPeak, double toughPeak)
    {
        Name = name;
        Type = type;
        Level = level;
        Hp = hp;
        MaxHp = hp;

        var levelModifier = level > 1 ? (double)level / 2 : 1;
        var trueStrengthPeak = strengthPeak * levelModifier;
        var trueToughPeak = toughPeak * level;
        Strength = new Random().NextSingle() * (trueStrengthPeak - 0.5) + 0.5;
        Toughness = new Random().NextSingle() * (trueToughPeak - 0.5) + 0.5;
    }

    public static Enemy GenerateGhoul(int level)
    {
        return new Enemy("Ghoul", "Melee", 40, FindEnemyLevel(level, 2, 1), 1, 1);
    }
    public static Enemy GenerateGhost(int level)
    {
        return new Enemy("Ghost", "Magic", 50, FindEnemyLevel(level, 1, 1), 1, 0.8);
    }
    public static Enemy GenerateShade(int level)
    {
        return new Enemy("Shade", "Deceit", 40, FindEnemyLevel(level, 2, 1), 1.2, 0.8);
    }
    public static Enemy GenerateBear(int level)
    {
        return new Enemy("Bear", "Melee", 100, FindEnemyLevel(level, 0, 2), 1.5, 1.5);
    }
    public static Enemy GenerateGiantSpider(int level)
    {
        return new Enemy("Giant Spider", "Deceit", 90, FindEnemyLevel(level, 1, 2), 2, 1);
    }


    private static int FindEnemyLevel(int level, int min, int max)
    {
        var num = new Random().Next(level - min, level + max);
        return num < 1 ? 1 : num;
    }


}
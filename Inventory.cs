using NewGame.Interface;
using NewGame.Scene;

namespace NewGame;

public class Inventory
{
    public List<IItems> Bag { get; set; }
    public int BagSize { get; }

    public int CurrentItemIndex;

    public delegate void ItemAction(int i);



    public Inventory()
    {
        Bag = new List<IItems>();
        BagSize = 4;
        CurrentItemIndex = 0;

        Bag.Add(Weapon.WeaponPool());
        Bag.Add(Weapon.WeaponPool());
        Bag.Add(Weapon.WeaponPool());


    }

    public void ShowInventory()
    {
        const int slotWidth = 15;
        const int boxWidth = 17;
        const char horizontalLine = '─';
        const char verticalLines = '│';
        const char cornerTopLeft = '┌';
        const char cornerTopRight = '┐';
        const char cornerBottomLeft = '└';
        const char cornerBottomRight = '┘';
        const char mergingCornerLeft = '├';
        const char mergingCornerRight = '┤';
        const char mergingCornerTop = '┬';
        const char mergingCornerBottom = '┴';
        const char mergingCornerMiddle = '┼';
        var horizontalLines = new string(horizontalLine, boxWidth);


        Console.Clear();
        Console.WriteLine($"{cornerTopLeft}{horizontalLines}{mergingCornerTop}{horizontalLines}{cornerTopRight}");

        for (var i = 0; i < BagSize; i++)
        {
            if (i % 2 == 0)
            {
                Console.Write(verticalLines);
            }

            if (i < Bag.Count)
            {
                Console.Write(CenterText(Bag[i].Name, boxWidth));
            }
            else
            {
                Console.Write(CenterText(" ", boxWidth));
            }

            Console.Write(verticalLines);

            if (i % 2 != 0)
            {
                Console.WriteLine();
                if (i != BagSize - 1)
                {
                    Console.WriteLine($"{mergingCornerLeft}{horizontalLines}{mergingCornerMiddle}{horizontalLines}{mergingCornerRight}");
                }
            }
        }

        Console.WriteLine($"{cornerBottomLeft}{horizontalLines}{mergingCornerBottom}{horizontalLines}{cornerBottomRight}");



        Console.WriteLine();
        Console.WriteLine($"{Bag.Count + 1}. Back");
    }
    //public string ShowInventory()
    //{
    //    const int slotWidth = 15;
    //    const int boxWidth = 17;
    //    const char horizontalLine = '─';
    //    const char verticalLines = '│';
    //    const char cornerTopLeft = '┌';
    //    const char cornerTopRight = '┐';
    //    const char cornerBottomLeft = '└';
    //    const char cornerBottomRight = '┘';
    //    const char mergingCornerLeft = '├';
    //    const char mergingCornerRight = '┤';
    //    const char mergingCornerTop = '┬';
    //    const char mergingCornerBottom = '┴';
    //    const char mergingCornerMiddle = '┼';
    //    var horizontalLines = new string(horizontalLine, boxWidth);

    //    var s = $"{cornerTopLeft}{horizontalLines}{mergingCornerTop}{horizontalLines}{cornerTopRight}";

    //    for (int i = 0; i < BagSize; i++)
    //    {
    //        if (i % 2 == 0)
    //        {
    //            s += "\n" + verticalLines;
    //        }

    //        if (i < Bag.Count)
    //        {
    //            s += CenterText($"{i}. " + Bag[i].Name, slotWidth);
    //        }
    //        else
    //        {
    //            s += CenterText("", slotWidth);
    //        }

    //        s += verticalLines;

    //        if (i % 2 != 0)
    //        {
    //            s += $"{mergingCornerLeft}{horizontalLines}{mergingCornerMiddle}{horizontalLines}{mergingCornerRight}";
    //        }

    //    }

    //    s += $"{cornerBottomLeft}{horizontalLines}{mergingCornerBottom}{horizontalLines}{cornerBottomRight}";

    //    return s;
    //}

    //public Menu ShowItem(Character character, int index)
    //{
    //    var item = Bag[index];
    //    switch (item)
    //    {
    //        case Weapon weapon:
    //            return new ItemMenu(
    //                $"{weapon.Name} \nDamage: {weapon.Damage}\n", 
    //                new Inventory.ItemAction[] { character.EquipWeapon, RemoveItem },
    //                new string[]{ "Equip.", "Drop." },
    //                new char[]{ '1', '2' },
    //                State.Character);
    //        case Item consumable:
    //            return new ItemMenu(
    //                $"{consumable.Name} \nEffect: {consumable.Effect}",
    //                new Inventory.ItemAction[] { (index) => character.ConsumeItem(index), (index) => RemoveItem(index) },
    //                new string[] { $"{consumable.FindVerb(consumable)}.", "Drop." },
    //                new char[] { '1', '2' },
    //                State.Character);
    //    }
    //    return null;
    //}
    public void AddItem(IItems item)
    {
        if (Bag.Count < BagSize)
        {
            Bag.Add(item);
            Console.WriteLine($"You put {item.Name} in your bag.");
        }
        else
        { 
            //Switch items if full
        }

    }

    private string CenterText(string text, int width)
    {
        return text.PadLeft((width - text.Length) / 2 + text.Length).PadRight(width);
    }

}
using NewGame.Interface;
using NewGame.Scene;

namespace NewGame
{
    public class Item : IItems
    {
        public string Name { get; }
        public string Effect { get; }
        public int Potency { get; }

        public Item(string name, string effect, int potency)
        {
            Name = name;
            Effect = effect;
            Potency = potency;
        }

        public static Item GenerateLoot()
        {
            var loot = new List<Item>
            {
                new Item("Holy Water", "Healing", 25),
                new Item("Health Potion", "Healing", 50),
                new Item("Scroll", "Experience", 25),
                new Item("Book", "Experience", 50),
            };
            return loot[new Random().Next(0, loot.Count)];
        }
        public Item GenerateEpicLoot()
        {
            var loot = new List<Item>
            {
                new Item("Potion of Youth", "Healing", 75),
                new Item("Great Book", "Experience", 75),
            };
            return loot[new Random().Next(0, loot.Count)];
        }

        public string FindVerb(IItems itemInQuestion)
        {
            var s = itemInQuestion switch
            {
                Item item => item.Effect == "Healing" ? "Drink" : "Read",
                Weapon => "Equip",
                _ => throw new ArgumentOutOfRangeException(nameof(itemInQuestion), itemInQuestion, null)
            };
            return s;
        }
    }
}
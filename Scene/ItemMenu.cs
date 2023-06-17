using NewGame.Interface;
using static NewGame.Inventory;

namespace NewGame.Scene
{
    public class ItemMenu : Menu, IScene
    {
        private new Inventory.ItemAction[] _actions;

        public ItemMenu(string menuText, Inventory.ItemAction[] actions, string[] choices, char[] choiceChars, State state)
            : base(menuText, null, choices, choiceChars, state)
        {
            _actions = actions;
        }

        public override void HandleInput()
        {
            while (true)
            {
                var choice = Console.ReadKey();
                for (var i = 0; i < _actions.Length; i++)
                {
                    if (choice.KeyChar != _choiceChars[i]) continue;
                    _actions[i](i-1);
                    return;

                }
            }
        }
    }
}
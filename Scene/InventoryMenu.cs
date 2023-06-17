using NewGame.Interface;

namespace NewGame.Scene;

public class InventoryMenu : IScene
{
    private Character _character;
    private State _state;
    private CharacterMenu.InventoryAction[] _action;
    public int ItemIndex;
    public InventoryMenu(Character player, CharacterMenu.InventoryAction[] action)
    {
        _character = player;
        _state = State.Character;
        _action = action;

    }
    public void Show()
    {
        _character.Inventory.ShowInventory();
    }

    public void HandleInput()
    {
        var count = _character.Inventory.Bag.Count;
        var input = Menu.GetNumFromUser(count + 1);

        if (input == count + 1)
        {
            _action[0](0);
            return;
        }

        ItemIndex = input - 1;
        _action[1](ItemIndex);
        Show();
    }
    
    public State UpdateState()
    {
        return _state;
    }
}

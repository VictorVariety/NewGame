using NewGame.Interface;

namespace NewGame.Scene;

public enum StatState
{
    Menu, Stats, Bag, Item
} 
public class CharacterMenu : IScene
{
    private Character _character;
    private IScene _menu;
    private State _state;
    private StatState _statState;
    private StatState _currentState;
    private int _itemIndex;
    private Menu.MenuOption _exit;

    public delegate void InventoryAction(int index);


    public CharacterMenu(Character character, Menu.MenuOption exit)
    {
        _character = character;
        _menu = new Menu($"{_character.Name} the {_character.Class}\n",
            new Menu.MenuOption[]{ ShowStats, ShowInventory, Exit }, 
            new string[] { "Stats", "Inventory", "Exit" },
            new char[]{ '1', '2', '3' },
            _state);
        _state = State.Character;
        _statState = StatState.Menu;
        _currentState = StatState.Menu;
        _exit = exit;

    }

    public void Show()
    {
        _menu.Show();
    }

    public void HandleInput()
    {

        if (_statState == _currentState) 
        {
            _menu.Show();
            _menu.HandleInput();

            if (_menu is InventoryMenu inventoryMenu)
            {
                _itemIndex = inventoryMenu.ItemIndex;
            }

            return;
        }


        _currentState = _statState;
        switch (_currentState)
        {
            case StatState.Menu:

                _menu = 
                    new Menu("",
                    new Menu.MenuOption[] { ShowStats, ShowInventory, Exit },
                    new string[] { "Stats", "Inventory", "Exit" },
                    new char[] { '1', '2', '3' },
                    _state);
                break;

            case StatState.Stats:

                _menu = 
                    new Menu(_character.ShowCharacterStats(),
                    new Menu.MenuOption[] { ShowMenu, ShowInventory, Exit },
                    new string[] { "Back.","Inventory", "Exit" },
                    new char[] { '1', '2', '3' },
                    _state);
                break;

            case StatState.Bag:
                _menu = new InventoryMenu(_character, new InventoryAction[]{ ExitInventory, ItemChoiceIndexForItemMenu });
                break;

            case StatState.Item:

                var item = _character.Inventory.Bag[_itemIndex];

                void RemoveItemGroup(int index)
                {
                    _character.RemoveItem(index);
                    ShowInventory();
                }

                if (item is Item consumable)
                {
                    void ConsumeItemGroup(int index)
                    {
                        _character.ConsumeItem(index);
                        ShowInventory();
                    }

                    _menu = new ItemMenu($"{consumable.Name}",
                        new[] { (Inventory.ItemAction)ConsumeItemGroup, RemoveItemGroup, ShowInventory },
                        new[] { $"{consumable.FindVerb(consumable)}", "Drop", "Back"},
                        new[] {'1', '2', '3'},
                        State.Character);
                }
                else
                {
                    var weapon = (Weapon)item;

                    void EquipWeaponGroup(int index)
                    {
                        _character.EquipWeapon(index);
                        ShowInventory();
                    }

                    _menu = new ItemMenu($"{weapon.Name}",
                        new[] { (Inventory.ItemAction)EquipWeaponGroup, RemoveItemGroup, ShowInventory },
                        new[] { "Equip", "Drop", "Back" },
                        new[] { '1', '2', '3' },
                        State.Character);
                }
                break;
        }

        
    }

    private void ExitInventory(int index)
    {
        _statState = StatState.Menu;
    }
    private void ItemChoiceIndexForItemMenu(int index)
    {
        _itemIndex = index;
        _statState = StatState.Item;
    }

    public State UpdateState()
    {
        return _state;
    }

    public void ShowStats()
    {
        _statState = StatState.Stats;
    }    
    public void ShowInventoryAfterItem()
    {
        Console.WriteLine();
        Console.WriteLine("Any button to continue.");
        Console.ReadKey();
        Console.Clear();

        _statState = StatState.Bag;
    }
    public void ShowInventory()
    {
        _statState = StatState.Bag;
    }
    public void ShowInventory(int i)
    {
        _statState = StatState.Bag;
    }    
    public void ShowMenu()
    {
        _statState = StatState.Menu;
    }
    public void ShowItem()
    {
        _statState = StatState.Item;
    }

    public void Exit()
    {
        _exit();
    }
}
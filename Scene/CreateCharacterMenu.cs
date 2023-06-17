namespace NewGame.Scene;

public class CreateCharacterMenu : Interface.IScene
{
    private readonly Menu _menu;
    private string _name;
    private bool _nameExists;
    private State _state;
    private CharacterCreation _creation;

    public delegate void CharacterCreation(string name, string type);
    public CreateCharacterMenu(CharacterCreation creation)
    {
        _name = "";
        _nameExists = false;
        _state = State.CreateCharacter;
        _menu = new Menu("You're a ", 
                new Menu.MenuOption[] { Warrior, Mage, Rogue }, 
                new[] { "Warrior", "Mage", "Rogue" }, 
                new[] { '1', '2', '3' },
                State.CreateCharacter);
        _creation = creation;
    }

    private void AskForName()
    {
        while (string.IsNullOrWhiteSpace(_name))
        {
            _name = Console.ReadLine();
        }
        _nameExists = true;
    }

    public void Show()
    {
        if (_nameExists)
        {
            _menu.Show();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("What's your name?");
        }
    }

    public void HandleInput()
    {
        if( _nameExists ) _menu.HandleInput();

        else AskForName();

    }

    public State UpdateState()
    {
        return _state;
    }


    private void Warrior()
    {
        _creation(_name, "Warrior");
        _state = State.MainMenu;
    }

    private void Mage()
    {
        _creation(_name, "Mage");
        _state = State.MainMenu;
    }

    private void Rogue()
    {
        _creation(_name, "Rogue");
        _state = State.MainMenu;
    }
}
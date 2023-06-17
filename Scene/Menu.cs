namespace NewGame.Scene;

public class Menu : Interface.IScene
{
    public string MenuText { get; set; }

    private readonly MenuOption[] _menuOption = null!; 
    protected string[] _choices = null!;
    protected char[] _choiceChars = null!;
    protected State _state;

    public delegate void MenuOption();

    public Menu(string menuText, MenuOption[] options, string[] choices, char[] choiceChars, State state)
    {
        MenuText = menuText;
        _menuOption = options;
        _choices = choices;
        _choiceChars = choiceChars;
        _state = state;
    }




    public void Show()
    {
        Console.Clear();
        Console.WriteLine(MenuText);
        if (_choices.Length != 0)
        {
            for (var i = 0; i < _choices.Length; i++)
            {
                Console.WriteLine($"{_choiceChars[i]}. {_choices[i]}");
            }
        }
    }

    public virtual void HandleInput()
    {
        while (true)
        {
            var input = Console.ReadKey();
            for (int i = 0; i < _choiceChars.Length; i++)
            {
                if (input.KeyChar == _choiceChars[i])
                {
                    _menuOption[i]();
                    return;
                }
            }
            Console.Clear();
            Show();
        }

    }

    public State UpdateState()
    {
        return _state;
    }

    public static void AnyButtonToContinue()
    {
        Console.WriteLine();
        Console.WriteLine("Press any button to continue..");
        Console.ReadKey();
        Console.Clear();
    }

    public static int GetNumFromUser(int maxChoice)
    {
        var topCursorPosition = Console.CursorTop;
        ConsoleKeyInfo keyInfo;
        do
        {
            keyInfo = Console.ReadKey();
            if (keyInfo.Key == ConsoleKey.Enter)
            {
                Console.WriteLine(); // Move to the next line
                Console.SetCursorPosition(0, topCursorPosition); // Move the cursor back to the top
                Console.Write("Invalid choice. Please try again: "); // Overwrite the line
            }
            else
            {
                if (int.TryParse(keyInfo.KeyChar.ToString(), out var choice) && choice >= 1 && choice <= maxChoice)
                {
                    Console.Clear();
                    Console.WriteLine(); // Move to the next line
                    return choice;
                }
            }
        } while (true);
    }
}
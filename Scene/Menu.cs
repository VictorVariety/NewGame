namespace NewGame.Scene;

public class Menu : Interface.IScene
{
    public string MenuText { get; set; }

    private readonly MenuOption[] _menuOption = null!; 
    protected string[] Choices = null!;
    protected char[] ChoiceChars = null!;
    protected State State;

    public delegate void MenuOption();

    public Menu(string menuText, MenuOption[] options, string[] choices, char[] choiceChars, State state)
    {
        MenuText = menuText;
        _menuOption = options;
        Choices = choices;
        ChoiceChars = choiceChars;
        State = state;
    }




    public void Show()
    {
        Console.Clear();
        Console.WriteLine(MenuText);
        if (Choices.Length == 0) return;
        for (var i = 0; i < Choices.Length; i++)
        {
            Console.WriteLine($"{ChoiceChars[i]}. {Choices[i]}");
        }
    }

    public virtual void HandleInput()
    {
        while (true)
        {
            var input = Console.ReadKey();
            for (int i = 0; i < ChoiceChars.Length; i++)
            {
                if (input.KeyChar == ChoiceChars[i])
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
        return State;
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
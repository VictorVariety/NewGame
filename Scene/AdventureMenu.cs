using NewGame.Interface;

namespace NewGame.Scene;

public class AdventureMenu : IScene
{
    private string _menuText;
    private EncounterOption[] _options;
    private string[] _choices;
    private char[] _choiceChars;

    public delegate void EncounterOption();

    public AdventureMenu(EncounterOption[] options, string[] choices, char[] choiceChars)
    {
        _menuText = "Where does your adventure take you?";
        _options = options;
        _choices = choices;
        _choiceChars = choiceChars;
    }
    public void Show()
    {
        Console.Clear();
        Console.WriteLine(_menuText);
        if (_choices.Length == 0) return;
        for (var i = 0; i < _choices.Length; i++)
        {
            Console.WriteLine($"{_choiceChars[i]}. {_choices[i]}");
        }
    }

    public void HandleInput()
    {
        while (true)
        {
            var input = Console.ReadKey();
            for (int i = 0; i < _choiceChars.Length; i++)
            {
                if (input.KeyChar == _choiceChars[i])
                {
                    _options[i]();
                    return;
                }
            }
            Console.Clear();
            Show();
        }
    }

    public State UpdateState()
    {
        throw new NotImplementedException();
    }
}
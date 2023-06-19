using NewGame.Interface;

namespace NewGame.Scene;

public class EncounterMenu : IScene
{
    private Encounters _encounter;
    private Character _character;

    

    public EncounterMenu(Character character, Encounters encounter)
    {
        _character = character;
        _encounter = encounter;
    }
    public void Show()
    {
        Console.WriteLine(_encounter.EncounterText);
        Console.WriteLine();
        Console.WriteLine("Any button to continue");
        Console.ReadKey();
        Console.Clear();

        if (_encounter.Enemy != null)
        {
            var fight = new Fight();
            fight.Combat(_character, _encounter.Enemy);
        }

        if (_encounter.Item != null)
        {

        }
    }

    public void HandleInput()
    {
        throw new NotImplementedException();
    }

    public State UpdateState()
    {
        throw new NotImplementedException();
    }
}
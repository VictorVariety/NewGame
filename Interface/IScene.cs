using NewGame.Scene;

namespace NewGame.Interface;

public interface IScene
{
    void Show();
    void HandleInput();
    State UpdateState();
}
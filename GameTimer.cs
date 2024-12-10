using Microsoft.Xna.Framework;
namespace spacewar;

public class GameTimer
{
    public float Elapsed { get; private set; } = 0f;
    private bool on = false;
    public void StartTimer(GameTime gameTime)
    {
        if (on)
        {
            Elapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        else
        {
            Elapsed = 0f;
            on = true;
        }
    }

    public bool Passed(float seconds)
    {
        if (Elapsed >= seconds) return true;
        else return false;
    }

    public void Clear()
    {
        Elapsed = 0f;
        on = false;
    }

}

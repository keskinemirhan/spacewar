using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace spacewar;
public class AnimatedTexture
{
    private int frameCount;
    private Texture2D myTexture;
    private float timePerFrame;
    public int Frame { get; private set; }
    private float totalElapsed;
    private bool isPaused;
    private bool loop;
    public float Rotation, Scale, Depth;
    public Vector2 Origin;

    public AnimatedTexture(Vector2 origin, Texture2D asset, float rotation, float scale, float depth, int frameCount, int framesPerSec, bool loop = true)
    {
        this.Origin = new Vector2(origin.X / frameCount, origin.Y);
        this.Rotation = rotation;
        this.Scale = scale;
        this.Depth = depth;
        this.frameCount = frameCount;
        this.loop = loop;
        timePerFrame = (float)1 / framesPerSec;
        Frame = 0;
        totalElapsed = 0;
        isPaused = false;
        myTexture = asset;
    }

    public void UpdateFrame(float elapsed)
    {
        if (isPaused)
            return;
        totalElapsed += elapsed;
        if (totalElapsed > timePerFrame)
        {
            Frame++;
            if (loop) Frame %= frameCount;
            else if (Frame >= frameCount) Frame = frameCount;
            totalElapsed -= timePerFrame;
        }
    }

    public void DrawFrame(SpriteBatch batch, Vector2 screenPos)
    {
        if (Frame < frameCount) DrawFrame(batch, Frame, screenPos);
    }

    public void DrawFrame(SpriteBatch batch, int frame, Vector2 screenPos)
    {
        int FrameWidth = myTexture.Width / frameCount;
        Rectangle sourcerect = new Rectangle(FrameWidth * frame, 0,
            FrameWidth, myTexture.Height);
        batch.Draw(myTexture, screenPos, sourcerect, Color.White,
            Rotation, Origin, Scale, SpriteEffects.None, Depth);
    }

    public bool IsPaused
    {
        get { return isPaused; }
    }

    public void Reset()
    {
        Frame = 0;
        totalElapsed = 0f;
    }

    public void Stop()
    {
        Pause();
        Reset();
    }

    public void Play()
    {
        isPaused = false;
    }

    public void Pause()
    {
        isPaused = true;
    }
}


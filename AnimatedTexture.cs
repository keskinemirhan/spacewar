using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace spacewar;
public class AnimatedTexture
{
    public int FrameCount { get; private set; }
    public int CurrentFrame { get; private set; }
    public float CurrentFrameRatio
    {
        get
        {
            return (float)(CurrentFrame + 1) / (float)FrameCount;
        }
    }
    public bool IsLastFrame
    {
        get
        {
            return CurrentFrame == FrameCount - 1;
        }
    }
    public bool IsFirstFrame
    {
        get
        {
            return CurrentFrame == 0;
        }
    }
    public bool AnimationEnded { get; private set; }
    public bool IsPaused { get; private set; }
    public bool Loop { get; private set; }
    public bool Hidden { get; private set; }
    public float Rotation, Scale, Depth;
    public Vector2 Origin;

    private float totalElapsed;
    private Texture2D texture;
    private float timePerFrame;

    public AnimatedTexture(Vector2 origin, Texture2D asset, float rotation, float scale, float depth, int frameCount, int framesPerSec, bool loop = true)
    {
        this.Origin = new Vector2(origin.X / frameCount, origin.Y);
        this.Rotation = rotation;
        this.Scale = scale;
        this.Depth = depth;
        this.FrameCount = frameCount;
        this.Loop = loop;
        timePerFrame = (float)1 / framesPerSec;
        CurrentFrame = 0;
        totalElapsed = 0;
        IsPaused = false;
        texture = asset;
        AnimationEnded = false;
        Hidden = false;
    }

    public void UpdateFrame(float elapsed)
    {
        if (IsPaused)
            return;
        totalElapsed += elapsed;
        if (totalElapsed > timePerFrame)
        {
            CurrentFrame++;
            if (Loop) CurrentFrame %= FrameCount;
            else if (CurrentFrame >= FrameCount)
            {
                CurrentFrame = FrameCount - 1;
                AnimationEnded = true;
            }
            totalElapsed -= timePerFrame;
        }
    }

    public void DrawFrame(SpriteBatch batch, Vector2 screenPos)
    {
        if (!Hidden) DrawFrame(batch, CurrentFrame, screenPos);
    }

    public void DrawFrame(SpriteBatch batch, int frame, Vector2 screenPos)
    {
        int FrameWidth = texture.Width / FrameCount;
        Rectangle sourcerect = new Rectangle(FrameWidth * frame, 0,
            FrameWidth, texture.Height);
        batch.Draw(texture, screenPos, sourcerect, Color.White,
            Rotation, Origin, Scale, SpriteEffects.None, Depth);
    }

    public void Reset()
    {
        AnimationEnded = false;
        CurrentFrame = 0;
        totalElapsed = 0f;
    }

    public void Stop()
    {
        Pause();
        Reset();
    }

    public void Play()
    {
        AnimationEnded = false;
        IsPaused = false;
    }

    public void Pause()
    {
        IsPaused = true;
    }

    public void Hide()
    {
        Hidden = true;
    }

    public void Show()
    {
        Hidden = false;
    }

    public void SwitchLoop(bool loop)
    {
        Loop = loop;
    }
}


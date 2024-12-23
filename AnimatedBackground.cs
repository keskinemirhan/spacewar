using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace spacewar;
public class AnimatedBackground
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
    private GraphicsDeviceManager device;
    private int frameWidth;
    private int frameHeight;

    public AnimatedBackground(GraphicsDeviceManager device, float scale, Texture2D asset, int frameCount, int framesPerSec)
    {
        this.Rotation = 0;
        this.Scale = scale;
        this.Depth = 0.5f;
        this.frameCount = frameCount;
        this.loop = true;
        timePerFrame = (float)1 / framesPerSec;
        Frame = 0;
        totalElapsed = 0;
        isPaused = false;
        myTexture = asset;
        this.device = device;
        this.frameWidth = asset.Width / frameCount;
        this.frameHeight = asset.Height;
        this.Origin = new Vector2(frameWidth / 2, frameHeight / 2);
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

    public void DrawFrame(SpriteBatch batch)
    {
        if (Frame < frameCount) DrawFrame(batch, Frame);
    }

    public void DrawFrame(SpriteBatch batch, int frame)
    {
        int FrameWidth = myTexture.Width / frameCount;
        Rectangle sourcerect = new Rectangle(FrameWidth * frame, 0,
            FrameWidth, myTexture.Height);
        var random = new System.Random(31);
        for (int i = 0; i <= device.PreferredBackBufferWidth; i += (int)(frameWidth * Scale))
        {
            for (int j = 0; j <= device.PreferredBackBufferHeight; j += (int)(frameHeight * Scale))
            {
                var rot = random.NextInt64(3);
                batch.Draw(myTexture, new Vector2(i, j), sourcerect, Color.White,
                    ((float)rot) * MathHelper.Pi / 2, Origin, Scale, SpriteEffects.None, Depth);
            }
        }
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


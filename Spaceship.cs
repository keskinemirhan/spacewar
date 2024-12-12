using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace spacewar;

abstract class Spaceship : IContext
{
    public int Health { get; private set; }
    public int Damage { get; private set; }
    public SpaceshipAssets Assets { get; private set; }

    protected Texture2D currentTexture;
    public Vector2 position;
    protected Weapon weapon;
    protected static GraphicsDeviceManager device;
    protected float speed;
    protected float maxSpeed;
    protected float acceleration;
    protected float deceleration;
    public float direction;
    protected float scale;
    protected Vector2 origin;


    protected Spaceship(Vector2 origin, Vector2 position,
            float scale, float direction, float speed, float maxSpeed, float acceleration, float deceleration, Weapon weapon, SpaceshipAssets assets)
    {
        this.origin = origin;
        this.position = position;
        this.direction = direction;
        this.speed = speed;
        this.maxSpeed = maxSpeed;
        this.scale = scale;
        this.acceleration = acceleration;
        this.deceleration = deceleration;
        this.weapon = weapon ?? throw new ArgumentNullException(nameof(weapon));
        this.Assets = assets ?? throw new ArgumentNullException(nameof(assets));
        currentTexture = Assets.Full;
    }

    public void Shoot(GameTime gameTime)
    {
        this.weapon.Shoot(gameTime);
    }

    public int TakeDamage(int amount)
    {
        this.Health -= amount;
        if (this.Health < 0) this.Health = 0;
        return this.Health;
    }

    public virtual void Move(float direction, float elapsed)
    {
        speed += elapsed * acceleration - elapsed * deceleration;
        if (speed < 0) speed = 0;
        else if (speed >= maxSpeed) speed = maxSpeed;
        var speedIncrease = elapsed * speed;
        position.Y -= (float)Math.Sin(direction + 2 * MathHelper.Pi / 4) * speedIncrease;
        position.X -= (float)Math.Cos(direction + 2 * MathHelper.Pi / 4) * speedIncrease;
    }

    public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        weapon.Position = position;
        weapon.Direction = direction;
        weapon.Draw(spriteBatch, gameTime);

        spriteBatch.Draw(
                currentTexture,
                position,
                null,
                Color.White,
                direction,
                origin,
                scale,
                SpriteEffects.None,
                0f
                );

    }

    public virtual void Update(GameTime gameTime)
    {
        weapon.Update(gameTime);
        this.Move(direction, (float)gameTime.ElapsedGameTime.TotalSeconds);
    }

    public static void LoadContent(ContentManager content, GraphicsDeviceManager device)
    {
        throw new System.NotImplementedException();
    }
}

public class SpaceshipAssets
{
    public Texture2D Full;
    public Texture2D SlightDamage;
    public Texture2D Damaged;
    public Texture2D VeryDamaged;
}

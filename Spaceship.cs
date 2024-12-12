using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace spacewar;

abstract class Spaceship : GameObject
{
    public int Health { get; private set; }
    public int Damage { get; private set; }
    public SpaceshipAssets Assets { get; private set; }

    protected Texture2D currentTexture;
    protected Weapon weapon;

    protected Spaceship(Vector2 origin, Vector2 position, float scale,
            float direction, float speed, float maxSpeed, float acceleration, float deceleration, float collisionRange, Weapon weapon, SpaceshipAssets assets)
        : base(origin, position, scale, direction, speed, maxSpeed, acceleration, deceleration, collisionRange)
    {
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
        Speed += elapsed * Acceleration - elapsed * Deceleration;
        if (Speed < 0) Speed = 0;
        else if (Speed >= MaxSpeed) Speed = MaxSpeed;
        var speedIncrease = elapsed * Speed;
        Position.Y -= (float)Math.Sin(direction + 2 * MathHelper.Pi / 4) * speedIncrease;
        Position.X -= (float)Math.Cos(direction + 2 * MathHelper.Pi / 4) * speedIncrease;
    }

    public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        weapon.Position = Position;
        weapon.Direction = Direction;
        weapon.Draw(spriteBatch, gameTime);

        spriteBatch.Draw(
                currentTexture,
                Position,
                null,
                Color.White,
                Direction,
                Origin,
                Scale,
                SpriteEffects.None,
                0f
                );

    }

    public override void Update(GameTime gameTime)
    {
        weapon.Update(gameTime);
        this.Move(Direction, (float)gameTime.ElapsedGameTime.TotalSeconds);
    }
}

public class SpaceshipAssets
{
    public Texture2D Full;
    public Texture2D SlightDamage;
    public Texture2D Damaged;
    public Texture2D VeryDamaged;
}

using Microsoft.Xna.Framework;

namespace spacewar;

abstract class EnemyShip : Spaceship
{
    public Vector2 PlayerPosition;
    public int ExperiencePrize { get; private set; }

    protected EnemyShip(Vector2 origin, Vector2 spawn,
            float scale, float direction, float speed, float maxSpeed, float acceleration, float deceleration, float collisionRange, int health, int experiencePrize, Weapon weapon, SpaceshipAssets assets)
         : base(origin, spawn, scale, direction, speed, maxSpeed, acceleration, deceleration, collisionRange, health, weapon, assets)

    {
        this.ExperiencePrize = experiencePrize;
    }

    protected abstract void Attack(GameTime gameTime);

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        this.Attack(gameTime);
    }
}

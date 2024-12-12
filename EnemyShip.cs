using Microsoft.Xna.Framework;

namespace spacewar;

abstract class EnemyShip : Spaceship
{
    public Vector2 PlayerPosition;

    protected EnemyShip(Vector2 origin, Vector2 spawn,
            float scale, float direction, float speed, float maxSpeed, float acceleration, float deceleration, Weapon weapon, SpaceshipAssets assets)
         : base(origin, spawn, scale, direction, speed, maxSpeed, acceleration, deceleration, 0, weapon, assets)
    {
    }

    protected abstract void Attack(GameTime gameTime);

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        this.Attack(gameTime);
    }
}

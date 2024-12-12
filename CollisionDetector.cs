using System;
namespace spacewar;

public static class CollisionDetector
{
    public static bool CheckCollision(GameObject obj1, GameObject obj2)
    {
        if ((float)Math.Sqrt(Math.Pow((double)(obj1.Position.X - obj2.Position.X), 2) + Math.Pow((double)(obj1.Position.Y - obj2.Position.Y), 2)) <= (double)(obj1.CollisionRange + obj2.CollisionRange))
        {
            return true;
        }
        return false;
    }
}


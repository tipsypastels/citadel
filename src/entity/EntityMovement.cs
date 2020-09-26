using Godot;

public class EntityMovement
{
  public static class Isometrics
  {
    public static Vector2 FromCartesian(Vector2 vec)
    {
      return new Vector2(vec.x - vec.y, (vec.x + vec.y) / 2);
    }
  }

  int Speed;
  int BaseFriction;
  int BaseAcceleration;

  public EntityMovement(int speed, int friction, int acceleration)
  {
    Speed = speed;
    BaseFriction = friction;
    BaseAcceleration = acceleration;
  }

  public Vector2 Transform(Vector2 vel, Vector2 input, float delta)
  {
    var inputNorm = Isometrics.FromCartesian(input.Normalized());

    if (inputNorm != Vector2.Zero)
    {
      return vel.MoveToward(inputNorm * Speed, delta * BaseAcceleration);
    }
    else
    {
      return vel.MoveToward(Vector2.Zero, delta * BaseFriction);
    }
  }
}
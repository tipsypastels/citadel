using Godot;

public class PlayerEntity : KinematicBody2D
{
  Vector2 Velocity = Vector2.Zero;

  static EntityMovement Walk = new EntityMovement(100, 1000, 500);
  static EntityMovement Run = new EntityMovement(200, 1600, 800);

  public bool IsInBlockingCutscene { get; private set; } = false;

  public override void _PhysicsProcess(float delta)
  {
    if (IsInBlockingCutscene)
    {
      return;
    }

    var input = GetInput();
    var movement = GetMovement();

    var newVelocity = movement.Transform(Velocity, input, delta);
    Velocity = MoveAndSlide(newVelocity);
  }

  public void StartBlockingCutscene()
  {
    IsInBlockingCutscene = true;
    Velocity = Vector2.Zero;
  }

  public void EndBlockingCutscene()
  {
    IsInBlockingCutscene = false;
  }

  Vector2 GetInput()
  {
    return new Vector2(
      GetStrength("world_move_right") - GetStrength("world_move_left"),
      GetStrength("world_move_down") - GetStrength("world_move_up")
    );
  }

  EntityMovement GetMovement()
  {
    if (Input.IsActionPressed("world_run"))
    {
      return Run;
    }

    return Walk;
  }

  float GetStrength(string key)
  {
    return Input.GetActionStrength(key);
  }
}

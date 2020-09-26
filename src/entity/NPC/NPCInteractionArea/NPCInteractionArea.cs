using Godot;

public class NPCInteractionArea : Area2D
{
  [Signal]
  private delegate void Interacted(
    Dialog dialog,
    PlayerEntity player
  );
}

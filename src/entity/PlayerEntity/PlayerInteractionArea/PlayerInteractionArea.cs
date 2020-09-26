using Godot;

public class PlayerInteractionArea : Area2D
{
  PlayerEntity Player;
  NPCInteractionArea FacingNPC;
  PackedScene DialogScene;
  Game Game;

  public override void _Ready()
  {
    Player = GetParent<PlayerEntity>();
    Game = GetNode<Game>("/root/Game");

    DialogScene = (PackedScene)ResourceLoader.Load(
      "res://src/dialog/Dialog.tscn"
    );
  }

  public override void _Process(float _delta)
  {
    if (Input.IsActionJustPressed("world_interact"))
    {
      if (!Player.IsInBlockingCutscene && FacingNPC != null)
      {
        var Dialog = (Dialog)DialogScene.Instance();

        Game.AddChild(Dialog);

        Player.StartBlockingCutscene();
        FacingNPC.EmitSignal("Interacted", Dialog, Player);
      }
    }
  }

  private void _OnInteractAreaEntered(NPCInteractionArea npc)
  {
    FacingNPC = npc;
  }

  private void _OnInteractAreaExited(NPCInteractionArea _npc)
  {
    FacingNPC = null;
  }
}

using Godot;

public class NPC : KinematicBody2D
{
  [Export(PropertyHint.MultilineText)] string Speech = "";
  string[] SpeechLines;

  public override void _Ready()
  {
    SpeechLines = Speech.Split('\n');
  }

  private async void _OnInteracted(Dialog dialog, PlayerEntity player)
  {
    for (int i = 0; i < SpeechLines.Length; i++)
    {
      await dialog.Msg(SpeechLines[i]);
    }

    dialog.QueueFree();
    player.EndBlockingCutscene();
  }
}

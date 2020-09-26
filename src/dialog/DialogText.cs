using Godot;
using System.Threading.Tasks;

public class DialogText : RichTextLabel
{
  Timer Timer;

  [Signal] private delegate void MessageChanged();

  public override void _Ready()
  {
    Timer = GetNode<Timer>("Timer");
    VisibleCharacters = 0;
  }

  public override void _Process(float _delta)
  {
    if (Input.IsActionJustPressed("world_interact"))
    {
      if (VisibleCharacters > GetTotalCharacterCount())
      {
        EmitSignal("MessageChanged");
      }
      else
      {
        VisibleCharacters = GetTotalCharacterCount();
      }
    }
  }

  public async Task Msg(string text)
  {
    ResetForNextMessage();
    BbcodeText = text;

    await ToSignal(this, "MessageChanged");
  }

  private void _OnTimerAdvance()
  {
    if (VisibleCharacters <= GetTotalCharacterCount())
    {
      VisibleCharacters += 1;
    }
    else
    {
      Timer.Stop();
    }
  }

  private void ResetForNextMessage()
  {
    VisibleCharacters = 0;
    Timer.Start();
  }
}



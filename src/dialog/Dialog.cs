using Godot;
using System.Threading.Tasks;

public class Dialog : Polygon2D
{
  DialogText Text;

  public override void _Ready()
  {
    Text = GetNode<DialogText>("DialogText");
  }

  public async Task Msg(string text)
  {
    await Text.Msg(text);
  }
}

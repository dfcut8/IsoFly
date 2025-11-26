using Godot;

public partial class Tile : Node2D
{
    public override void _Ready()
    {
        var finalPos = Position;
        Position = new Vector2(Position.X, Position.Y - 500);
        var tween = GetTree().CreateTween();
        tween.SetEase(Tween.EaseType.In);
        tween.SetTrans(Tween.TransitionType.Quart);
        tween.TweenProperty(this, "position", finalPos, 2f);
    }
}

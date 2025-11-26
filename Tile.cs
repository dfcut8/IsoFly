using Godot;

public partial class Tile : Node2D
{
    private Vector2 finalPosition;

    public override void _Ready()
    {
        finalPosition = Position;
        Position = new Vector2(Position.X, Position.Y - 1000);
    }

    public void PlayAnimation()
    {
        var tween = GetTree().CreateTween();
        tween.SetEase(Tween.EaseType.In);
        tween.SetTrans(Tween.TransitionType.Quart);
        tween.TweenProperty(this, "position", finalPosition, 2f);
    }
}

using Godot;

public partial class Main : Node2D
{
    [Export]
    private TileMapLayer tileMapLayer;

    public override void _Ready()
    {
        CallDeferred(MethodName.GetTileMapChildren);
    }

    private void GetTileMapChildren()
    {
        var tmlChildren = tileMapLayer.GetChildren();
        var delay = 0.0f;
        foreach (var child in tmlChildren)
        {
            if (child is Tile tile)
            {
                GetTree().CreateTimer(delay).Timeout += () => tile.PlayAnimation();
                delay += 0.1f;
            }
        }
    }
}

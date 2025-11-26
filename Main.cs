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

        // Sort children by their tilemap coordinates
        var sortedChildren = new System.Collections.Generic.List<Node>(tmlChildren);
        sortedChildren.Sort(
            (a, b) =>
            {
                var nodeA = a as Node2D;
                var nodeB = b as Node2D;
                var coordA = tileMapLayer.LocalToMap(nodeA.Position);
                var coordB = tileMapLayer.LocalToMap(nodeB.Position);

                // Sort by Y first, then X (reversed)
                int yCompare = coordB.Y.CompareTo(coordA.Y);
                return yCompare != 0 ? yCompare : coordB.X.CompareTo(coordA.X);
            }
        );

        var delay = 0.0f;
        foreach (var child in sortedChildren)
        {
            if (child is Tile tile)
            {
                GetTree().CreateTimer(delay).Timeout += () => tile.PlayAnimation();
                delay += 0.1f;
            }
        }
    }
}

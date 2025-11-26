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

        // Group children by Y coordinate
        var lineGroups = new System.Collections.Generic.Dictionary<
            int,
            System.Collections.Generic.List<Node>
        >();
        foreach (var child in tmlChildren)
        {
            var nodeChild = child as Node2D;
            var coord = tileMapLayer.LocalToMap(nodeChild.Position);

            if (!lineGroups.ContainsKey(coord.Y))
            {
                lineGroups[coord.Y] = new System.Collections.Generic.List<Node>();
            }
            lineGroups[coord.Y].Add(child);
        }

        // Sort lines by Y coordinate (descending)
        var sortedLines = new System.Collections.Generic.List<int>(lineGroups.Keys);
        sortedLines.Sort((a, b) => b.CompareTo(a));

        // Play animations for each line with delay between lines
        var lineDelay = 0.0f;
        foreach (var y in sortedLines)
        {
            var line = lineGroups[y];
            // Sort tiles in line by X coordinate (descending)
            line.Sort(
                (a, b) =>
                {
                    var nodeA = a as Node2D;
                    var nodeB = b as Node2D;
                    var coordA = tileMapLayer.LocalToMap(nodeA.Position);
                    var coordB = tileMapLayer.LocalToMap(nodeB.Position);
                    return coordB.X.CompareTo(coordA.X);
                }
            );

            foreach (var child in line)
            {
                if (child is Tile tile)
                {
                    GetTree().CreateTimer(lineDelay).Timeout += () => tile.PlayAnimation();
                }
            }
            lineDelay += 0.05f;
        }
    }
}

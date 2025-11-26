using Godot;

public partial class Main : Node2D
{
    [Export]
    private TileMapLayer tileMapLayer;

    [Export]
    private bool showCoordinateOverlay = true;

    [Export]
    private Color coordinateColor = Colors.White;

    [Export]
    private int coordinateFontSize = 14;

    [Export]
    private float fallDuration = 2.0f;

    [Export]
    private float fallHeight = 500.0f;

    private Font coordinateFont;

    public override void _Ready()
    {
        // Load default font - will use system font
        coordinateFont = new SystemFont();

        // Get all tiles in the tilemap and print their names
        GetTileScenes();
    }

    private void GetTileScenes()
    {
        if (tileMapLayer == null || tileMapLayer.TileSet == null)
            return;

        // Get all used cells
        var usedCells = tileMapLayer.GetUsedCells();
        GD.Print($"Total tiles in tilemap: {usedCells.Count}");

        foreach (Vector2I cellCoords in usedCells)
        {
            // Get the source ID for this cell
            int sourceId = tileMapLayer.GetCellSourceId(cellCoords);

            // Get the source from the tileset
            TileSetSource source = tileMapLayer.TileSet.GetSource(sourceId);

            // Check if it's a TileSetScenesCollectionSource (which contains scenes)
            // if (source is TileSetScenesCollectionSource sceneSource)
            // {
            //     int altId = tileMapLayer.GetCellAlternativeTile(cellCoords);
            //     // The assigned PackedScene.
            //     PackedScene scene = sceneSource.GetSceneTileScene(altId);
            //     GD.Print(scene);
            // }
        }
    }
}

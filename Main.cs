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
    private int coordinateFontSize = 16;

    private Font coordinateFont;
    private Vector2 mousePos;

    public override void _Ready()
    {
        // Load default font - will use system font
        coordinateFont = new SystemFont();
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion mouseMotion)
        {
            mousePos = mouseMotion.Position;
            QueueRedraw();
        }
    }

    public override void _Draw()
    {
        if (!showCoordinateOverlay || tileMapLayer == null)
            return;

        // Get the tile coordinates at the mouse position
        Vector2I tileCoords = tileMapLayer.LocalToMap(GetLocalMousePosition());

        // Draw the coordinate text at the mouse position with offset
        string coordText = $"({tileCoords.X}, {tileCoords.Y})";
        Vector2 drawPos = mousePos + new Vector2(10, -20);

        // Draw background rectangle for better readability
        Vector2 textSize = coordinateFont.GetStringSize(
            coordText,
            HorizontalAlignment.Left,
            -1,
            coordinateFontSize
        );
        Rect2 bgRect = new Rect2(drawPos - new Vector2(5, 5), textSize + new Vector2(10, 10));
        DrawRect(bgRect, new Color(0, 0, 0, 0.7f));

        // Draw the coordinate text
        DrawString(
            coordinateFont,
            drawPos,
            coordText,
            HorizontalAlignment.Left,
            -1,
            coordinateFontSize,
            coordinateColor
        );
    }
}

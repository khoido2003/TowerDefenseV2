using UnityEngine;

public class BuildCell : MonoBehaviour, IGridCell
{
    public Vector2 gridPosition;
    private GridSystem<BuildCell> gridSystem;
    private bool isOccupied;

    public BuildCell(GridSystem<BuildCell> gridSystem, Vector2 gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
    }

    public string DebugTextDisplay()
    {
        return "x=" + gridPosition.x + ", " + "y=" + gridPosition.y;
    }

    public bool GetIsOccupied()
    {
        return isOccupied;
    }

    public void SetIsOccupied(bool isOccupied)
    {
        this.isOccupied = isOccupied;
    }
}

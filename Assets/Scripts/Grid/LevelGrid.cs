using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public static LevelGrid Instance { get; private set; }

    [SerializeField]
    private int width = 10;

    [SerializeField]
    private int height = 10;

    [SerializeField]
    private float cellSize = 2f;

    [SerializeField]
    private Transform buildCellPrefab;

    [SerializeField]
    private LayerMask obstackleLayerMask;

    private GridSystem<BuildCell> buildGrid;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one LevelGrid instance!");
        }

        Instance = this;

        buildGrid = new GridSystem<BuildCell>(
            width,
            height,
            cellSize,
            (GridSystem<BuildCell> g, Vector2 gridPosition) =>
            {
                return new BuildCell(g, gridPosition);
            }
        );

        // DEBUG MODE
        // buildGrid.ShowGridDebugObject(buildCellPrefab);

        Setup();
    }

    private void Setup()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector2 gridPosition = new Vector2(x, z);

                Vector3 worldPosition = buildGrid.GetWorldPosition(gridPosition);

                // From the underground, shoot a ray to find if there is obstackle on top of the grid position
                float raycastOffsetDistance = 5f;
                if (
                    Physics.Raycast(
                        worldPosition + Vector3.down * raycastOffsetDistance,
                        Vector3.up,
                        raycastOffsetDistance * 2,
                        obstackleLayerMask
                    )
                )
                {
                    BuildCell buildCell = GetBuildCellAtPosition(x, z);
                    buildCell.SetIsOccupied(true);
                }
            }
        }
    }

    public BuildCell GetBuildCellAtPosition(int x, int z)
    {
        return buildGrid.GetGridCell(x, z);
    }

    public Vector3 GetSnappedWorldPosition(Vector3 worldPosition)
    {
        Vector2 gridPosition = buildGrid.GetGridPosition(worldPosition);
        return buildGrid.GetWorldPosition(gridPosition);
    }

    public BuildCheckResult CanBuildAtGridPosition(Vector3 worldPosition)
    {
        TowerTypeSO activeTowerType = TowerManager.Instance.GetActiveTowerType();
        Vector2Int footprintSize = activeTowerType.footPrintSize;

        List<Vector2Int> footprintCells = buildGrid.GetFootPrintCells(worldPosition, footprintSize);

        foreach (var cellPos in footprintCells)
        {
            if (cellPos.x < 0 || cellPos.y < 0 || cellPos.x > width || cellPos.y >= height)
            {
                return new BuildCheckResult(false, "Out of bounds");
            }

            if (buildGrid.GetGridCell(cellPos.x, cellPos.y).GetIsOccupied())
            {
                return new BuildCheckResult(false, $"Cell {cellPos} already occupied!");
            }
        }
        return new BuildCheckResult(true, "");
    }

    public void SetOccupied(Vector3 worldPosition, bool occupied)
    {
        Vector2 gridPos = buildGrid.GetGridPosition(worldPosition);
        BuildCell cell = buildGrid.GetGridCell((int)gridPos.x, (int)gridPos.y);
        cell.SetIsOccupied(occupied);
    }

    public GridSystem<BuildCell> GetBuildGrid()
    {
        return buildGrid;
    }

    public float GetCellSize()
    {
        return cellSize;
    }

    public int GetHeight()
    {
        return height;
    }

    public int GetWidth()
    {
        return width;
    }

    public Vector3 GetWorldPosition(Vector2 gridPosition)
    {
        return buildGrid.GetWorldPosition(gridPosition);
    }

    public Vector2 GetGridPosition(Vector3 worldPosition)
    {
        return buildGrid.GetGridPosition(worldPosition);
    }
}

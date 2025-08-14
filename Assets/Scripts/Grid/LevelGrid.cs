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

    public bool CanBuildAtGridPosition(Vector3 worldPosition)
    {
        Vector2 gridPosition = buildGrid.GetGridPosition(worldPosition);

        if (
            gridPosition.x < 0
            || gridPosition.x >= width
            || gridPosition.y < 0
            || gridPosition.y >= height
        )
        {
            return false;
        }

        BuildCell cell = buildGrid.GetGridCell((int)gridPosition.x, (int)gridPosition.y);

        return !cell.GetIsOccupied();
    }

    public void SetOccupied(Vector3 worldPosition, bool occupied)
    {
        Vector2 gridPos = buildGrid.GetGridPosition(worldPosition);
        BuildCell cell = buildGrid.GetGridCell((int)gridPos.x, (int)gridPos.y);
        cell.SetIsOccupied(occupied);
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

using System;
using UnityEngine;

public class GridSystem<TGridCell>
    where TGridCell : IGridCell
{
    private int width;
    private int height;
    private float cellSize;

    private TGridCell[][] gridCellArray;

    public GridSystem(
        int width,
        int height,
        float cellSize,
        Func<GridSystem<TGridCell>, Vector2, TGridCell> createGridCellFunc
    )
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridCellArray = new TGridCell[width][];

        for (int x = 0; x < width; x++)
        {
            gridCellArray[x] = new TGridCell[height];

            for (int z = 0; z < height; z++)
            {
                Vector2 gridPosition = new Vector2(x, z);

                gridCellArray[x][z] = createGridCellFunc(this, gridPosition);
            }
        }
    }

    public Vector3 GetWorldPosition(Vector2 gridPosition)
    {
        return new Vector3(gridPosition.x, 0, gridPosition.y) * cellSize;
    }

    public Vector2 GetGridPosition(Vector3 worldPosition)
    {
        return new Vector2(
            Mathf.RoundToInt(worldPosition.x / cellSize),
            Mathf.RoundToInt(worldPosition.z / cellSize)
        );
    }

    public TGridCell GetGridCell(int x, int z)
    {
        return gridCellArray[x][z];
    }

    public void ShowGridDebugObject(Transform gridDebugObjectPrefab)
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector2 gridPosition = new Vector2(x, z);

                Transform debugTransform = GameObject.Instantiate(
                    gridDebugObjectPrefab,
                    GetWorldPosition(gridPosition),
                    Quaternion.identity
                );
                GridDebugObject gridDebugObject = debugTransform.GetComponent<GridDebugObject>();

                gridDebugObject.SetGridCell(GetGridCell(x, z));
            }
        }
    }

    public int GetHeight()
    {
        return height;
    }

    public int GetWidth()
    {
        return width;
    }
}

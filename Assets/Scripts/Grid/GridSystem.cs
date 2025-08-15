using System;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem<TGridCell>
    where TGridCell : IGridCell
{
    private int width;
    private int height;
    private float cellSize;

    private TGridCell[][] gridCellArray;

    private List<Vector2Int> footPrintBuffers;

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
        footPrintBuffers = new();

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

    public Vector3 GetWorldCenteredStartPosition(
        Vector3 centerWorldPosition,
        Vector2Int footprintSize
    )
    {
        float halfCellX = (footprintSize.x - 1) / 2f;
        float halfCellY = (footprintSize.y - 1) / 2f;

        Vector3 offset = new Vector3(halfCellX * cellSize, 0, halfCellY * cellSize);

        return centerWorldPosition - offset;
    }

    public List<Vector2Int> GetFootPrintCells(Vector3 centerWorldPosition, Vector2Int footprintSize)
    {
        footPrintBuffers.Clear();

        Vector3 startPosition = GetWorldCenteredStartPosition(centerWorldPosition, footprintSize);

        Vector2 startGridPosition = GetGridPosition(startPosition);

        for (int x = 0; x < footprintSize.x; x++)
        {
            for (int z = 0; z < footprintSize.y; z++)
            {
                footPrintBuffers.Add(
                    new Vector2Int((int)startGridPosition.x + x, (int)startGridPosition.y + z)
                );
            }
        }

        return footPrintBuffers;
    }
}

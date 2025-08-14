using System;
using System.Collections.Generic;
using UnityEngine;

public class GridSytemVisual : MonoBehaviour
{
    public static GridSytemVisual Instance { get; private set; }

    private List<GridCellVisual> pooledGridCellVisuals = new();

    [SerializeField]
    private GridCellVisual gridCellVisualPrefab;

    [SerializeField]
    private int maxPoolCells;

    [SerializeField]
    private Material validMaterial;

    [SerializeField]
    private Material invalidMaterial;

    //////////////////////////////////////////////////////////////////////


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one GridSytemVisual!");
        }

        Instance = this;
    }

    private void Start()
    {
        InitPool();
    }

    private void InitPool()
    {
        for (int i = 0; i < maxPoolCells; i++)
        {
            GridCellVisual newCell = Instantiate(gridCellVisualPrefab, transform);
            newCell.Hide();
            pooledGridCellVisuals.Add(newCell);
        }
    }

    public void ShowFootPrint(Vector3 startWorldPosition, Vector2Int footprintSize)
    {
        HideAll();

        float cellSize = LevelGrid.Instance.GetCellSize();

        // Number of cells from the center to one edge (half size)
        float halfCellsX = (footprintSize.x - 1) / 2f;
        float halfCellsZ = (footprintSize.y - 1) / 2f;

        // Convert half cell counts into world space units
        float offsetX = halfCellsX * cellSize;
        float offsetZ = halfCellsZ * cellSize;

        // The final offset vector to shift the footprint so it's centered
        Vector3 footprintOffset = new Vector3(offsetX, 0, offsetZ);

        // Shift start position so the footprint is centered
        Vector3 centeredStartPosition = startWorldPosition - footprintOffset;

        int index = 0;

        for (int x = 0; x < footprintSize.x; x++)
        {
            for (int z = 0; z < footprintSize.y; z++)
            {
                // Only show the amount of grid visual needed of each building
                if (index >= pooledGridCellVisuals.Count)
                    return;

                Vector2 gridCellPosition = new Vector2(x, z);

                Vector3 gridCellWorldPosition = LevelGrid.Instance.GetSnappedWorldPosition(
                    centeredStartPosition + LevelGrid.Instance.GetWorldPosition(gridCellPosition)
                );

                bool canBuild = LevelGrid.Instance.CanBuildAtGridPosition(gridCellWorldPosition);

                pooledGridCellVisuals[index].transform.position = gridCellWorldPosition;
                pooledGridCellVisuals[index].Show(canBuild ? validMaterial : invalidMaterial);

                index++;
            }
        }
    }

    public void HideAll()
    {
        foreach (var cell in pooledGridCellVisuals)
        {
            cell.Hide();
        }
    }
}

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

        List<Vector2Int> cells = LevelGrid
            .Instance.GetBuildGrid()
            .GetFootPrintCells(startWorldPosition, footprintSize);

        BuildCheckResult result = LevelGrid.Instance.CanBuildAtGridPosition(startWorldPosition);

        int index = 0;

        foreach (var cellPos in cells)
        {
            if (index >= pooledGridCellVisuals.Count)
            {
                return;
            }

            Vector3 cellWorldPos = LevelGrid.Instance.GetWorldPosition(cellPos);
            pooledGridCellVisuals[index].transform.position = cellWorldPos;
            pooledGridCellVisuals[index].Show(result.canBuild ? validMaterial : invalidMaterial);

            index++;
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

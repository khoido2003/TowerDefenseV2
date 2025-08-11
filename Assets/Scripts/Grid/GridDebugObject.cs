using TMPro;
using UnityEngine;

public class GridDebugObject : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro textDisplay;

    [SerializeField]
    private Renderer quadRenderer;

    private IGridCell gridCell;

    private void Update()
    {
        textDisplay.SetText(gridCell.DebugTextDisplay());

        if (gridCell is BuildCell buildCell && buildCell.GetIsOccupied())
        {
            textDisplay.text += " + something here!";
        }
    }

    public void SetGridCell(IGridCell gridCell)
    {
        this.gridCell = gridCell;
    }
}

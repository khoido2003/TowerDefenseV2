using UnityEngine;

public class GridCellVisual : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer meshRenderer;

    public void Show(Material material)
    {
        meshRenderer.enabled = true;
        meshRenderer.material = material;
    }

    public void Hide()
    {
        if (meshRenderer != null)
        {
            meshRenderer.enabled = false;
        }
    }
}

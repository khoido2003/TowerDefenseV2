using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public static TowerManager Instance { get; private set; }

    [SerializeField]
    private TowerTypeSO activeTowerType;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one TowerManager Instance!");
        }

        Instance = this;
    }

    private void Update()
    {
        if (InputManager.Instance.GetLeftMouseClick())
        {
            Vector3 mouseWorldPos = MouseWorld.Instance.GetWorldPosition();
            Vector3 snappedPos = LevelGrid.Instance.GetSnappedWorldPosition(mouseWorldPos);

            if (LevelGrid.Instance.CanBuildAtGridPosition(snappedPos))
            {
                ConstructionTower.SpawnConstructionTower(snappedPos, activeTowerType);
                LevelGrid.Instance.SetOccupied(snappedPos, true);
            }
            else
            {
                Debug.Log("Can't build here!");
            }
        }
    }
}

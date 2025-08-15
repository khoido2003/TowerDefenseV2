using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerManager : MonoBehaviour
{
    public static TowerManager Instance { get; private set; }

    [SerializeField]
    private TowerTypeSO activeTowerType;

    public event EventHandler<OnActiveTowerTypeChangedArgs> OnActiveTowerTypeChanged;

    public class OnActiveTowerTypeChangedArgs
    {
        public TowerTypeSO activeTowerType;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one TowerManager Instance!");
        }

        Instance = this;
        SetActiveTowerType(activeTowerType);
    }

    private void Start() { }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            // Mouse is over UI, so ignore map building
            return;
        }

        if (InputManager.Instance.GetLeftMouseClick())
        {
            Vector3 mouseWorldPos = MouseWorld.Instance.GetWorldPosition();
            Vector3 snappedPos = LevelGrid.Instance.GetSnappedWorldPosition(mouseWorldPos);

            BuildCheckResult result = LevelGrid.Instance.CanBuildAtGridPosition(snappedPos);

            if (result.canBuild)
            {
                ConstructionTower.SpawnConstructionTower(snappedPos, activeTowerType);

                LevelGrid.Instance.SetOccupied(snappedPos, true);
            }
            else
            {
                Debug.Log($"Can't build here: {result.reason}");
            }
        }
    }

    public void SetActiveTowerType(TowerTypeSO activeTowerType)
    {
        this.activeTowerType = activeTowerType;

        // Trigger ghost building
        OnActiveTowerTypeChanged?.Invoke(
            this,
            new OnActiveTowerTypeChangedArgs { activeTowerType = activeTowerType }
        );
    }

    public TowerTypeSO GetActiveTowerType()
    {
        return activeTowerType;
    }
}

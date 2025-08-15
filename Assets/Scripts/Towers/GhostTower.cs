using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class GhostTower : MonoBehaviour
{
    public static GhostTower Instance { get; private set; }

    private TowerTypeSO activeTowerType;
    private Transform currentGhostInstance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one GhostTower Instance!");
        }

        Instance = this;
    }

    private void Start()
    {
        TowerManager.Instance.OnActiveTowerTypeChanged += TowerManager_OnActiveTowerTypeChanged;
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Hide();
            GridSytemVisual.Instance.HideAll();
            return;
        }

        if (currentGhostInstance != null)
        {
            Vector3 mouseWorldPos = MouseWorld.Instance.GetWorldPosition();
            currentGhostInstance.position = mouseWorldPos;

            GridSytemVisual.Instance.ShowFootPrint(mouseWorldPos, activeTowerType.footPrintSize);
        }
    }

    private void OnDisable()
    {
        GridSytemVisual.Instance.HideAll();
    }

    private void TowerManager_OnActiveTowerTypeChanged(
        object sender,
        TowerManager.OnActiveTowerTypeChangedArgs e
    )
    {
        if (e.activeTowerType == null)
        {
            Hide();
            return;
        }

        this.activeTowerType = e.activeTowerType;

        currentGhostInstance = Instantiate(
            activeTowerType.ghostTower,
            transform.position,
            Quaternion.identity
        );
        Show();
    }

    public void Hide()
    {
        if (currentGhostInstance != null)
        {
            currentGhostInstance.gameObject.SetActive(false);
        }
        GridSytemVisual.Instance.HideAll();
    }

    public void Show()
    {
        if (currentGhostInstance != null)
        {
            currentGhostInstance.gameObject.SetActive(true);
        }
    }
}

using UnityEngine;

public class ConstructionTower : MonoBehaviour
{
    private float constructionTimer;
    private float constructionTimerMax;
    private TowerTypeSO towerType;

    private ProgressBarCircle progressBar;

    private void Start()
    {
        progressBar = gameObject.GetComponentInChildren<ProgressBarCircle>();
    }

    private void Update()
    {
        constructionTimer -= Time.deltaTime;
        if (progressBar != null)
        {
            progressBar.SetProgress(constructionTimer, constructionTimerMax);
        }

        if (constructionTimer <= 0f)
        {
            Instantiate(towerType.completePhasePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public static ConstructionTower SpawnConstructionTower(
        Vector3 worldPosition,
        TowerTypeSO activeTowerType
    )
    {
        Transform constructionTowerTransform = Instantiate(
            activeTowerType.constructionPhasePrefab,
            worldPosition,
            Quaternion.identity
        );

        ConstructionTower constructionTower =
            constructionTowerTransform.GetComponent<ConstructionTower>();

        constructionTower.towerType = activeTowerType;
        constructionTower.constructionTimerMax = activeTowerType.constructionTimerMax;
        constructionTower.constructionTimer = constructionTower.constructionTimerMax;

        return constructionTower;
    }
}

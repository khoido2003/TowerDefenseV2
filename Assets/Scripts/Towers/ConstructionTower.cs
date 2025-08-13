using UnityEngine;

public class ConstructionTower : MonoBehaviour
{
    private float constructionTimer;
    private float constructionTimerMax;
    private TowerTypeSO towerTypeSO;

    public static void SpawnConstructionTower(Vector3 worldPosition, TowerTypeSO activeTowerType)
    {
        Transform constructionTowerTransform = Instantiate(
            activeTowerType.constructionPhasePrefab,
            worldPosition,
            Quaternion.identity
        );
    }
}

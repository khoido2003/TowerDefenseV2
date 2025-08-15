using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/TowerType")]
public class TowerTypeSO : ScriptableObject
{
    public string nameString;
    public Transform completePhasePrefab;
    public Transform constructionPhasePrefab;
    public Transform ghostTower;

    public Sprite sprite;
    public int healthAmountMax;
    public int cost;
    public Vector2Int footPrintSize;
    public float constructionTimerMax;
}

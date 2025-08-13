using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/TowerType")]
public class TowerTypeSO : ScriptableObject
{
    public string nameString;
    public Transform completePhasePrefab;
    public Transform constructionPhasePrefab;

    public Sprite sprite;
    public int healthAmountMax;
    public int cost;
}

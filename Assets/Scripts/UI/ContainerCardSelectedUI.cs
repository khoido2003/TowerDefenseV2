using UnityEngine;
using UnityEngine.UI;

public class ContainerCardSelectedUI : MonoBehaviour
{
    [SerializeField]
    private TowerTypeListSO towerTypeListSO;

    [SerializeField]
    private Transform cardUiPrefab;

    private void Awake()
    {
        foreach (TowerTypeSO towerTypeSO in towerTypeListSO.towerSOList)
        {
            Transform cardUITransform = Instantiate(cardUiPrefab, transform);
            CardUI cardUI = cardUITransform.GetComponent<CardUI>();
            cardUI.SetSprite(towerTypeSO.sprite);
            cardUI.SetTextCost(towerTypeSO.cost.ToString());
        }
    }
}

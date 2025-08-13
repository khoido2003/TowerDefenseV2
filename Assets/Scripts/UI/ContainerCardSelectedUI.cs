using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContainerCardSelectedUI : MonoBehaviour
{
    [SerializeField]
    private TowerTypeListSO towerTypeListSO;

    [SerializeField]
    private Transform cardUiPrefab;

    private CardUI selectedCard;
    private Dictionary<TowerTypeSO, Transform> cardTransformDictionary;

    private void Awake()
    {
        cardTransformDictionary = new();

        foreach (TowerTypeSO towerTypeSO in towerTypeListSO.towerSOList)
        {
            Transform cardUITransform = Instantiate(cardUiPrefab, transform);
            cardTransformDictionary[towerTypeSO] = cardUITransform;

            CardUI cardUI = cardUITransform.GetComponent<CardUI>();
            cardUI.SetSprite(towerTypeSO.sprite);
            cardUI.SetTextCost(towerTypeSO.cost.ToString());

            Button button = cardUITransform.GetComponentInChildren<Button>();

            CardUI capturedCard = cardUI;
            button.onClick.AddListener(() => OnCardClicked(capturedCard, towerTypeSO));
        }
    }

    private void Update() { }

    private void OnCardClicked(CardUI clickedCard, TowerTypeSO towerType)
    {
        if (selectedCard != null)
        {
            selectedCard.HideSelected();
        }

        TowerManager.Instance.SetActiveTowerType(towerType);
        selectedCard = clickedCard;
        selectedCard.ShowSelected();
    }
}

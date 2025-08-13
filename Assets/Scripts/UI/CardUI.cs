using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    [SerializeField]
    private Image iconImage;

    [SerializeField]
    private TextMeshProUGUI textCost;

    [SerializeField]
    private Image selected;

    private void Awake() { }

    private void Start()
    {
        HideSelected();
    }

    public void HideSelected()
    {
        selected.gameObject.SetActive(false);
    }

    public void ShowSelected()
    {
        selected.gameObject.SetActive(true);
    }

    public void SetTextCost(string cost)
    {
        textCost.SetText(cost);
    }

    public void SetSprite(Sprite sprite)
    {
        iconImage.sprite = sprite;
    }
}

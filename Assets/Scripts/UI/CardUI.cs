using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Image iconImage;

    [SerializeField]
    private TextMeshProUGUI textCost;

    [SerializeField]
    private Image selected;

    private Vector3 originalScale;
    private float scale = 1.3f;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    private void Start()
    {
        HideSelected();
    }

    public void HideSelected()
    {
        selected.gameObject.SetActive(false);
        transform.localScale = originalScale;
    }

    public void ShowSelected()
    {
        selected.gameObject.SetActive(true);
        transform.localScale = new Vector3(scale, scale, scale);
    }

    public void SetTextCost(string cost)
    {
        textCost.SetText(cost);
    }

    public void SetSprite(Sprite sprite)
    {
        iconImage.sprite = sprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GhostTower.Instance.Show();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GhostTower.Instance.Hide();
    }
}

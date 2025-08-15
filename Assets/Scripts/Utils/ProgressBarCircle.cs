using UnityEngine;
using UnityEngine.UI;

public class ProgressBarCircle : MonoBehaviour
{
    [SerializeField]
    private Image progressImage;

    private float timer;
    private float timerMax;

    public void SetProgress(float timer, float timerMax)
    {
        this.timer = timer;
        this.timerMax = timerMax;

        if (progressImage != null && timerMax > 0)
        {
            float progress = Mathf.Clamp01(timer / timerMax);
            progressImage.fillAmount = progress;
        }
    }
}

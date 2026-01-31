using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
public class Factory : MonoBehaviour
{
    public int type;
    public float timerMask;
    public int numberMask;
    public Slider timerSlider;

    private float sliderTimer;

    void Start()
    {
        timerSlider.maxValue = timerMask;
        timerSlider.value = 0;
        StartTimer();
    }

    public void StartTimer()
    {
        StartCoroutine(StartTheTimerTicker());
    }

    IEnumerator StartTheTimerTicker()
    {
        while (true)
        {
            sliderTimer += Time.deltaTime;
            timerSlider.value = sliderTimer;

            if (sliderTimer >= timerMask)
            {
                GameManager.Instance.IncrementMask(numberMask);
                timerSlider.value = 0;
                sliderTimer = 0;
            }
            yield return new WaitForSeconds(0.001f);
        }
 
    }
}

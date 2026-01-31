using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.FilePathAttribute;
public class Factory : MonoBehaviour
{
    public int type;
    public float timerMask;
    public int numberMask;
    public Slider timerSlider;
    private int rotation;

    private float sliderTimer;

    public GameObject ressource;

    void Start()
    {
        timerSlider.maxValue = timerMask;
        timerSlider.value = 0;
        rotation = GameManager.Instance.rotation;
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
                Vector3 position = transform.position;
                float angle = (3.0f + (rotation * 2)) * Mathf.PI / 6;
                position.y += Mathf.Sin(angle) * 0.6f;
                position.x += Mathf.Cos(angle) * 0.6f;
                Instantiate(ressource, position, transform.rotation);
                timerSlider.value = 0;
                sliderTimer = 0;
            }
            yield return new WaitForSeconds(0.001f);
        }
 
    }
}

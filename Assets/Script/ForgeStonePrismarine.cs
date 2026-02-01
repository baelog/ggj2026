using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.FilePathAttribute;

public class ForgeStonePrismarine : MonoBehaviour
{
    public string name = "Light Prismarine";
    private int rockNumber = 0;
    private int prismarineNumber = 0;
    public GameObject ressource;
    public float timerRessource;
    public int numberRessource = 1;
    public Slider timerSlider;
    private float sliderTimer;
    private int rotation = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rotation = GameManager.Instance.rotation;
        transform.rotation = Quaternion.Euler(0, 0, 60 * rotation);
        timerSlider.maxValue = timerRessource;
        timerSlider.value = 0;
        rotation = GameManager.Instance.rotation;
        StartTimer();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D rb = other.attachedRigidbody;
        if (rb == null || other.tag != Tags.Item) return;

        ObjectMoving itemBehaviour = other.GetComponent<ObjectMoving>();

        if (itemBehaviour.name != "Rock" && itemBehaviour.name != "Prismarine")
        {
            Destroy(other.gameObject);
            return;
        }

        switch (itemBehaviour.name)
        {
            case "Rock":
                rockNumber++;
                break;
            case "Prismarine":
                prismarineNumber++;
                break;
            default:
                break;
        }

        Destroy(other.gameObject);
    }

    public void StartTimer()
    {
        StartCoroutine(StartTheTimerTicker());
    }

    IEnumerator StartTheTimerTicker()
    {
        while (true)
        {
            //Debug.Log(rockNumber)
            ; if (rockNumber >= 1 && prismarineNumber >= 1)
            {
                sliderTimer += Time.deltaTime;
                timerSlider.value = sliderTimer;

                if (sliderTimer >= timerRessource)
                {
                    Vector3 position = transform.position;
                    float angle = (3.0f + (rotation * 2)) * Mathf.PI / 6;
                    position.y += Mathf.Sin(angle) * 0.6f;
                    position.x += Mathf.Cos(angle) * 0.6f;
                    for (int i = 0; i < numberRessource; i++)
                    {
                        Instantiate(ressource, position, transform.rotation);
                    }
                    timerSlider.value = 0;
                    sliderTimer = 0;
                    rockNumber--;
                    prismarineNumber--;
                }
            }
            yield return new WaitForSeconds(0.001f);
        }

    }
}

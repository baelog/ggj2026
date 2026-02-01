using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.FilePathAttribute;

public class ForgeIronStone : MonoBehaviour
{
    private int rockNumber = 0;
    private int ironNumber = 0;
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
        Debug.Log(itemBehaviour.name);
        if (itemBehaviour.name != "Rock" && itemBehaviour.name != "Iron")
        {
            Destroy(other.gameObject);
            return;
        }

        switch (itemBehaviour.name)
        {
            case "Rock":
                Debug.Log("stone");
                rockNumber++;
                break;
            case "Iron":
                Debug.Log("iron");
                ironNumber++;
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
            ; if (rockNumber >= 1 && ironNumber >= 1)
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
                    ironNumber--;
                }
            }
            yield return new WaitForSeconds(0.001f);
        }

    }
}

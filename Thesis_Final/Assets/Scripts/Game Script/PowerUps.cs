using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerUps : MonoBehaviour
{
    private TimerUI timerUI;
    public GameObject[] gameObjects;
    public GameObject powerUpText;
    public GameObject sliderGameObject;
    public Slider powerUpSlider;
    public TMP_Text powerUpName;
    private float powerUpDuration = 10f;
    public bool isPayamanOn = false;
    public bool isGoodsOn = false;
    public bool isTaympersOn = false;


    void Start()
    {
        timerUI = FindObjectOfType<TimerUI>();
        for (int i = 0; i < gameObjects.Length; i++)
        {
            bool purchased = PlayerPrefs.GetInt("PowerUp_" + i, 0) == 1;

            gameObjects[i].SetActive(purchased);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 rayPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero);

            if (hit.collider != null)
            {
                GameObject clickedObject = hit.collider.gameObject;

                for (int i = 0; i < gameObjects.Length; i++)
                {
                    if (clickedObject == gameObjects[i])
                    {
                        switch (gameObjects[i].name)
                        {
                            case "Goods Lang":
                                StartCoroutine(ActivateGoods(gameObjects[i].name));
                                break;
                            case "Payaman":
                                StartCoroutine(ActivatePayaman(gameObjects[i].name));
                                break;
                            case "Taympers":
                                StartCoroutine(ActivateTaympers(gameObjects[i].name));
                                break;
                        }
                        break;
                    }
                }
            }
        }
    }
    private bool IsAnyPowerUpActive()
    {
        return isPayamanOn || isGoodsOn || isTaympersOn;
    }
    private IEnumerator ActivatePayaman(string name)
    {
        if (IsAnyPowerUpActive()) yield break;
        powerUpName.text = name + " Activated";
        isPayamanOn = true;
        powerUpText.SetActive(true);
        StartCoroutine(PopUpDuration());
        sliderGameObject.SetActive(true);

        float timer = powerUpDuration;
        float sliderTimer = timer;

        while (timer > 0f)
        {
            yield return null;

            timer -= Time.deltaTime;
            sliderTimer -= Time.deltaTime; 

            powerUpSlider.value = sliderTimer / powerUpDuration; 

            if (sliderTimer <= 0f)
            {
                sliderTimer = 0f;
            }
        }

        sliderGameObject.SetActive(false);
        isPayamanOn = false;

        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (gameObjects[i].name == name)
            {
                gameObjects[i].SetActive(false);
                break;
            }
        }
    }
    private IEnumerator ActivateGoods(string name)
    {
        if (IsAnyPowerUpActive()) yield break;
        powerUpName.text = name + " Activated";
        isGoodsOn = true;
        powerUpText.SetActive(true);
        sliderGameObject.SetActive(true);
        StartCoroutine(PopUpDuration());

        float timer = powerUpDuration;
        float sliderTimer = timer;

        while (timer > 0f)
        {
            yield return null;

            timer -= Time.deltaTime;
            sliderTimer -= Time.deltaTime;

            powerUpSlider.value = sliderTimer / powerUpDuration;

            if (sliderTimer <= 0f)
            {
                sliderTimer = 0f;
            }
        }

        sliderGameObject.SetActive(false);
        isGoodsOn = false;

        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (gameObjects[i].name == name)
            {
                gameObjects[i].SetActive(false);
                break;
            }
        }
    }
    private IEnumerator ActivateTaympers(string name)
    {
        if (IsAnyPowerUpActive()) yield break;
        powerUpName.text = name + " Activated";
        isTaympersOn = true;
        powerUpText.SetActive(true);
        sliderGameObject.SetActive(true);
        timerUI.StopTimer(false);
        StartCoroutine(PopUpDuration());

        float timer = powerUpDuration;
        float sliderTimer = timer;

        while (timer > 0f)
        {
            yield return null;

            timer -= Time.deltaTime;
            sliderTimer -= Time.deltaTime;

            powerUpSlider.value = sliderTimer / powerUpDuration;

            if (sliderTimer <= 0f)
            {
                sliderTimer = 0f;
            }
        }

        sliderGameObject.SetActive(false);
        timerUI.StopTimer(true);
        isTaympersOn = false;

        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (gameObjects[i].name == name)
            {
                gameObjects[i].SetActive(false);
                break;
            }
        }
    }
    private IEnumerator PopUpDuration()
    {
        yield return new WaitForSeconds(3);
        powerUpText.SetActive(false);
    }
}

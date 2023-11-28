using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Taympers : MonoBehaviour
{
    private Payaman payaman;
    private GoodsLang goodsLang;

    public bool isTaympers = false;

    public Slider powerUpSlider;
    public GameObject powerUpText;
    public GameObject sliderGameObject;
    private TimerUI timerUI;
    public TMP_Text powerUpName;
    private float powerUpDuration = 10f;

    private void Start()
    {
        payaman = FindObjectOfType<Payaman>();
        goodsLang = FindObjectOfType<GoodsLang>();
        if (timerUI == null)
        {
            timerUI = FindObjectOfType<TimerUI>();
        }
        powerUpSlider.maxValue = powerUpDuration;
        powerUpSlider.value = powerUpDuration;
    }

    private void OnMouseDown()
    {
        if (payaman.isPayamanOn == false && goodsLang.isGoodsOn == false && isTaympers == false)
        {
            StartCoroutine(ActivateGoods());
        }
    }

    private IEnumerator ActivateGoods()
    {
        powerUpName.text = "Taympers Activated";
        isTaympers = true;
        powerUpText.SetActive(true);
        sliderGameObject.SetActive(true);
        timerUI.StopTimer(false);
        StartCoroutine(PopUpDuration());

        float timer = powerUpDuration;
        while (timer > 0f)
        {
            yield return null;
            timer -= Time.deltaTime;
            powerUpSlider.value = timer;
        }
        sliderGameObject.SetActive(false);
        timerUI.StopTimer(true);
        isTaympers = false;
        gameObject.SetActive(false);
    }
    private IEnumerator PopUpDuration()
    {
        yield return new WaitForSeconds(3);
        powerUpText.SetActive(false);

    }
}

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoodsLang : MonoBehaviour
{
    private Taympers taympers;
    private Payaman payaman;
    public bool isGoodsOn = false;
    public Slider powerUpSlider;
    public GameObject powerUpText;
    public GameObject sliderGameObject;
    public TMP_Text powerUpName;
    private float powerUpDuration = 15f;

    private void Start()
    {
        taympers = FindObjectOfType<Taympers>();
        payaman = FindObjectOfType<Payaman>();
        powerUpSlider.maxValue = powerUpDuration;
        powerUpSlider.value = powerUpDuration;
    }

    private void OnMouseDown()
    {
        if (payaman.isPowerUpOn== false && isGoodsOn == false && taympers.isTaympers == false)
        {
            StartCoroutine(ActivateGoods());
        }
    }

    private IEnumerator ActivateGoods()
    {
        powerUpName.text = "Goods Lang Activated";
        isGoodsOn = true;
        powerUpText.SetActive(true);
        sliderGameObject.SetActive(true);
        StartCoroutine(PopUpDuration());

        float timer = powerUpDuration;
        while (timer > 0f)
        {
            yield return null;
            timer -= Time.deltaTime;
            powerUpSlider.value = timer;
        }
        sliderGameObject.SetActive(false);
        isGoodsOn = false;
        gameObject.SetActive(false);

    }
    private IEnumerator PopUpDuration()
    {
        yield return new WaitForSeconds(3);
        powerUpText.SetActive(false);

    }
}

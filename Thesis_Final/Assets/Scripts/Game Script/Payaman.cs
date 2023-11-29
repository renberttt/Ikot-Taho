using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Payaman : MonoBehaviour
{
    private Taympers taympers;
    private GoodsLang goodsLang;
    public bool isPowerUpOn = false;
    public Slider powerUpSlider;
    public GameObject powerUpText;
    public GameObject sliderGameObject;
    public TMP_Text powerUpName;
    private float powerUpDuration = 10f;

    private void Start()
    {
        taympers = FindObjectOfType<Taympers>();
        goodsLang = FindObjectOfType<GoodsLang>();
        powerUpSlider.maxValue = powerUpDuration;
        powerUpSlider.value = powerUpDuration;
    }

    private void OnMouseDown()
    {
        if (taympers.isTaympers == false && goodsLang.isGoodsOn == false && isPowerUpOn == false)
        {
            StartCoroutine(ActivateGoods());
        }
    }

    private IEnumerator ActivateGoods()
    {
        powerUpName.text = "Payaman Activated";
        isPowerUpOn = true;
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
        isPowerUpOn = false;
        gameObject.SetActive(false);

    }
    private IEnumerator PopUpDuration()
    {
        yield return new WaitForSeconds(3);
        powerUpText.SetActive(false);

    }
}
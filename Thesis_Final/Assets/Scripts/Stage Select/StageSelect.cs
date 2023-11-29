using UnityEngine;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour
{
    public Button[] buttons;

    void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            bool purchased = PlayerPrefs.GetInt("ShopItem_" + i, 0) == 1;

            if (purchased)
            {
                buttons[i].interactable = true;
            }
            else
            {
                buttons[i].interactable = false;
            }
        }
    }
}

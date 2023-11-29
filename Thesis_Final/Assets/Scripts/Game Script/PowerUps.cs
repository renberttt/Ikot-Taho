using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public GameObject[] gameObjects;

    void Start()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            bool purchased = PlayerPrefs.GetInt("PowerUp_" + i, 0) == 1;

            gameObjects[i].SetActive(purchased);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelector : MonoBehaviour
{
    [SerializeField] public GameObject cscs;
    
    private void Start()
    {
        cscs.SetActive(false);
    }
}

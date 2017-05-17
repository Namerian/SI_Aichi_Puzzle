using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUiManager : MonoBehaviour
{
    public static GameUiManager Instance { get; private set; }

    [Header("ingame UI")]

    [SerializeField]
    private Slider _kyoiSlider;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void SetKyoiSliderValue(float value)
    {
        _kyoiSlider.value = value;
    }
}

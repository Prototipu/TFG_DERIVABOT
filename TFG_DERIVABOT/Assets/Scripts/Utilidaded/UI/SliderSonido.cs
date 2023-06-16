using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderSonido : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;

    private void Start()
    {
        _slider.value = GameManager.Instance.Volumen;

        _slider.onValueChanged.AddListener((value) =>
        {
            GameManager.Instance.SliderVolumen(value);
        });
    }
}

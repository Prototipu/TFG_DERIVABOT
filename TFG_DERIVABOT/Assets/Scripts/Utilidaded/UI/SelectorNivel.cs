using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectorNivel : MonoBehaviour
{
    [SerializeField]
    private string _nivel;

    [SerializeField]
    private Button _button;

    [SerializeField]
    private SelectorNivelManager _manager;

    private void Awake()
    {
        _button.onClick.AddListener(() =>
        {
            _manager.SeleccionarNivel(_nivel);
        });
    }
}

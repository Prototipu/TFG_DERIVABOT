using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HerramientaSeleccionada : MonoBehaviour
{
    [SerializeField]
    private Movimiento _movimiento;

    [SerializeField]
    private Image _image;

    [SerializeField]
    private List<Sprite> _sprites;

    public void SeleccionarHerramienta(ManagerHerramientas.EHerramienta herramienta)
    {
        _image.sprite = _sprites[(int)herramienta - 1];
        _movimiento.MoverRect(true);
    }


    public void Salir()
    {
        _movimiento.MoverRect(false);
    }
}

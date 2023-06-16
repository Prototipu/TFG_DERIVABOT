using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.UI;

public class CheckResultado : MonoBehaviour
{

    [SerializeField]
    private Movimiento _botonCheck;

    [SerializeField]
    private Image _image;

    public void Check()
    {
        if (!_botonCheck.EnMovimiento && _botonCheck.Inicio)
        {
            bool ret = ManagerUILevel.Instance.CheckResultado();

            if (!ret)
            {
                StopAllCoroutines();
                StartCoroutine(CambioColor());
            }
        }
    }


    private IEnumerator CambioColor()
    {
        float progreso = 0f;

        float tiempo = 0.3f;

        while (progreso < tiempo)
        {
            _image.color = Color.Lerp(Color.red, Color.white, progreso / tiempo);

            progreso += Time.deltaTime;

            yield return null;
        }

        _image.color = Color.white;
    }
}

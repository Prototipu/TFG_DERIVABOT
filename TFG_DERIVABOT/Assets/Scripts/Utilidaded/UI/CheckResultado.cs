using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class CheckResultado : MonoBehaviour
{

    [SerializeField]
    private Movimiento _botonCheck;

    [SerializeField]
    private SpriteRenderer _sprite;

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

        float tiempo = 0.1f;

        while (progreso < tiempo)
        {
            _sprite.color = Color.Lerp(Color.red, Color.white, progreso / tiempo);

            progreso += Time.deltaTime;

            yield return null;
        }
    }
}

using Derivadas_LIB;
using Derivadas_LIB.Funciones;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorCablePotencial : SelectorCable
{
    public Potencial Fx;

    private float progreso = 0;

    private bool _flag = false;

    private void Start()
    {
        if (!_collider)
        {
            Destroy(gameObject);
            throw new System.Exception($"No collider or animator found on {gameObject}");
        }
        else if (!Fx.Derivado)
        {
            _init = true;
            ManagerHerramientas.Instance.Cable.OnIniciar += Cable_OnIniciar;
            ManagerHerramientas.Instance.Cable.OnSalir += Cable_OnSalir;
            if (!ManagerHerramientas.Instance.Cable.Iniciada)
                gameObject.SetActive(false);
        }
        else
            Destroy(gameObject);
    }

    protected override void ChildUpdate()
    {
        if (_flag)
        {
            _cargando = false;
            Fx.Cargar();
            Destroy(gameObject);
        }
    }

    protected override void CancelarCarga()
    {
        StopAllCoroutines();
        StartCoroutine(CambiarColor(false));
    }

    protected override void ComenzarCarga()
    {
        if (!_flag)
        {
            StopAllCoroutines();
            StartCoroutine(CambiarColor(true));
        }
    }

    private IEnumerator CambiarColor(bool cargando)
    {
        Func<bool> check = () =>
        {
            if (cargando)
                return progreso < 1.0f;
            else
                return progreso > 0f;
        };

        while (check())
        {
            Color c = Color.Lerp(Fx.ColorNoDerivado, Fx.ColorDerivado, progreso);

            Fx.SpriteK.color = c;
            Fx.SpriteExp.color = c;

            progreso += Time.deltaTime * (cargando ? 1f : -1f);

            yield return null;
        }

        Color final = cargando ? Fx.ColorDerivado : Fx.ColorNoDerivado;

        Fx.SpriteK.color = final;
        Fx.SpriteExp.color = final;

        if (cargando)
            _flag = true;
    }
}

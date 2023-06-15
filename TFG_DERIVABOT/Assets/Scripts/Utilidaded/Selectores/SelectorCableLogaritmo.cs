using Derivadas_LIB;
using Derivadas_LIB.Funciones;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorCableLogaritmo : SelectorCable
{
    public Logaritmica Fx;

    [SerializeField]
    private SpriteRenderer _sprite;

    private float progreso = 0;

    [SerializeField]
    private Color _inicio, _fin;

    private bool _flag = false;

    private void Start()
    {
        if (!_collider || !_sprite)
        {
            Destroy(gameObject);
            throw new System.Exception($"No collider or animator found on {gameObject}");
        }
        else
        {
            _init = true;
            ManagerHerramientas.Instance.Cable.OnIniciar += Cable_OnIniciar;
            ManagerHerramientas.Instance.Cable.OnSalir += Cable_OnSalir;
            if (!ManagerHerramientas.Instance.Cable.Iniciada)
                gameObject.SetActive(false);
        }
    }

    protected override void ChildUpdate()
    {
        if (_flag)
        {
            _cargando = false;
            Fx.Cargar();
        }
    }

    protected override void CancelarCarga()
    {
        StopAllCoroutines();
        StartCoroutine(CambiarColor(false));
    }

    protected override void ComenzarCarga()
    {
        StopAllCoroutines();
        StartCoroutine(CambiarColor(true));
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
            _sprite.color = Color.Lerp(_inicio, _fin, progreso);

            progreso += Time.deltaTime * (cargando ? 1f : -1f);

            yield return null;
        }

        if (cargando)
            _flag = true;
    }
}

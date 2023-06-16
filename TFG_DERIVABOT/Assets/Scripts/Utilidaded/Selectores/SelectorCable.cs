using Derivadas_LIB;
using Derivadas_LIB.Funciones;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SelectorCable : MonoBehaviour
{
    [SerializeField]
    protected Collider2D _collider;

    protected bool _cargando, _init = false;

    public void Cable_OnIniciar()
    {
        gameObject.SetActive(true);
    }

    public void Cable_OnSalir()
    {
        gameObject.SetActive(false);
    }

    protected void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(t.position);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);


            switch (t.phase)
            {
                case TouchPhase.Began:
                    if (hit.collider && hit.collider == _collider)
                    {
                        _cargando = true;
                        ComenzarCarga();
                    }
                    break;
                case TouchPhase.Moved:
                    if (_cargando && (!hit.collider || hit.collider != _collider))
                    {
                        _cargando = false;
                        CancelarCarga();
                    }
                    break;
                case TouchPhase.Ended:
                    if (_cargando)
                    {
                        _cargando = false;
                        CancelarCarga();
                    }
                    break;
            }
        }

        if (_cargando)
            ChildUpdate();
    }

    protected abstract void ChildUpdate();


    protected abstract void CancelarCarga();


    protected abstract void ComenzarCarga();


    private void OnDestroy()
    {
        if (_init)
        {
            ManagerHerramientas.Instance.Cable.OnIniciar -= Cable_OnIniciar;
            ManagerHerramientas.Instance.Cable.OnSalir -= Cable_OnSalir;
        }
    }
}

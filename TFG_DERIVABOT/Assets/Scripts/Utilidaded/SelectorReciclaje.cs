using Derivadas_LIB;
using Derivadas_LIB.Funciones;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SelectorClonacion;

public class SelectorReciclaje : MonoBehaviour
{
    public Incognita Fx;

    [SerializeField]
    private Collider2D _collider;

    void Start()
    {
        if (!_collider)
        {
            Destroy(gameObject);
            throw new System.Exception($"No collider found on {gameObject}");
        }
        else
        {
            ManagerHerramientas.Instance.Reciclaje.OnIniciar += Reciclaje_OnIniciar; ;
            ManagerHerramientas.Instance.Reciclaje.OnSalir += Reciclaje_OnSalir;
            if (!ManagerHerramientas.Instance.Reciclaje.Iniciada)
                gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Began)
            {
                Vector2 posicion = Camera.main.ScreenToWorldPoint(t.position);

                if (_collider.OverlapPoint(posicion) && Fx.Reciclable )
                {
                    Fx.Reciclar();
                }
            }
        }
    }

    private void Reciclaje_OnIniciar()
    {
        if (Fx.Reciclable)
            gameObject.SetActive(true);
    }

    private void Reciclaje_OnSalir()
    {
        gameObject.SetActive(false);
    }
}

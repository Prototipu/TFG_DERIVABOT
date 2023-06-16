using Derivadas_LIB;
using Derivadas_LIB.Funciones;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorEmpaquetado : MonoBehaviour
{
    public Funcion Fx;

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
            ManagerHerramientas.Instance.Empaquetado.OnIniciar += Empaquetado_OnIniciar;
            ManagerHerramientas.Instance.Empaquetado.OnSalir += Empaquetado_OnSalir;
            if (!ManagerHerramientas.Instance.Empaquetado.Iniciada)
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

                if (_collider.OverlapPoint(posicion))
                {
                    ManagerFunciones.Instance.EmpaquetarFuncion(Fx, 1, 2);
                    ManagerHerramientas.Instance.Salir();
                }
            }
        }
    }

    private void Empaquetado_OnIniciar()
    {
        gameObject.SetActive(true);
    }

    private void Empaquetado_OnSalir()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        ManagerHerramientas.Instance.Empaquetado.OnIniciar -= Empaquetado_OnIniciar; ;
        ManagerHerramientas.Instance.Empaquetado.OnSalir -= Empaquetado_OnSalir;
    }
}

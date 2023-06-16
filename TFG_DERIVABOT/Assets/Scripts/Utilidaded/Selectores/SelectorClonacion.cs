using Derivadas_LIB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorClonacion : MonoBehaviour
{
    public enum TipoSelector
    {
        S_FUNCION,
        S_POSICION
    }

    public Funcion Fx;

    public TipoSelector Tipo;

    [SerializeField]
    private Collider2D _collider;

    [SerializeField]
    private SpriteRenderer _sprite;

    private void Start()
    {
        if (!_collider || !_sprite)
        {
            Destroy(gameObject);
            throw new System.Exception($"No collider or sprite found on {gameObject}");
        }
        else
        {
            ManagerHerramientas.Instance.Clonacion.CambioEstado += CambioEstado;
            ManagerHerramientas.Instance.Clonacion.OnSalir += Clonacion_OnSalir;

            if (Tipo == TipoSelector.S_FUNCION)
                _sprite.color = new Color(1, 1, 0, 50f / 255f);
            else
                _sprite.color = new Color(0, 1, 1, 50f / 255f);

            if (!ManagerHerramientas.Instance.Clonacion.Iniciada)
                gameObject.SetActive(false);
        }
    }

    private void Clonacion_OnSalir()
    {
        gameObject.SetActive(false);
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
                    if (Tipo == TipoSelector.S_FUNCION)
                        ManagerHerramientas.Instance.Clonacion.FuncionSeleccionada(this);
                    else
                        ManagerHerramientas.Instance.Clonacion.PosicionSeleccionada(this);
                }
            }
        }
    }

    private void CambioEstado(bool estado, TipoSelector tipo)
    {
        if (tipo == Tipo)
            gameObject.SetActive(estado);
    }

    private void OnDestroy()
    {
        ManagerHerramientas.Instance.Clonacion.CambioEstado -= CambioEstado;
        ManagerHerramientas.Instance.Clonacion.OnSalir -= Clonacion_OnSalir;
    }
}

using Derivadas_LIB;
using Derivadas_LIB.Funciones;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorCable : MonoBehaviour
{
    public Incognita Fx;

    [SerializeField]
    private Collider2D _collider;

    [SerializeField]
    private ControladorAnimacionesInc _animator;

    private bool _cargando, _init = false;

    private

    void Start()
    {
        if (!_collider || !_animator)
        {
            Destroy(gameObject);
            throw new System.Exception($"No collider or animator found on {gameObject}");
        }
        else if (!Fx.Reciclable && !Fx.Derivado)
        {
            _init = true;
            ManagerHerramientas.Instance.Cable.OnIniciar += Cable_OnIniciar;
            ManagerHerramientas.Instance.Cable.OnSalir += Cable_OnSalir;
            if (!ManagerHerramientas.Instance.Reciclaje.Iniciada)
                gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Cable_OnIniciar()
    {
        if (!Fx.Reciclable)
            gameObject.SetActive(true);
    }

    private void Cable_OnSalir()
    {
        gameObject.SetActive(false);
    }

    private void Update()
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
                        _animator.Play("RobotDerivado");
                    }
                    break;
                case TouchPhase.Moved:
                    if (_cargando && (!hit.collider || hit.collider != _collider))
                        CancelarCarga();
                    break;
                case TouchPhase.Ended:
                    if (_cargando)
                        CancelarCarga();
                    break;
            }
        }

        if (_cargando)
        {
            AnimatorStateInfo info = _animator.GetEstado();
            if (info.normalizedTime >= 1 && !info.loop)
            {
                _cargando = false;
                Fx.Cargar();
                Destroy(gameObject);
            }
        }
    }


    private void CancelarCarga()
    {
        Debug.Log("Cancelando Carga");

        _cargando = false;

        float progreso = _animator.GetEstado().normalizedTime;

        _animator.Play("RobotDerivadoInverso", 0, 1 - progreso);
    }


    private void OnDestroy()
    {
        if (_init)
        {
            ManagerHerramientas.Instance.Cable.OnIniciar -= Cable_OnIniciar;
            ManagerHerramientas.Instance.Cable.OnSalir -= Cable_OnSalir;
        }
    }
}

using Derivadas_LIB;
using Derivadas_LIB.Funciones;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorCableIncognita : SelectorCable
{
    public Incognita Fx;

    [SerializeField]
    private ControladorAnimacionesInc _animator;

    private void Start()
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
            if (!ManagerHerramientas.Instance.Cable.Iniciada)
                gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected override void ChildUpdate()
    {
        AnimatorStateInfo info = _animator.GetEstado();
        if (info.normalizedTime >= 1 && !info.loop)
        {
            _cargando = false;
            Fx.Cargar();
            Destroy(gameObject);
        }
    }


    protected override void CancelarCarga()
    {
        Debug.Log("Cancelando Carga");

        _cargando = false;

        float progreso = _animator.GetEstado().normalizedTime;

        _animator.Play("RobotDerivadoInverso", 0, 1 - progreso);
    }

    protected override void ComenzarCarga()
    {
        _animator.Play("RobotDerivado");
    }
}

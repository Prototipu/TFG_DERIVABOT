using Derivadas_LIB;
using Derivadas_LIB.Funciones;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ManagerClonacion : Herramienta
{
    public delegate void DlgAvisoEstado(bool estado, SelectorClonacion.TipoSelector tipo);

    public event DlgAvisoEstado CambioEstado;

    private Funcion _funcionSeleccionada = null, _posicionSeleccionada;

    private Estado _estado = Estado.NINGUNO;

    private enum Estado
    {
        NINGUNO,
        S_FUNCION,
        S_POSICION,
        S_OPERACION
    }

    protected override void IIniciar()
    {
        _funcionSeleccionada = null;
        _posicionSeleccionada = null;
        _estado = Estado.NINGUNO;

        SeleccionarFuncion();
    }

    public void SeleccionarFuncion()
    {
        _funcionSeleccionada = null;
        CambioEstado?.Invoke(true, SelectorClonacion.TipoSelector.S_FUNCION);
        _estado = Estado.S_FUNCION;
    }

    public void FuncionSeleccionada(SelectorClonacion selector)
    {
        if (_estado != Estado.S_FUNCION)
            return;
        _funcionSeleccionada = selector.Fx;
        CambioEstado?.Invoke(false, SelectorClonacion.TipoSelector.S_FUNCION);
        SeleccionarPosicion();
    }

    public void SeleccionarPosicion()
    {
        _posicionSeleccionada = null;
        CambioEstado?.Invoke(true, SelectorClonacion.TipoSelector.S_POSICION);
        _estado = Estado.S_POSICION;
    }

    public void PosicionSeleccionada(SelectorClonacion selector)
    {
        if (_estado != Estado.S_POSICION)
            return;
        _posicionSeleccionada = selector.Fx;
        CambioEstado?.Invoke(false, SelectorClonacion.TipoSelector.S_POSICION);
        SeleccionarOperador();
    }

    public void SeleccionarOperador()
    {
        _estado = Estado.S_OPERACION;
        OperadorSeleccionado<Suma>();
    }

    public void OperadorSeleccionado<T>() where T : Funcion
    {
        if (_estado != Estado.S_OPERACION)
            return;
        ManagerFunciones.Instance.AcoplarFuncion<T>((Funcion)_funcionSeleccionada.Clone(), _posicionSeleccionada);
        Salir();
    }

    protected override void ISalir()
    {
        _funcionSeleccionada = null;
        _posicionSeleccionada = null;
        
        _estado = Estado.NINGUNO;
    }
}

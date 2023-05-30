using Derivadas_LIB;
using Derivadas_LIB.Funciones;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class ManagerClonacion
{
    public delegate void DlgAvisoEstado(bool estado, SelectorClonacion.TipoSelector tipo);

    public static event DlgAvisoEstado CambioEstado;

    private static Funcion _funcionSeleccionada = null, _posicionSeleccionada;

    private static Estado _estado = Estado.NINGUNO;

    private enum Estado
    {
        NINGUNO,
        S_FUNCION,
        S_POSICION,
        S_OPERACION
    }


    public static void SeleccionarFuncion()
    {
        _funcionSeleccionada = null;
        CambioEstado?.Invoke(true, SelectorClonacion.TipoSelector.S_FUNCION);
        _estado = Estado.S_FUNCION;
    }

    public static void FuncionSeleccionada(SelectorClonacion selector)
    {
        if (_estado != Estado.S_FUNCION)
            return;
        _funcionSeleccionada = selector.Fx;
        CambioEstado?.Invoke(false, SelectorClonacion.TipoSelector.S_FUNCION);
        SeleccionarPosicion();
    }

    public static void SeleccionarPosicion()
    {
        _posicionSeleccionada = null;
        CambioEstado?.Invoke(true, SelectorClonacion.TipoSelector.S_POSICION);
        _estado = Estado.S_POSICION;
    }

    public static void PosicionSeleccionada(SelectorClonacion selector)
    {
        if (_estado != Estado.S_POSICION)
            return;
        _posicionSeleccionada = selector.Fx;
        CambioEstado?.Invoke(false, SelectorClonacion.TipoSelector.S_POSICION);
        SeleccionarOperador();
    }

    public static void SeleccionarOperador()
    {
        _estado = Estado.S_OPERACION;
        OperadorSeleccionado<Suma>();
    }

    public static void OperadorSeleccionado<T>() where T : Funcion
    {
        if (_estado != Estado.S_OPERACION)
            return;
        ManagerFunciones.Instance.AcoplarFuncion<T>((Funcion)_funcionSeleccionada.Clone(), _posicionSeleccionada);
        Salir();
    }

    public static void Salir()
    {
        _funcionSeleccionada = null;
        _posicionSeleccionada = null;

        CambioEstado?.Invoke(false, SelectorClonacion.TipoSelector.S_FUNCION);
        CambioEstado?.Invoke(false, SelectorClonacion.TipoSelector.S_POSICION);
            
        _estado = Estado.NINGUNO;
    }

}

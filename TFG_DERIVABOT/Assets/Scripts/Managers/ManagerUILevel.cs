using Derivadas_LIB.Funciones;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerUILevel : ManagerI
{
    [SerializeField]
    private Movimiento _panelOperadores, _botonDeshacer, _botonSalirHerr, _panelHerrSeleccionada;

    public static ManagerUILevel Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void HerramientaSeleccionada(ManagerHerramientas.EHerramienta herramienta)
    {
        _botonDeshacer.MoverRect(false);
        _botonSalirHerr.MoverRect(true);

        _panelHerrSeleccionada.MoverRect(true);
        // Cambiar icono en panel
    }


    public void Deshacer()
    {
        if (!_botonDeshacer.EnMovimiento && _group.Herramientas.Herramienta == ManagerHerramientas.EHerramienta.Ninguna)
            _group.Funciones.Deshacer();
    }


    public void SalirBotonHerramientas()
    {
        if (!_botonSalirHerr.EnMovimiento && _group.Herramientas.Herramienta != ManagerHerramientas.EHerramienta.Ninguna)
            _group.Herramientas.Salir();
    }

    internal void SalirHerramientas()
    {
        _botonDeshacer.MoverRect(true);
        _botonSalirHerr.MoverRect(false);

        _panelHerrSeleccionada.MoverRect(false);
    }

    public void PanelSelecionOperadores()
    {
        _panelOperadores.MoverRect(true);
    }

    public void SeleccionarOperador(int operador)
    {
        if (!_panelOperadores.EnMovimiento && 
            _group.Herramientas.Herramienta == ManagerHerramientas.EHerramienta.Clonacion && 
            _group.Herramientas.Clonacion.Estd == ManagerClonacion.Estado.S_OPERACION)
        {
            switch (operador)
            {
                case 0:
                    _group.Herramientas.Clonacion.OperadorSeleccionado<Suma>();
                    break;
                case 1:
                    _group.Herramientas.Clonacion.OperadorSeleccionado<Resta>();
                    break;
                case 2:
                    _group.Herramientas.Clonacion.OperadorSeleccionado<Multiplicacion>();
                    break;
                default:
                    return;
            }

            _panelOperadores.MoverRect(false);
        }
    }
}

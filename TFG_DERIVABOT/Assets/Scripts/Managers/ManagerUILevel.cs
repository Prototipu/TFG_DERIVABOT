using Derivadas_LIB.Funciones;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerUILevel : ManagerI
{
    [SerializeField]
    private Movimiento _panelOperadores, _botonDeshacer, _botonSalirHerr, _botonCheck, _panelHerramientas;

    [SerializeField]
    private HerramientaSeleccionada _panelHerrSeleccionada;

    [SerializeField]
    private GameObject _panelVictoria;

    public static ManagerUILevel Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void HerramientaSeleccionada(ManagerHerramientas.EHerramienta herramienta)
    {
        _botonDeshacer.MoverRect(false);
        _botonSalirHerr.MoverRect(true);
        _botonCheck.MoverRect(false);

        _panelHerrSeleccionada.SeleccionarHerramienta(herramienta);
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
        _botonCheck.MoverRect(true);

        _panelHerrSeleccionada.Salir();

        if (_panelOperadores.Inicio)
            _panelOperadores.MoverRect(false);

    }

    public bool CheckResultado()
    {
        bool ret = _group.Funciones.CheckResultado();

        if (ret)
        {
            _panelVictoria.SetActive(true);

            _botonDeshacer.MoverRect(false);
            _botonCheck.MoverRect(false);

            Destroy(_panelHerramientas.transform.parent.gameObject);

        }

        return ret;
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

    public void SeleccionarHerrameinta(int herramienta)
    {
        if (!_panelHerramientas.EnMovimiento)
        {
            MoverPanelHerramientas();
            ManagerHerramientas.Instance.IniciarHerramienta((ManagerHerramientas.EHerramienta)herramienta);
        }
    }

    public void MoverPanelHerramientas()
    {
        if (ManagerHerramientas.Instance.Herramienta == ManagerHerramientas.EHerramienta.Ninguna)
            _panelHerramientas.MoverRect();
    }


    public void VolverAlSelector()
    {
        SceneManager.LoadScene("SelectorNiveles");
    }
}

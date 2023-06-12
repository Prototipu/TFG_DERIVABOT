using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ManagerHerramientas;

public class ManagerHerramientas : MonoBehaviour
{

    public static ManagerHerramientas Instance;

    private Dictionary<EHerramienta, Herramienta> Herramientas = new Dictionary<EHerramienta, Herramienta>()
    {
        {EHerramienta.Ninguna, null },
        {EHerramienta.Clonacion, new ManagerClonacion() },
        {EHerramienta.Reciclaje, new ManagerReciclaje() },
        {EHerramienta.Cable, new ManagerCable() },
        {EHerramienta.Empaquetado, new ManagerEmpaquetado() }
    };

    public ManagerClonacion Clonacion => Herramientas[EHerramienta.Clonacion] as ManagerClonacion;
    public ManagerReciclaje Reciclaje => Herramientas[EHerramienta.Reciclaje] as ManagerReciclaje;
    public ManagerCable Cable => Herramientas[EHerramienta.Cable] as ManagerCable;
    public ManagerEmpaquetado Empaquetado => Herramientas[EHerramienta.Empaquetado] as ManagerEmpaquetado;

    public enum EHerramienta
    {
        Ninguna,
        Clonacion,
        Reciclaje,
        Cable,
        Empaquetado
    }

    private EHerramienta _herramienta;

    private void Awake()
    {
        Instance = this;
        Clonacion.OnSalir += () =>
        {
            ManejarSalida(EHerramienta.Clonacion);
        };

        Reciclaje.OnSalir += () =>
        {
            ManejarSalida(EHerramienta.Reciclaje);
        };

        Cable.OnSalir += () =>
        {
            ManejarSalida(EHerramienta.Cable);
        };

        Empaquetado.OnSalir += () =>
        {
            ManejarSalida(EHerramienta.Empaquetado);
        };

        _herramienta = EHerramienta.Ninguna;
    }


    public void IniciarHerramienta(EHerramienta herramienta)
    {
        if (_herramienta == EHerramienta.Ninguna)
        {
           if(herramienta == EHerramienta.Ninguna)
                throw new System.Exception("No puedes inciar la herramienta Ninguna");

            Herramientas[herramienta].Iniciar();
            _herramienta = herramienta;
        }
        else
            throw new System.Exception($"Para empezar el uso de una herramienta deber�as cerrar primero {_herramienta}");
    }


    public void Salir()
    {
        if (_herramienta != EHerramienta.Ninguna)
            Herramientas[_herramienta].Salir();
        else
            throw new System.Exception($"No puedes cancelar si no est�s usando ninguna herramienta");
    }


    public void ManejarSalida(EHerramienta herramienta)
    {
        if (herramienta == _herramienta)
            _herramienta = EHerramienta.Ninguna;
        else
            throw new System.Exception($"Herramienta no manejada se cerr� fuera de �mbito, herramienta {herramienta}");
    }
}

using Derivadas_LIB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MultiControladorC : ControladorC
{
    [SerializeField]
    private bool paredes = true;

    [SerializeField]
    private ControladorCaja ContrCaja;

    [SerializeField]
    private ControladorParedes ContrParedes;

    private void Awake()
    {
        if(paredes) {
            ContrCaja.gameObject.SetActive(false);
            ContrParedes.gameObject.SetActive(true);
        }
        else
        {
            ContrCaja.gameObject.SetActive(true);
            ContrParedes.gameObject.SetActive(false);
        }
    }

    public override void EncajarFuncion(Funcion funcion, bool izquierda)
    {
        if (paredes)
            ContrParedes.EncajarFuncion(funcion, izquierda);
        else
            ContrCaja.EncajarFuncion(funcion, izquierda);
    }

    public override Transform GetPunto(Punto punto)
    {
        if (paredes)
            return ContrParedes.GetPunto(punto);
        else
            return ContrCaja.GetPunto(punto);
    }

    public override float Altura()
    {
        if (paredes)
            return ContrParedes.Altura();
        else
            return ContrCaja.Altura();
    }

    public override float Anchura()
    {
        if (paredes)
            return ContrParedes.Anchura();
        else
            return ContrCaja.Anchura();
    }
}
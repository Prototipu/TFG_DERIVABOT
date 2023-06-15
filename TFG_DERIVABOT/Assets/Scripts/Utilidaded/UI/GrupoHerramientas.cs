using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrupoHerramientas : MonoBehaviour
{
    [SerializeField]
    private Movimiento _movimientoPanel;

    public void SeleccionarHerrameinta(int herramienta)
    {
        if (!_movimientoPanel.EnMovimiento)
        {
            MoverPanel();
            ManagerHerramientas.Instance.IniciarHerramienta((ManagerHerramientas.EHerramienta)herramienta);
        }
    }

    public void MoverPanel()
    {
        if (ManagerHerramientas.Instance.Herramienta == ManagerHerramientas.EHerramienta.Ninguna)
            _movimientoPanel.MoverRect();
    }
}

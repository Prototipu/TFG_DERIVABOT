using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class Herramienta
{

    public delegate void DlgAviso();

    public event DlgAviso OnSalir;
    public event DlgAviso OnIniciar;

    public bool Iniciada = false;

    protected abstract void IIniciar();

    public void Iniciar()
    {
        Iniciada = true;
        IIniciar();
        OnIniciar?.Invoke();
    }

    protected abstract void ISalir();

    public void Salir()
    {
        Iniciada = false;
        ISalir();
        OnSalir?.Invoke();
    }

}

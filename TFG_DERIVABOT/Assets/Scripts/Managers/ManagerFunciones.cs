using Derivadas_LIB;
using Derivadas_LIB.Funciones;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ManagerFunciones : MonoBehaviour
{

    public static readonly Dictionary<Type, int> FunctionTypes = new Dictionary<Type, int>()
        {
            {typeof(Suma),0 },
            {typeof(Resta),1 },
            {typeof(Multiplicacion),2 },
            {typeof(Division),3 },
            {typeof(Potencial),4 },
            {typeof(Exponencial),5 },
            {typeof(Logaritmica),6 },
            {typeof(Incognita),7 },
        };

    [SerializeField]
    private List<GameObject> _prefabsFunciones;

    [SerializeField]
    private Funcion _funcionSuperior = null;

    private Stack<NodoFuncion> _stackFunciones = new Stack<NodoFuncion>();

    private Transform Root;

    private string _respuesta;

    public delegate void DlgEscalarFuncion(Vector3 pos, Vector2 size);

    public event DlgEscalarFuncion OnFuncionEscalada;

    public static ManagerFunciones Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        Root = new GameObject("Root").transform;
    }

    public void InitNivel(string nivel)
    {
        _funcionSuperior = CrearFuncionString(nivel);

        EscalarFuncion();

        //Funcion derivada = _funcionSuperior.Derivada();

        //_respuesta = ParserFunciones.FormatearFuncion(derivada);


        //Debug.Log($"Nivel cargado: {ParserFunciones.FormatearFuncion(_funcionSuperior)}");
        //Debug.Log($"Respuesta esperada: {_respuesta}");


        //Debug.Log($"Nivel cargado: {ParserFunciones.ParsearString(_funcionSuperior, null)}");
        //Debug.Log($"Respuesta esperada: {ParserFunciones.ParsearString(derivada, null)}");

        //if (derivada)
        //    Destroy(derivada.gameObject);
    }

    public void AcoplarFuncion<T>(Funcion nuevaFuncion, Funcion original) where T : Funcion
    {
        _stackFunciones.Push(ParserFunciones.ConstruirArbolNodal(_funcionSuperior));

        Funcion newFx;
        Funcion superior = original.FuncionSuperior;
        switch (FunctionTypes[typeof(T)])
        {
            case 0: // Suma
            case 1: // Resta
            case 2: // Multiplicacion
                newFx = GetFuncion<T>(original, nuevaFuncion);
                break;
            default:
                throw new Exception($"El acople solo admite suma, resta y multiplicación. Pasaste el tipo {typeof(T)}");
        }

        if (superior)
        {
            newFx.transform.parent = superior.transform;
            superior.Swap(original, newFx);
            newFx.FuncionSuperior = superior;
        }

        EscalarFuncion();
    }

    public void Reciclar(Funcion funcion)
    {
        _stackFunciones.Push(ParserFunciones.ConstruirArbolNodal(_funcionSuperior));
        NodoFuncion nodo = ParserFunciones.ConstruirArbolNodal(_funcionSuperior.CheckEstado());

        Destroy(_funcionSuperior.gameObject);

        _funcionSuperior = CrearFuncionNodos(nodo);

        EscalarFuncion();
    }

    public void Deshacer()
    {
        if (_stackFunciones.Count > 0)
        {
            NodoFuncion nodo = _stackFunciones.Pop();

            if (_funcionSuperior)
                Destroy(_funcionSuperior.gameObject);

            _funcionSuperior = CrearFuncionNodos(nodo);

            EscalarFuncion();
        }
    }


    private void EscalarFuncion()
    {
        if (_funcionSuperior)
        {
            while (_funcionSuperior.FuncionSuperior)
                _funcionSuperior = _funcionSuperior.FuncionSuperior;

            _funcionSuperior.Escalar();

            OnFuncionEscalada?.Invoke(_funcionSuperior.anclajes.Centro(),
                new Vector2(
                    _funcionSuperior.anclajes.Anchura(),
                    _funcionSuperior.anclajes.Altura()));
        }
    }

    #region Creador


    private Funcion CrearFuncionString(string nivel)
    {
        return CrearFuncionSRecursivo(new Queue<string>(nivel.Split(' '))); ;
    }

    private Funcion CrearFuncionSRecursivo(Queue<string> elementos)
    {
        Funcion uX;
        Funcion vX;
        int k;
        int exp;

        switch (elementos.Dequeue())
        {
            case "X":
                int.TryParse(elementos.Dequeue(), out k);
                int.TryParse(elementos.Dequeue(), out exp);
                return GetFuncion<Incognita>(k, exp, false);

            case "SUM":
                uX = CrearFuncionSRecursivo(elementos);
                vX = CrearFuncionSRecursivo(elementos);
                return GetFuncion<Suma>(uX, vX);

            case "RES":
                uX = CrearFuncionSRecursivo(elementos);
                vX = CrearFuncionSRecursivo(elementos);
                return GetFuncion<Resta>(uX, vX);

            case "MUL":
                uX = CrearFuncionSRecursivo(elementos);
                vX = CrearFuncionSRecursivo(elementos);
                return GetFuncion<Multiplicacion>(uX, vX);

            case "DIV":
                uX = CrearFuncionSRecursivo(elementos);
                vX = CrearFuncionSRecursivo(elementos);
                return GetFuncion<Division>(uX, vX);

            case "POT":
                int.TryParse(elementos.Dequeue(), out k);
                int.TryParse(elementos.Dequeue(), out exp);
                uX = CrearFuncionSRecursivo(elementos);
                return GetFuncion<Potencial>(k, uX, exp);

            case "EXP":
                uX = CrearFuncionSRecursivo(elementos);
                return GetFuncion<Exponencial>(uX);

            case "LOG":
                uX = CrearFuncionSRecursivo(elementos);
                return GetFuncion<Logaritmica>(uX);
        }

        return null;
    }

    private Funcion CrearFuncionNodos(NodoFuncion nodo)
    {
        if (nodo == null)
            return null;

        switch (nodo)
        {
            case NIncognita i:
                return GetFuncion<Incognita>(i.K, i.Exponente, i.Derivado);
            case NFuncionFx Fx:
                switch (Fx.tipo)
                {
                    case NFuncionFx.Tipo.EXP:
                        return GetFuncion<Exponencial>(CrearFuncionNodos(Fx.Inferior));
                    case NFuncionFx.Tipo.LOG:
                        return GetFuncion<Logaritmica>(CrearFuncionNodos(Fx.Inferior));
                    case NFuncionFx.Tipo.POT:
                        return GetFuncion<Potencial>(Fx.Prms[0], CrearFuncionNodos(Fx.Inferior), Fx.Prms[1]);
                }
                break;
            case NFuncion2Fx UxVx:
                switch (UxVx.tipo)
                {
                    case NFuncion2Fx.Tipo.SUM:
                        return GetFuncion<Suma>(CrearFuncionNodos(UxVx.Ux), CrearFuncionNodos(UxVx.Vx));
                    case NFuncion2Fx.Tipo.RES:
                        return GetFuncion<Resta>(CrearFuncionNodos(UxVx.Ux), CrearFuncionNodos(UxVx.Vx));
                    case NFuncion2Fx.Tipo.MUL:
                        return GetFuncion<Multiplicacion>(CrearFuncionNodos(UxVx.Ux), CrearFuncionNodos(UxVx.Vx));
                    case NFuncion2Fx.Tipo.DIV:
                        return GetFuncion<Division>(CrearFuncionNodos(UxVx.Ux), CrearFuncionNodos(UxVx.Vx));
                }
                break;
        }

        throw new Exception("La creación por nodo se salió de ámbito");
    }

    public T GetFuncion<T>(params object[] prms) where T : Funcion
    {
        T funcion = Instantiate(_prefabsFunciones[FunctionTypes[typeof(T)]], Vector3.zero, Quaternion.identity).GetComponent<T>();
        funcion.gameObject.name = funcion.GetType().Name;
        funcion.transform.parent = Root;

        Funcion uX = null, vX = null;

        switch (funcion)
        {
            case Incognita i:
                i.Init((int)prms[0], (int)prms[1], (bool)prms[2]);
                break;
            case Suma s:
                uX = prms[0] as Funcion;
                vX = prms[1] as Funcion;
                s.Init(uX, vX);
                break;

            case Resta r:
                uX = prms[0] as Funcion;
                vX = prms[1] as Funcion;
                r.Init(uX, vX);
                break;

            case Multiplicacion m:
                uX = prms[0] as Funcion;
                vX = prms[1] as Funcion;
                m.Init(uX, vX);
                break;

            case Division div:
                uX = prms[0] as Funcion;
                vX = prms[1] as Funcion;
                div.Init(uX, vX);
                break;

            case Potencial p:
                uX = prms[1] as Funcion;
                p.Init((int)prms[0], uX, (int)prms[2]);
                break;

            case Exponencial e:
                uX = prms[0] as Funcion;
                e.Init(uX);

                break;
            case Logaritmica l:
                uX = prms[0] as Funcion;
                l.Init(uX);
                break;
        }

        if (uX)
        {
            uX.FuncionSuperior = funcion;
            uX.transform.parent = funcion.transform;
        }
        if (vX)
        {
            vX.FuncionSuperior = funcion;
            vX.transform.parent = funcion.transform;
        }

        return funcion;
    }

    #endregion
}
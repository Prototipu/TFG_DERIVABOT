using Derivadas_LIB;
using Derivadas_LIB.Funciones;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

    private Dictionary<Funcion, Transform> _arbolFunciones = new Dictionary<Funcion, Transform>();

    private Transform Padre;

    public static ManagerFunciones Instance { get; private set; }

    private bool first = true;


    public Funcion testResta;
    public Funcion testInc;

    private void Awake()
    {
        Instance = this;
        Padre = new GameObject("Root").transform;
    }

    public T GetFuncion<T>(params object[] prms) where T : Funcion
    {
        T funcion = Instantiate(_prefabsFunciones[FunctionTypes[typeof(T)]], Vector3.zero, Quaternion.identity).GetComponent<T>();
        funcion.gameObject.name = funcion.GetType().Name;
        funcion.transform.parent = Padre;

        Funcion uX = null, vX = null;
        GameObject elem1 = null, elem2 = null;

        switch (funcion)
        {
            case Incognita i:
                i.Init((int)prms[0], (int)prms[1]);
                break;
            case Suma s:
                uX = prms[0] as Funcion;
                vX = prms[1] as Funcion;
                s.Init(uX, vX);

                elem1 = new GameObject("Ux");
                elem2 = new GameObject("Vx");
                break;

            case Resta r:
                uX = prms[0] as Funcion;
                vX = prms[1] as Funcion;
                r.Init(uX, vX);


                if (first)
                {
                    testResta = r;
                    testInc = vX;
                }

                first = false;

                elem1 = new GameObject("Ux");
                elem2 = new GameObject("Vx");
                break;

            case Multiplicacion m:
                uX = prms[0] as Funcion;
                vX = prms[1] as Funcion;
                m.Init(uX, vX);

                elem1 = new GameObject("Ux");
                elem2 = new GameObject("Vx");
                break;

            case Division div:
                uX = prms[0] as Funcion;
                vX = prms[1] as Funcion;
                div.Init(uX, vX);

                elem1 = new GameObject("Ux");
                elem2 = new GameObject("Vx");
                break;

            case Potencial p:
                uX = prms[1] as Funcion;
                p.Init((int)prms[0], uX, (int)prms[2]);

                elem1 = new GameObject("Fx");
                break;

            case Exponencial e:
                uX = prms[0] as Funcion;
                e.Init(uX);

                elem1 = new GameObject("Fx");
                break;
            case Logaritmica l:
                uX = prms[0] as Funcion;
                l.Init(uX);

                elem1 = new GameObject("Fx");
                break;
        }

        if (elem1 && uX)
        {
            uX.FuncionSuperior = funcion;
            elem1.transform.parent = funcion.transform;
            uX.transform.parent = elem1.transform;

            _arbolFunciones[uX] = elem1.transform;
        }
        if (elem2 && vX)
        {
            vX.FuncionSuperior = funcion;
            elem2.transform.parent = funcion.transform;
            vX.transform.parent = elem2.transform;

            _arbolFunciones[vX] = elem2.transform;
        }

        CheckArbol(funcion);
        _arbolFunciones[funcion] = Padre;

        return funcion;
    }

    public void AcoplarFuncion<T>(Funcion nuevaFuncion, Funcion original) where T : Funcion
    {
        Transform root = _arbolFunciones[original];

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
            newFx.transform.parent = root;
            superior.Swap(original, newFx);

            newFx = superior;
            superior = newFx.FuncionSuperior;

            while (superior)
            {
                superior.Swap(newFx, newFx);

                newFx = superior;
                superior = newFx.FuncionSuperior;
            }
        }
    }

    private void CheckArbol(Funcion nuevaFuncion)
    {
        if (!_funcionSuperior)
            _funcionSuperior = nuevaFuncion;
        else if (_funcionSuperior != nuevaFuncion)
        {
            int Fx = 0;
            int Fsx = 0;
            ParserFunciones.ChildCount(nuevaFuncion.transform, ref Fx);
            ParserFunciones.ChildCount(_funcionSuperior.transform, ref Fsx);
            if (Fx > Fsx)
                _funcionSuperior = nuevaFuncion;
        }
    }
}
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

    public static ManagerFunciones Instance { get; private set; }


    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Instance._prefabsFunciones = _prefabsFunciones;
            Destroy(Instance.gameObject);
        }
    }

    public T GetFuncion<T>(params object[] prms) where T : Funcion
    {
        T funcion = Instantiate(_prefabsFunciones[FunctionTypes[typeof(T)]], Vector3.zero, Quaternion.identity).GetComponent<T>();
        funcion.gameObject.name = funcion.GetType().Name;

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
            elem1.transform.parent = funcion.transform;
            uX.transform.parent = elem1.transform;
        }
        if (elem2 && vX)
        {
            elem2.transform.parent = funcion.transform;
            vX.transform.parent = elem2.transform;
        }

        CheckArbolyEscalar(funcion);

        return funcion;
    }

    private void CheckArbolyEscalar(Funcion nuevaFuncion)
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
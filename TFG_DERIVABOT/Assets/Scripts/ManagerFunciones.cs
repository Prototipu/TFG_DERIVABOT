using Derivadas_LIB;
using System.Collections.Generic;
using UnityEngine;

public class ManagerFunciones : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _prefabsFunciones;

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

    public T GetFuncion<T>() where T : Funcion
    {
        T funcion = Instantiate(_prefabsFunciones[ParserFunciones.FunctionTypes[typeof(T)]], Vector3.zero, Quaternion.identity).GetComponent<T>();

        return funcion;
    }
}
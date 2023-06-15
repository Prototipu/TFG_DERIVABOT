using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerGroup : MonoBehaviour
{

    // ESTA CLASE SOLO SIRVE PARA ASEGURAR LA REFERENCIA ENTRE LOS MANAGERS EN LA ESCENA DEL NIVEL SIN COMPLICARSE POR
    // SI EL SINGLETON DE CADA CLASE ESTÁ INICIALIZADO

    [SerializeField]
    private ManagerFunciones _funciones;
    public ManagerFunciones Funciones => _funciones;

    [SerializeField]
    private ManagerHerramientas _herramientas;
    public ManagerHerramientas Herramientas => _herramientas;

    [SerializeField]
    private ManagerUILevel _levelUI;
    public ManagerUILevel LevelUI => _levelUI;
    
}

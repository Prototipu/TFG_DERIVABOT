using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckResultado : MonoBehaviour
{

    [SerializeField]
    private Movimiento _botonCheck;

    public void Check()
    {
        if (!_botonCheck.EnMovimiento && _botonCheck.Inicio)
        {
            bool ret = ManagerUILevel.Instance.CheckResultado();

            if (!ret)
                ;
        }
    }
}

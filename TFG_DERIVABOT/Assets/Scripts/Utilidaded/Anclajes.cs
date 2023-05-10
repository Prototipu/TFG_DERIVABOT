using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Punto : int
{
    N,
    S,
    E,
    W
}

public class Anclajes : MonoBehaviour
{
    public Transform[] Puntos;

    private Transform padre;

    [SerializeField]
    private SpriteRenderer _referencia;


    private void Awake()
    {
        padre = transform.parent;
    }

    public float Altura()
    {
        return Vector2.Distance(GetPunto(Punto.N).position, GetPunto(Punto.S).position);
    }

    public float Anchura()
    {
        return Vector2.Distance(GetPunto(Punto.E).position, GetPunto(Punto.W).position);
    }

    public void Anclar(Transform posicion, Punto punto) {

        Vector3 offsetPunto = padre.position - GetPunto(punto).position;

        transform.parent.position = posicion.position + offsetPunto;
    }

    public Transform GetPunto(Punto p)
    {
        return Puntos[(int)p];
    }


#if !UNITY_EDITOR
    private void Start()
    {
        Destroy(_referencia.gameObject);
    }

#else
    private void Update()
    {
        Vector2 espacio = new Vector2(
            Vector2.Distance(GetPunto(Punto.E).position, GetPunto(Punto.W).position),
            Vector2.Distance(GetPunto(Punto.N).position, GetPunto(Punto.S).position));

        _referencia.size = espacio;

        Vector2 nuevaPos = new Vector2(
            (GetPunto(Punto.E).localPosition.x + GetPunto(Punto.W).localPosition.x) / 2,
           (GetPunto(Punto.N).localPosition.y + GetPunto(Punto.S).localPosition.y) / 2);
        _referencia.transform.localPosition = nuevaPos;
    }
#endif
}

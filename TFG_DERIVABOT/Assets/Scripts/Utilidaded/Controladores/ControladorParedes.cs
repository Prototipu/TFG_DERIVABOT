using Derivadas_LIB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorParedes : ControladorC
{
    [SerializeField]
    private Anclajes _izquierdo, _derecho;

    private SpriteRenderer _SIzquierdo, _SDerecho; 

    private float _AltoL;


    private void Awake()
    {
        _SIzquierdo = _izquierdo.transform.parent.GetComponent<SpriteRenderer>();
        _SDerecho = _derecho.transform.parent.GetComponent<SpriteRenderer>();

        _AltoL = _SIzquierdo.bounds.size.y;
    }

    public override void EncajarFuncion(Funcion funcion, bool izquierda)
    {
        Anclajes an = funcion.anclajes;

        float h = an.Altura();

        Vector3 b = _derecho.GetPunto(Punto.S).position;

        _SIzquierdo.transform.localScale = Vector3.one;
        _SDerecho.transform.localScale = Vector3.one;

        float ratioH = h / _AltoL;

        Transform paredE = _SDerecho.transform;
        Transform paredW = _SIzquierdo.transform;

        paredE.localScale = new Vector2(1, ratioH);
        paredW.localScale = new Vector2(1, ratioH);

        if (izquierda)
        {
            paredE.transform.position = new Vector2(paredE.transform.position.x,
                b.y + h / 2);
            an.Anclar(_derecho.GetPunto(Punto.W), Punto.E);

            _izquierdo.Anclar(an.GetPunto(Punto.W), Punto.E);

        }
        else
        {
            paredW.transform.position = new Vector2(paredW.transform.position.x,
                b.y + h / 2);
            an.Anclar(_izquierdo.GetPunto(Punto.E), Punto.W);
            _derecho.Anclar(an.GetPunto(Punto.E), Punto.W);
        }
    }

    public override Transform GetPunto(Punto punto)
    {
        if(punto == Punto.E)
            return _derecho.GetPunto(punto);
        else
            return _izquierdo.GetPunto(punto);
    }

    public override float Altura()
    {
        return Vector2.Distance(_izquierdo.GetPunto(Punto.N).position, _izquierdo.GetPunto(Punto.S).position);
    }

    public override float Anchura()
    {
        return Vector2.Distance(_derecho.GetPunto(Punto.E).position, _izquierdo.GetPunto(Punto.W).position);
    }
}

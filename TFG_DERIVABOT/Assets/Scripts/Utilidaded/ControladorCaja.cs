using Derivadas_LIB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorCaja : MonoBehaviour
{
    [SerializeField]
    private List<Anclajes> _Anclajes;

    private List<SpriteRenderer> _Paredes;

    [SerializeField]
    private SpriteRenderer _Fondo;

    private float alturaEste, alturaNorte, anchoEste, anchoNorte;


    private void Awake()
    {
        _Paredes = new List<SpriteRenderer>();

        foreach (var p in _Anclajes)
            _Paredes.Add(p.transform.parent.GetComponent<SpriteRenderer>());

        alturaEste = _Paredes[(int)Punto.E].bounds.size.y;
        anchoEste = _Paredes[(int)Punto.E].bounds.size.x;

        alturaNorte = _Paredes[(int)Punto.N].bounds.size.y;
        anchoNorte = _Paredes[(int)Punto.N].bounds.size.x;
    }

    public void EncajarFuncion(Funcion funcion, bool izquierda)
    {
        Anclajes an = funcion.anclajes;

        float h = an.Altura();
        float w = an.Anchura();

        foreach (var l in _Paredes)
            l.transform.localScale = Vector3.one;


        float ratioH = (h + alturaNorte * 2) / alturaEste;
        float ratioW = (w + anchoEste) / anchoNorte;


        Transform paredN = _Paredes[(int)Punto.N].transform;
        Transform paredS = _Paredes[(int)Punto.S].transform;
        Transform paredE = _Paredes[(int)Punto.E].transform;
        Transform paredW = _Paredes[(int)Punto.W].transform;

        paredN.localScale = new Vector2(ratioW * 1.005f, 1);
        paredS.localScale = new Vector2(ratioW * 1.005f, 1);
        paredE.localScale = new Vector2(1, ratioH);
        paredW.localScale = new Vector2(1, ratioH);

        if (izquierda)
        {
            paredE.transform.position = new Vector2(paredE.transform.position.x,
                _Anclajes[(int)Punto.S].GetPunto(Punto.N).position.y + h / 2);

            an.Anclar(_Anclajes[(int)Punto.E].GetPunto(Punto.W), Punto.E);
            _Anclajes[(int)Punto.W].Anclar(an.GetPunto(Punto.W), Punto.E);
        }
        else
        {
            paredW.transform.position = new Vector2(paredW.transform.position.x,
                _Anclajes[(int)Punto.S].GetPunto(Punto.N).position.y + h / 2);

            an.Anclar(_Anclajes[(int)Punto.W].GetPunto(Punto.E), Punto.W);
            _Anclajes[(int)Punto.E].Anclar(an.GetPunto(Punto.E), Punto.W);
        }

        _Anclajes[(int)Punto.N].Anclar(an.GetPunto(Punto.N), Punto.S);
        _Anclajes[(int)Punto.S].Anclar(an.GetPunto(Punto.S), Punto.N);



        Vector2 espacio = new Vector2(
            Vector2.Distance(GetPunto(Punto.E).position, GetPunto(Punto.W).position),
            Vector2.Distance(GetPunto(Punto.N).position, GetPunto(Punto.S).position));

        _Fondo.size = espacio * 0.95f;

        Vector2 nuevaPos = new Vector2(
            (GetPunto(Punto.E).position.x + GetPunto(Punto.W).position.x) / 2,
           (GetPunto(Punto.N).position.y + GetPunto(Punto.S).position.y) / 2);
        _Fondo.transform.position = nuevaPos;
    }


    public Transform GetPunto(Punto punto)
    {
        return _Anclajes[(int)punto].GetPunto(punto);
    }

    public float Altura()
    {
        return Vector2.Distance(GetPunto(Punto.N).position, GetPunto(Punto.S).position);
    }

    public float Anchura()
    {
        return Vector2.Distance(GetPunto(Punto.E).position, GetPunto(Punto.W).position);
    }
}

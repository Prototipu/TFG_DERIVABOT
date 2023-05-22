using Derivadas_LIB;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ControladorCaja : MonoBehaviour
{
    [SerializeField]
    private List<Anclajes> _Paredes, _Exterior, _Interior;

    private List<SpriteRenderer> _SpriteParedes, _SpriteInt, _SpriteExt;

    [SerializeField]
    private SpriteRenderer _Fondo;

    private float _AltoL, _AnchoL, _AltoI, _AnchoI, _AltoE;


    private void Awake()
    {
        _SpriteParedes = new List<SpriteRenderer>();
        _SpriteInt = new List<SpriteRenderer>();
        _SpriteExt = new List<SpriteRenderer>();

        foreach (var p in _Paredes)
            _SpriteParedes.Add(p.transform.parent.GetComponent<SpriteRenderer>());

        foreach (var i in _Interior)
            _SpriteInt.Add(i.transform.parent.GetComponent<SpriteRenderer>());

        foreach (var e in _Exterior)
            _SpriteExt.Add(e.transform.parent.GetComponent<SpriteRenderer>());

        _AltoL = _SpriteParedes[(int)Punto.E].bounds.size.y;
        _AnchoL = _SpriteParedes[(int)Punto.E].bounds.size.x;

        _AltoI = _SpriteInt[(int)Punto.E].bounds.size.y;
        _AnchoI = _SpriteInt[(int)Punto.E].bounds.size.x;


        _AltoE = _SpriteExt[(int)Punto.E].bounds.size.y;
    }

    public void EncajarFuncion(Funcion funcion, bool izquierda)
    {
        Anclajes an = funcion.anclajes;

        float h = an.Altura();
        float w = an.Anchura();

        foreach (var l in _SpriteParedes)
            l.transform.localScale = Vector3.one;


        float ratioH = (h + (_AnchoL * 2) + (_AnchoI * 2)) / _AltoL;
        float ratioW = (w + (_AnchoL * 2) + (_AnchoI * 2)) / _AltoL;


        float ratioEH = (h + (_AnchoL * 2) + (_AnchoI * 4)) / _AltoE;
        float ratioEW = (w + (_AnchoL * 2) + (_AnchoI * 4)) / _AltoE;


        float ratioIH = (h + (_AnchoI * 2)) / _AltoI;
        float ratioIW = (w + (_AnchoI * 2)) / _AltoI;

        Transform paredN = _SpriteParedes[(int)Punto.N].transform;
        Transform paredS = _SpriteParedes[(int)Punto.S].transform;
        Transform paredE = _SpriteParedes[(int)Punto.E].transform;
        Transform paredW = _SpriteParedes[(int)Punto.W].transform;

        Transform interiorN = _SpriteInt[(int)Punto.N].transform;
        Transform interiorS = _SpriteInt[(int)Punto.S].transform;
        Transform interiorE = _SpriteInt[(int)Punto.E].transform;
        Transform interiorW = _SpriteInt[(int)Punto.W].transform;

        Transform exteriorN = _SpriteExt[(int)Punto.N].transform;
        Transform exteriorS = _SpriteExt[(int)Punto.S].transform;
        Transform exteriorE = _SpriteExt[(int)Punto.E].transform;
        Transform exteriorW = _SpriteExt[(int)Punto.W].transform;

        paredN.localScale = new Vector2(ratioW, 1);
        paredS.localScale = new Vector2(ratioW, 1);
        paredE.localScale = new Vector2(1, ratioH);
        paredW.localScale = new Vector2(1, ratioH);

        interiorN.localScale = new Vector2(ratioIW, 1);
        interiorS.localScale = new Vector2(ratioIW, 1);
        interiorE.localScale = new Vector2(1, ratioIH);
        interiorW.localScale = new Vector2(1, ratioIH);

        exteriorN.localScale = new Vector2(ratioEW, 1);
        exteriorS.localScale = new Vector2(ratioEW, 1);
        exteriorE.localScale = new Vector2(1, ratioEH);
        exteriorW.localScale = new Vector2(1, ratioEH);

        if (izquierda)
        {
            interiorE.transform.position = new Vector2(interiorE.transform.position.x,
                _Interior[(int)Punto.S].GetPunto(Punto.N).position.y + h / 2);

            an.Anclar(_Interior[(int)Punto.E].GetPunto(Punto.W), Punto.E);
            _Interior[(int)Punto.W].Anclar(an.GetPunto(Punto.W), Punto.E);
        }
        else
        {
            interiorW.transform.position = new Vector2(interiorW.transform.position.x,
                _Interior[(int)Punto.S].GetPunto(Punto.N).position.y + h / 2);

            an.Anclar(_Interior[(int)Punto.W].GetPunto(Punto.E), Punto.W);
            _Interior[(int)Punto.E].Anclar(an.GetPunto(Punto.E), Punto.W);
        }

        _Interior[(int)Punto.N].Anclar(an.GetPunto(Punto.N), Punto.S);
        _Interior[(int)Punto.S].Anclar(an.GetPunto(Punto.S), Punto.N);


        _Paredes[(int)Punto.N].Anclar(_Interior[(int)Punto.N].GetPunto(Punto.N), Punto.S);
        _Paredes[(int)Punto.S].Anclar(_Interior[(int)Punto.S].GetPunto(Punto.S), Punto.N);
        _Paredes[(int)Punto.E].Anclar(_Interior[(int)Punto.E].GetPunto(Punto.E), Punto.W);
        _Paredes[(int)Punto.W].Anclar(_Interior[(int)Punto.W].GetPunto(Punto.W), Punto.E);



        _Exterior[(int)Punto.N].Anclar(_Paredes[(int)Punto.N].GetPunto(Punto.N), Punto.S);
        _Exterior[(int)Punto.S].Anclar(_Paredes[(int)Punto.S].GetPunto(Punto.S), Punto.N);
        _Exterior[(int)Punto.E].Anclar(_Paredes[(int)Punto.E].GetPunto(Punto.E), Punto.W);
        _Exterior[(int)Punto.W].Anclar(_Paredes[(int)Punto.W].GetPunto(Punto.W), Punto.E);



        Vector2 espacio = new Vector2(
                        Vector2.Distance(
                        _Paredes[(int)Punto.E].GetPunto(Punto.W).position,
                        _Paredes[(int)Punto.W].GetPunto(Punto.E).position),

                        Vector2.Distance(
                        _Paredes[(int)Punto.N].GetPunto(Punto.S).position,
                        _Paredes[(int)Punto.S].GetPunto(Punto.N).position));

        _Fondo.size = espacio;

        Vector2 nuevaPos = new Vector2(
            (GetPunto(Punto.E).position.x + GetPunto(Punto.W).position.x) / 2,
           (GetPunto(Punto.N).position.y + GetPunto(Punto.S).position.y) / 2);
        _Fondo.transform.position = nuevaPos;
    }


    public Transform GetPunto(Punto punto)
    {
        return _Exterior[(int)punto].GetPunto(punto);
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

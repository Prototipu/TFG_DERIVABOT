using Derivadas_LIB.Funciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Derivadas_LIB
{
    public static class ParserFunciones
    {

        public static string ParsearString(Funcion funcion, Type lastType)
        {


            if (!funcion)
                return "-";

            bool parCheck = lastType != null;
            bool specialCheck = lastType == typeof(Division) || lastType == typeof(Multiplicacion) ||
                lastType == typeof(Logaritmica) || lastType == typeof(Exponencial);

            Type Ftype = funcion.GetType();

            switch (funcion)
            {
                case Incognita i:
                    string iret = $"{(i.K != 0 || (i.K == 1 && i.Exponente == 0) ? i.K.ToString() : "")}{(i.Exponente > 0 ? "x" + (i.Exponente > 1 ? $"^{i.Exponente}" : "") : "")}";

                    if (iret != "" && specialCheck)
                        iret = "(" + iret + ")";

                    return iret;

                case Potencial p:
                    string pFx = ParsearString(p.Fx, Ftype);

                    if (pFx == "")
                        return "";

                    string kT = p.K > 1 ? p.K.ToString() : "";
                    string eT = p.Exponente > 0 ? $"({pFx})" + (p.Exponente > 1 ? $"^{p.Exponente}" : "") : "";

                    return kT + eT;

                case Exponencial e:
                    string eFx = ParsearString(e.Fx, Ftype);

                    return $"e^{eFx}";

                case Logaritmica l:
                    string lFx = ParsearString(l.Fx, Ftype);

                    return $"ln({lFx})";

                case Suma s:
                    string pUxS = ParsearString(s.Ux, Ftype);
                    string pVxS = ParsearString(s.Vx, Ftype);

                    if (pUxS == "")
                        return pVxS;
                    if (pVxS == "")
                        return pUxS;

                    string retS = parCheck ? "(" : "";

                    retS += $"{pUxS} + {pVxS}{(parCheck ? ")" : "")}";

                    return retS;

                case Resta r:
                    string pUxR = ParsearString(r.Ux, Ftype);
                    string pVxR = ParsearString(r.Vx, Ftype);

                    if (pUxR == "")
                        return pVxR;
                    if (pVxR == "")
                        return pUxR;


                    string retR = parCheck ? "(" : "";

                    retR += $"{pUxR} - {pVxR}{(parCheck ? ")" : "")}";

                    return retR;

                case Multiplicacion m:
                    string pUxM = ParsearString(m.Ux, Ftype);
                    string pVxM = ParsearString(m.Vx, Ftype);

                    if (pUxM == "" || pVxM == "")
                        return "";

                    string retM = specialCheck ? "(" : "";

                    retM += $"{pUxM} * {pVxM}{(specialCheck ? ")" : "")}";

                    return retM;

                case Division div:

                    string pUxD = ParsearString(div.Ux, Ftype);
                    string pVxD = ParsearString(div.Vx, Ftype);

                    if (pUxD == "")
                        return "";
                    if (pVxD == "")
                        return "Infinity";

                    string retD = specialCheck ? "(" : "";

                    retD += $"{pUxD} / {pVxD}{(specialCheck ? ")" : "")}";

                    return retD;

            }

            return "";
        }

        public static string FormatearFuncion(Funcion funcion)
        {
            if (!funcion)
                return "-";

            switch (funcion)
            {
                case Incognita i:
                    return $"X {i.K} {i.Exponente}";

                case Potencial p:
                    return $"POT {p.K} {p.Exponente} {FormatearFuncion(p.Fx)}";

                case Exponencial e:
                    return $"EXP {FormatearFuncion(e.Fx)}";

                case Logaritmica l:
                    return $"LOG {FormatearFuncion(l.Fx)}";

                case Suma s:
                    return $"SUM {FormatearFuncion(s.Ux)} {FormatearFuncion(s.Vx)}";

                case Resta r:
                    return $"RES {FormatearFuncion(r.Ux)} {FormatearFuncion(r.Vx)}";

                case Multiplicacion m:
                    return $"MUL {FormatearFuncion(m.Ux)} {FormatearFuncion(m.Vx)}";

                case Division div:
                    return $"DIV {FormatearFuncion(div.Ux)} {FormatearFuncion(div.Vx)}";

            }

            return "";
        }

        public static void ChildCount(Transform parent, ref int count)
        {
            foreach (Transform t in parent)
            {
                count++;
                ChildCount(t, ref count);
            }
        }

        public static NodoFuncion ConstruirArbolNodal(Funcion funcion)
        {
            if (!funcion)
                return null;

            switch (funcion)
            {
                case Incognita i:
                    return new NIncognita(i.K, i.Exponente, i.Derivado);

                case Potencial p:
                    return new NFuncionFx(ConstruirArbolNodal(p.Fx), NFuncionFx.Tipo.POT, p.K, p.Exponente);

                case Exponencial e:
                    return new NFuncionFx(ConstruirArbolNodal(e.Fx), NFuncionFx.Tipo.EXP, null);

                case Logaritmica l:
                    return new NFuncionFx(ConstruirArbolNodal(l.Fx), NFuncionFx.Tipo.LOG, null);

                case Suma s:
                    return new NFuncion2Fx(ConstruirArbolNodal(s.Ux), ConstruirArbolNodal(s.Vx), NFuncion2Fx.Tipo.SUM);

                case Resta r:
                    return new NFuncion2Fx(ConstruirArbolNodal(r.Ux), ConstruirArbolNodal(r.Vx), NFuncion2Fx.Tipo.RES);

                case Multiplicacion m:
                    return new NFuncion2Fx(ConstruirArbolNodal(m.Ux), ConstruirArbolNodal(m.Vx), NFuncion2Fx.Tipo.MUL);

                case Division div:
                    return new NFuncion2Fx(ConstruirArbolNodal(div.Ux), ConstruirArbolNodal(div.Vx), NFuncion2Fx.Tipo.DIV);
            }

            return null;
        }
    }


    public abstract class NodoFuncion
    {
        public NodoFuncion Superior;

        public void SetSuperior(NodoFuncion superior)
        {
            Superior = superior;
        }
    }


    public class NFuncionFx : NodoFuncion
    {
        public enum Tipo
        {
            EXP,
            LOG,
            POT
        }

        public Tipo tipo;

        public object[] Prms;

        public NodoFuncion Inferior;

        public NFuncionFx(NodoFuncion inferior, Tipo t, params object[] prms)
        {
            Inferior = inferior;
            Inferior.SetSuperior(this);
            Prms = prms;
            tipo = t;
        }
    }

    public class NFuncion2Fx : NodoFuncion
    {
        public enum Tipo
        {
            SUM,
            RES,
            MUL,
            DIV
        }

        public Tipo tipo;

        public NodoFuncion Ux, Vx;

        public NFuncion2Fx(NodoFuncion ux, NodoFuncion vx, Tipo t)
        {
            Ux = ux;
            Ux.SetSuperior(this);
            Vx = vx;
            Vx.SetSuperior(this);
            tipo = t;
        }
    }

    public class NIncognita : NodoFuncion
    {
        public int Exponente;
        public int K;
        public bool Derivado;

        public NIncognita(int k, int exp, bool der)
        {
            K = k;
            Exponente = exp;
            Derivado = der;
        }
    }
}

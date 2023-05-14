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
            return "";
        }

        public static Funcion CrearFuncion(string nivel)
        {
            return CrearFuncion(new Queue<string>(nivel.Split(' ')));
        }

        private static Funcion CrearFuncion(Queue<string> elementos)
        {
            Funcion uX = null;
            Funcion vX = null;
            int k = 0;
            int exp = 0;

            switch (elementos.Dequeue())
            {
                case "X":
                    int.TryParse(elementos.Dequeue(), out k);
                    int.TryParse(elementos.Dequeue(), out exp);
                    return ManagerFunciones.Instance.GetFuncion<Incognita>(k, exp);

                case "SUM":
                    uX = CrearFuncion(elementos);
                    vX = CrearFuncion(elementos);
                    return ManagerFunciones.Instance.GetFuncion<Suma>(uX, vX);

                case "RES":
                    uX = CrearFuncion(elementos);
                    vX = CrearFuncion(elementos);
                    return ManagerFunciones.Instance.GetFuncion<Resta>(uX, vX);

                case "MUL":
                    uX = CrearFuncion(elementos);
                    vX = CrearFuncion(elementos);
                    return ManagerFunciones.Instance.GetFuncion<Multiplicacion>(uX, vX);

                case "DIV":
                    uX = CrearFuncion(elementos);
                    vX = CrearFuncion(elementos);
                    return ManagerFunciones.Instance.GetFuncion<Division>(uX, vX);

                case "POT":
                    int.TryParse(elementos.Dequeue(), out k);
                    int.TryParse(elementos.Dequeue(), out exp);
                    uX = CrearFuncion(elementos);
                    return ManagerFunciones.Instance.GetFuncion<Potencial>(k, uX, exp);

                case "EXP":
                    uX = CrearFuncion(elementos);
                    return ManagerFunciones.Instance.GetFuncion<Exponencial>(uX);

                case "LOG":
                    uX = CrearFuncion(elementos);
                    return ManagerFunciones.Instance.GetFuncion<Logaritmica>(uX);
            }

            return null;
        }

        public static Bounds BoundsCombinados(GameObject g)
        {
            SpriteRenderer[] renderers = g.GetComponentsInChildren<SpriteRenderer>();
            if (renderers.Length == 0) return new Bounds();

            Bounds combinedBounds = renderers[0].bounds;

            for (int i = 1; i < renderers.Length; i++)
            {
                combinedBounds.Encapsulate(renderers[i].bounds);
            }

            return combinedBounds;
        }

        public static void ChildCount(Transform parent, ref int count)
        {
            foreach (Transform t in parent)
            {
                count++;
                ChildCount(t, ref count);
            }
        }
    }

}

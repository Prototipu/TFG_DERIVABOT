using Derivadas_LIB.Funciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derivadas_LIB
{
    public static class ParserFunciones
    {
        public static string ParsearString(Funcion funcion, Funcion.Type lastType)
        {

            bool parCheck = lastType != Funcion.Type.None;
            bool specialCheck = lastType == Funcion.Type.Division || lastType == Funcion.Type.Multiplicacion ||
                lastType == Funcion.Type.Logaritmica || lastType == Funcion.Type.Exponencial;
            switch (funcion.Ftype)
            {
                case Funcion.Type.Incognita:
                    Incognita i = funcion as Incognita;
                    if (i == null)
                        throw new Exception("El parametro recibido fue null");

                    string iret = $"{(i.K > 1 || (i.K == 1 && i.Exponente == 0) ? i.K.ToString() : "")}{(i.Exponente > 0 ? "x" + (i.Exponente > 1 ? $"^{i.Exponente}" : "") : "")}";

                    if (iret != "" && specialCheck)
                        iret = "(" + iret + ")";

                    return iret;
                case Funcion.Type.Potencial:
                    Potencial p = funcion as Potencial;

                    if (p == null)
                        throw new Exception("El parametro recibido fue null");

                    string pFx = ParsearString(p.Fx, p.Ftype);

                    if (pFx == "")
                        return "";

                    string kT = p.K > 1 ? p.K.ToString() : "";
                    string eT = p.Exponente > 0 ? $"({pFx})" + (p.Exponente > 1 ? $"^{p.Exponente}" : "") : "";

                    return kT + eT;

                case Funcion.Type.Exponencial:
                    Exponencial e = funcion as Exponencial;
                    if (e == null)
                        throw new Exception("El parametro recibido fue null");

                    string eFx = ParsearString(e.Fx, e.Ftype);

                    return $"e^{eFx}";

                case Funcion.Type.Logaritmica:
                    Logaritmica l = funcion as Logaritmica;

                    if (l == null)
                        throw new Exception("El parametro recibido fue null");

                    string lFx = ParsearString(l.Fx, l.Ftype);

                    return $"ln{lFx}";

                case Funcion.Type.Suma:
                    Suma s = funcion as Suma;

                    if (s == null)
                        throw new Exception("El parametro recibido fue null");

                    string pUxS = ParsearString(s.Ux, s.Ftype);
                    string pVxS = ParsearString(s.Vx, s.Ftype);

                    if (pUxS == "")
                        return pVxS;
                    if (pVxS == "")
                        return pUxS;

                    string retS = parCheck ? "(" : "";

                    retS += $"{pUxS} + {pVxS}{(parCheck ? ")" : "")}";

                    return retS;
                case Funcion.Type.Resta:
                    Resta r = funcion as Resta;

                    if (r == null)
                        throw new Exception("El parametro recibido fue null");

                    string pUxR = ParsearString(r.Ux, r.Ftype);
                    string pVxR = ParsearString(r.Vx, r.Ftype);

                    if (pUxR == "")
                        return pVxR;
                    if (pVxR == "")
                        return pUxR;


                    string retR = parCheck ? "(" : "";

                    retR += $"{pUxR} - {pVxR}{(parCheck ? ")" : "")}";

                    return retR;

                case Funcion.Type.Multiplicacion:
                    Multiplicacion m = funcion as Multiplicacion;

                    if (m == null)
                        throw new Exception("El parametro recibido fue null");

                    string pUxM = ParsearString(m.Ux, m.Ftype);
                    string pVxM = ParsearString(m.Vx, m.Ftype);

                    if (pUxM == "" || pVxM == "")
                        return "";

                    string retM = specialCheck ? "(" : "";

                    retM += $"{pUxM} * {pVxM}{(specialCheck ? ")" : "")}";

                    return retM;

                case Funcion.Type.Division:
                    Division div = funcion as Division;

                    if (div == null)
                        throw new Exception("El parametro recibido fue null");

                    string pUxD = ParsearString(div.Ux, div.Ftype);
                    string pVxD = ParsearString(div.Vx, div.Ftype);

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
    }
}

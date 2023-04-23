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
        public static readonly Dictionary<Type, int> FunctionTypes = new Dictionary<Type, int>()
        {
            {typeof(Suma),0 },
            {typeof(Resta),1 },
            {typeof(Multiplicacion),2 },
            {typeof(Division),3 },
            {typeof(Potencial),4 },
            {typeof(Exponencial),5 },
            {typeof(Logaritmica),6 },
            {typeof(Incognita),7 },
        };

        public static string ParsearString(Funcion funcion, Type lastType)
        {
            bool parCheck = lastType != null;
            bool specialCheck = lastType == typeof(Division) || lastType == typeof(Multiplicacion) ||
                lastType == typeof(Logaritmica) || lastType == typeof(Exponencial);

            Type Ftype = funcion.GetType();

            switch (funcion)
            {
                case Incognita i:
                    string iret = $"{(i.K > 1 || (i.K == 1 && i.Exponente == 0) ? i.K.ToString() : "")}{(i.Exponente > 0 ? "x" + (i.Exponente > 1 ? $"^{i.Exponente}" : "") : "")}";

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

                    return $"ln{lFx}";

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

        public static Funcion CrearFuncion(string nivel)
        {
            return CrearFuncion(new Queue<string>(nivel.Split(' ')));
        }

        private static Funcion CrearFuncion(Queue<string> elementos)
        {
            switch (elementos.Dequeue())
            {
                case "F":
                    int.TryParse(elementos.Dequeue(), out int k);
                    int.TryParse(elementos.Dequeue(), out int exp);
                    Incognita i = ManagerFunciones.Instance.GetFuncion<Incognita>();
                    i.Init(k, exp);
                    return i;

                case "SUM":
                    Funcion uXs = CrearFuncion(elementos);
                    Funcion vXs = CrearFuncion(elementos);
                    Suma suma = ManagerFunciones.Instance.GetFuncion<Suma>();
                    suma.Init(uXs, vXs);
                    return suma;

                case "RES":
                    Funcion uXr = CrearFuncion(elementos);
                    Funcion vXr = CrearFuncion(elementos);
                    Resta resta = ManagerFunciones.Instance.GetFuncion<Resta>();
                    resta.Init(uXr, vXr);
                    return resta;

                case "DIV":
                    Funcion uXd = CrearFuncion(elementos);
                    Funcion vXd = CrearFuncion(elementos);
                    Division div = ManagerFunciones.Instance.GetFuncion<Division>();
                    div.Init(uXd, vXd);
                    return div;
            }

            return null;
        }
    }

}

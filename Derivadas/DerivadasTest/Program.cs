using Derivadas_LIB;
using Derivadas_LIB.Funciones;

public static class Program
{
    [STAThread]
    public static void Main()
    {
        Incognita eX = new Incognita(3, 3);

        Logaritmica uX = new Logaritmica(new Incognita(4, 5));
        Exponencial vX = new Exponencial((Funcion)eX.Clone());

        Suma suma = new Suma(uX, vX);

        Multiplicacion mult = new Multiplicacion(eX, suma);

        Potencial p = new Potencial(5, (Funcion)eX.Clone(), 3);

        Division div = new Division(mult, p);

        //Incognita uX = new Incognita(1, 1);

        //Suma suma = new Suma((Funcion)uX.Clone(), (Funcion)uX.Clone());

        //Multiplicacion mul = new Multiplicacion(uX, suma);

        //Division div = new Division(mul, (Funcion)uX.Clone());


        Console.WriteLine(ParserFunciones.ParsearString(div, Funcion.Type.None));

        Console.WriteLine(ParserFunciones.ParsearString(div.Derivada(), Funcion.Type.None));
    }
}


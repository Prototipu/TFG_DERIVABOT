using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Derivadas_LIB.Funciones
{
    public class Potencial : Funcion
    {
        public int Exponente;

        public Funcion Fx;

        public int K;

        public bool Derivado;

        [SerializeField]
        public ControladorParedes _paredes;

        [SerializeField]
        private Anclajes _expA, _K_A, _exteriorE, _exteriorW;

        private float _kEspacioBase, _expEspacioBase, _paredEspacioBase;

        [SerializeField]
        private TMP_Text _textoK, _textoExponente;

        public SpriteRenderer SpriteK, SpriteExp;

        public Color ColorNoDerivado, ColorDerivado;

        private void Awake()
        {
            SpriteRenderer spritePared = _exteriorE.transform.parent.GetComponent<SpriteRenderer>();
            SpriteRenderer spriteK = _K_A.transform.parent.GetComponent<SpriteRenderer>();
            SpriteRenderer spriteExp = _expA.transform.parent.GetComponentInChildren<SpriteRenderer>();

            if (!spriteExp || !spriteK || !spritePared)
            {
                Destroy(gameObject);
                throw new Exception($"El potencial no tiene un SpriteRenderer asignado");
            }

            _paredEspacioBase = spritePared.bounds.size.y;
            _kEspacioBase = spriteK.bounds.size.y;
            _expEspacioBase = spriteExp.bounds.size.y;
        }


        public void Init(int k, Funcion fX, int exponente, bool derivado = false)
        {
            Exponente = exponente;
            Fx = fX;
            K = k;

            _textoK.text = K.ToString();
            _textoExponente.text = Exponente.ToString();

            Derivado = derivado;

            if (Derivado)
            {
                SpriteK.color = ColorDerivado;
                SpriteExp.color = ColorDerivado;
            }
            else
            {
                SpriteK.color = ColorNoDerivado;
                SpriteExp.color = ColorNoDerivado;
            }
        }

        public override Funcion Derivada()
        {
            Funcion dFx = Fx.Derivada();

            if (dFx)
            {
                Potencial p = ManagerFunciones.Instance.GetFuncion<Potencial>(K * Exponente, Fx.Clone(), Exponente - 1, true);

                return ManagerFunciones.Instance.GetFuncion<Multiplicacion>(p, dFx);
            }
            else
                return null;
        }

        public override void Swap(Funcion oldFx, Funcion newFx)
        {
            Init(K, newFx, Exponente);
        }

        public override object Clone()
        {
            return ManagerFunciones.Instance.GetFuncion<Potencial>(K, (Funcion)Fx.Clone(), Exponente, Derivado);
        }

        public override void Escalar()
        {
            Fx.Escalar();

            _paredes.EncajarFuncion(Fx, false);


            float maxY = _paredes.GetPunto(Punto.N).position.y;
            float minY = _paredes.GetPunto(Punto.S).position.y;


            float dist = maxY - minY;

            float ratioK = (dist * 0.5f) / _kEspacioBase;
            float ratioExp = (dist * 0.25f) / _expEspacioBase;

            float ratioParedes = dist / _paredEspacioBase;

            _exteriorE.transform.parent.localScale = new Vector2(1, ratioParedes);
            _exteriorW.transform.parent.localScale = new Vector2(1, ratioParedes);
            _K_A.transform.parent.localScale = Vector2.one * ratioK;
            _expA.transform.parent.localScale = Vector2.one * ratioExp;

            _exteriorE.Anclar(_paredes.GetPunto(Punto.E), Punto.W);
            _exteriorW.Anclar(_paredes.GetPunto(Punto.W), Punto.E);

            _K_A.Anclar(_exteriorW.GetPunto(Punto.W), Punto.E);

            _expA.Anclar(_exteriorE.GetPunto(Punto.E), Punto.W);

            _expA.transform.parent.Translate(Vector2.up * ((dist / 2) - (_expEspacioBase * ratioExp) / 2));

            float pMedioHorizontal = (_expA.GetPunto(Punto.E).position.x + _K_A.GetPunto(Punto.W).position.x) / 2;

            anclajes.GetPunto(Punto.N).position = new Vector2(pMedioHorizontal, maxY);
            anclajes.GetPunto(Punto.S).position = new Vector2(pMedioHorizontal, minY);
            anclajes.GetPunto(Punto.E).position = new Vector2(_expA.GetPunto(Punto.E).position.x, _K_A.GetPunto(Punto.E).position.y);
            anclajes.GetPunto(Punto.W).position = _K_A.GetPunto(Punto.W).position;
        }

        public void Cargar()
        {
            ManagerFunciones.Instance.GuardarStack();

            K *= Exponente;
            Exponente--;
            Derivado = true;

            _textoK.text = K.ToString();
            _textoExponente.text = Exponente.ToString();

            SpriteK.color = ColorDerivado;
            SpriteExp.color = ColorDerivado;
        }

        public override Funcion CheckEstado()
        {
            Fx = Fx.CheckEstado();

            if (Fx)
                return this;
            else return null;
        }
    }
}

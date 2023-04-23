using System;
using UnityEngine;

namespace Derivadas_LIB
{
    public abstract class Funcion : MonoBehaviour, ICloneable
    {

        [SerializeField]
        SpriteRenderer _maxSpace;

        public void Escalar(SpriteRenderer scaler)
        {
            transform.SetParent(scaler.transform);
            transform.localPosition = Vector3.zero;

            if (!_maxSpace) return;

            //float width = _maxSpace.sprite.bounds.size.x;
            //float height = _maxSpace.sprite.bounds.size.y;

            //float widthScale = scaler.sprite.bounds.size.x / width;
            //float heightScale = scaler.sprite.bounds.size.y / height;
            //float scale = Mathf.Min(widthScale, heightScale);

            transform.localScale = Vector3.one;

            EscalarI(null);
        }

        public abstract void EscalarI(SpriteRenderer scaler);

        public abstract Funcion Derivada();
        public abstract object Clone();
    }
}
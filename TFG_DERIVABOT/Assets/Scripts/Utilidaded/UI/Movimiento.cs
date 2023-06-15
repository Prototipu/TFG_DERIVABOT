using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Movimiento : MonoBehaviour
{
    [SerializeField]
    private Vector2 _p1, _p2;

    private RectTransform _rectTransform;

    private bool _movimiento;
    public bool EnMovimiento => _movimiento;

    private bool _inicio = true;


    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void MoverRect()
    {
        if (!_movimiento)
        {
            _movimiento = true;
            _inicio = !_inicio;

            StartCoroutine(MovimientoRect());
        }
    }

    public void MoverRect(bool inicio)
    {
        if (!_movimiento)
        {
            _movimiento = true;
            _inicio = inicio;

            StartCoroutine(MovimientoRect());
        }
    }


    private IEnumerator MovimientoRect()
    {
        Vector2 target = _inicio ? _p1 : _p2;
        Vector2 start = _inicio ? _p2 : _p1;

        float elapsed = 0f;
        float duration = 0.2f;


        while (elapsed < duration)
        {
            _rectTransform.anchoredPosition = Vector2.Lerp(start, target, elapsed / duration);

            elapsed += Time.deltaTime;

            yield return null;
        }

        _rectTransform.anchoredPosition = target;

        _movimiento = false;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}

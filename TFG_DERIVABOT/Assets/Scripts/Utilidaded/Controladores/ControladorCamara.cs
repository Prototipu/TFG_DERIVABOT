using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorCamara : MonoBehaviour
{
    [SerializeField]
    private Camera _camara;

    [SerializeField]
    private float _velocidadPan = 0.01f, _velocidadZoom = 0.5f;

    private float _maxOrth = 10;

    private Vector2 _anteriorPosicion1, _anteriorPosicion2;

    private Vector2 _maxBounds, _minBounds;

    private Vector2 _distanciaCamara;

    [SerializeField]
    ManagerFunciones _managerFunciones;


    private void Awake()
    {
        _managerFunciones.OnFuncionEscalada += OnFuncionEscalada;
    }

    private void Update()
    {
        CalcularDistancias();

        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            // If it's the start of touch, save the position
            if (touch.phase == TouchPhase.Began)
            {
                _anteriorPosicion1 = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                // Calculate the new camera position
                Vector2 newTouchPosition = touch.position;
                Vector3 displacement = _camara.ScreenToWorldPoint(_anteriorPosicion1) - _camara.ScreenToWorldPoint(newTouchPosition);

                // Move the camera
                Vector3 newPosition = _camara.transform.position + displacement * _velocidadPan;

                // Clamp the position to the min and max bounds
                newPosition.x = Mathf.Clamp(newPosition.x, _minBounds.x + _distanciaCamara.x, _maxBounds.x - _distanciaCamara.x);
                newPosition.y = Mathf.Clamp(newPosition.y, _minBounds.y + _distanciaCamara.y, _maxBounds.y - _distanciaCamara.y);

                _camara.transform.position = newPosition;

                _anteriorPosicion1 = newTouchPosition;
            }
        }
        else if (Input.touchCount > 1) // If two fingers are touching the screen
        {
            // Store both touch positions
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
            {
                _anteriorPosicion1 = touchZero.position;
                _anteriorPosicion2 = touchOne.position;
            }
            else if (touchZero.phase == TouchPhase.Moved || touchOne.phase == TouchPhase.Moved)
            {
                Vector2 newTouchPosition = touchZero.position;
                Vector2 newTouchPosition2 = touchOne.position;

                float oldTouchDeltaMagnitude = (_anteriorPosicion1 - _anteriorPosicion2).magnitude;
                float newTouchDeltaMagnitude = (newTouchPosition - newTouchPosition2).magnitude;

                float deltaMagnitudeDiff = oldTouchDeltaMagnitude - newTouchDeltaMagnitude;

                _camara.orthographicSize += deltaMagnitudeDiff * _velocidadZoom;
                _camara.orthographicSize = Mathf.Clamp(_camara.orthographicSize, 0.1f, _maxOrth);

                _anteriorPosicion1 = newTouchPosition;
                _anteriorPosicion2 = newTouchPosition2;
                CalcularDistancias();
            }
        }
    }


    private void OnFuncionEscalada(Vector3 pos, Vector2 size)
    {
        size *= 1.25f;

        _camara.transform.position = new Vector3(pos.x, pos.y, _camara.transform.position.z);

        float ratio = size.x / size.y;

        if (_camara.aspect < ratio)
            _camara.orthographicSize = size.x / 2 / _camara.aspect;
        else
            _camara.orthographicSize = size.y / 2;

        _maxOrth = _camara.orthographicSize;

        Vector2 halfSize = new Vector2(_camara.aspect * _camara.orthographicSize, _camara.orthographicSize);
        _maxBounds = (Vector2)_camara.transform.position + halfSize;
        _minBounds = (Vector2)_camara.transform.position - halfSize;

        CalcularDistancias();
    }

    private void CalcularDistancias()
    {
        // Get the world coordinates of the center of the camera's viewport
        Vector3 centerViewportPoint = new Vector3(0.5f, 0.5f, _camara.nearClipPlane);
        Vector3 centerWorldPoint = _camara.ViewportToWorldPoint(centerViewportPoint);

        // Get the world coordinates of the corner of the camera's viewport
        Vector3 cornerViewportPoint = new Vector3(1, 1, _camara.nearClipPlane); // or (0, 0) for the opposite corner
        Vector3 cornerWorldPoint = _camara.ViewportToWorldPoint(cornerViewportPoint);

        // Calculate the distance
        _distanciaCamara = cornerWorldPoint - centerWorldPoint;
    }


    private void OnDestroy()
    {
        if (ManagerFunciones.Instance)
            ManagerFunciones.Instance.OnFuncionEscalada -= OnFuncionEscalada;
    }
}

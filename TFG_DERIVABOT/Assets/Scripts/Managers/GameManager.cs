using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private string _nivelActual = "SUM X 2 3 X 4 1";


    public delegate void DlgCambioVolumen(float volumen);

    public event DlgCambioVolumen OnCambioVolumen;

    private float _volumen = 0.5f;

    public float Volumen => _volumen;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }


    public void CargarNivel(string nivel)
    {
        _nivelActual = nivel;
        SceneManager.LoadScene("Nivel");
    }


    public string GetNivelActual()
    {
        return _nivelActual;
    }


    public void SliderVolumen(float volumen)
    {
        _volumen = volumen;
        OnCambioVolumen?.Invoke(volumen);
    }
}

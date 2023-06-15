using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private string _nivelActual = "";

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
}

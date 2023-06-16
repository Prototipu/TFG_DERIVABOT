using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic Instance;

    [SerializeField]
    private AudioSource _source;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        GameManager.Instance.OnCambioVolumen += OnCambioVolumen;
    }

    private void OnCambioVolumen(float volumen)
    {
        _source.volume = volumen;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnCambioVolumen -= OnCambioVolumen;
    }
}

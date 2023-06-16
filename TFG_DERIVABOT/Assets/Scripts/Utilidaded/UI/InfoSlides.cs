using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoSlides : MonoBehaviour
{
    //[SerializeField]

    [SerializeField]
    private Image _image;


    [SerializeField]
    private List<Sprite> _slides;

    private int _slidePos;


    private void Awake()
    {
        _image.sprite = _slides[0];
    }

    public void Next()
    {
        _slidePos = (_slidePos + 1) % _slides.Count;

        _image.sprite = _slides[_slidePos];
    }


    public void Previous()
    {
        _slidePos = (_slidePos - 1 + _slides.Count) % _slides.Count;

        _image.sprite = _slides[_slidePos];
    }
}

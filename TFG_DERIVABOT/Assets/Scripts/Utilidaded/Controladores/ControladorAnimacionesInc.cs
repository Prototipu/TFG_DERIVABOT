using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorAnimacionesInc : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    private bool _lockAnimation = false;


    private void Start()
    {
        StartCoroutine(RandomIdle());
    }

    public void Play(string animacion, bool lockAnimation = false)
    {
        if (!_lockAnimation)
        {
            _animator.Play(animacion);
            _lockAnimation = lockAnimation;
        }
    }

    public void Play(string animacion, int capa, float time)
    {
        if (!_lockAnimation)
        {
            _animator.Play(animacion, capa, time);
        }
    }

    public AnimatorStateInfo GetEstado()
    {
        return _animator.GetCurrentAnimatorStateInfo(0);
    }


    private IEnumerator RandomIdle()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(10f, 25f));

            if (_lockAnimation)
                break;

            if (_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "IdleAnimation")
                Play($"IdleVariant{Random.Range(1, 3)}");
        }
    }


    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}

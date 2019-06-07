using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScreenHandler : MonoBehaviour
{
    [SerializeField] private Image black;
    [SerializeField] private Animator anim;

    private void Start()
    {
        StartCoroutine(Fading());
    }

    IEnumerator Fading()
    {
        anim.SetBool("FadeIn", true);
        yield return new WaitForSeconds(2f);
        anim.SetBool("FadeOut", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(1);
    }
}

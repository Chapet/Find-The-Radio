﻿ using System;
 using System.Collections;
 using System.Collections.Generic;
 using TMPro;
 using UnityEngine;

public class SnackbarController : MonoBehaviour
{

    /*public void OnEnable()
    {
        Animator a = this.gameObject.GetComponent<Animator>();
        StartCoroutine(Animation(a));

    }*/
    

    [SerializeField] private TextMeshProUGUI text;
    private float animDuration = 0.4f;

    public void ShowSnackBar(String txt)
    {
		this.gameObject.SetActive(true);
        Animator anim = gameObject.GetComponent<Animator>();
        gameObject.SetActive(true);
        StartCoroutine(Animation(anim, 1.5f, txt));
    }

    private IEnumerator Animation(Animator anim, float showDuration, string txt)
    {
        while (!anim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            yield return new WaitForSeconds(animDuration / 4f);
        }
        text.SetText(txt);
        anim.SetBool("Hide", false);
        anim.SetBool("Show", true);
        yield return new WaitForSeconds(animDuration + showDuration);
        anim.SetBool("Hide", true);
        anim.SetBool("Show", false);
        yield return new WaitForSeconds(animDuration);
        anim.SetBool("Hide", false);
		this.gameObject.SetActive(false);
    }

    /*
    public void ResetAnimation()
    {
        this.gameObject.GetComponent<Animator>().SetBool("do",false);
    }
    */

}

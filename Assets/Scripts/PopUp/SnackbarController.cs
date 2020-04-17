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

    public void StartSnackBare(String txt)
    {
        text.SetText(txt);
        gameObject.SetActive(true);
        StartCoroutine(Animation(this.gameObject.GetComponent<Animator>()));
    }

    private IEnumerator Animation(Animator animator)
    {
        animator.SetBool("do",true);
        yield return new WaitForSeconds(2);
    }

    public void ResetAnimation()
    {
        this.gameObject.GetComponent<Animator>().SetBool("do",false);
    }

}

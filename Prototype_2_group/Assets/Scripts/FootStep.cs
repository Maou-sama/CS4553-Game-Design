using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FootStep : MonoBehaviour {
    public float existTime;

    SpriteRenderer spr;
	// Use this for initialization
	void Start () {
        spr = GetComponent<SpriteRenderer>();
        spr.DOFade(0f, existTime).OnComplete(() => { Destroy(gameObject); }).Play();
    }

}

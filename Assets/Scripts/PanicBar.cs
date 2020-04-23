﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanicBar : MonoBehaviour
{

    private Slider panicBar;
    private Image barFill;
    // Start is called before the first frame update
    void Start()
    {
        panicBar = GetComponent<Slider>();
        barFill = panicBar.fillRect.GetComponentInChildren<Image>();
        panicBar.value = panicBar.minValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (panicBar.value < panicBar.maxValue) {
            panicBar.value += Time.deltaTime;

            if(panicBar.value >= panicBar.maxValue) {
                GameManager.panic = true;
            }
        }

        if (panicBar.value == 100) {

            float lerpValue = Mathf.Pow(Mathf.Sin(Time.time*5), 2);

            barFill.color = Color.Lerp(Color.red, Color.yellow, lerpValue);
        }

        if (Input.GetKey(KeyCode.Equals)) {
            panicBar.value = 99;
        }
    }
}
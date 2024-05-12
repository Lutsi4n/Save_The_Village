using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Image_Timer : MonoBehaviour
{
    public float MaxTime;
    public bool tick;
    private Image img;
    private float currentTime;

    void Start()
    {
        img = GetComponent<Image>();
        currentTime = MaxTime;
    }

    void Update()
    {
        tick = false;
        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            tick = true;
            currentTime = MaxTime;
        }

        img.fillAmount = currentTime / MaxTime;
    }
}

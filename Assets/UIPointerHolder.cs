using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPointerHolder : MonoBehaviour
{
    private static UIPointerHolder _instance;
    public static UIPointerHolder getInstance => _instance;


    public Slider musicSlider;
    public Slider soundSlider;


    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else if(_instance != this)
        {
            Destroy(this.gameObject);
        }
    }
}

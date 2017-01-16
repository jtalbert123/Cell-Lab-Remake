using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ColorSlider : MonoBehaviour {

    public Slider.SliderEvent OnValueChanged;

    public float value
    {
        get { return slider.value; }
        set { slider.value = value; }
    }

    public Channel channel;
    private Slider slider;
    private Text label;

    public enum Channel
    {
        Red, Green, Blue
    }

    // Use this for initialization
    void Awake () {
        slider = GetComponentInChildren<Slider>();
        label = GetComponentInChildren<Text>();
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    void Start()
    {
        label.text = channel.ToString();
    }

    public void OnSliderValueChanged(float value)
    {
        if (OnValueChanged != null)
        {
            OnValueChanged.Invoke(value);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CellColor : MonoBehaviour {

    public Slider.SliderEvent OnValueChanged;

    public Color Color
    {
        get { return new Color(Red.value, Green.value, Blue.value); }
        set
        {
            Red.value = value.r;
            Green.value = value.g;
            Blue.value = value.b;
        }
    }

    public ColorSlider Red;
    public ColorSlider Green;
    public ColorSlider Blue;

    private GenomeEditor editor;

    // Use this for initialization
    void Awake () {
        editor = GetComponentInParent<GenomeEditor>();
        Red.OnValueChanged.AddListener(OnSliderValueChanged);
        Green.OnValueChanged.AddListener(OnSliderValueChanged);
        Blue.OnValueChanged.AddListener(OnSliderValueChanged);

        Red.channel = ColorSlider.Channel.Red;
        Green.channel = ColorSlider.Channel.Green;
        Blue.channel = ColorSlider.Channel.Blue;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EditModeChanged()
    {
        Color = editor.CurrentGenome.modes[editor.CurrentMode].Color;
    }

    public void OnSliderValueChanged(float value)
    {
        editor.CurrentGenome.modes[editor.CurrentMode].Color = Color;

        if (OnValueChanged != null)
        {
            OnValueChanged.Invoke(value);
        }
    }
}

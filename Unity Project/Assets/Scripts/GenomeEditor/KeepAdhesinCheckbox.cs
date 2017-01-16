using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeepAdhesinCheckbox : MonoBehaviour {

    private GenomeEditor editor;

    private Toggle toggle;
    private bool SavedState;

    public Child child;

    public enum Child
    {
        One = 1,
        Two = 2
    }

    // Use this for initialization
    void Awake()
    {
        editor = GetComponentInParent<GenomeEditor>();
        toggle = GetComponent<Toggle>();
    }

    private void Start()
    {
    }

    public void EditModeChanged()
    {
        if (child == Child.One)
        {
            toggle.isOn = editor.CurrentGenome.modes[editor.CurrentMode].Child1KeepAdhesin;
        }
        else
        {
            toggle.isOn = editor.CurrentGenome.modes[editor.CurrentMode].Child2KeepAdhesin;
        }
    }

    public void SelectedModeChanged()
    {
        if (child == Child.One)
        {
            editor.CurrentGenome.modes[editor.CurrentMode].Child1KeepAdhesin = toggle.isOn;
        }
        else
        {
            editor.CurrentGenome.modes[editor.CurrentMode].Child2KeepAdhesin = toggle.isOn;
        }
    }
}

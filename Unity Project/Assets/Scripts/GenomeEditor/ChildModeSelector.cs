using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChildModeSelector : MonoBehaviour {

    public enum Child
    {
        One = 1,
        Two = 2,
    }

    public Child child;
    public Text textObject;

    private GenomeEditor editor;

    private Dropdown dropdown;

    // Use this for initialization
    void Start () {
        textObject.text = "Child " + (int)child + ":";
        editor = GetComponentInParent<GenomeEditor>();
        dropdown = GetComponentInChildren<Dropdown>();
    }

    public void EditModeChanged()
    {
        if (child == Child.One)
        {
            dropdown.value = editor.CurrentGenome[editor.CurrentMode].Child1ModeIndex;
        }
        else
        {
            dropdown.value = editor.CurrentGenome[editor.CurrentMode].Child2ModeIndex;
        }
    }

    public void SelectedModeChanged(int mode)
    {
        if (child == Child.One)
        {
            editor.CurrentGenome.modes[editor.CurrentMode].Child1ModeIndex = dropdown.value;
        }
        else
        {
            editor.CurrentGenome.modes[editor.CurrentMode].Child2ModeIndex = dropdown.value;
        }
    }
}

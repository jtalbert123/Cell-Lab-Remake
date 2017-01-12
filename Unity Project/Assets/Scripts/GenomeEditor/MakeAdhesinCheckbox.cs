using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeAdhesinCheckbox : MonoBehaviour {

    private GenomeEditor editor;

    private Toggle toggle;

    // Use this for initialization
    void Start()
    {
        editor = GetComponentInParent<GenomeEditor>();
        toggle = GetComponent<Toggle>();
    }

    public void EditModeChanged()
    {
        toggle.isOn = editor.CurrentGenome.modes[editor.CurrentMode].MakeAdhesin;
    }

    public void SelectedModeChanged(int mode)
    {
        editor.CurrentGenome.modes[editor.CurrentMode].MakeAdhesin = toggle.isOn;
    }
}

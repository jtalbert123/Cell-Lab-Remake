using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateGenomeEditorCurrentModeWithDropdown : MonoBehaviour {

    private GenomeEditor editor;

    private Dropdown dropdown;

    // Use this for initialization
    void Awake()
    {
        editor = GetComponentInParent<GenomeEditor>();
        dropdown = GetComponent<Dropdown>();
    }

    public void SelectedModeChanged(int mode)
    {
        editor.CurrentMode = dropdown.value;
    }
}

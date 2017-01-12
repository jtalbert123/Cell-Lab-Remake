﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellModeSelector : MonoBehaviour {

    private Dropdown dropdown;
    private GenomeEditor editor;

    // Use this for initialization
    void Start () {
        dropdown = GetComponent<Dropdown>();
        editor = GetComponentInParent<GenomeEditor>();

        GenomeModesChanged();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void GenomeModesChanged()
    {
        if (editor.CurrentGenome.ModeCount < dropdown.options.Count)
        {
            dropdown.options.RemoveRange(editor.CurrentGenome.ModeCount, dropdown.options.Count - editor.CurrentGenome.ModeCount);
        }
        else
        {
            for (int i = dropdown.options.Count; i < editor.CurrentGenome.ModeCount; i++)
            {
                dropdown.options.Add(new Dropdown.OptionData("Mode " + i));
            }
        }
    }
}

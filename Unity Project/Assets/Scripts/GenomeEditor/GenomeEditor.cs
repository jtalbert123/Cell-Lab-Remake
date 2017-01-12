using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenomeEditor : MonoBehaviour {

    public bool active;

    public bool Active
    {
        get { return active; }
        set
        {
            active = value;
            gameObject.SetActive(value);
        }
    }

    public Genome CurrentGenome;

    private int currentMode;
    public int CurrentMode
    {
        get { return currentMode; }
        set
        {
            bool changed = (currentMode != value);
            currentMode = value;
            if (changed)
            {
                BroadcastMessage("EditModeChanged");
            }
        }
    }

    private void Awake()
    {
        CurrentGenome = new Genome(20);
    }

    // Use this for initialization
    void Start () {
        
    }

    private void CallGenomeModeAdded(CellMode mode)
    {
        BroadcastMessage("GenomeModeAdded", mode);
        BroadcastMessage("GenomeModesChanged");
    }

    public CellMode AddMode()
    {
        CellMode mode = new CellMode(CurrentGenome.ModeCount);
        CurrentGenome.AddMode(mode);

        CallGenomeModeAdded(mode);
        return mode;
    }
}

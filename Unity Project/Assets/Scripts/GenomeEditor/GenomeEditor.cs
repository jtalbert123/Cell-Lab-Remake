using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    //private Texture2D background;
    //private GUIStyle style;

    //private void OnGUI()
    //{
    //    if (style == null)
    //    {
    //        background = MakeTex(2, 2, Color.gray);
    //        style = new GUIStyle(GUI.skin.box);
    //        style.normal.background = background;
    //    }
    //    if (active)
    //    {
    //        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Genome Editor", style);
    //        GUI.(new Rect(5, 25, 100, 30), );
    //    }
    //}

    private Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; ++i)
        {
            pix[i] = col;
        }
        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
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

public interface StateSaving
{
    void SaveState();

    void RestoreState();
}
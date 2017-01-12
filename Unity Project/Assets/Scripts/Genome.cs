using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public struct Genome
{
    public IEnumerable<CellMode> Modes { get { return modes; } }
    public int ModeCount { get { return modes == null ? 0 : modes.Length; } }
    public CellMode InitialMode { get { return modes[InitialModeIndex]; } }
    public int InitialModeIndex;

    public CellMode[] modes;

    public static Genome DefaultGenome { get; private set; }

    static Genome()
    {
        Genome genome = new Genome();
        genome.InitialModeIndex = 0;
        CellMode mode1 = new CellMode(0);
        genome.AddMode(mode1);
    }

    public Genome(int count)
    {
        InitialModeIndex = 0;
        modes = new CellMode[count];
        for (int i = 0; i < count; i++)
        {
            modes[i] = new CellMode(i);
        }
    }

    public Genome Clone()
    {
        Genome other = new Genome();
        other.modes = modes.Clone() as CellMode[];
        other.InitialModeIndex = InitialModeIndex;
        return other;
    }

    public CellMode this[int index]
    {
        get { return modes[index]; }
    }

    internal void AddMode(CellMode mode)
    {
        CellMode[] newarr = new CellMode[ModeCount + 1];
        for (int i = 0; i < ModeCount; i++)
        {
            newarr[i] = modes[i];
        }
        newarr[ModeCount] = mode;
        modes = newarr;
    }
}

[Serializable]
public enum CellType
{
    Photocyte
}

[Serializable]
public struct CellMode
{
    public CellType Type;
    public Color Color;
    public bool MakeAdhesin;
    public float SplitMass;
    public float SplitAngle;
    public int Child1ModeIndex;
    public int Child2ModeIndex;
    public bool Child1KeepAdhesin;
    public bool Child2KeepAdhesin;
    public float Child1Angle;
    public float Child2Angle;

    public CellMode(int index)
    {
        Type = CellType.Photocyte;
        Child1ModeIndex = index;
        Child2ModeIndex = index;
        Child1KeepAdhesin = false;
        Child2KeepAdhesin = false;
        Color = Color.green;
        MakeAdhesin = false;
        SplitMass = 2.54f;
        SplitAngle = 0f;
        Child1Angle = 0f;
        Child2Angle = 0f;
    }
}
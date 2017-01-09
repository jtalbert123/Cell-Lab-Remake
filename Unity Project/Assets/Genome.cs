using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Genome : ICloneable
{
    public IEnumerable<CellMode> Modes { get { return modes; } }
    public CellMode InitialMode { get; set; }

    private List<CellMode> modes;

    public Genome()
    {
        modes = new List<CellMode>();
        InitialMode = null;
    }

    public object Clone()
    {
        Genome other = new Genome();
        foreach (CellMode mode in modes)
        {
            CellMode clone = new CellMode(mode);
            other.modes.Add(clone);
        }
        for (int i = 0; i < modes.Count; i++)
        {
            CellMode local = modes[i];
            CellMode remote = other.modes[i];
            remote.Child1 = other.modes[modes.IndexOf(local.Child1)];
            remote.Child2 = other.modes[modes.IndexOf(local.Child2)];
        }
        return other;
    }
}

public enum CellType
{
    Photocyte
}

public class CellMode
{
    public CellType Type { get; set; }
    public float SplitMass { get; set; }
    public CellMode Child1 { get; set; }
    public CellMode Child2 { get; set; }
    public Color Color { get; set; }

    public CellMode()
    {

    }

        /// <summary>
        /// Copies the value parameters from other to the new mode.
        /// Does not copy child references.
        /// </summary>
        /// <param name = "other" > the mode to copy from</param>
        internal CellMode(CellMode other)
    {
        Type = other.Type;
        SplitMass = other.SplitMass;
    }
}
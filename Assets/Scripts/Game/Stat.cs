using System.Collections;
using System.Collections.Generic;
using System;
public class Stat
{
   public float baseValue;
   public string name;
   public string desc;
   private readonly List<StatModifier> modifiers;
    // Start is called before the first frame update
    public Stat(string _name, string _desc, float value)
    {
      name = _name;
      desc = _desc;
      baseValue = value;
      modifiers = new List<StatModifier>();
    }

    public float Value { get { return CalculateFinalValue(); } }

    public void AddModifier(StatModifier mod)
    {
      modifiers.Add(mod);
    }

    public bool RemoveModifier(StatModifier mod)
    {
      return modifiers.Remove(mod);
    }

    private float CalculateFinalValue()
    {

      float finalValue = baseValue;

      for (int i = 0; i < modifiers.Count; i++)
      {
          finalValue += modifiers[i].value;
      }

      return (float)Math.Round(finalValue, 0);
    }

}

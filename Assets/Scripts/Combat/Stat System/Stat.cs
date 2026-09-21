using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    public float Value {
        get
        {
            if (isDirty)
            {
                value = CalculateFinalValue();
                isDirty = false;
            }

            return value;
        }
    }

    public float baseValue;

    private List<StatModifier> modifiers;
    private bool isDirty = true;
    private float value;

    public Stat(float _baseValue)
    {
        baseValue = _baseValue;
        modifiers = new List<StatModifier>();
    }

    public void Initialize()
    {
        isDirty = true;
        modifiers = new List<StatModifier>();
    }

    public void AddModifier(StatModifier mod)
    {
        isDirty = true;
        modifiers.Add(mod);
        modifiers.Sort(SortMod);
    }

    public bool RemoveModifier(StatModifier mod)
    {
        if (modifiers.Remove(mod))
        {
            isDirty = true;
            return true;
        }

        return false;
    }

    public bool RemoveAllModifiersFromSource(object source)
    {
        bool didRemove = false;

        for (int i = modifiers.Count - 1; i >= 0; i--)
        {
            if (modifiers[i].Source == source)
            {
                modifiers.RemoveAt(i);
                isDirty = true;

                didRemove = true;
            }
        }

        return didRemove;
    }

    private float CalculateFinalValue()
    {
        float finalValue = baseValue;
        float sumPercentAdd = 0f;

        for (int i = 0; i < modifiers.Count; i++)
        {
            StatModifier mod = modifiers[i];

            if (mod.Type == StatModType.Flat)
            {
                finalValue += mod.Value;
            }
            else if (mod.Type == StatModType.PercentAdd)
            {
                sumPercentAdd += mod.Value;
                if (i + 1 >= modifiers.Count || modifiers[i + 1].Type != StatModType.PercentAdd)
                {
                    finalValue *= 1 + sumPercentAdd;
                    sumPercentAdd = 0f;
                }
            }
            else if (mod.Type == StatModType.PercentMult)
            {
                finalValue *= 1 + mod.Value;
            }
        }

        return Mathf.FloorToInt(finalValue);
    }

    private int SortMod(StatModifier a, StatModifier b)
    {
        if (a.Order < b.Order)
        {
            return -1;
        }

        if (a.Order > b.Order)
        {
            return 1;
        }

        return 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Growable;

public class GrowableManager : MonoBehaviour
{
    public Growable CurrentGrowable;

    public Impact CurrentTemperatureImpact;
    public Impact CurrentSunImpact;
    public Impact CurrentWaterImpact;

    public int CurrentTemperatureRating;
    public int CurrentSunRating;
    public int CurrentWaterRating;
    
    void Start()
    {
        CurrentTemperatureRating = Random.Range(0, CurrentGrowable.TempImpact.Count);
        CurrentSunRating = Random.Range(0, CurrentGrowable.SunImpact.Count);
        CurrentWaterRating = Random.Range(0, CurrentGrowable.WaterImpact.Count);
        UpdateUpdateImpacts();
    }

    public void UpdateUpdateImpacts()
    {
        CurrentTemperatureImpact = CurrentGrowable.TempImpact[CurrentTemperatureRating];
        CurrentSunImpact = CurrentGrowable.SunImpact[CurrentSunRating];
        CurrentWaterImpact = CurrentGrowable.WaterImpact[CurrentWaterRating];
    }

    public void ModifyRating(ImpactProperty prop, int value, bool isSilent = false)
    {
        print("Modifying Growable: " + prop + " | " + value);
        switch (prop)
        {
            case ImpactProperty.Temperature:
                CurrentTemperatureRating = Mathf.Clamp(CurrentTemperatureRating + value, 0, CurrentGrowable.TempImpact.Count - 1);                
                break;
            case ImpactProperty.Sun:
                CurrentSunRating = Mathf.Clamp(CurrentSunRating + value, 0, CurrentGrowable.SunImpact.Count - 1);
                break;
            default:
                CurrentWaterRating = Mathf.Clamp(CurrentWaterRating + value, 0, CurrentGrowable.WaterImpact.Count - 1);
                break;
        }
        UpdateUpdateImpacts();
        if (isSilent) return;
        InfoBubble.instance.Show(this);
    }

}

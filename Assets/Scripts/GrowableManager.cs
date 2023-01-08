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

    public Sprite IconPositive;
    public Sprite IconNegative;
    public Sprite IconNeutral;

    public SpriteRenderer IconTemp;
    public SpriteRenderer IconSun;
    public SpriteRenderer IconWater;

    public bool IsShowingIcons;
    public SpriteRenderer PlantImage;

    void Start()
    {
        GameManager.instance.Controller.Level.IconToggle.performed += IconToggle_performed;

        CurrentTemperatureRating = Random.Range(0, CurrentGrowable.TempImpact.Count);
        CurrentSunRating = Random.Range(0, CurrentGrowable.SunImpact.Count);
        CurrentWaterRating = Random.Range(0, CurrentGrowable.WaterImpact.Count);
        UpdateUpdateImpacts();
        //HideIcons();
        //ShowIcons();
    }

    private void IconToggle_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        ToggleIcons();
    }

    public void UpdateUpdateImpacts()
    {
        CurrentTemperatureImpact = CurrentGrowable.TempImpact[CurrentTemperatureRating];
        CurrentSunImpact = CurrentGrowable.SunImpact[CurrentSunRating];
        CurrentWaterImpact = CurrentGrowable.WaterImpact[CurrentWaterRating];

        IconTemp.sprite = GetIcons(CurrentGrowable.TempImpact, CurrentTemperatureRating);
        IconSun.sprite = GetIcons(CurrentGrowable.SunImpact, CurrentSunRating);
        IconWater.sprite = GetIcons(CurrentGrowable.WaterImpact, CurrentWaterRating);
    }

    public void ModifyRating(ImpactProperty prop, int value, bool isSilent = false)
    {
        //print("Modifying Growable: " + prop + " | " + value);
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

    public Sprite GetIcons(List<Impact> impacts, int curRating)
    {
        switch (impacts[curRating]) 
        { 
            case Impact.Positive:
                return IconPositive;                
            case Impact.Negative:
                return IconNegative;
            default:
                return IconNeutral;               
        }
    }


    public void ToggleIcons()
    {
        if (IsShowingIcons)
        {
            HideIcons();
        }
        else
        {
            ShowIcons();
        }
    }

    public void ShowIcons()
    {
        IsShowingIcons = true;
        PlantImage.color = Color.white.ReplaceA(0.6f);
        IconTemp.gameObject.SetActive(true);
        IconSun.gameObject.SetActive(true);
        IconWater.gameObject.SetActive(true);
    }

    public void HideIcons()
    {
        IsShowingIcons = false;
        PlantImage.color = Color.white.ReplaceA(1f);
        IconTemp.gameObject.SetActive(false);
        IconSun.gameObject.SetActive(false);
        IconWater.gameObject.SetActive(false);
    }

    public int GetHarvest()
    {
        if(CurrentGrowable.TempImpact[CurrentTemperatureRating] == Impact.Positive &&
            CurrentGrowable.SunImpact[CurrentSunRating] == Impact.Positive &&
            CurrentGrowable.WaterImpact[CurrentWaterRating] == Impact.Positive)
        {
            this.gameObject.SetActive(false);
            print("harvested " + this.gameObject.name);
            return 1;
        }
        return 0;
    }

    public int TryReap()
    {
        if (CurrentGrowable.TempImpact[CurrentTemperatureRating] == Impact.Negative &&
            CurrentGrowable.SunImpact[CurrentSunRating] == Impact.Negative &&
            CurrentGrowable.WaterImpact[CurrentWaterRating] == Impact.Negative)
        {
            this.gameObject.SetActive(false);
            print("reaped " + this.gameObject.name);
            return 1;
        }
        return 0;
    }

}

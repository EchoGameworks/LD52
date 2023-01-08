using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoBubble : MonoBehaviour
{
    public static InfoBubble instance;

    private RectTransform rt;

    public RatingBar TemperatureBar;
    public RatingBar SunBar;
    public RatingBar WaterBar;

    public bool IsShowing;

    void Awake()
    {
        //Singleton
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        rt = GetComponent<RectTransform>();
        rt.anchoredPosition3D = Vector3.zero.ReplaceX(-200f);
    }

    public void Show(GrowableManager gm)
    {
        TemperatureBar.UpdateIcons(gm.CurrentGrowable.TempImpact, gm.CurrentTemperatureRating);
        SunBar.UpdateIcons(gm.CurrentGrowable.SunImpact, gm.CurrentSunRating);
        WaterBar.UpdateIcons(gm.CurrentGrowable.WaterImpact, gm.CurrentWaterRating);
        if (!IsShowing)
        {
            IsShowing = true;
            LeanTween.value(gameObject, rt.anchoredPosition3D.x, 150f, 0.3f)
                .setEaseInOutCirc()
                .setOnUpdate((float val) => rt.anchoredPosition3D = Vector3.zero.ReplaceX(val));
        }

    }

    public void Hide()
    {
        if (IsShowing)
        {
            IsShowing = false;
            LeanTween.value(gameObject, rt.anchoredPosition3D.x, -200f, 0.3f)
                .setEaseInOutCirc()
                .setOnUpdate((float val) => rt.anchoredPosition3D = Vector3.zero.ReplaceX(val));
        }
    }
}

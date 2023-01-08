using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GrowableManager CurrentGrowableManager;
    public Vector2Int GridPosition;
    public SpriteRenderer SingleTarget;
    private SpriteRenderer tileSR;

    public bool IsHighlighted;
    public void Init(Vector2Int gridPosition)
    {
        tileSR = GetComponent<SpriteRenderer>();
        GridPosition = gridPosition;
        HideSingleTarget();
    }

    public void SetGrowable(GrowableManager gm)
    {
        CurrentGrowableManager = gm;
        gm.transform.position = this.transform.position;
    }

    public void Highlight()
    {
        if (IsHighlighted) return;
        IsHighlighted = true;

        tileSR.color = new Color(100f / 255f, 100f / 255f, 100f / 255f);
    }

    public void UnHighlight()
    {
        if(!IsHighlighted) return;
        IsHighlighted = false;
        tileSR.color = Color.white;
    }

    public void ShowSingleTarget(Sprite previewSymbol)
    {
        SingleTarget.sprite = previewSymbol;
        SingleTarget.gameObject.SetActive(true);
    }

    public void HideSingleTarget()
    {
        SingleTarget?.gameObject.SetActive(false);
    }
}

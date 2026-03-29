using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// NGUI stub: atlas asset containing packed sprite definitions and material reference.
/// Will be replaced with functional uGUI equivalent in Phase 8.
/// </summary>
public class UIAtlas : MonoBehaviour
{
    public List<UISpriteData> spriteList = new List<UISpriteData>();
    public Material spriteMaterial;
    public UIAtlas replacement;
    public float pixelSize = 1f;

    public UISpriteData GetSprite(string name) { return null; }

    public string GetRandomSprite(string startsWith) { return ""; }

    public BetterList<string> GetListOfSprites() { return new BetterList<string>(); }

    public BetterList<string> GetListOfSprites(string match) { return new BetterList<string>(); }

    public bool References(UIAtlas atlas) { return false; }

    public void MarkSpriteListAsChanged() { }
}

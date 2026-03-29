using UnityEngine;

/// <summary>
/// NGUI stub: font asset component holding bitmap font or dynamic font reference.
/// Provides CalculatePrintedSize for text measurement and SymbolStyle enum.
/// Will be replaced with functional uGUI equivalent in Phase 8.
/// </summary>
public class UIFont : MonoBehaviour
{
    public enum SymbolStyle
    {
        None,
        Normal,
        Colored,
    }

    public BMFont bmFont;
    public Font dynamicFont;
    public int dynamicFontSize = 16;
    public UIAtlas atlas;
    public string spriteName = "";
    public Rect uvRect = new Rect(0f, 0f, 1f, 1f);

    public int size { get { return dynamicFontSize; } set { dynamicFontSize = value; } }

    public int defaultSize { get { return dynamicFontSize; } }

    public bool hasSymbols { get { return false; } }

    public Vector2 CalculatePrintedSize(string text, bool encoding, SymbolStyle symbolStyle)
    {
        return Vector2.zero;
    }

    public void Print(string text, Color32 color, BetterList<Vector3> verts,
        BetterList<Vector2> uvs, BetterList<Color32> cols,
        bool encoding, SymbolStyle symbolStyle, NGUIText.Alignment alignment,
        int lineWidth, bool premultiply)
    {
    }
}

/// <summary>
/// NGUI stub: bitmap font glyph data container.
/// </summary>
public class BMFont
{
    public int charSize { get { return 16; } }
    public int baseOffset { get { return 0; } }
    public int texWidth { get { return 0; } }
    public int texHeight { get { return 0; } }
    public int glyphCount { get { return 0; } }
    public bool isValid { get { return false; } }
}

/// <summary>
/// NGUI stub: generic resizable list used internally by NGUI.
/// </summary>
public class BetterList<T>
{
    public T[] buffer;
    public int size;

    public void Add(T item) { }
    public void Clear() { }
    public void Release() { }
    public void RemoveAt(int index) { }
    public T this[int i] { get { return default; } set { } }
}

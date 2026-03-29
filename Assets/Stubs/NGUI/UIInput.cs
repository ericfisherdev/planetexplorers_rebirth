using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// NGUI stub: text input field with validation, selection, and keyboard type support.
/// Will be replaced with functional uGUI equivalent in Phase 8.
/// </summary>
public class UIInput : MonoBehaviour
{
    public enum InputType
    {
        Standard,
        AutoCorrect,
        Password,
    }

    public enum Validation
    {
        None,
        Integer,
        Float,
        Alphanumeric,
        Username,
        Name,
        Filename,
    }

    public enum KeyboardType
    {
        Default,
        ASCIICapable,
        NumbersAndPunctuation,
        URL,
        NumberPad,
        PhonePad,
        NamePhonePad,
        EmailAddress,
    }

    public enum OnReturnKey
    {
        Default,
        Submit,
        NewLine,
    }

    public static UIInput current;
    public static UIInput selection;

    public UILabel label;
    public UILabel activeTextColor;
    public Color caretColor = new Color(1f, 1f, 1f, 1f);
    public Color selectionColor = new Color(1f, 223f / 255f, 141f / 255f, 0.5f);
    public int maxChars;
    public InputType inputType = InputType.Standard;
    public Validation validation = Validation.None;
    public KeyboardType keyboardType = KeyboardType.Default;
    public OnReturnKey onReturnKey = OnReturnKey.Default;
    public bool hideInput;
    public bool selectAllTextOnFocus = true;
    public bool submitOnUnselect;
    public char savedAs;

    public List<EventDelegate> onSubmit = new List<EventDelegate>();
    public List<EventDelegate> onChange = new List<EventDelegate>();

    public string defaultText = "";

    public string value
    {
        get { return text; }
        set { text = value; }
    }

    public string text = "";

    public bool isSelected { get; set; }

    public int cursorPosition { get; set; }

    public int selectionStart { get; set; }

    public int selectionEnd { get; set; }

    public bool selected
    {
        get { return isSelected; }
        set { isSelected = value; }
    }

    public void Submit() { }

    public void UpdateLabel() { }

    public void RemoveFocus() { isSelected = false; }
}

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class IPAddressField : MonoBehaviour
{
    private TMP_InputField inputField;
    private ColorBlock inputFieldColorBlock;

    [SerializeField]
    private Color invalidIPColor;

    [SerializeField]
    private Color validIPColor;

    public bool Valid { get; private set; }
    public string IP_Address => inputField.text;

    void Awake()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.onEndEdit.AddListener(Validate);
        inputFieldColorBlock = inputField.colors;
    }

    private void Validate(string newValue)
    {
        if (Valid = IsIPAddress(newValue))
        {
            inputFieldColorBlock.normalColor = validIPColor;
            inputFieldColorBlock.selectedColor = validIPColor;
            inputFieldColorBlock.highlightedColor = validIPColor.SetAlpha(0.5f);
        }
        else
        {
            inputFieldColorBlock.normalColor = invalidIPColor;
            inputFieldColorBlock.selectedColor = invalidIPColor;
            inputFieldColorBlock.highlightedColor = invalidIPColor.SetAlpha(0.5f);
        }
            

        inputField.colors = inputFieldColorBlock;
    }

    private bool IsIPAddress(string value)
    {
        //Check ipv4
        return Regex.IsMatch(value, "^([0-9]{1,3}\\.){3}[0-9]{1,3}$")
        //Check ipv6
            || Regex.IsMatch(value, "^([0-9a-f]{0,4}\\:){7}[0-9a-f]{0,4}$");
    }

}

public static class ColorExtension
{
    public static Color SetAlpha(this Color color, float a) => new Color(color.r, color.g, color.b, a);
}

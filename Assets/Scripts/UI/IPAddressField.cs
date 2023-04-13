using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Net;

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

    [SerializeField]
    private bool allowIPv4;
    [SerializeField]
    private bool allowIPv6;

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
        if (IPAddress.TryParse(value, out IPAddress address))
        {
            return address.AddressFamily switch
            {
                System.Net.Sockets.AddressFamily.InterNetwork => allowIPv4,
                System.Net.Sockets.AddressFamily.InterNetworkV6 => allowIPv6,
                _ => false,
            };
        }
        return false;
    }

}

public static class ColorExtension
{
    public static Color SetAlpha(this Color color, float a) => new Color(color.r, color.g, color.b, a);
}

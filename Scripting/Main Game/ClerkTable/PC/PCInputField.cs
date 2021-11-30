using UnityEngine.UI;
using UnityEngine;
using System.Text.RegularExpressions;

public class PCInputField : MonoBehaviour
{
    private GameObject pcInputField;
    private Text textField;
    public string Text { get; private set; }
    public bool activeSelf { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        pcInputField = GameObject.Find("PC_INPUT_FIELD");
        textField = pcInputField.GetComponent<Text>();
    }

    public void SetActive(bool isActive)
    {
        pcInputField.SetActive(isActive);
        activeSelf = isActive;

        if (!activeSelf)
            Clear();
    }

    public void CaptureNumericInputFromField(string playerInput, int maxInputSize = 5)
    {
        if (Regex.IsMatch(playerInput, "[0-9]") && Text.Length <= maxInputSize)
        {
            Text = textField.text += playerInput;
        }
        else if(playerInput == "backspace")
        {
            OnBackspaceTryEraseLastInput();
        }
    }

     public bool OnBackspaceTryEraseLastInput()
    {
        if (pcInputField.activeSelf && Text.Length > 0)
        {
            Text = textField.text = textField.text.Remove(textField.text.Length - 1);
        }
        return false;
    }

    public void Clear()
    {
        Text = textField.text = "";
    }

}

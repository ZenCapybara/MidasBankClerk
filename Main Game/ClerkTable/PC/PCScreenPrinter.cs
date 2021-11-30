using UnityEngine.UI;
using UnityEngine;

public class PCScreenPrinter : MonoBehaviour
{
    private Text textoDaTelaDoPC;

    public void Write(string newInput)
    {
        textoDaTelaDoPC.text = newInput;
    }
    // Start is called before the first frame update
    void Awake()
    {
        textoDaTelaDoPC = 
            GameObject.Find("PC_INTERACTING_TXT")
            .GetComponent<Text>();
    }

}

using UnityEngine;

public class KeypadInputHandler : MonoBehaviour
{
    MidasOS pcScript;

    // Start is called before the first frame update
    void Start()
    {
        pcScript = ScriptFinder.Get<MidasOS>();
    }

    public void KeyHandler(string input)
    {
        pcScript.PCInputDistributor(input);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            KeyHandler("P");


        if (Input.GetKeyDown(KeyCode.X))
            KeyHandler("X");


        if (Input.GetKeyDown(KeyCode.Q))
            KeyHandler("Q");


        if (Input.GetKeyDown(KeyCode.Backspace))
            KeyHandler("backspace");


        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
            KeyHandler("enter");


        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyUp(KeyCode.Keypad1))
            KeyHandler("1");


        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyUp(KeyCode.Keypad2))
            KeyHandler("2");


        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyUp(KeyCode.Keypad3))
            KeyHandler("3");


        if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyUp(KeyCode.Keypad4))
            KeyHandler("4");


        if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyUp(KeyCode.Keypad5))
            KeyHandler("5");


        if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyUp(KeyCode.Keypad6))
            KeyHandler("6");


        if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyUp(KeyCode.Keypad7))
            KeyHandler("7");


        if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyUp(KeyCode.Keypad8))
            KeyHandler("8");


        if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyUp(KeyCode.Keypad9))
            KeyHandler("9");


        if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyUp(KeyCode.Keypad0))
            KeyHandler("0");

    }
}

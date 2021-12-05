using UnityEngine.UI;
using UnityEngine;

public class LoadingScreenMechanics : MonoBehaviour
{
    private GameObject loadingScreen;

    private void Start()
    {
        loadingScreen = GameObject.Find("LoadingScreen");
        if (loadingScreen == null)
            Debug.LogWarning("Check LoadingScreen object name");
        UnblockScreen();
    }

    public void BlockScreen(string standbyPopUpMessage)
    {
        SetPopUpMessage(standbyPopUpMessage);
        loadingScreen.SetActive(true);
    }

    public void UnblockScreen()
    {
        loadingScreen.SetActive(false);
    }

    public bool IsActive()
    {
        return loadingScreen.activeSelf;
    }

    private void SetPopUpMessage(string standbyPopUpMessage)
    {
        loadingScreen.GetComponentInChildren<Text>().text = standbyPopUpMessage;
    }

}

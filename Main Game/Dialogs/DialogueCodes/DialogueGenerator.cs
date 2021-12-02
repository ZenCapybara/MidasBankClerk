using UnityEngine.UI;
using UnityEngine;
using static Dialogue;

public class DialogueGenerator : MonoBehaviour
{
    private int numberOfDialogueEntries;
    private Color lightColor;
    private Color darkColor;
    private Dialogue[] publishedDialogue;
    private Dialogue newDialogueToPrint;

    private GameObject[] dialogueBoxes;

    void Awake()
    {
        lightColor =
            GameObject.Find("TextoDeDialogo1").
            GetComponent<Image>()
            .color;

        darkColor = 
            GameObject.Find("TextoDeDialogo2").
            GetComponent<Image>()
            .color;

        numberOfDialogueEntries = 0;
        publishedDialogue = new Dialogue[4];
        for (int i = 0; i < 4; i++)
        {
            publishedDialogue[i] = new Dialogue(Owner.Undefiened);
            publishedDialogue[i].Text = "";
        }
        dialogueBoxes = new GameObject[4];
        for (int i = 0; i < 4; i++)
        {
            dialogueBoxes[i] = GameObject.Find("TextoDeDialogo" + i);
            dialogueBoxes[i].SetActive(false);
        }
    }

    public void ReceiveDialogue(Dialogue inputDialogue)
    {
        newDialogueToPrint = inputDialogue;
        ChooseDialoguePosition();
    }

    private void ChooseDialoguePosition()
    {
        if (numberOfDialogueEntries < 4)
        {
            dialogueBoxes[numberOfDialogueEntries].SetActive(true);
            ChooseDialoguePrintMethodByDialogueOwner(numberOfDialogueEntries);
        }
        else
        {
            ScrollOverflowingDialogueBoxes();
            ChooseDialoguePrintMethodByDialogueOwner(3);
        }

    }

    private void ScrollOverflowingDialogueBoxes()
    {
        for (int i = 0; i < 3; i++)
        {
            publishedDialogue[i].Text = publishedDialogue[i + 1].Text;
            dialogueBoxes[i].GetComponentInChildren<Text>().text = publishedDialogue[i].Text;
            publishedDialogue[i].MyOwner = publishedDialogue[i + 1].MyOwner;


            if(publishedDialogue[i].MyOwner == Owner.Clerk)
            {
                ColorDialogueBox(darkColor, dialogueBoxes[i]);
            }
            else
            {
                ColorDialogueBox(lightColor, dialogueBoxes[i]);
            }
        }

        publishedDialogue[3].Text = "";
        dialogueBoxes[3].GetComponentInChildren<Text>().text = publishedDialogue[3].Text;
        publishedDialogue[3].MyOwner = Owner.Undefiened;

    }

    private void ChooseDialoguePrintMethodByDialogueOwner(int positionToPrintIn)
    {
        publishedDialogue[positionToPrintIn].MyOwner = newDialogueToPrint.MyOwner;
        publishedDialogue[positionToPrintIn].Text = newDialogueToPrint.Text;

        if (newDialogueToPrint.MyOwner == Owner.Clerk)
        {
            ColorDialogueBox(darkColor, dialogueBoxes[positionToPrintIn]);
            dialogueBoxes[positionToPrintIn].GetComponentInChildren<Text>().text = newDialogueToPrint.Text;
        }
        else if (newDialogueToPrint.MyOwner == Owner.Client)
        {
            ColorDialogueBox(lightColor, dialogueBoxes[positionToPrintIn]);
            PrintDialogueLetterByLetterWithDelay(positionToPrintIn);
        }
        numberOfDialogueEntries++;
    }

    private void ColorDialogueBox(Color color, GameObject dialogueBox)
    {
        if(color == darkColor)
        {
            dialogueBox.GetComponent<Image>().color = darkColor;
            dialogueBox.GetComponentInChildren<Text>().color = lightColor;
        }
        else
        {
            dialogueBox.GetComponent<Image>().color = lightColor;
            dialogueBox.GetComponentInChildren<Text>().color = darkColor;
        }

    }

    private void PrintDialogueLetterByLetterWithDelay(int positionToPrintIn)
    {
        foreach (char c in newDialogueToPrint.Text)
        {
            dialogueBoxes[positionToPrintIn].GetComponentInChildren<Text>().text += c;
        }
    }

    public void finishDialogue()
    {
        for (int i = 0; i < 4; i++)
        {
            publishedDialogue[i].Text = "";
            dialogueBoxes[i].GetComponentInChildren<Text>().text = "";
            dialogueBoxes[i].SetActive(false);
        }
        numberOfDialogueEntries = 0;
    }

}

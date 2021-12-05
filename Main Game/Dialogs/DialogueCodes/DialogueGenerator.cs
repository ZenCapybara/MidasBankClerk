using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using static Dialogue;

public class DialogueGenerator : MonoBehaviour
{
    private int dialogueBoxPosition;
    private Color clientBoxColor;
    private Color clerkBoxColor;
    private List<Dialogue> Dialogs;
    private int dialogPointer = 0;
    private DelayMechanic delayManager;
    private bool isPrinting = false;
    const float DELAY = 0.03f;
    private float printDelayCountdown = DELAY;
    private int currentLetterBeingPrinted;

    private GameObject[] dialogueBoxes;

    void Awake()
    {
        Dialogs = new List<Dialogue>();
        delayManager = ScriptFinder.Get<DelayMechanic>();
        clientBoxColor =
            GameObject.Find("TextoDeDialogo0").
            GetComponent<Image>()
            .color;

        clerkBoxColor =
            GameObject.Find("TextoDeDialogo1").
            GetComponent<Image>()
            .color;

        dialogueBoxes = new GameObject[4];
        for (int i = 0; i < 4; i++)
        {
            dialogueBoxes[i] = GameObject.Find("TextoDeDialogo" + i);
            dialogueBoxes[i].SetActive(false);
        }

    }

    public void ReceiveDialogue(Dialogue inputDialogue, string delayMessage)
    {
        delayManager.RequestDelay(delayMessage);
        Dialogs.Add(inputDialogue);

        if (Dialogs.Count - 1 == dialogPointer)
            StartPrintingDialogue();
    }

    public void FinishDialog()
    {
        for (int i = 0; i < 4; i++)
        {
            dialogueBoxes[i].GetComponentInChildren<Text>().text = "";
            dialogueBoxes[i].SetActive(false);
        }

        Dialogs.Clear();
        dialogPointer = 0;
    }

    private void StartPrintingDialogue()
    {
        isPrinting = true;
        currentLetterBeingPrinted = 0;

        if (dialogPointer < 4)
        {
            dialogueBoxes[dialogPointer].SetActive(true);
            dialogueBoxPosition = dialogPointer;
        }
        else
        {
            ScrollOverflowingDialogueBoxes();
            dialogueBoxes[3].GetComponentInChildren<Text>().text = "";
            dialogueBoxPosition = 3;
        }
        ConfigureDialogueToOwnerSpecs();
    }

    private void ScrollOverflowingDialogueBoxes()
    {
        for (int i = 0; i < 3; i++)
        {
            int previousDialog = dialogPointer - 3 + i;
            dialogueBoxes[i].GetComponentInChildren<Text>().text
                = Dialogs[previousDialog].Text;

            if (Dialogs[previousDialog].MyOwner == Owner.Clerk)
            {
                ColorDialogueBox(clerkBoxColor, dialogueBoxes[i]);
                //SetDialogueAlignment(Owner.Clerk, i);
            }
            else
            {
                ColorDialogueBox(clientBoxColor, dialogueBoxes[i]);
                //SetDialogueAlignment(Owner.Client, i);
            }
        }

    }

    private void ConfigureDialogueToOwnerSpecs()
    {
        if (Dialogs[dialogPointer].MyOwner == Owner.Clerk)
        {
            ColorDialogueBox(clerkBoxColor, dialogueBoxes[dialogueBoxPosition]);
            //SetDialogueAlignment(Owner.Clerk, dialogueBoxPosition);
        }
        else if (Dialogs[dialogPointer].MyOwner == Owner.Client)
        {
            ColorDialogueBox(clientBoxColor, dialogueBoxes[dialogueBoxPosition]);
            //SetDialogueAlignment(Owner.Client, dialogueBoxPosition);
        }
    }

    private void ColorDialogueBox(Color color, GameObject dialogueBox)
    {
        if (color == clerkBoxColor)
        {
            dialogueBox.GetComponent<Image>().color = clerkBoxColor;
        }
        else
        {
            dialogueBox.GetComponent<Image>().color = clientBoxColor;
        }
    }
    //!!! ALSO UNCOMMENT CONFIGUREDIALOGUETOOWNERSPECS AND SCROLL
    //private void SetDialogueAlignment(Owner owner, int dialogue)
    //{
    //    if (owner == Owner.Clerk)
    //    {
    //        dialogueBoxes[dialogue]
    //            .GetComponentInChildren<Text>()
    //            .alignment = TextAnchor.MiddleRight;
    //    }
    //    if (owner == Owner.Client)
    //    {
    //        dialogueBoxes[dialogue]
    //            .GetComponentInChildren<Text>()
    //            .alignment = TextAnchor.MiddleLeft;
    //    }
    //}

    private void PrintDialogueLetterByLetterWithDelay()
    {
        printDelayCountdown -= Time.deltaTime;
        if (printDelayCountdown > 0)
            return;
        if (currentLetterBeingPrinted < Dialogs[dialogPointer].Text.Length)
        {
            dialogueBoxes[dialogueBoxPosition].GetComponentInChildren<Text>().text
                += Dialogs[dialogPointer].Text[currentLetterBeingPrinted];

            printDelayCountdown = DELAY;
            currentLetterBeingPrinted++;
        }
        else if (printDelayCountdown < -1)
        {
            dialogPointer++;

            delayManager.YieldDelay();
            isPrinting = false;

            if (dialogPointer < Dialogs.Count)
                StartPrintingDialogue();
        }
    }

    private void Update()
    {
        if (isPrinting)
            PrintDialogueLetterByLetterWithDelay();
    }

}

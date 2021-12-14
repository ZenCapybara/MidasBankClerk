using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using static Dialogue;

public class DialogueGenerator : MonoBehaviour
{
    private int dialogueBoxPosition;
    private List<Dialogue> Dialogs;
    private Sprite clientDialogueBloonIMG;
    private Sprite clerkDialogueBloonIMG;
    private int dialogPointer = 0;
    private DelayMechanic delayManager;
    private bool isPrinting = false;
    const float DELAY = 0.03f;
    private float printDelayCountdown = DELAY;
    private int currentLetterBeingPrinted;
    private Button downScrollButton, upScrollButton;


    private GameObject[] dialogueBoxes;

    void Awake()
    {
        Dialogs = new List<Dialogue>();
        delayManager = ScriptFinder.Get<DelayMechanic>();
        clientDialogueBloonIMG =
            GameObject.Find("TextoDeDialogo0").
            GetComponent<Image>().sprite;

        clerkDialogueBloonIMG =
            GameObject.Find("TextoDeDialogo1").
            GetComponent<Image>().sprite;

        dialogueBoxes = new GameObject[4];
        for (int i = 0; i < 4; i++)
        {
            dialogueBoxes[i] = GameObject.Find("TextoDeDialogo" + i);
            dialogueBoxes[i].SetActive(false);
        }

        upScrollButton = GameObject.Find("ScrollUpButton").GetComponent<Button>();
        downScrollButton = GameObject.Find("ScrollDownButton").GetComponent<Button>();
        LockScrollButton();
    }

    public void ReceiveDialogue(Dialogue inputDialogue, string delayMessage)
    {
        delayManager.RequestDelay(delayMessage);
        Dialogs.Add(inputDialogue);

        if (!isPrinting)
        {
            dialogPointer = Dialogs.Count - 1;
            StartPrintingDialogue();
        }

    }

    public void ScrollDialogueUp()
    {
        dialogPointer--;
        ScrollDialogue();
        UnlockScrollButton();
    }

    public void ScrollDialogueDown()
    {
        dialogPointer++;
        ScrollDialogue();
        UnlockScrollButton();
    }

    private void LockScrollButton()
    {
        downScrollButton.interactable = false;
        upScrollButton.interactable = false;
    }

    private void UnlockScrollButton()
    {
        if (dialogPointer > 3)
            upScrollButton.interactable = true;
        else
            upScrollButton.interactable = false;

        if (dialogPointer < Dialogs.Count - 1)
            downScrollButton.interactable = true;
        else
            downScrollButton.interactable = false;
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
        LockScrollButton();
        currentLetterBeingPrinted = 0;

        if (dialogPointer < 4)
        {
            dialogueBoxes[dialogPointer].SetActive(true);
            dialogueBoxPosition = dialogPointer;
        }
        else
        {
            ScrollDialogue();
            dialogueBoxes[3].GetComponentInChildren<Text>().text = "";
            dialogueBoxPosition = 3;
        }
        ConfigureDialogueToOwnerSpecs();
    }

    private void ScrollDialogue()
    {
        for (int i = 0; i < 4; i++)
        {
            int previousDialog = dialogPointer - 3 + i;
            dialogueBoxes[i].GetComponentInChildren<Text>().text
                = Dialogs[previousDialog].Text;

            if (Dialogs[previousDialog].MyOwner == Owner.Clerk)
            {
                SetDialogueBloonImage(clerkDialogueBloonIMG, dialogueBoxes[i]);
                //SetDialogueAlignment(Owner.Clerk, i);
            }
            else
            {
                SetDialogueBloonImage(clientDialogueBloonIMG, dialogueBoxes[i]);
                //SetDialogueAlignment(Owner.Client, i);
            }
        }

    }

    private void ConfigureDialogueToOwnerSpecs()
    {
        if (Dialogs[dialogPointer].MyOwner == Owner.Clerk)
        {
            SetDialogueBloonImage(clerkDialogueBloonIMG, dialogueBoxes[dialogueBoxPosition]);
            //SetDialogueAlignment(Owner.Clerk, dialogueBoxPosition);
        }
        else if (Dialogs[dialogPointer].MyOwner == Owner.Client)
        {
            SetDialogueBloonImage(clientDialogueBloonIMG, dialogueBoxes[dialogueBoxPosition]);
            //SetDialogueAlignment(Owner.Client, dialogueBoxPosition);
        }
    }

    private void SetDialogueBloonImage(Sprite sprite, GameObject dialogueBox)
    {
        if (sprite == clerkDialogueBloonIMG)
        {
            dialogueBox.GetComponent<Image>().sprite = clerkDialogueBloonIMG;
        }
        else
        {
            dialogueBox.GetComponent<Image>().sprite = clientDialogueBloonIMG;
        }
    }

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
            else
            {
                dialogPointer = Dialogs.Count - 1;
                UnlockScrollButton();
            }
        }
    }

    private void Update()
    {
        if (isPrinting)
            PrintDialogueLetterByLetterWithDelay();
    }

}

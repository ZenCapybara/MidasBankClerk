public class Dialogue
{
    public enum Owner
    {
        Client,
        Clerk,
        Undefiened // should be used only on unprinted dialogues.
    }

    public Owner MyOwner { get; set; }
    public string Text { get; set; }

    public Dialogue(Owner dialogueOwner)
    {
        MyOwner = dialogueOwner;
    }


}

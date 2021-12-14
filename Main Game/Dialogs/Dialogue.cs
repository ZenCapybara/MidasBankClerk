public class Dialogue
{
    public enum Owner
    {
        Client,
        Clerk,
        Undefiened // should be used only on unprinted dialogues.
    }

    public Owner MyOwner { get; }
    public string Text { get; }
    public string pcStandbyText { get; }


    public Dialogue(Owner dialogueOwner, string Text = "", string pcStandbyText = "")
    {
        MyOwner = dialogueOwner;
        this.Text = Text;
        this.pcStandbyText = pcStandbyText;

    }


}

using UnityEngine.UI;

public class IdleState : PCState
{
    public override string Evoke()
    {
        indicadorGuicheEmAtendimento.SetActive(false);
        indicadorClienteIdentificado.SetActive(false);
        indicadorClienteIdentificado.GetComponent<Text>().text = "---";

        pcInputField.SetActive(false);

        return "\n\n\"Nosso toque, seu dinheiro\"\n\n\nCAIXA LIVRE";
    }
}

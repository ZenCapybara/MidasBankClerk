public class WaintingForClientBankCardState : PCState
{
    public override string Evoke()
    {
        indicadorGuicheEmAtendimento.SetActive(true);
        indicadorClienteIdentificado.SetActive(true);

        pcInputField.SetActive(false);

        return $"AGUARDANDO CARTÃO. . .";
    }

}

public class ClientAuthenticatedState : PCState
{
    public override string Evoke()
    {
        indicadorGuicheEmAtendimento.SetActive(true);
        indicadorClienteIdentificado.SetActive(true);

        pcInputField.SetActive(false);

        return "\nCLIENTE AUTENTICADO COM SUCESSO!\n ---------------------- " +
            "\n" +
            "\n" +
            "\n" +
            "\n" +
            "\n" +
            "\n ← RETORNAR.";
    }
}

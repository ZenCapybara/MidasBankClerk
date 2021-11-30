public class AwaitingForClientPasswordState : PCState
{
    public override string Evoke()
    {
        indicadorGuicheEmAtendimento.SetActive(true);
        indicadorClienteIdentificado.SetActive(true);

        pcInputField.SetActive(true);

        return "\nAGUARDANDO SENHA DO CLIENTE\n ---------------------- " +
             "\n1- INICIAR AUTENTICAÇÃO MECÂNICA" +
             "\n" +
             "\n" +
             "\n" +
             "\n" +
             "\n ← RETORNAR.";
    }

}

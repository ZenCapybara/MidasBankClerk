
public class AwaitingWithdrawAmountInputState : PCState
{
    public override string Evoke()
    {
        indicadorGuicheEmAtendimento.SetActive(true);
        indicadorClienteIdentificado.SetActive(true);

        pcInputField.SetActive(true);

        return "\nINSIRA O VALOR DO SAQUE\n ---------------------- " +
             "\n" +
             "\n" +
             "\n" +
             "\n" +
             "\n" +
             "\n ← RETORNAR.";
    }
}

public class ErrorState : PCState
{
    string errorMessage;

    public ErrorState(string errorMessage)
    {
        this.errorMessage = $"\n{errorMessage}";
    }

    public override string Evoke()
    {
        indicadorGuicheEmAtendimento.SetActive(true);
        indicadorClienteIdentificado.SetActive(true);

        pcInputField.SetActive(false);

        return $"ATENÇÃO! ERRO NA OPERAÇÃO!\n ---------------------- " +
               $"{errorMessage}" +
               $"\n\n ← RETORNAR.";
    }

}

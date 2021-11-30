public class OperationCompleteState : PCState
{
    public override string Evoke()
    {
        indicadorGuicheEmAtendimento.SetActive(true);
        indicadorClienteIdentificado.SetActive(true);

        pcInputField.SetActive(true);

        return "OPERAÇÃO BEM SUCEDIDA!!!\n ---------------------- " +
         "\nCONSIDERE A OPORTUNIDADE" +
         "\nPARA REALIZAR UMA VENDA" +
         "\nOU APERTE \"X\" PARA ENCERRAR." +
         "\n" +
         "\n ← RETORNAR.";
    }
}

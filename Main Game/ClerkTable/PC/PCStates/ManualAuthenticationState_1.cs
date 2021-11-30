public class ManualAuthenticationState : PCState
{    public override string Evoke()
    {
        indicadorGuicheEmAtendimento.SetActive(true);
        indicadorClienteIdentificado.SetActive(true);

        pcInputField.SetActive(true);

        Client accountOwnerInfo =
                ScriptFinder.Get<ClientCueMechanics>()
                .ClientAccessedOnPc;

        return $"CONFIRA OS DADOS DO CLIENTE." +
         $"\n ---------------------- " +
         $"\n1- DADOS CONFEREM." +
         $"\n2- DADOS INCONSISTENTES" +
         $"\n ---------------------- " +
         $"\nNome: {accountOwnerInfo.trueName} {accountOwnerInfo.trueSurname}" +
         $"\nNº da Conta: {accountOwnerInfo.trueAccountNumber}" +
         $"\nRG: {accountOwnerInfo.trueIdentityNumber}" +
         $"\nIdade: {accountOwnerInfo.trueAge}" +
         $"\n" +
         "\n ← RETORNAR.";
    }
}

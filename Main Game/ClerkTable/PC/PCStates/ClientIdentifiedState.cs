using UnityEngine.UI;

public class ClientIdentifiedState : PCState
{
    public override string Evoke()
    {
        indicadorGuicheEmAtendimento.SetActive(true);
        indicadorClienteIdentificado.SetActive(true);

        pcInputField.SetActive(false);

        Client accountOwnerInfo = ScriptFinder.Get<ClientCueMechanics>().ClientAccessedOnPc;

        if(accountOwnerInfo != null)
        {
            indicadorClienteIdentificado.GetComponentInChildren<Text>().text = $"Cliente: {accountOwnerInfo.trueName.ToUpper()} {accountOwnerInfo.trueSurname.ToUpper()}";

            return $"CLIENTE: " +
                   $"\nNome: {accountOwnerInfo.trueName} {accountOwnerInfo.trueSurname}" +
                   $"\nNº da Conta: {accountOwnerInfo.trueAccountNumber}" +
                   $"\nRG: {accountOwnerInfo.trueIdentityNumber}" +
                   $"\nIdade: {accountOwnerInfo.trueBirthday}" +
                   $"\nSaldo: {accountOwnerInfo.saldo},00" +
                   $"\n\nAPERTE 'ENTER' PARA CONTINUAR.";
        }
        else
        {
            return $"CLIENTE: " +
                   $"\nCLIENTE NÃO ENCONTRADO!" +
                   $"\nCONFIRA OS DADOS DO CLIENTE." +
                   $"\n" +
                   $"\n" +
                   $"\n" +
                   $"\n\nAPERTE 'ENTER' PARA CONTINUAR.";
        }

    }

}

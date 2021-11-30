using UnityEngine;
using UnityEngine.UI;

public class ClientIdentifiedState : PCState
{
    public override string Evoke()
    {
        indicadorGuicheEmAtendimento.SetActive(true);
        indicadorClienteIdentificado.SetActive(true);

        pcInputField.SetActive(false);

        Client accountOwnerInfo =
                GameObject.Find("GameMechanicScripts")
                .GetComponent<ClientCueMechanics>()
                .ClientAccessedOnPc;

        if(accountOwnerInfo != null)
        {
            indicadorClienteIdentificado.GetComponentInChildren<Text>().text = $"Cliente: {accountOwnerInfo.trueName + accountOwnerInfo.trueSurname}";

            return $"CLIENTE: " +
                   $"\nNome: {accountOwnerInfo.trueName} {accountOwnerInfo.trueSurname}" +
                   $"\nNº da Conta: {accountOwnerInfo.trueAccountNumber}" +
                   $"\nRG: {accountOwnerInfo.trueIdentityNumber}" +
                   $"\nIdade: {accountOwnerInfo.trueAge}" +
                   $"\nSaldo: {accountOwnerInfo.saldo},00" +
                   $"\n\n<- Retornar";
        }
        else
        {
            return $"CLIENTE: " +
                   $"\nCLIENTE NÃO ENCONTRADO!" +
                   $"\nCONFIRA OS DADOS DO CLIENTE." +
                   $"\n" +
                   $"\n" +
                   $"\n" +
                   $"\n\n<- Retornar";
        }

    }

}

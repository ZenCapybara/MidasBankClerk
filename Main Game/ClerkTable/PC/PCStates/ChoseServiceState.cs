using UnityEngine;

public class ChoseServiceState : PCState
{
    public override string Evoke()
    {
        indicadorGuicheEmAtendimento.SetActive(true);
        indicadorClienteIdentificado.SetActive(true);

        pcInputField.SetActive(false);

        return "\nSELECIONE O SERVIÇO\n ---------------------- " +
            "\n1- SAQUE EM DINHEIRO" +
            "\n2- PAGAR CHEQUE" +
            "\n3- RETIRAR CHEQUE" +
            "\n4- PAGAR BOLETO" +
            "\n5- OFERECER OUTROS SERVIÇOS" +
            "\n ← RETORNAR.";
    }
}


using UnityEngine;

public class ChoseClientIDMethodState : PCState
{    public override string Evoke()
    {
        indicadorGuicheEmAtendimento.SetActive(true);
        indicadorClienteIdentificado.SetActive(true);

        pcInputField.SetActive(false);

        return "\nSELECIONE O MODO DE IDENTIFICAÇÃO\n ---------------------- " +
            "\n1- CARTÃO" +
            "\n2- IDENTIDADE" +
            "\n" +
            "\n" +
            "\n" +
            "\n ← RETORNAR.";
    }
}

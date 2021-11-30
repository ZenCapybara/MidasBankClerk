using UnityEngine;

public class ClientIDCardRequestState : PCState
{    public override string Evoke()
    {
        indicadorGuicheEmAtendimento.SetActive(true);
        indicadorClienteIdentificado.SetActive(true);

        pcInputField.SetActive(true);

        return "\nINSIRA O RG DO CLIENTE\n ---------------------- " +
             "\n" +
             "\n" +
             "\n" +
             "\n" +
             "\n" +
             "\n ← RETORNAR.";
    }
}

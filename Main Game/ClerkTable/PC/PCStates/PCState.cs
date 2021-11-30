using UnityEngine;

public abstract class PCState
{
    protected string screenOutputText;
    protected PCInputField pcInputField;
    protected GameObject
        indicadorGuicheEmAtendimento,
        indicadorClienteIdentificado;

    public PCState()
    {
        FetchScreenElementsForActivationAndDeactivation();
    }

    protected void FetchScreenElementsForActivationAndDeactivation()
    {
        indicadorGuicheEmAtendimento = GameObject.Find("PC_GUICHE_EM_ATENDIMENTO");
        indicadorClienteIdentificado = GameObject.Find("PC_CLIENTE_IDENTIFICADO");
        pcInputField = ScriptFinder.Get<PCInputField>();
    }

    public abstract string Evoke();
}

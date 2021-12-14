using UnityEngine;
using UnityEngine.UI;

public class DemandMechanics : MonoBehaviour
{
    //Variáveis de Interação com Elementos do Unity
    private DialogueGenerator outputDialogue;
    private Client activeClient;
    private Dialogue clientDialogue;
    private DeliverObjectsMechanics clientObjectHandler;
    //private GameObject clientHandoverObjectPanel;

    void Start()
    {
        outputDialogue = ScriptFinder.Get<DialogueGenerator>();
        clientObjectHandler = ScriptFinder.Get<DeliverObjectsMechanics>();

        //Capture Client Object Description Box (placeholder for actual object art)
        //clientHandoverObjectPanel = GameObject.Find("ClientIDCard");
        //clientHandoverObjectPanel.SetActive(false);

    }

    //**************** Operacional *****************
 
    public void Say(string inputText, string pcDelayMessage)
    {
        clientDialogue = new Dialogue(Dialogue.Owner.Client, inputText, pcDelayMessage);
        outputDialogue.ReceiveDialogue(clientDialogue, pcDelayMessage);
    }

    public void TerminarAtendimento()
    {
        if (!activeClient.demandaAtendida)
        {
            activeClient.ReduceMoodDueToBadService(10);
        }
        Say(DialogoDemandaConcluida.GetDialogo(activeClient.humor), "Concluíndo Atendimento...");
        clientObjectHandler.ClearTable();
    }

    public void ClientPresentation(Client nextClient)
    {
        activeClient = nextClient;
        Say(CumprimentosTXT.getCumprimentos(), "Aguardando Cliente...");
    }

    public bool RequestClientCard()
    {
        if (activeClient.portaCartao)
        {
            Say(DialogoCartaoSolicitado.GetDialogue(1), "Aguardando Cartão do Cliente...");
            return true;
        }
        else
        {
            Say(DialogoCartaoSolicitado.GetDialogue(3), "Aguardando Cartão do Cliente...");
            return false;
        }
    }

    public void InformCardIsInvalid()
    {
        Say(DialogoCartaoSolicitado.GetDialogue(2), "Cartão Inválido!");
    }

    public bool RequestClientPassword()
    {
        //não colocará senha incorreta
        if (Random.Range(0, 100) < 80)
        {
            if (activeClient.remembersPassword)
            {
                Say(DialogoSenhaSolicitada.GetDialogue(1), "Aguardando senha...");
                return true;
            }
            Say(DialogoSenhaSolicitada.GetDialogue(3), "Aguardando senha...");
            return false;
        }
        //senha incorreta
        Say(DialogoSenhaSolicitada.GetDialogue(2), "Aguardando senha...");
        return false;

    }

    private void PresentID()
    {
        clientObjectHandler.PresentID(activeClient);
    }

    public bool RequestClientIDCard()
    {
        if (activeClient.portaIdentidade)
        {
            Say(DialogoRGSolicitado.GetDialogue(DialogoRGSolicitado.estadoDoRg.tenhoRG), "Carregando...");
            PresentID();
            return true;
        }
        else
        {
            Say(DialogoRGSolicitado.GetDialogue(DialogoRGSolicitado.estadoDoRg.naoTenhoRG), "Carregando...");
            return false;
        }
    }

    public void RequestClientDesiredService()
    {
        Say(DialogosDoSaque.GetDialogue(1), "Carregando...");
    }

    public void RequestWithDrawValue()
    {

        if (activeClient.demanda == 0)
        {
            string output =
                DialogosDoSaque.GetDialogue(2).
                Replace("$", "$" + activeClient.valorDesejadoParaSaqueOuDeposito.ToString() + ".");

            Say(output, "Carregando...");
        }
        else
        {
            activeClient.ReduceMoodDueToBadService();
            Say(ErroNoAtendimento.GetDialogue(), "Carregando...");
            RequestClientDesiredService();
        }

    }

    public void InsuficientFundForWithdraw()
    {
        activeClient.ReduceMoodDueToBadService();

        if (activeClient.saldo > activeClient.valorDesejadoParaSaqueOuDeposito)
        {
            activeClient.ReduceMoodDueToBadService();
            Say(ErroNoAtendimento.GetDialogue($"Eu pedi ${activeClient.valorDesejadoParaSaqueOuDeposito}!"), "Carregando...");
            return;
        }

        Say(DialogosDoSaque.GetDialogue(3), "Carregando...");

        if (Random.Range(0, 50) < 50 && activeClient.saldo > 0)
        {
            activeClient.SetValorDesejadoParaSaqueOuDeposito();
        }
        else
        {
            activeClient.EndDemand();
            TerminarAtendimento();
        }
    }

    public void FecharDemanda()
    {
        //Colocar o Script para Verificação Aqui.
        activeClient.demandaAtendida = true;
        Say("Hmmm... era isso! Gratidão!", "Concluíndo Serviço...");

    }

}

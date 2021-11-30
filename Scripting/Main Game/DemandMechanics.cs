using UnityEngine;
using UnityEngine.UI;

public class DemandMechanics : MonoBehaviour
{
    //Variáveis de Interação com Elementos do Unity
    private DialogueGenerator outputDialogue;
    private Client activeClient;
    private Dialogue clientDialogue;
    private GameObject clientHandoverObjectPanel;
    private Text clientDeliveredObjectDescription;

    void Start()
    {
        clientDialogue = new Dialogue(Dialogue.Owner.Client);
        outputDialogue = ScriptFinder.Get<DialogueGenerator>();

        //Capture Client Object Description Box (placeholder for actual object art)
        clientHandoverObjectPanel = GameObject.Find("ClientIDCard");
        clientDeliveredObjectDescription = clientHandoverObjectPanel.GetComponentInChildren<Text>();
        clientHandoverObjectPanel.SetActive(false);

    }

    /*********************************
    *         Operacional
    ********************************/
    public void Say(string inputText)
    {
        clientDialogue.Text = inputText;
        outputDialogue.ReceiveDialogue(clientDialogue);
    }

    public void TerminarAtendimento()
    {
        if (!activeClient.demandaAtendida)
        {
            Debug.Log("Cliente dispensado com demanda não-concluída");
            activeClient.ReduceMoodDueToBadService(10);
        }
        clientHandoverObjectPanel.SetActive(false);
        Say(DialogoDemandaConcluida.GetDialogo(activeClient.humor));
    }

    public void ClientPresentation(Client nextClient)
    {
        activeClient = nextClient;
        ScriptFinder.Get<DialogueGenerator>().finishDialogue();

        //Roll for 70% chance of greeting
        if (Random.Range(0, 10) < 7)
        {
            Say(CumprimentosTXT.getCumprimentos());
        }
    }

    public bool RequestClientCard()
    {
        if (activeClient.portaCartao)
        {
            Say(DialogoCartaoSolicitado.GetDialogue(true));
            return true;
        }
        else
        {
            Say(DialogoCartaoSolicitado.GetDialogue(false));
            return false;
        }
    }

    public bool RequestClientPassword()
    {
        //não colocará senha incorreta
        if (Random.Range(0, 100) < 80)
        {
            if (activeClient.remembersPassword)
            {
                Say(DialogoSenhaSolicitada.GetDialogue(1));
                return true;
            }
            Say(DialogoSenhaSolicitada.GetDialogue(3));
            return false;
        }
        //senha incorreta
        Say(DialogoSenhaSolicitada.GetDialogue(2));
        return false;

    }

    private void PresentID()
    {
        clientHandoverObjectPanel.SetActive(true);
        clientHandoverObjectPanel.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 25));
        clientDeliveredObjectDescription.text =
            $"CARTEIRA DE IDENTIDADE\n\n" +
            $"NOME: {activeClient.GetName()}\n" +
            $"RG: {activeClient.trueIdentityNumber}";
    }

    public bool RequestClientIDCard()
    {
        if (activeClient.portaIdentidade)
        {
            Say(DialogoRGSolicitado.GetDialogue(DialogoRGSolicitado.estadoDoRg.tenhoRG));
            PresentID();
            return true;
        }
        else
        {
            Say(DialogoRGSolicitado.GetDialogue(DialogoRGSolicitado.estadoDoRg.naoTenhoRG));
            return false;
        }
    }

    public void RequestClientDesiredService()
    {
        Say(DialogosDoSaque.GetDialogue(1));
    }

    public void RequestWithDrawValue()
    {
        if (activeClient.demanda == 0)
        {
            Say(DialogosDoSaque.GetDialogue(2)
                + activeClient.valorDesejadoParaSaqueOuDeposito
                + ".");
        }
        else
        {
            activeClient.ReduceMoodDueToBadService();
            Say(ErroNoAtendimento.GetDialogue());
            RequestClientDesiredService();
        }

    }

    public void InsuficientFundForWithdraw()
    {
        activeClient.ReduceMoodDueToBadService();

        if (activeClient.saldo > activeClient.valorDesejadoParaSaqueOuDeposito)
        {
            activeClient.ReduceMoodDueToBadService();
            Say(ErroNoAtendimento.GetDialogue($"Eu pedi ${activeClient.valorDesejadoParaSaqueOuDeposito}!"));
            return;
        }

        Say(DialogosDoSaque.GetDialogue(3));

        if (Random.Range(0, 50) < 50 && activeClient.saldo > 0)
        {
            activeClient.SetValorDesejadoParaSaqueOuDeposito();
            RequestWithDrawValue();
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
        Say("Hmmm... era isso! Gratidão!");

    }

}

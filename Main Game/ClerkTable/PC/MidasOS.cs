using UnityEngine;
using static PCNavigationMechanics;
public class MidasOS : MonoBehaviour
{
    private Dialogue clerkFeedback;
    private DialogueGenerator dialogueGenerator;
    private DemandMechanics clientDemandScript;
    private PCInputField pcInputField;
    private ClientCueMechanics clientCue;
    private LoadingScreenMechanics loadingScreen;
    private PCNavigationMechanics stateNavigator;
    private Client activeClient = null;

    private bool clientIsIdentified = false;
    private bool clientIsAuthenticated = false;

    enum SelectedService
    {
        Unselected,
        Withdraw,
        Deposit,
        CashCheck,
        PayBill,
        BuyNewService,
    }

    private SelectedService currentService;

    private void Start()
    {
        //Captures PC for interaction
        loadingScreen = ScriptFinder.Get<LoadingScreenMechanics>();
        stateNavigator = GetComponent<PCNavigationMechanics>();
        pcInputField = GetComponent<PCInputField>();
        clientDemandScript = ScriptFinder.Get<DemandMechanics>();
        clientCue = ScriptFinder.Get<ClientCueMechanics>();
        dialogueGenerator = ScriptFinder.Get<DialogueGenerator>();

        //Startup state for game startup
        stateNavigator.PCStateChangeAndAdvanceToStateOnTaskReturn
                (
                PCStateCode.Idle,
                PCStateCode.Idle
                );
    }

    //**************** Public Methods **********************

    public void InputHandler(string playerInput)
    {
        if (loadingScreen.IsActive())
            return;

        switch (playerInput)
        {
            case "X":
                if (activeClient != null)
                {
                    EndSession();
                }

                return;

            case "P":

                if (stateNavigator.pcCurrentState == PCStateCode.Idle)
                {
                    StartSession();
                }

                return;

            case "backspace":
                if (!pcInputField.activeSelf || pcInputField.Text.Length == 0)
                {
                    stateNavigator.ReturnToPreviousState();
                    playerInput = "FirstCall";
                }
                break;
        }

        if (activeClient == null)
            return;

        switch (stateNavigator.pcCurrentState)
        {
            case PCStateCode.Idle:
                ChooseService(playerInput);
                break;

            case PCStateCode.ChooseService:
                ChooseService(playerInput);
                break;

            case PCStateCode.ClientIDCardRequest:
                IdentifyByIdCard(playerInput);
                break;

            case PCStateCode.AwaitingWithdrawAmountInput:
                WithdrawMoney(playerInput);
                break;

            case PCStateCode.AwaitingForClientPassword:
                AuthenticateClient(playerInput);
                break;

            case PCStateCode.AwaitingMechanicalAuthenticationConfirmation:
                ManualAuthentication(playerInput);
                break;

            case PCStateCode.ClientIdentified:
                ReturnIdentifiedClient(playerInput);
                break;

            case PCStateCode.OperationComplete:
                break;

            case PCStateCode.ClientAutenticated:
                ReturnAuthenticatedClient(playerInput);
                break;

            default:
                Debug.Log($"State {stateNavigator.pcCurrentState} not linked yet");
                break;

        }

    }

    public void LockPC(string standbyPopUpMessage)
    {
        loadingScreen.BlockScreen(standbyPopUpMessage);
    }

    public void UnlockPC()
    {
        loadingScreen.UnblockScreen();
    }

    //**************** Private Methods **********************

    private PCStateCode ConvertServiceIntoPCStateCode(SelectedService targetSelectedService)
    {
        switch (targetSelectedService)
        {
            case SelectedService.Withdraw:
                return PCStateCode.AwaitingWithdrawAmountInput;

            default:
                return PCStateCode.ChooseService;
        }
    }

    private void StartSession()
    {
        dialogueGenerator.FinishDialog();
        activeClient = clientCue.CallNextClient();
        clientDemandScript.ClientPresentation(activeClient);
        ChooseService();
    }

    private void EndSession()
    {
        if (activeClient.demandaAtendida)
            ClerkFeedback("Obrigado por escolher Midas! Volte Sempre!\nCAIXA LIVRE!!!", "Aguardando Cliente.");
        else
            ClerkFeedback("Não tenho como te ajudar. Preciso atender o próximo.\nCAIXA LIVRE!!!", "Aguardando Cliente.");
        clientDemandScript.TerminarAtendimento();
        stateNavigator.PCStateChangeAndAdvanceToStateOnTaskReturn(PCStateCode.Idle, PCStateCode.Idle);
        clientIsIdentified = false;
        clientIsAuthenticated = false;
        activeClient = null;
        stateNavigator.Clear();
    }

    private void ClerkFeedback(string clerkFeedbackText, string pcStandbyMessage)
    {
        this.clerkFeedback = new Dialogue(Dialogue.Owner.Clerk, clerkFeedbackText, pcStandbyMessage);
        dialogueGenerator.ReceiveDialogue(clerkFeedback, pcStandbyMessage);
    }

    private void ChooseService(string playerInput = "FirstCall")
    {
        switch (playerInput)
        {
            case "FirstCall":
                if (!activeClient.demandaAtendida)
                {
                    ClerkFeedback("De qual Midas-Serviço você precisa?", "Carregando...");
                    clientDemandScript.RequestClientDesiredService();
                }
                stateNavigator.Clear();
                stateNavigator.PCStateChangeAndAdvanceToStateOnTaskReturn(PCStateCode.ChooseService, PCStateCode.ChooseService);
                return;

            case "1":
                WithdrawMoney();
                return;

            default:
                return;
        }

    }

    private void IdentifyByBankCard()
    {
        ClerkFeedback("Insira seu cartão por favor.", "Aguardando Cartão do Cliente.");
        stateNavigator.PCStateChange(PCStateCode.WaitingForClientBankCard);

        if (clientDemandScript.RequestClientCard())
        {
            if (clientCue.GetClientByAccount(activeClient.GetAccount()) != null)
            {
                ReturnIdentifiedClient();
            }
            else
            {
                ClerkFeedback("Seu cartão está inválido!", "Carregando...");
                clientDemandScript.InformCardIsInvalid();
                stateNavigator.ForgetPreviousState();
                stateNavigator.LaunchErrorMessage(PCStateCode.ClientIDCardRequest, $"Cartão Inválido!!!\n\n\n");
            }
        }
        else
        {
            stateNavigator.PCStateChange(PCStateCode.ClientIDCardRequest);
            stateNavigator.ForgetPreviousState();
            IdentifyByIdCard();
        }
    }

    private void IdentifyByIdCard(string playerInput = "FirstCall")
    {
        if (playerInput == "FirstCall")
        {
            stateNavigator.PCStateChange(PCStateCode.ClientIDCardRequest);

            if (!activeClient.AlreadyHandedID)
            {
                ClerkFeedback("Preciso de sua Carteira de Identidade por favor.", "Carregando...");
                clientDemandScript.RequestClientIDCard();
            }

            return;
        }

        pcInputField.CaptureNumericInputFromField(playerInput, 8);

        if (playerInput == "enter" && pcInputField.Text.Length > 0)
        {
            int clientIDNumber = int.Parse(pcInputField.Text);
            pcInputField.Clear();
            if (clientCue.GetClientByID(clientIDNumber) != null)
            {
                ReturnIdentifiedClient();
            }
            else
            {
                stateNavigator.LaunchErrorMessage
                    (
                    "Cliente Não Encontrado!\n" +
                    "Verifique os Dados do Cliente."
                    );
            }
        }
    }

    private void ReturnIdentifiedClient(string playerInput = "FirstCall")
    {
        if(playerInput == "FirstCall")
        {
            clientIsIdentified = true;
            stateNavigator.ForgetPreviousState();
            stateNavigator.PCStateChangeAndAdvanceToStateOnTaskReturn
                (
                PCStateCode.ClientIdentified,
                ConvertServiceIntoPCStateCode(currentService)
                );
        }
        else if (playerInput == "enter")
        {
            InputHandler("backspace");
            return;
        }

    }

    private void AuthenticateClient(string playerInput = "FirstCall")
    {
        if (playerInput == "FirstCall")
        {
            ClerkFeedback("Insira sua senha por favor!", "Aguardando Senha...");
            stateNavigator.PCStateChange(PCStateCode.AwaitingForClientPassword);

            if (clientDemandScript.RequestClientPassword())
            {
                ReturnAuthenticatedClient();
            }
            else
            {
                if (activeClient.remembersPassword)
                {
                    stateNavigator.LaunchErrorMessage(
                        PCStateCode.AwaitingForClientPassword,
                        "SENHA INCORRETA!");

                }
            }
        }

        if (playerInput == "1")
            ManualAuthentication();
    }

    private void ManualAuthentication(string playerInput = "FirstCall")
    {
        if (playerInput == "FirstCall")
        {
            if (!activeClient.AlreadyHandedID)
            {
                ClerkFeedback("Preciso de sua Carteira de Identidade por favor.", "Carregando...");
                clientDemandScript.RequestClientIDCard();
            }
            stateNavigator.PCStateChange(PCStateCode.AwaitingMechanicalAuthenticationConfirmation);
            return;
        }

        if (playerInput == "1")
        {
            ReturnAuthenticatedClient();
        }
        else if (playerInput == "2")
        {
            stateNavigator.Clear();
            stateNavigator.LaunchErrorMessage
                 (
                 "Cliente não Autenticado!\n" +
                 "X- Terminar atendimento."
                 );
        }

    }

    private void ReturnAuthenticatedClient(string playerInput = "FirstCall")
    {

        if (playerInput == "FirstCall")
        {
            clientIsAuthenticated = true;
            stateNavigator.ForgetPreviousState();
            stateNavigator.PCStateChangeAndAdvanceToStateOnTaskReturn
                (
                PCStateCode.ClientAutenticated,
                ConvertServiceIntoPCStateCode(currentService)
                );
            return;
        }
        else if (playerInput == "enter")
        {
            InputHandler("backspace");
            return;
        }
    }

    private void WithdrawMoney(string playerInput = "FirstCall")
    {
        currentService = SelectedService.Withdraw;
        if (!clientIsIdentified)
        {
            IdentifyByBankCard();
            return;
        }

        if (clientIsAuthenticated)
        {
            OperationCompleted();
            return;
        }

        stateNavigator.PCStateChange(PCStateCode.AwaitingWithdrawAmountInput);

        if (playerInput == "FirstCall")
        {
            ClerkFeedback("Quanto dinheiro você quer sacar?", "Carregando...");
            clientDemandScript.RequestWithDrawValue();
        }

        pcInputField.CaptureNumericInputFromField(playerInput, 6);

        if (playerInput == "enter" && pcInputField.Text.Length > 0)
        {
            if (activeClient.saldo >= int.Parse(pcInputField.Text))
            {
                AuthenticateClient();
                pcInputField.Clear();
                return;
            }
            else
            {
                stateNavigator.LaunchErrorMessage(PCStateCode.AwaitingWithdrawAmountInput, "Saldo Insuficiente!");
                ClerkFeedback($"Saldo insuficiente! Você tem {activeClient.saldo}.", "Carregando...");
                clientDemandScript.InsuficientFundForWithdraw();
                pcInputField.Clear();
                return;
            }
        }
    }

    private void DepositMoney(string playerInput = "FirstCall")
    {

    }

    private void CashCheck(string playerInput = "FirstCall")
    {

    }

    private void PayBill(string playerInput = "FirstCall")
    {

    }

    private void OperationCompleted()
    {
        pcInputField.Clear();
        stateNavigator.Clear();
        stateNavigator.PCStateChangeAndAdvanceToStateOnTaskReturn
            (
            PCStateCode.OperationComplete,
            PCStateCode.ChooseService
            );
        clientDemandScript.FecharDemanda();
        clientIsAuthenticated = false;
    }

}

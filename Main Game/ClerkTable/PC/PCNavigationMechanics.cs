using UnityEngine;
using System.Collections.Generic;

public class PCNavigationMechanics : MonoBehaviour
{
    public enum PCStateCode
    {
        Idle,
        ChooseService,
        WaitingForClientBankCard,
        ClientIDCardRequest,
        ClientIdentified,
        AwaitingForClientPassword,
        AwaitingMechanicalAuthenticationConfirmation,
        ClientAutenticated,
        AwaitingWithdrawAmountInput,
        OperationComplete,
        OperationError
    }

    private PCScreenPrinter pcScreen;
    private PCState[] pcStates;
    public PCStateCode pcCurrentState { get; private set; }
    private List<PCStateCode> pcPreviousState = new List<PCStateCode> { PCStateCode.Idle };

    private void Awake()
    {
        pcScreen = GetComponent<PCScreenPrinter>();
        pcStates = new PCState[]{

                    new IdleState(),
                    new ChoseServiceState(),
                    new WaintingForClientBankCardState(),
                    new ClientIDCardRequestState(),
                    new ClientIdentifiedState(),
                    new AwaitingForClientPasswordState(),
                    new ManualAuthenticationState(),
                    new ClientAuthenticatedState(),
                    new AwaitingWithdrawAmountInputState(),
                    new OperationCompleteState(),
        };
    }

    public void PCStateChangeAndAdvanceToStateOnTaskReturn(PCStateCode nextState, PCStateCode returnToThisState)
    {
        if (pcPreviousState.Count > 1)
            ForgetPreviousState();

        pcPreviousState.Add(returnToThisState);

        PCStateChange(nextState);
    }

    //Sobrecarga utiliza o estado do pc que chama a função como tela de retorno de navegação.
    public void RememberMe()
    {
        pcPreviousState.Add(pcCurrentState);
    }

    public void ForgetPreviousState()
    {
        if(pcPreviousState.Count > 1)
            pcPreviousState.RemoveAt(pcPreviousState.Count - 1);
    }

    public void PCStateChange(PCStateCode nextState)
    {
        pcCurrentState = nextState;
        pcScreen.Write(pcStates[(int)nextState].Evoke());
    }

    public PCStateCode GetPreviousState(int indexJump = 1)
    {
        for(int i = 1; i < indexJump; i++)
        {
            pcPreviousState.RemoveAt(pcPreviousState.Count - 1);
        }
        return pcPreviousState[pcPreviousState.Count - indexJump];
    }

    public void ReturnToPreviousState()
    {
        pcCurrentState = pcPreviousState[pcPreviousState.Count - 1];
        pcScreen.Write(pcStates[(int)pcCurrentState].Evoke());
        if (pcPreviousState.Count > 1)
                ForgetPreviousState();
    }

    public void LaunchErrorMessage(string errorMessage = "\n\n\n\n")
    {
        pcPreviousState.Add(pcCurrentState);
        pcCurrentState = PCStateCode.OperationError;

        pcScreen.Write(new ErrorState(errorMessage).Evoke());
    }

    public void LaunchErrorMessage(PCStateCode nextPCStageAfterErrorstring, string errorMessage = "\n\n\n\n")
    {
        pcPreviousState.Add(nextPCStageAfterErrorstring);
        pcCurrentState = PCStateCode.OperationError;

        pcScreen.Write(new ErrorState(errorMessage).Evoke());
    }

    public void Clear()
    {
        pcPreviousState.Clear();
    }
}

public class CashMechanic : TableTopMovebleObject
{
    // This var should be left public,
    // as it's value is definied in unity
    // editor (cashValue)
    public string cashValue; 

    Client activeClient;

    protected override void DoubleClick()
    {
        RemoveMe();
    }

    protected override void Drop()
    {
        if (CheckIfDroppedOverCashRegister())
        {
            CashRegister.AddOrTakeFromCashBalance(int.Parse(cashValue));
            RemoveMe();
        }

        if (CheckIfDroppedOverClientDeliveryArea())
        {
            DeliverToClient();            
        }


    }

    void DeliverToClient()
    {
            activeClient = ScriptFinder.Get<ClientCueMechanics>().GetCurrentClient();
                    if (activeClient == null) return;

            activeClient.DepositMoney(int.Parse(cashValue));
    }

}

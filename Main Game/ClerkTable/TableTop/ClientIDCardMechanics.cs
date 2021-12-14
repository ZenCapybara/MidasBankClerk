using UnityEngine;
public class ClientIDCardMechanics : TableTopMovebleObject, IDeliverable
{
    private Client activeClient;

    protected override void DoubleClick()
    {
        DeliverToClient();
    }

    protected override void Drop()
    {
        if (CheckIfDroppedOverClientDeliveryArea())
        {
            DeliverToClient();
        }
    }

    public void DeliverToClient()
    {
        isClicked = false;

        activeClient = ScriptFinder.Get<ClientCueMechanics>().GetCurrentClient();

        activeClient.ReturnID();

        transform.localPosition = new Vector3(0, 0, 0);

        gameObject.SetActive(false);
    }

}

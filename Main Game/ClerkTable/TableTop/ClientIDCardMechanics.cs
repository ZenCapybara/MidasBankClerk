using UnityEngine;
public class ClientIDCardMechanics : TableTopMovebleObject
{
    private Client activeClient;

    protected override void DoubleClick()
    {
        ReturnIdToClient();
    }

    protected override void Drop()
    {
        if (CheckIfDroppedOverClientDeliveryArea())
        {
            ReturnIdToClient();
        }
    }

    void ReturnIdToClient()
    {
        isClicked = false;

        activeClient = ScriptFinder.Get<ClientCueMechanics>().GetCurrentClient();

        activeClient.ReturnID();

        transform.localPosition = new Vector3(0, 0, 0);

        gameObject.SetActive(false);
    }

}

using UnityEngine.UI;
using UnityEngine;

public class DeliverObjectsMechanics : MonoBehaviour
{
    private GameObject clientHandoverObjectPanel;
    private bool isDeliverLocked = false;
    private bool isDeliverIDCued = false;
    private Client activeClient;

    // Start is called before the first frame update
    void Start()
    {
        clientHandoverObjectPanel = GameObject.Find("ClientIDCard");
        clientHandoverObjectPanel.SetActive(false);
    }

    public void PresentID(Client activeClient)
    {
        this.activeClient = activeClient;
        isDeliverIDCued = true;
    }

    private void PutIDOnTable()
    {
        clientHandoverObjectPanel.SetActive(true);
        //clientHandoverObjectPanel.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 25));
        GameObject.Find("IDNameTXT").GetComponent<Text>().text =
            $"NOME : {activeClient.GetName()}";
        GameObject.Find("IDBirthTXT").GetComponent<Text>().text =
            $"NASCIMENTO : {activeClient.GetBirthday().Day}/{activeClient.GetBirthday().Month}/{activeClient.GetBirthday().Year}";
        GameObject.Find("IDNumberTXT").GetComponent<Text>().text =
            $"IDENTIDADE : {activeClient.GetID()}";
        isDeliverIDCued = false;
    }

    public void DeliverLock()
    {
        isDeliverLocked = true;
    }

    public void DeliverUnlock()
    {
        isDeliverLocked = false;
    }

    public void ClearTable()
    {
        clientHandoverObjectPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDeliverLocked)
        {
            if (isDeliverIDCued)
            {
                PutIDOnTable();
            }
        }
    }
}

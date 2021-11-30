using UnityEngine;
using System.Collections.Generic;

public class ClientCueMechanics : MonoBehaviour
{
    //Variáveis Globais
    private int senhaAtual; // senha (ordem na fila) do cliente em atendimento.
    public GameObject clientPrefab;
    private List<Client> filaDeClientes = new List<Client>(100);
    public Client ClientAccessedOnPc { get; private set; }

    public Client CallNextClient()
    {
        senhaAtual++;
        if(senhaAtual == 100) senhaAtual = 0; // linha temporária enquanto o número de clientes é finito e o tempo não passa.
        return filaDeClientes[senhaAtual];
    }

    public Client GetCurrentClient()
    {
        return (senhaAtual >= 0) 
            ? filaDeClientes[senhaAtual] 
            : null;
    }

    public Client GetClientByAccount(int accountNumber)
    {
        foreach(Client cliente in filaDeClientes)
        {
            if (cliente.trueAccountNumber == accountNumber)
            {
                return cliente;
            }
        }
        return null;
    }

    public Client GetClientByID(int idNumber)
    {
        foreach(Client cliente in filaDeClientes)
        {
            if (cliente.trueIdentityNumber == idNumber)
            {
                return cliente;
            }
        }
        return null;
    }

    public Client GetClientByObjectID(int objectID)
    {
        return filaDeClientes[objectID];
    }

    // Start is called before the first frame update
    void Awake()
    {
        //
        senhaAtual = -1;
        //Inicia Fila de Clientes
        for (int i = 0; i < 100; i++)
        {
            filaDeClientes.Add(Instantiate(clientPrefab).GetComponent<Client>());
            filaDeClientes[i].ClientObjectID = i;
        }
    }

}

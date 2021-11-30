using UnityEngine;

public class Client : MonoBehaviour
{
    //Game Mechanic Control
    public int ClientObjectID { private get; set; }

    //ID
    private bool isFakingInformation = false;
    public string trueName { get; private set; }
    private string falseName = "";
    public string trueSurname { get; private set; }
    private string falseSurname = "";
    public int trueAccountNumber { get; private set;  }
    private int falseAccountNumber = -1;
    public int trueIdentityNumber { get; private set; }
    private int falseIdentityNumber = -1;
    public bool AlreadyHandedID { get; private set; }
    public int trueAge { get; private set; }
    private int falseAge;
    public int humor { get; private set; }
    private int caution { get; set; }
    public bool portaCartao { get; private set; }
    public bool portaIdentidade { get; private set; }
    public bool remembersPassword { get; private set; }

    enum Sex
    {
        Male,
        Female
    }
    Sex sex;

    //Operacional
    public int demanda { get; private set; }
    public bool demandaAtendida { get; set; }
    public int saldo { get; private set; }
    public int valorDesejadoParaSaqueOuDeposito { get; private set; }

    //Demandas
    enum Demanda
    {
        SacarDinheiro,
    }

    // Inicialização de Valores
    void Start()
    {
        //True ID Builder
        if (Random.Range(0, 1) == 0)
        {
            sex = Sex.Male;
            trueName = BancoDeNomes.GetNomeDeHomem();
        }
        else
        {
            sex = Sex.Female;
            trueName = BancoDeNomes.GetNomeDeMulher();
        }

        trueSurname = BancoDeNomes.GetSobrenome();
        trueIdentityNumber = Random.Range(1000000, 9999999);
        trueAccountNumber = Random.Range(1000000, 9999999);
        trueAge = Random.Range(18, 90);
        if (Random.Range(0, 100) < 80) { portaCartao = true; } else { portaCartao = false; }
        if (Random.Range(0, 100) < 80) { portaIdentidade = true; } else { portaIdentidade = false; }
        if (Random.Range(0, 100) < 80) { remembersPassword = true; } else { remembersPassword = false; }
        humor = Random.Range(5, 11);
        caution = Random.Range(0, 11);
        //Adicionar RANDOM em demandas após implementação de outras demandas.
        demanda = (int)Demanda.SacarDinheiro;
        demandaAtendida = false;
        AlreadyHandedID = false;
        saldo = Random.Range(-200, 2000);
        valorDesejadoParaSaqueOuDeposito = Random.Range(1, 999);

        //Fake ID Builder
        if (Random.Range(0, 100) <= 10)
            isFakingInformation = true;

        if (isFakingInformation)
        {
            bool isStolen = Random.Range(0, 100) < 30;
            //stolen ID
            if (isStolen && ClientObjectID > 0)
            {
                int robbedClientObjectID = Random.Range(0, ClientObjectID);
                Client robbedClient = ScriptFinder.Get<ClientCueMechanics>().
                                      GetClientByObjectID(robbedClientObjectID);
                falseName = robbedClient.trueName;
                falseSurname = robbedClient.trueSurname;
                falseAccountNumber = robbedClient.trueAccountNumber;
                falseIdentityNumber = robbedClient.trueIdentityNumber;
                falseAge = robbedClient.trueAge;
            }
            //forged ID
            else
            if (Random.Range(0, 1) == 0)
            {
                sex = Sex.Male;
                falseName = BancoDeNomes.GetNomeDeHomem();
            }
            else
            {
                sex = Sex.Female;
                falseName = BancoDeNomes.GetNomeDeMulher();
            }
            falseSurname = BancoDeNomes.GetSobrenome();
            falseAccountNumber = Random.Range(1000000, 9999999);
            falseIdentityNumber = Random.Range(1000000, 9999999);
            falseAge = Random.Range(18, 90);
        }

    }

    /******************************************/
    //     NON-STANDARD GETTERS & SETTERS
    /******************************************/

    public override bool Equals(object other)
    {
        Client otherCLient = other as Client;
        if (otherCLient == null || otherCLient.trueIdentityNumber != this.trueIdentityNumber)
        {
            return false;
        }        
        return true;
    }

    public string GetName()
    {
        if (isFakingInformation)
        {
            {
                if (sex == Sex.Male)
                    return $"Sr. {falseName} {falseSurname}";                
                return $"Sra. {falseName} {falseSurname}";
            }
        }
        else
        {
            if(sex == Sex.Male)
                return $"Sr. {trueName} {trueSurname}";
            return $"Sra. {trueName} {trueSurname}";
        }
    }

    public int GetAccount()
    {
        if (isFakingInformation)
            return falseAccountNumber;

        return trueAccountNumber;
    }

    public int GetID()
    {
        AlreadyHandedID = true;

        if (isFakingInformation)
            return falseIdentityNumber;

        return trueIdentityNumber;
    }

    public void DepositMoney(int dinheiro)
    {
        saldo += dinheiro;
    }

    public bool WithdrawMoney(int dinheiro)
    {
        if (saldo < dinheiro)
        {
            ReduceMoodDueToBadService();
            return false;
        }

        saldo -= dinheiro;
        return true;
    }

    public void SetValorDesejadoParaSaqueOuDeposito()
    {
        valorDesejadoParaSaqueOuDeposito = Random.Range(1, saldo);
    }

    public void ReturnID()
    {
        AlreadyHandedID = false;
    }

    public void ReduceMoodDueToBadService(int valorDeReducao = 1)
    {
        humor -= valorDeReducao;
    }

    public void EndDemand()
    {
        demandaAtendida = true;
        humor++;
    }

}

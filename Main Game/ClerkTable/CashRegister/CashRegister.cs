
using UnityEngine;

public class CashRegister : MonoBehaviour
{
    private GameObject mesaDeAtendimento;

    //Para Criação das Notas no Balcão
    public GameObject notaDeUm, notaDeDois, notaDeCinco, notaDeDez, notaDeCinquenta, notaDeCem;
    private GameObject novaNotaInstanciada;
    public static int CashBalance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        //Capture Table For Interaction
        mesaDeAtendimento = GameObject.Find("AreaDeTrabalho");
    }

    public void PutCashOnTable(string cashFlutuation)
    {
        switch (cashFlutuation)
        {
            case "1":
                novaNotaInstanciada = Instantiate(notaDeUm, mesaDeAtendimento.gameObject.transform);
                break;
            case "2":
                novaNotaInstanciada = Instantiate(notaDeDois, mesaDeAtendimento.gameObject.transform);
                break;
            case "5":
                novaNotaInstanciada = Instantiate(notaDeCinco, mesaDeAtendimento.gameObject.transform);
                break;
            case "10":
                novaNotaInstanciada = Instantiate(notaDeDez, mesaDeAtendimento.gameObject.transform);
                break;
            case "50":
                novaNotaInstanciada = Instantiate(notaDeCinquenta, mesaDeAtendimento.gameObject.transform);
                break;
            case "100":
                novaNotaInstanciada = Instantiate(notaDeCem, mesaDeAtendimento.gameObject.transform);
                break;
            default:
                break;
        }
        // -> Uncomment if Regina decides the objects should be tilted on the table. // novaNotaInstanciada.transform.Rotate(0, 0, Random.Range(-20, 20));
        novaNotaInstanciada.transform.position += new Vector3(Random.Range(-125, 150), Random.Range(-225, 25));
    }

    public static void AddOrTakeFromCashBalance(int cashVariation)
    {
        CashBalance += cashVariation;
    }
}

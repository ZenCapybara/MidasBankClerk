
using UnityEngine;

public class CashRegister : MonoBehaviour
{
    private GameObject mesaDeAtendimento;

    //Para Criação das Notas no Balcão
    public GameObject notaDeUm, notaDeCinco, notaDeDez, notaDeCinquenta;
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
                novaNotaInstanciada.transform.Rotate(0, 0, Random.Range(-20, 20));
                novaNotaInstanciada.transform.position += new Vector3(Random.Range(-150, 150), 0);
                break;
            case "5":
                novaNotaInstanciada = Instantiate(notaDeCinco, mesaDeAtendimento.gameObject.transform);
                novaNotaInstanciada.transform.Rotate(0, 0, Random.Range(-20, 20));
                novaNotaInstanciada.transform.position += new Vector3(Random.Range(-150, 150), 0);
                break;
            case "10":
                novaNotaInstanciada = Instantiate(notaDeDez, mesaDeAtendimento.gameObject.transform);
                novaNotaInstanciada.transform.Rotate(0, 0, Random.Range(-20, 20));
                novaNotaInstanciada.transform.position += new Vector3(Random.Range(-150, 150), 0);
                break;
            case "50":
                novaNotaInstanciada = Instantiate(notaDeCinquenta, mesaDeAtendimento.gameObject.transform);
                novaNotaInstanciada.transform.Rotate(0, 0, Random.Range(-20, 20));
                novaNotaInstanciada.transform.position += new Vector3(Random.Range(-150, 150), 0);
                break;
            default:
                break;
        }
    }

    public static void AddOrTakeFromCashBalance(int cashVariation)
    {
        CashBalance += cashVariation;
    }
}

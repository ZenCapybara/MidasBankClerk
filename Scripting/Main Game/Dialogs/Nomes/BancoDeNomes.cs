using UnityEngine;

public static class BancoDeNomes
{
    private static string[] nomeHomem = {
        "John",
        "Jack",
        "Bob",
        };

    public static string GetNomeDeHomem()
    {
        return (nomeHomem[Random.Range(0, nomeHomem.Length - 1)]);
    }

    private static string[] nomeMulher = {
        "Mary",
        "Stacy",
        "Cris",
        };

    public static string GetNomeDeMulher()
    {
        return (nomeMulher[Random.Range(0, nomeMulher.Length - 1)]);
    }

    private static string[] sobrenome = {
        "Doe",
        "Deer",
        "Smith",
        };

    public static string GetSobrenome()
    {
        return (sobrenome[Random.Range(0, sobrenome.Length - 1)]);
    }
}

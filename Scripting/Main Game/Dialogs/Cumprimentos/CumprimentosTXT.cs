using UnityEngine;

public static class CumprimentosTXT
{
    private static string[] Cumprimentos =
        {
        "Bom dia.",
        "Hmmm... Você não parece ser bom nisso.",
        "Você de novo? Que saco.",
        "Me ajuda... ME AJUDA! AJUDAAAA!!!!!"
    };

    public static string getCumprimentos()
    {
        return Cumprimentos[Random.Range(0, Cumprimentos.Length - 1)];
    }
}

using UnityEngine;

public static class ErroNoAtendimento
{
    private static string[] reclamacao =
    {
        "Atendimento incorreto! Estou insatisfeito. (PLACEHOLDER1)",
        "Atendimento incorreto! Estou insatisfeito. (PLACEHOLDER2)"
    };


    public static string GetDialogue(string extraInput = "")
    {
        return extraInput + " " + reclamacao[Random.Range(0, reclamacao.Length - 1)];
    }
}

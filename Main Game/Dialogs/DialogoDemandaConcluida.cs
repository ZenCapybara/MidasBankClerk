using UnityEngine;

public static class DialogoDemandaConcluida
{
    private static readonly string[] BemHumorado =
    {
        "Muito bom! Por hoje é só!",
        "Bom o atendimento! Depois eu volto!",
        "Ok! Eu só precisava disso! Até mais!"
    };

    private static readonly string[] Neutro =
    {
        "Era só isso. Até a próxima.",
        "Era só isso. Vou embora.",
        "Hmmm... Tchau então."
    };

    private static readonly string[] MalHumorado =
{
        "Vou tentar voltar outra hora. Quem sabe pego outra pessoa no caixa.",
        "Que atendimento mediocre. Vou embora.",
        "Eu devia ter usado o banheiro e ido embora."
    };

    public static string GetDialogo(int humor)
    {
        if (humor > 3)
        {
            return BemHumorado[Random.Range(0, BemHumorado.Length - 1)];
        }
        else if (humor > 0)
        {
            return Neutro[Random.Range(0, Neutro.Length - 1)];
        }
        else 
        {
            return MalHumorado[Random.Range(0, MalHumorado.Length - 1)];
        }
    }
}

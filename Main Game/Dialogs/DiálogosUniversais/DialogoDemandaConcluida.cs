using UnityEngine;

public static class DialogoDemandaConcluida
{
    private static readonly string[] BemHumorado =
    {
        "Gostei do atendimento. Obrigado (PLACEHOLDER1 - Humor Bom)",
        "Gostei do atendimento. Obrigado (PLACEHOLDER2 - Humor Bom)"
    };

    private static readonly string[] Neutro =
    {
        "Já tenho o que queria. Obrigado (PLACEHOLDER1 - Humor Neutro)",
        "Já tenho o que queria. Obrigado (PLACEHOLDER2 - Humor Neutro)"
    };

    private static readonly string[] MalHumorado =
{
        "Péssimo atendimento. Vou embora (PLACEHOLDER1 - Humor Ruim)",
        "Péssimo atendimento. Vou embora (PLACEHOLDER2 - Humor Ruim)"
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

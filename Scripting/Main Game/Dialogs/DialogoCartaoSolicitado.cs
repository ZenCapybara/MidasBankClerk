using UnityEngine;

public static class DialogoCartaoSolicitado
{
    private static string[] TenhoCartao =
    {
        "Tá aqui meu cartão. Mas vou precisar dele de volta.",
        "Hmmm... Suspeito. Está aqui. Mas estou de olho.",
        "Aiaiai, que saco. Tá aqui meu cartão.",
        "Meu cartão é platinum plus. Oiá só."
    };

    private static string[] NaoTenhoCartao =
    {
        "Xiii, meu cachorro comeu.",
        "Hmmm, eu jurava que estava na bolsa. Não trouxe.",
        "Jamais daria meu cartão para um desconhecido!",
        "Hmmm... e se eu não quiser dar meu cartão?"
};

    static public string GetDialogue(bool haveCard)
    {
        if (haveCard)
        {
            return TenhoCartao[Random.Range(0, TenhoCartao.Length - 1)];
        }
        return NaoTenhoCartao[Random.Range(0, NaoTenhoCartao.Length - 1)];
    }
}
using UnityEngine;

public static class DialogoCartaoSolicitado
{
    private static string[] TenhoCartao =
    {
        "Aqui está (PLACEHOLDER 1)",
        "Aqui está (PLACEHOLDER 2)"
    };

    private static string[] CartaoInvalido =
    {
        "Ok, cartão inválido (PLACEHOLDER 1)",
        "Ok, cartão inválido (PLACEHOLDER 2)"
    };

    private static string[] NaoTenhoCartao =
    {
        "Estou sem cartão (PLACEHOLDER 1)",
        "Estou sem cartão (PLACEHOLDER 2)"

    };

    /// <summary>
    /// Tipo 1 - Aqui Está Meu Cartão, Tipo 2- Cartão Inválido, Tipo 3 - Esqueci Meu Cartão
    /// </summary>
    /// <param name="tipoDeDialogo"></param>
    /// <returns></returns>
    static public string GetDialogue(int tipoDeDialogo)
    {
        switch (tipoDeDialogo)
        {
            case 1:
                return TenhoCartao[Random.Range(0, TenhoCartao.Length - 1)];
            case 2:
                return CartaoInvalido[Random.Range(0, CartaoInvalido.Length - 1)];
            case 3:
                return NaoTenhoCartao[Random.Range(0, NaoTenhoCartao.Length - 1)];
            default:
                Debug.LogError("Solicitado Diálogo Inválido!");
                return "Diálogo Inválido";
        }

    }
}
using UnityEngine;

/// <summary>
/// Estágios do saque: 1- Informar que quer sacar; 2- Informa valor do saque; 3- Reage a saldo insuficiente.
/// </summary>
public static class DialogosDoSaque
{
    private static string[] EstagioUmQueroSacar =
    {
        "Preciso sacar dinheiro hoje.",
        "Tenho que pagar uma conta urgente! Preciso retirar um dinheiro.",
        "Meu chihuahua está doente, preciso de dinheiro para pagar o veterinário."
            ,"Money, money, money!",
        "Será que você pode sacar uma grana para mim?",
        "Quero sacar meu dinheiro, por favor.",
        "Isso não é um banco? Vim pegar dinheiro. Oras.",
        "Me dá dinheiro.",
        "Estou com dificuldade de sacar no caixa eletrônico. Preciso de ajuda.",
        "Nesse mundo cão o dinheiro é meu irmão. Preciso fazer um saque!",
        "Psiu! Vim sacar dinheiro, mas seja discreto."
    };

    //Tomás, nessa do saque o programa vai substituir o $ pelo valor.
    //Então coloca o $ na posição onde a pessoa fala o valor.
    private static string[] EstagioDoisValorDoSaque =
    {
        "Quero sacar $ (PLACEHOLDER1)",
        "Quero sacar $ (PLACEHOLDER2)"
    };

    private static string[] EstagioTresSaldoInsuficiente =
    {
        "Parece que estou sem sorte hoje... (PLACEHOLDER?:)",
    };

    /// <summary>
    /// 1- Quero Sacar // 2- Valor do Saque // 3- Saldo Insuficiente
    /// </summary>
    public static string GetDialogue(int estagioDaDemanda)
    {
        if (estagioDaDemanda == 1)
        {
            return EstagioUmQueroSacar[Random.Range(0, EstagioUmQueroSacar.Length - 1)];
        }
        if (estagioDaDemanda == 2)
        {
            return EstagioDoisValorDoSaque[Random.Range(0, EstagioDoisValorDoSaque.Length - 1)];
        }
        if (estagioDaDemanda == 3)
        {
            return EstagioTresSaldoInsuficiente[Random.Range(0, EstagioTresSaldoInsuficiente.Length - 1)];
        }

        return "Ó não! Algum bug no sistema de diálogo! Melhor falar com o programador...";
    }
}

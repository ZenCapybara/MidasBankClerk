using UnityEngine;

public static class DialogoSenhaSolicitada
{
    private static string[] AquiEstaMinhaSenha =
    {
        "Ok, vou digitar a senha. (PLACEHOLDER1)",
        "Ok, vou digitar a senha. (PLACEHOLDER2)"
    };

    private static string[] ErreiMinhaSenha =
    {
        "Errei minha senha. Quero tentar de novo. (PLACEHOLDER1)",
        "Errei minha senha. Quero tentar de novo. (PLACEHOLDER2)"
    };

    private static string[] NaoSeiMinhaSenha =
    {
        "Esqueci minha senha. Não adianta tentar de novo. (PLACEHOLDER1)",
        "Esqueci minha senha. Não adianta tentar de novo. (PLACEHOLDER1)"
    };

    /// <summary>
    /// Devolve um diálogo conforme a circunstância. 1 - Aqui está minha senha; 2- Errei a senha; 3 - Esqueci a senha.
    /// </summary>
    /// <param name="typeOfDialogue">1 - Aqui está minha senha; 2- Errei a senha; 3 - Esqueci a senha.</param>
    /// <returns></returns>
    public static string GetDialogue(int typeOfDialogue)
    {
        if (typeOfDialogue == 1)
        {
            return AquiEstaMinhaSenha[Random.Range(0, AquiEstaMinhaSenha.Length)];
        }
        else if (typeOfDialogue == 2)
        {
            return ErreiMinhaSenha[Random.Range(0, ErreiMinhaSenha.Length)];
        }
        else if (typeOfDialogue == 3)
        {
            return NaoSeiMinhaSenha[Random.Range(0, NaoSeiMinhaSenha.Length)];
        }

        return "ERROR: Dialogue option not avialble.";
    }
}

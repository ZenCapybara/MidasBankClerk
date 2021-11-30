using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DialogoRGSolicitado
{
    public enum estadoDoRg
    {
        tenhoRG,
        naoTenhoRG,
        jaEntregueiRG
    }

    private static string[] TenhoRG =
        {
        "Tá aqui minha identidade. Mas vou precisar dela de volta.",
        "Hmmm... Suspeito. Está aqui. Mas estou de olho.",
        "Aiaiai, que saco. Tá aqui minha identidade.",
        "Minha identidade é novinha. Oia só."
    };

    private static string[] NaoTenhoRG =
    {
        "Xiii, meu cachorro comeu.",
        "Hmmm, eu jurava que estava na bolsa. Não trouxe.",
        "Jamais daria minha identidade para um desconhecido!",
        "Hmmm... e se eu não quiser dar minha identidade?"
    };

    private static string[] VcJaTemMinhaRG =
{
        "Você PERDEU MINHA RG?",
        "Uai... outra RG?",
        "Sabe o que é isso na sua mesa? ISSO MESMO! MINHA RG! Hmf."
    };


    public static string GetDialogue(estadoDoRg rg)
    {
        if (rg == estadoDoRg.tenhoRG)        
            return TenhoRG[Random.Range(0, TenhoRG.Length - 1)];

        if (rg == estadoDoRg.jaEntregueiRG)
            return VcJaTemMinhaRG[Random.Range(0, VcJaTemMinhaRG.Length - 1)];

        return NaoTenhoRG[Random.Range(0, NaoTenhoRG.Length - 1)];

    }
}

using UnityEngine;

public static class ErroNoAtendimento
{
    private static string[] reclamacao =
          {
        "Foi isso que eu pedi? FOI ISSO QUE EU PEDI?",
        "Hmmm... vamos começar de novo? Prestando atenção dessa vez?",
        "Aiaiai, que saco. Vou falar mais uma vez. Mais uma vez!",
        "Me parece que alguém não vai receber boa nota no atendimento.",
        "Que poxa. Será que alguém melhor pode me atender?",
        "Perdão... Acho que fiz confusão..."
    };


    public static string GetDialogue(string extraInput = "")
    {
        return extraInput + " " +  reclamacao[Random.Range(0, reclamacao.Length - 1)];
    }
}

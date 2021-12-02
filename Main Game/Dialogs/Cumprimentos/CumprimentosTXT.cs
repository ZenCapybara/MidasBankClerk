using UnityEngine;

public static class CumprimentosTXT
{
    private static string[] Cumprimentos =
        {
        "Olá!","Bom dia!",
        "Oi, tudo bem?",
        "Olá, tudo bom?",
        "Olá, como vai?",
        "Oi...", 
        "E aqui estou eu novamente...",
        "Esse lugar tem um cheiro estranho, você está sentindo?",
        "Estava atravessando a rua e vi um cachorro fazendo bananeira. O quão estranho é isso?",
        "Nossa, o útimo atendimento foi muito demorado. Seja rápido.",
        "As vezes sinto que você não se importa comigo.",
        "Você é o atendente novo, certo?",
    };

    public static string getCumprimentos()
    {
        return Cumprimentos[Random.Range(0, Cumprimentos.Length - 1)];
    }
}

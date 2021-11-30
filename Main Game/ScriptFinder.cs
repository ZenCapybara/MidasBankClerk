using UnityEngine;
public static class ScriptFinder 
{
    public static Script Get<Script>()
    {
        return GameObject.Find("SCRIPTS").GetComponentInChildren<Script>();
    }   
}

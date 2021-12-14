using UnityEngine;
using System.Collections.Generic;

public class HandAllMechanics: MonoBehaviour
{
    GameObject tableWithObjects;
    void Start()
    {
        tableWithObjects = GameObject.Find("AreaDeTrabalho");
    }

    public void CollectAll()
    {
        List<GameObject> deliverableObjects = new List<GameObject>();

        for(int i = 0; i < tableWithObjects.transform.childCount; i++)
        {
            deliverableObjects.Add(tableWithObjects.transform.GetChild(i).gameObject);
        }

        foreach (GameObject tableObject in deliverableObjects)
             tableObject.GetComponent<IDeliverable>().DeliverToClient();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckEvent : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject inOfTruckPosition;
    [SerializeField] private GameObject outOfTruckPosition;

    private void TransferPlayerToTruck()
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        player.transform.position = inOfTruckPosition.transform.position;
        player.transform.SetParent(inOfTruckPosition.transform);
    }

    private void TransferPlayerOutOfTruck()
    {
        player.transform.parent = null;
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = outOfTruckPosition.transform.position;
        player.GetComponent<CharacterController>().enabled = true;
        player.GetComponent<PlayerMovement>().enabled = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckEvent : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject truckCamera;

    public void TransferPlayer()
    {
        player.SetActive(false);
        truckCamera.SetActive(true);
    }
}

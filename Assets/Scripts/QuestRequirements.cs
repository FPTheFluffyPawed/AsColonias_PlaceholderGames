using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestRequirements : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToGet;
    [SerializeField] private Interactive rewardToGive;
    [SerializeField] private Interactive removeFromInventory;
    [SerializeField] private PlayerInteraction playerInventory;
    [SerializeField] private PlayerInterface playerInterface;
    [SerializeField] private string newObjectiveText;

    private bool acquiredAll = false;

    private void FixedUpdate()
    {
        CheckQuestStatus();
    }

    private void CheckQuestStatus()
    {
        for(int i = 0; i < objectsToGet.Length; i++)
        {
            if (objectsToGet[i].activeSelf)
            {
                acquiredAll = false;
                break;
            }
            else
                acquiredAll = true;
        }

        if(acquiredAll == true)
        {
            if (removeFromInventory != null)
                playerInventory.inventory.Remove(removeFromInventory);

            playerInterface.UpdateObjectiveText(newObjectiveText);
            playerInventory.inventory.Add(rewardToGive);

            gameObject.SetActive(false);
        }
    }
}

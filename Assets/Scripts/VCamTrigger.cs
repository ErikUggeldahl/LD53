using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class VCamTrigger : MonoBehaviour
{
    [SerializeField] private GameObject vCam;
    [SerializeField] private DialogueInfo dialogueInfo;
    [SerializeField] private GameObject dialogueUI;
    [SerializeField] private TMP_Text nameField;
    public Game.Character Character;

    [SerializeField] private UnityEvent onEnter;

    private void Start()
    {
        dialogueUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        vCam.SetActive(true);
        dialogueInfo.VCam.SetActive(true);
        
        dialogueUI.SetActive(true);
        nameField.text = dialogueInfo.Name;

        onEnter.Invoke();
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        vCam.SetActive(false);
        dialogueInfo.VCam.SetActive(false);
        
        dialogueUI.SetActive(false);
    }
}

using UnityEngine;
using TMPro;

public class TorchPickup : MonoBehaviour
{
    public Light torchLight; // Assign in inspector
    public GameObject visualTorch; // The mesh/model
    private bool isPlayerInRange = false;

    public TMP_Text torchText; // Assign in inspector

    public GameObject playerTorch;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            torchText.text = "Press F to pick up the torch";
            torchText.gameObject.SetActive(true); // Show the pickup text
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (torchText != null)
            {
                torchText.text = "";
                torchText.gameObject.SetActive(false); // Hide the pickup text

            }
            
        }
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            GiveTorchToPlayer();
        }
    }

    void GiveTorchToPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Transform torchHold = player.transform.Find("TorchHold");
        playerTorch.SetActive(true);

        if (torchHold != null)
        {
            
            // Instantiate a new torch light on player
            GameObject torchInstance = new GameObject("PlayerTorchLight");
            torchInstance.transform.SetParent(torchHold);
            torchInstance.transform.localPosition = Vector3.zero;

            Light newLight = torchInstance.AddComponent<Light>();
            newLight.type = torchLight.type;
            newLight.color = torchLight.color;
            newLight.intensity = torchLight.intensity;
            newLight.range = torchLight.range;
            newLight.spotAngle = torchLight.spotAngle;

            // Optional: Add flame particles or attach a torch model
        }

        // Hide or disable original torch
        visualTorch.SetActive(false);
        torchLight.enabled = false;
        Destroy(this.gameObject, 0.5f); // Or disable if you want to reuse it
    }
}
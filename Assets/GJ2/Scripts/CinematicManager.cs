using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class CinematicManager : MonoBehaviour, INotificationReceiver
{
    [Header("Camera Setup")]
    public Camera fpsCamera; // Reference to the FPS camera component
    public CinemachineCamera[] dollyCams; // Array of dolly cameras (optional, for manual control)

    [Header("Timeline Settings")]
    public PlayableDirector timelineDirector; // Reference to your Timeline's PlayableDirector

    private bool cinematicActive = true;

    void Start()
    {
        // Ensure FPS camera starts disabled during cinematic
        if (fpsCamera != null)
            fpsCamera.gameObject.SetActive(false);
    }

    // This method is called when a Signal is emitted from Timeline
    public void OnNotify(Playable origin, INotification notification, object context)
    {
        // Check if this is a Signal notification
        if (notification is SignalEmitter signalEmitter)
        {
            // You can check for specific signals by name if needed
            // For now, any signal will trigger the FPS camera switch
            EnableFPSCam();
        }
    }

    public void EnableFPSCam()
    {
        if (!cinematicActive) return; // Already switched

        cinematicActive = false;

        // Lower all dolly cam priorities (if you want to manage them here)
        foreach (var cam in dollyCams)
        {
            if (cam != null)
                cam.Priority = 0;
        }

        // Enable the FPS camera
        if (fpsCamera != null)
        {
            fpsCamera.gameObject.SetActive(true);
            Debug.Log("Timeline signal received - FPS Camera activated");
        }

        // Optional: Stop the timeline if needed
        if (timelineDirector != null && timelineDirector.state == PlayState.Playing)
        {
            // Uncomment if you want to stop timeline after signal
            // timelineDirector.Stop();
        }
    }

    // Optional: Method to restart cinematic
    public void RestartCinematic()
    {
        cinematicActive = true;

        if (fpsCamera != null)
            fpsCamera.gameObject.SetActive(false);

        if (timelineDirector != null)
        {
            timelineDirector.time = 0;
            timelineDirector.Play();
        }
    }

    // Optional: Manual method to enable FPS (can be called from other scripts)
    public void ForceEnableFPSCam()
    {
        EnableFPSCam();
    }
}
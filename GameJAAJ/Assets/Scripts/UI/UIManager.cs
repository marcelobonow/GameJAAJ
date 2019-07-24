using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class UIManager : MonoBehaviour
{
    [SerializeField] protected Image crosshair;
    [SerializeField] protected FirstPersonController firstPersonController;
    [SerializeField] protected CharacterController characterController;
    [SerializeField] protected TextMeshProUGUI interactableText;
    [SerializeField] protected TextMeshProUGUI detectiveModeTimeRemaning;
    [SerializeField] protected TextMeshProUGUI detectiveModeTimeToRecharge;
    [SerializeField] protected GameObject pauseMenu;
    [SerializeField] protected Button resumeButton;
    [SerializeField] protected Button controlsButton;
    [SerializeField] protected Button quitButton;

    protected static UIManager instance;
    private static bool isPaused;

    private void Awake()
    {
        if(instance != null)
            Destroy(this);
        instance = this;
        resumeButton.onClick.AddListener(OnResumePressed);
        controlsButton.onClick.AddListener(OnControlsPressed);
        quitButton.onClick.AddListener(OnQuitPressed);
        OnResumePressed();
    }

    private void Update()
    {
        if(Input.GetButton("Menu"))
            Pause();
    }


    public void OnControlsPressed()
    {

    }
    public void OnQuitPressed() => Application.Quit();

    public static void Pause()
    {
        instance.pauseMenu.gameObject.SetActive(true);
        isPaused = true;
        instance.firstPersonController.enabled = false;
        instance.characterController.enabled = false;
        Time.timeScale = 0;
    }
    public static void OnResumePressed()
    {
        instance.pauseMenu.gameObject.SetActive(false);
        isPaused = false;
        instance.firstPersonController.enabled = true;
        instance.characterController.enabled = true;
        Time.timeScale = 1;
    }

    public static bool IsPaused() => isPaused;

    public static void SetCrosshairColor(Color color) => instance.crosshair.color = color;
    public static void SetEnableCrosshair(bool state) => instance.crosshair.enabled = state;

    public static void SetInteractMessage(bool active) => instance.interactableText.gameObject.SetActive(active);
    public static void SetEnableDetectiveMode(bool state)
    {
        instance.detectiveModeTimeRemaning.gameObject.SetActive(state);
        instance.detectiveModeTimeToRecharge.gameObject.SetActive(!state);
    }
    public static void SetDetectiveModeTimeRemaning(float timeRemaning) => instance.detectiveModeTimeRemaning.text = Mathf.Ceil(timeRemaning).ToString();
    public static void SetDetectiveModeTimeToRecharge(string timeToRecharge, Color color)
    {
        instance.detectiveModeTimeToRecharge.text = timeToRecharge;
        instance.detectiveModeTimeToRecharge.color = color;
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private static Image _cursor;
    public Sprite[] sprites;
    private static Sprite[] _sprites;

    private static Image _duration;
    public Image duration;

    public Image playerHealth;

    public Text fpsCounter;
    public Text time;   

    public CanvasGroup overlay;
    public CanvasGroup menu;
    public CanvasGroup console;
    public CanvasGroup inventory;

    public static UIManager instance;

    public GameObject buttons;

    private Button[] mainMenuButtons;
    private int selectedIndex = 0;
    private bool changed = false;
    private bool rapidSelect = false;
    private float rapidSelectDuration = 0.4f;
    private float rapidChangeDuration = 0.05f;
    private float axisValue = 0f;

    void Start()
    {
        if(instance == null)
            instance = this;

        mainMenuButtons = buttons.GetComponentsInChildren<Button>();

        _cursor = GetComponentInChildren<Image>();
        _sprites = sprites;
        _duration = duration;

        StartCoroutine(FPS());
    }

    void Update()
    {
        /*if (InputManager.Controller)
        {
            axisValue = Input.GetAxis("Vertical") + Input.GetAxis("VDpad");
            if (Input.GetButtonDown("A"))
            {
                mainMenuButtons[selectedIndex].onClick.Invoke();
            }
            if (Mathf.Abs(axisValue) == 1 && !changed)
            {
                if (rapidSelect)
                {
                    StartCoroutine(RapidChangeWait());
                }
                else
                {
                    StartCoroutine(RapidSelectWait());
                }

                changed = true;

                selectedIndex -= Mathf.RoundToInt(axisValue);
                selectedIndex = Mathf.Clamp(selectedIndex, 0, mainMenuButtons.Length - 1);
            }
            if (Mathf.Abs(axisValue) != 1)
            {
                StopCoroutine(RapidSelectWait());
                changed = false;
                rapidSelect = false;
            }
            mainMenuButtons[selectedIndex].Select();
        }*/

        int hours = Mathf.FloorToInt(DataController.data.currentSave.currentTime);
        int minutes = Mathf.FloorToInt((DataController.data.currentSave.currentTime - hours) * 60);

        string M = " AM";
        if(hours > 12)
        {
            hours -= 12;
            M = " PM";
        }

        time.text = hours.ToString() + ":" + minutes.ToString("D2") + M;

        if(Input.GetButtonDown("Menu"))
        {
            if (ConsoleManager.open)
            {
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                ConsoleManager.open = false;
                ToggleCanvasGroups(true, false , false, false);
            }
            else
            {
                if(InputManager.Controller)
                    Cursor.lockState = CursorLockMode.None;
                ToggleCanvasGroups(false, false, true, false);
            }
        }

        if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.T))
        {
            Time.timeScale = 0;
            ConsoleManager.open = true;
            Cursor.lockState = CursorLockMode.None;
            ToggleCanvasGroups(false, true, false, false);
        }
    }

    public void UpdateHealthBar()
    {
        float fillAmmount = Player.health / Player.maxHealth;
        playerHealth.fillAmount = fillAmmount;
    }

    IEnumerator RapidSelectWait()
    {
        yield return new WaitForSeconds(rapidSelectDuration);
        changed = false;
        rapidSelect = true;
    }

    IEnumerator RapidChangeWait()
    {
        yield return new WaitForSeconds(rapidChangeDuration);
        changed = false;
    }

    public void ToggleCanvasGroups(bool overlay, bool console, bool menu, bool inventory)
    {
        this.overlay.alpha = overlay ? 1 : 0;
        this.overlay.interactable = overlay;
        this.overlay.blocksRaycasts = overlay;

        this.console.alpha = console ? 1 : 0;
        this.console.interactable = console;
        this.console.blocksRaycasts = console;

        this.menu.alpha = menu ? 1 : 0;
        this.menu.interactable = menu;
        this.menu.blocksRaycasts = menu;

        this.inventory.alpha = inventory ? 1 : 0;
        this.inventory.interactable = inventory;
        this.inventory.blocksRaycasts = inventory;
    }

    public void MainMenuClicked()
    {
        SceneManager.LoadScene(0);
    }

    public void SaveGameClicked()
    {
        Player.Save();
        ToggleCanvasGroups(true, false, false, false);
        ConsoleManager.Log("Game saved to save slot 1.");
    }

    public void ResumeClicked()
    {
        ToggleCanvasGroups(true, false, false, false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    IEnumerator FPS()
    {
        while (true)
        {
            fpsCounter.text = "FPS: " + Mathf.Floor(1 / Time.unscaledDeltaTime);
            yield return new WaitForSeconds(1);
        }
    }

    public static void Duration(float percentage)
    {       
        _duration.fillAmount = percentage;
    }

    public static void ChangeCursor(int cursorId)
    {
        _cursor.sprite = _sprites[cursorId];
    }
}

public struct CursorType
{
    /// <summary>
    /// Dot cursor.
    /// </summary>
    public const int DEFAULT = 0;

    /// <summary>
    /// Hexagon cursor.
    /// </summary>
    public const int HEXAGON = 1;
}

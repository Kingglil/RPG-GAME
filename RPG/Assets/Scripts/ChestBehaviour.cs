using UnityEngine;

public class ChestBehaviour : MonoBehaviour
{

    public string rarity;
    public InventoryItem[] inventory;
    public RuntimeAnimatorController animController;
    public LayerMask layerMask;

    private Animator animator;

    public static bool opened = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (opened)
        {
            if (Input.GetButtonDown("B"))
            {
                Close();
            }
        }
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, 6f, layerMask))
            {               
                if (hitInfo.transform == transform)
                {
                    UIManager.ChangeCursor(CursorType.HEXAGON);
                    if (Input.GetButtonDown("A"))
                    {
                        Open();
                    }
                }
                else
                {
                    UIManager.ChangeCursor(CursorType.DEFAULT);
                }
            }
            else
            {
                UIManager.ChangeCursor(CursorType.DEFAULT);
            }
        }
    }

    public void ClearInventory(int itemsCount)
    {
        inventory = new InventoryItem[itemsCount];
    }

    void Open()
    {
        UIManager.instance.ToggleCanvasGroups(false, false, false, true);

        opened = true;
        animator.runtimeAnimatorController = animController;
        animator.SetBool("IsOpen", true);
        animator.SetInteger("ChestName", name[name.Length - 1] - 48);
    }

    void Close()
    {
        UIManager.instance.ToggleCanvasGroups(true, false, false, false);
        animator.SetBool("IsOpen", false);
    }
}

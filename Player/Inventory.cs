using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Inventory : MonoBehaviour {

    public List<GameObject> inventory = new List<GameObject>();

    public GUISkin invSkin;
    private GUIStyle myStyle = new GUIStyle();
    public Equipment equipment;

    public string invTooltip = "";

	public bool openInv = false;
    public bool openContextMenu = false;
    public bool HoveringInvItem = false;
    public int clickedItem;
    private Rect invWindowRect = new Rect(Screen.width/2, 0, 300, 200);
    private Rect ContextMenuRect;
	void OnGUI()
    {
        GUI.skin = invSkin;
		if(openInv==true)
        {
            invWindowRect = GUI.Window(0, invWindowRect, OpenInventory, "Inventory"); 
		}
        if (openContextMenu == true && openInv == true)
        {
            ContextMenu(clickedItem, ContextMenuRect);
        }
        if (invTooltip != "" && openContextMenu == false && openInv == true)
        {
            GUI.depth = -1;
            Vector2 BoxSize = myStyle.CalcSize(new GUIContent(invTooltip));
            Debug.Log("BoxSizeX:" + BoxSize.x + "|| BoxSizeY" + BoxSize.y);
            GUI.Label(new Rect(Input.mousePosition.x +10, Screen.height - Input.mousePosition.y, BoxSize.x+10, BoxSize.y+10), invTooltip);
            
        }
	}
    public Vector2 scrollPosition;

    void OpenInventory(int id)
    {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(280), GUILayout.Height(170));
        for (int i = 0; i < inventory.Count; i++ )
        {
            inventory[i].GetComponent<Item>().MouseOver = false;
            GUILayout.BeginHorizontal();
            GUI.depth = 2;
            if (GUILayout.Button(new GUIContent(inventory[i].GetComponent<Item>().ItemName.ToString(), inventory[i].GetComponent<Item>().tooltip)))
            {
                openContextMenu = true;
                clickedItem = i;
                ContextMenuRect = new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, 150, 150);
            }
            GUILayout.EndHorizontal();
            if (Event.current.type == EventType.Repaint)
            {
                invTooltip = GUI.tooltip;
            }
            /*if (Event.current.type == EventType.Repaint &&
            GUILayoutUtility.GetLastRect().Contains(Event.current.mousePosition))
            {
                Debug.Log("Hoveringitem" + i);
            }*/
        }
        GUILayout.EndScrollView();
        
        GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
	}

    void ContextMenu(int id, Rect rect)
    {
        GUILayout.BeginArea(rect);
        GUILayout.BeginHorizontal();
        if(GUILayout.Button("Drop : " + inventory[id].GetComponent<Item>().ItemName.ToString())){
            inventory[id].gameObject.transform.position = this.transform.position + Vector3.forward + Vector3.up;
            inventory[id].gameObject.SetActive(true);
            inventory[id].gameObject.rigidbody.isKinematic = false;
            inventory[id].gameObject.transform.parent = null;
            inventory.RemoveAt(id);
            openContextMenu = false;
        }
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Equip : " + inventory[id].GetComponent<Item>().ItemName.ToString()))
        {
            equipment.rightHand = inventory[id];
            equipment.rightHandEquiped = true;
            equipment.rightHand.transform.parent = GetComponentInChildren<RightHand>().gameObject.transform;
            equipment.rightHand.transform.position = GetComponentInChildren<RightHand>().transform.position;
            equipment.rightHand.transform.rotation = GetComponentInChildren<RightHand>().transform.rotation;
            equipment.rightHand.gameObject.SetActive(true);
            equipment.rightHand.gameObject.rigidbody.isKinematic = true;
            equipment.PassAttributes();
            inventory.RemoveAt(id);
            openContextMenu = false;
        }
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
        if (GUI.Button(new Rect(0, 0, Screen.width, Screen.height),"","InvisibleButton"))
        {
            openContextMenu = false;
        }
        GUI.depth = 0;
    }

	void Start () {
        equipment = GetComponent<Equipment>();
	}

	void Update () {
		if(Input.GetKeyDown(KeyCode.I) && networkView.isMine){
			openInv = !openInv;
		}
		getItemsRaycast();
	}
	
    void getItemsRaycast(){
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetKeyDown(KeyCode.E) && networkView.isMine)
        {
            RaycastHit hit;
            //Debug.DrawRay(ray.origin, ray.direction , Color.white);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "Item")
                {
                    inventory.Add(hit.collider.gameObject);
                    hit.collider.gameObject.SetActive(false);
                }
                else
                {
                    Debug.Log("Hit: " + hit.collider.gameObject.name);
                }
            }
        }
	}
}

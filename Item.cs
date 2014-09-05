using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
	public string ItemName;
	public string ItemType;
    public string ItemClass;
    public int Damage;

    public int Strength;
    public int Vitality;
    public int Dexterity;
    public int Agility;
    public int Wisdom;
    public int Intelligence;

    public string tooltip = "";
	public bool MouseOver = false;
    private GUIStyle itemStyle = new GUIStyle();
    public GUISkin itemSkin;

	public string tooltipString(){
        string String = "";
        if (ItemType == "Normal")
        {
            String += "<color=grey>" + ItemName + "\n" + "Rarity:" + ItemType + "</color>";
        }
        if (ItemType == "Rare")
        {
            String += "<color=olive>" + ItemName + "\n" + "Rarity:" + ItemType + "</color>";
        }
        if (Strength != 0 || Vitality != 0 || Dexterity != 0 || Agility != 0 || Wisdom != 0 || Intelligence != 0)
        {
            String += "<color=lightblue>\nStats:</color>";
        }
        if (Strength != 0)
        {
            String += "<color=lime>\nStrength : " + Strength + "</color>";
        }
        if (Vitality != 0)
        {
            String += "<color=lime>\nVitality : " + Vitality + "</color>";
        }
        if (Dexterity != 0)
        {
            String += "<color=lime>\nDexterity : " + Dexterity + "</color>";
        }
        if (Agility != 0)
        {
            String += "<color=lime>\nAgility : " + Agility + "</color>";
        }
        if (Wisdom != 0)
        {
            String += "<color=lime>\nWisdom : " + Wisdom + "</color>";
        }
        if (Intelligence != 0)
        {
            String += "<color=lime>\nIntelligence : " + Intelligence + "</color>";
        }
		return String;
	}

	void OnGUI(){
        GUI.skin = itemSkin;
		if(MouseOver == true)
        {
            Vector2 BoxSize = itemStyle.CalcSize(new GUIContent(tooltip));
            GUI.Label(new Rect(Input.mousePosition.x + 5, Screen.height - Input.mousePosition.y, BoxSize.x+10,BoxSize.y+10), tooltip);
		} 
	}

	void OnMouseDown(){

	}
	void Start () {
        tooltip = tooltipString();
        networkView.observed = GetComponent<Item>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.I))
        {
            MouseOver = false;
        }
        
	}
	void OnMouseEnter(){
		MouseOver = true;
	}
	void OnMouseExit(){
		MouseOver = false;
	}

    void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    {
        bool isKinematic = gameObject.rigidbody.isKinematic;
        bool isActive = gameObject.activeSelf;
        Vector3 syncPosition = Vector3.zero;
        Vector3 syncParentPosition = Vector3.zero;
        Quaternion syncParentRotation = Quaternion.identity;
        Quaternion syncRotation = Quaternion.identity;
        if (stream.isWriting)
        {
            syncPosition = transform.position;
            stream.Serialize(ref syncPosition);

            syncRotation = transform.rotation;
            stream.Serialize(ref syncRotation);
            
            isActive = gameObject.activeSelf;
            stream.Serialize(ref isActive);

            isKinematic = rigidbody.isKinematic;
            stream.Serialize(ref isKinematic);
            if (transform.parent != null)
            {
                syncParentPosition = transform.parent.position;
                stream.Serialize(ref syncParentPosition);
                syncParentRotation = transform.parent.rotation;
                stream.Serialize(ref syncParentRotation);
            }
        }
        else
        {
            stream.Serialize(ref syncPosition);
            transform.position = syncPosition;

            stream.Serialize(ref syncRotation);
            transform.rotation = syncRotation;

            stream.Serialize(ref isActive);
            gameObject.SetActive(isActive);

            stream.Serialize(ref isKinematic);
            rigidbody.isKinematic = isKinematic;

            if (transform.parent != null)
            {
                stream.Serialize(ref syncParentPosition);
                transform.parent.position = syncParentPosition;
                stream.Serialize(ref syncParentRotation);
                transform.parent.rotation = syncParentRotation;
                
            }
        }
    }
}

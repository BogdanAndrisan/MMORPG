using UnityEngine;
using System.Collections;

public class Equipment : MonoBehaviour {

    public Inventory inventory;
    public Attributes attributes;
    public GameObject rightHand;
    public bool rightHandEquiped;

    private Rect charWindowRect = new Rect(50, 50, 250, 400);
    private bool charWindowOpen = false;
    void OnGUI()
    {
        if (charWindowOpen == true)
        {
            charWindowRect = GUI.Window(2, charWindowRect, OpenCharacterWindow, "Character");
        }
    }

    public void PassAttributes()
    {
        if (rightHand != null)
        {
            attributes.Strength += rightHand.GetComponent<Item>().Strength;
            attributes.Vitality += rightHand.GetComponent<Item>().Vitality;
            attributes.Dexterity += rightHand.GetComponent<Item>().Dexterity;
            attributes.Agility += rightHand.GetComponent<Item>().Agility;
            attributes.Wisdom += rightHand.GetComponent<Item>().Wisdom;
            attributes.Intelligence += rightHand.GetComponent<Item>().Intelligence;
            attributes.Recalculate();
        }
    }
    public void RemoveAttributes()
    {
        if (rightHand != null)
        {
            attributes.Strength -= rightHand.GetComponent<Item>().Strength;
            attributes.Vitality -= rightHand.GetComponent<Item>().Vitality;
            attributes.Dexterity -= rightHand.GetComponent<Item>().Dexterity;
            attributes.Agility -= rightHand.GetComponent<Item>().Agility;
            attributes.Wisdom -= rightHand.GetComponent<Item>().Wisdom;
            attributes.Intelligence -= rightHand.GetComponent<Item>().Intelligence;
            attributes.Recalculate();
        }
    }
    void OpenCharacterWindow(int id)
    {
        //========= Right Hand ===============
        if (rightHandEquiped == true)
        {
            if (GUI.Button(new Rect (5,20,50,50),"RightEQ"))
            {
                inventory.inventory.Add(rightHand);
                rightHandEquiped = false;
                rightHand.gameObject.SetActive(false);
                RemoveAttributes();
                rightHand = null;
            }
        }else{
            GUI.Box(new Rect (5,20,50,50),"Right");
        }

        //========== Stregth box ===============
        GUI.Box(new Rect(80, 20, 100, 20), "Strength : " + attributes.Strength.ToString());
        if (GUI.Button(new Rect(185, 20, 20, 20), "+"))
        {
            if (attributes.StatPoints > 0)
            {
                attributes.Strength++;
                attributes.StatPoints--;
                attributes.Recalculate();
            }
        }
        if (GUI.Button(new Rect(210, 20, 20, 20), "-"))
        {
            if (attributes.StatPoints < attributes.Level * 10)
            {
                attributes.Strength--;
                attributes.StatPoints++;
                attributes.Recalculate();
            }
        }
        //========== Vitality box ===============
        GUI.Box(new Rect(80, 45, 100, 20), "Vitality : " + attributes.Vitality.ToString());
        if (GUI.Button(new Rect(185, 45, 20, 20), "+"))
        {
            if (attributes.StatPoints > 0)
            {
                attributes.Vitality++;
                attributes.StatPoints--;
                attributes.Recalculate();
            }
        }
        if (GUI.Button(new Rect(210, 45, 20, 20), "-"))
        {
            if (attributes.StatPoints < attributes.Level * 10)
            {
                attributes.Vitality--;
                attributes.StatPoints++;
                attributes.Recalculate();
            }
        }
        //========== Dexterity box ===============
        GUI.Box(new Rect(80, 70, 100, 20), "Dexterity : " + attributes.Dexterity.ToString());
        if (GUI.Button(new Rect(185, 70, 20, 20), "+"))
        {
            if (attributes.StatPoints > 0)
            {
                attributes.Dexterity++;
                attributes.StatPoints--;
                attributes.Recalculate();
            }
        }
        if (GUI.Button(new Rect(210, 70, 20, 20), "-"))
        {
            if (attributes.StatPoints < attributes.Level * 10)
            {
                attributes.Dexterity--;
                attributes.StatPoints++;
                attributes.Recalculate();
            }
        }
        //========== Agility box ===============
        GUI.Box(new Rect(80, 95, 100, 20), "Agility : " + attributes.Agility.ToString());
        if (GUI.Button(new Rect(185, 95, 20, 20), "+"))
        {
            if (attributes.StatPoints > 0)
            {
                attributes.Agility++;
                attributes.StatPoints--;
                attributes.Recalculate();
            }
        }
        if (GUI.Button(new Rect(210, 95, 20, 20), "-"))
        {
            if (attributes.StatPoints < attributes.Level * 10)
            {
                attributes.Agility--;
                attributes.StatPoints++;
                attributes.Recalculate();
            }
        }
        //========== Wisdom box ===============
        GUI.Box(new Rect(80, 120, 100, 20), "Wisdom : " + attributes.Wisdom.ToString());
        if (GUI.Button(new Rect(185, 120, 20, 20), "+"))
        {
            if (attributes.StatPoints > 0)
            {
                attributes.Wisdom++;
                attributes.StatPoints--;
                attributes.Recalculate();
            }
        }
        if (GUI.Button(new Rect(210, 120, 20, 20), "-"))
        {
            if (attributes.StatPoints < attributes.Level * 10)
            {
                attributes.Wisdom--;
                attributes.StatPoints++;
                attributes.Recalculate();
            }
        }
        //========== Intelligence box ===============
        GUI.Box(new Rect(80, 145, 100, 20), "Intelligence : " + attributes.Intelligence.ToString());
        if (GUI.Button(new Rect(185, 145, 20, 20), "+"))
        {
            if (attributes.StatPoints > 0)
            {
                attributes.Intelligence++;
                attributes.StatPoints--;
                attributes.Recalculate();
            }
        }
        if (GUI.Button(new Rect(210, 145, 20, 20), "-"))
        {
            if (attributes.StatPoints < attributes.Level * 10)
            {
                attributes.Intelligence--;
                attributes.StatPoints++;
                attributes.Recalculate();
            }
        }
        //========== Exp box ===============
        GUI.Box(new Rect(80, 170, 100, 20), "Exp : " + attributes.Experience.ToString());
        if (GUI.Button(new Rect(185, 170, 20, 20), "+"))
        {
            attributes.Experience+=10;
            attributes.Recalculate();
        }
        if (GUI.Button(new Rect(210, 170, 20, 20), "-"))
        {
            attributes.Experience -= 10;
            attributes.Recalculate();
        }
        //========== Stat Points box ===============
        GUI.Box(new Rect(80, 195, 100, 20), "Stat Points : " + attributes.StatPoints.ToString());

        GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
    }
	void Start () {
        inventory = GetComponent<Inventory>();
        attributes = GetComponent<Attributes>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.C) && networkView.isMine)
        {
            charWindowOpen = !charWindowOpen;
        }
	}
}

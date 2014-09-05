using UnityEngine;
using System.Collections;

public class Attributes : MonoBehaviour {

    public int HitPoints;
    public int MaxHitPoints;
    public int ManaPoints;
    public int MaxManaPoints;

    public int Level = 1;
    public int Experience;
    public int StatPoints = 10;

    public int Damage;

    public int Strength;
    public int Vitality;
    public int Dexterity;
    public int Agility;
    public int Wisdom;
    public int Intelligence;

    public float delay = 1f;
    public void GetItemAttributes(GameObject Item){
        
    }

    public void Recalculate()
    {
        CalcMaxHitPoints();
        CalcMaxManaPoints();
    }
   public void CalcMaxHitPoints()
    {
        MaxHitPoints = 50 + Strength * 3 + Vitality * 10 + Agility * 2;
    }
    public void CalcMaxManaPoints()
    {
        MaxManaPoints = 50 + Intelligence * 4 + Wisdom * 10;
    }
	void Start () {
        Recalculate();
        HitPoints = MaxHitPoints;
        ManaPoints = MaxManaPoints;
	}
	
	// Update is called once per frame
	void Update () {
        if (Experience >= Level * 100)
        {
            Experience = Experience - Level * 100;
            Level++;
            StatPoints += 10;
            Recalculate();
            HitPoints = MaxHitPoints;
            ManaPoints = MaxManaPoints;
        }
        delay -= Time.deltaTime;
        if (delay <= 0)
        {
            Regeneration();
            delay = 1f;
        }
	}
    void Regeneration() {
        if (HitPoints < MaxHitPoints)
        {
            HitPoints++;
        }
        if (ManaPoints < MaxManaPoints)
        {
            ManaPoints++;
        }
    }
}

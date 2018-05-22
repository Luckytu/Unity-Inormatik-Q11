using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public Card[] cards;

    private int level;
    private int experiencePoints;
    private int healthPoints;
    public int actionPoints;
    private int cardPoints;
    public float maxHeight;

	// Use this for initialization
	void Start ()
    {
        cards = new Card[6];

        gameObject.AddComponent<Armour>();
        gameObject.AddComponent<Gun>();

        cards = gameObject.GetComponents<Card>();

        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].setUnit(this);

            if (cards[i].isPassive())
            {
                cards[i].passiveBehaviour();
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void updateActionPoints(int actionPoints)
    {
        this.actionPoints += actionPoints; 
    }

    public void addHealthPoints(int healthPoints) { this.healthPoints += healthPoints; }

    public int getActionPoints() { return actionPoints; }
    public float getMaxHeight() { return maxHeight; }
}

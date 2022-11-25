using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallStats : MonoBehaviour
{
	public static int Rounds;
	public GameObject roundsText;

	public static WallStats instance;

	void Start ()
	{
		instance = this;
		Rounds = 0;
	}

	private void Update() {
		roundsText.GetComponent<Text>().text = "Fase"+Rounds;

		if(Rounds > 5){
			GameController.instance.WinGame();
		}
	}

	public void ChangeRound(){
        Rounds ++;
        roundsText.GetComponent<Animator>().SetBool("Active", true);
    }
}

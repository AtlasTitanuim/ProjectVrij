﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextLevel : MonoBehaviour {
	public int LevelInteger;
	private GameObject player;
	private GameObject monster;
	public GameObject textObject;


	void Update(){
		if(monster != null && player != null){
			textObject.SetActive(true);
			Debug.Log("there's a player and Monster");
			if(Input.GetAxis("Vertical") > 0.9f){
				Application.LoadLevel(LevelInteger);
			}
		} else {
			textObject.SetActive(false);
		}
	}
	void OnTriggerStay2D(Collider2D other){
		if(other.tag == "Player"){
			player = other.transform.gameObject;
		}
		if(other.tag == "Monster"){
			monster = other.transform.gameObject;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.tag == "Player"){
			player = null;
		}
		if(other.tag == "Monster"){
			monster = null;
		}
	}
}

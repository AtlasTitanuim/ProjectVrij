﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupObject : MonoBehaviour{
	public bool picked = false;
	public GameObject Object;
	private Color OriginalColor;
	public MouseText mouse;
	public Vector3 size;
	void Update(){
		//Debug.Log(MousePlace);
		if(Object != null){
			//Debug.Log(TextObject.GetComponent<Text>().text);
			if(!picked){
				mouse.DisplayText("Click E to pick up");
				if(Input.GetButtonDown("Fire1")){
					mouse.DisplayText("");
					if(Object.transform.parent != null){
						if(Object.transform.parent.tag == "UsedGum"){
							Object.transform.parent.tag = "Gum";
							Object.transform.parent.gameObject.layer = 11;
						}
					}
					//Debug.Log("pickup");
					if(Object.GetComponent<stickToGum>() == true){
						Object.GetComponent<stickToGum>().enabled = true;
					}
					Object.transform.parent = this.transform;
					Object.transform.localScale = size*3.5f;
					//Object.transform.position = this.transform.position;
					//Object.transform.rotation = this.transform.rotation;
					Object.GetComponent<Rigidbody2D>().gravityScale = 0;
					Object.GetComponent<Collider2D>().isTrigger = true;
					Object.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
					Object.transform.localPosition = new Vector3(0,0,0);
					picked = true;
				}
			} else {
				Object.transform.position = transform.position;
				if(Input.GetButtonDown("Fire1")){
					//Debug.Log("drop");
					Object.transform.parent = null;
					Object.GetComponent<Rigidbody2D>().gravityScale = 3;
					Object.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
					Object.GetComponent<Collider2D>().isTrigger = false;
					Object.transform.localScale = size;
					Object = null;
					picked = false;
				}
			}
		}
	}
	void OnTriggerStay2D(Collider2D other){
		if(Object == null){
			if(other.transform.tag == "Pickup"){
				Object = other.gameObject;
				Object.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.02f);
			}
		}
		if(other.transform.tag == "Crate"){
			mouse.DisplayText("Looks too heavy to pickup...");
		}
		if(other.transform.tag == "Pushable"){
			mouse.DisplayText("Press mouse 1 to push");
			if(Input.GetButtonDown("Fire3")){
				Debug.Log("Yeet");
				other.transform.GetComponent<Pushable>().ChangeColliders();
			}
		}
		if(other.GetComponent<LockThePainting>()){
			if(other.GetComponent<LockThePainting>().locked){
				mouse.DisplayText("The painting is perfectly blocking the rain");
			}
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.transform.tag == "Pickup"){
			if(Object != null){
				Object.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1f);
				mouse.DisplayText("");
				Object = null;
			}
		}
		if(other.transform.tag == "Crate"){
			mouse.DisplayText("");
		}
		if(other.transform.tag == "Pushable"){
			mouse.DisplayText("");
		}
		if(other.GetComponent<LockThePainting>()){
			mouse.DisplayText("");
		}
	}
}

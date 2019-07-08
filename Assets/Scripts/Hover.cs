using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hover : MonoBehaviour {

   public float floatLevel = 4.0F;
   public float floatWeight = 2.0F;
   public float bounceDamp = 0.05F;
   public Vector3 buoyancyCenterOffset;

   private float forceFactor;
   private Vector3 actionPoint;
   private Vector3 upLift;

   private Rigidbody thisRigidbody;

   void Start(){

   		thisRigidbody = this.GetComponent<Rigidbody>();

   		buoyancyCenterOffset = new Vector3 (Random.Range(0,2),Random.Range(0,2),Random.Range(0,2));

		transform.rotation = Random.rotation;

		//randomize things a little
		floatLevel = Random.Range(floatLevel, (floatLevel + 0.2F));
		floatWeight = Random.Range(floatWeight, (floatWeight + 0.1F));
		bounceDamp = Random.Range(bounceDamp, (bounceDamp + 0.1F));
   }

   void Update(){

   		//actionPoint = transform.position + transform.TransformDirection(buoyancyCenterOffset);
   		actionPoint = transform.position;

   		forceFactor = floatLevel - ((actionPoint.y - floatLevel) / floatWeight);

   		forceFactor = Random.Range(forceFactor, (forceFactor + 1F));

   		if(forceFactor > 0F){

   			upLift = -Physics.gravity * (forceFactor - thisRigidbody.velocity.y * bounceDamp);

   			thisRigidbody.AddForceAtPosition(upLift, actionPoint);

   		}
		
		float speed = 10;
   		GetComponent<Rigidbody>().AddRelativeForce(Random.onUnitSphere * speed);
   }
}
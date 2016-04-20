using UnityEngine;
using System.Collections;

public class Wave {

	Vector3 startPos;
	Vector3 currentPos;

	public Wave(){
		//startPos = Mathf.Rand
		//Random.Range(-10.0F, 10.0F)
	}
	

	public void move () {
	
	}
}


/*
 * 
 * Class createwave:
  Direction = vector3( 0-45, 0, 0-45)
  MaxCycles: 5.
  Startpos = vector3()
  NumOfCycles = 0

Start():
 Pos = startPos

Update():
  Whilst cycles > max cycles:
    Movewave()

Movewave():
 Sine(direction)
 Or
Big sine wave(direction)
 If y-pos = startpos.y twice then cycle complete.
 
Destroy:
 Remove self from waves array
 Destroy self */
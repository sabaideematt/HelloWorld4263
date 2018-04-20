using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NumberManager : MonoBehaviour {
	public GameObject[] boxes = new GameObject[7];
	public int[] numbers = new int[7];

	// Use this for initialization
	void Start () {
		boxes[0] = GameObject.Find("platform_Bricks 1");
		boxes[1] = GameObject.Find("platform_Bricks 2");
		boxes[2] = GameObject.Find("platform_Bricks 3");
		boxes[3] = GameObject.Find("platform_Bricks 4");
		boxes[4] = GameObject.Find("platform_Bricks 5");
		boxes[5] = GameObject.Find("platform_Bricks 6");
		boxes[6] = GameObject.Find("platform_Bricks 7");

		numbers[0] = boxes[0].GetComponentInChildren<ButtonController> ().number;
		numbers[1] = boxes[1].GetComponentInChildren<ButtonController> ().number;
		numbers[2] = boxes[2].GetComponentInChildren<ButtonController> ().number;
		numbers[3] = boxes[3].GetComponentInChildren<ButtonController> ().number;
		numbers[4] = boxes[4].GetComponentInChildren<ButtonController> ().number;
		numbers[5] = boxes[5].GetComponentInChildren<ButtonController> ().number;
		numbers[6] = boxes[6].GetComponentInChildren<ButtonController> ().number;
	}

	// Update is called once per frame
	void Update () {
		numbers[0] = boxes[0].GetComponentInChildren<ButtonController> ().number;
		numbers[1] = boxes[1].GetComponentInChildren<ButtonController> ().number;
		numbers[2] = boxes[2].GetComponentInChildren<ButtonController> ().number;
		numbers[3] = boxes[3].GetComponentInChildren<ButtonController> ().number;
		numbers[4] = boxes[4].GetComponentInChildren<ButtonController> ().number;
		numbers[5] = boxes[5].GetComponentInChildren<ButtonController> ().number;
		numbers[6] = boxes[6].GetComponentInChildren<ButtonController> ().number;

		if (numbers [1] < numbers [3] && numbers [4] > numbers [3]) {
			if (numbers [0] < numbers [1] && numbers [2] > numbers [1]) {
				if (numbers [6] < numbers [4] && numbers [5] > numbers [4]) {
					//you win
				}
			}
		}
	}
}

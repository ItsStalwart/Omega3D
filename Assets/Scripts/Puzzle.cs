using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle {

	public List<int[,]> genPos;
	public List<Signal> genSig;

	public List<int[,]> recPos;
	public List<int> recTypes;
	public List<string> recDirections;
	public List<int> recLowerLimits;
	public List<int> recUpperLimits;
}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightFieldSim : RandomSim {

	protected float[][] velocites;
	public float dumpValue = 0.99f;
	public float powerAddMax = 5;
	public float iterAddPower = 3;

	public override void Init ()
	{
		velocites = new float[countX][];
		values = new float[countX][];

		for(int i = 0;i<countX;i++)
		{
			velocites[i] = new float[countY];
			values[i] = new float[countY];
			for(int j = 0;j<countY;j++)
				values[i][j] = velocites[i][j] = 0;
		}

		if(visualization != null)
			visualization.Init(countX,countY,AddValueAtPoint);
	}


	private void AddValueAtPoint(Point p)
	{
		values[p.x][p.y] = Mathf.Min(powerAddMax,values[p.x][p.y]+iterAddPower);
	}

	void Update()
	{
		for(int i = 0;i<countX;i++)
		{
			for(int j = 0;j<countY;j++)
			{
				velocites[i][j] += 
					(values[Mathf.Max(0,i - 1)][j] + 
					values[Mathf.Min(i + 1,countX-1)][j] + 
					values[i][Mathf.Max(0,j - 1)] +
					values[i][Mathf.Min(j + 1,countY-1)])/4 - values[i][j];
				
				velocites[i][j] *= dumpValue;
			}
		}

		for(int i = 0;i<countX;i++)
		{
			for(int j = 0;j<countY;j++)
			{
				values[i][j] += velocites[i][j];
			}
		}


		if(visualization != null)
			visualization.UpdateGrid(values);
	}

}
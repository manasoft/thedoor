﻿using UnityEngine;
using System.Collections;


public class AcSelectButtons : MonoBehaviour
{
	// ========================================================================== //
	// ========================================================================== //
	/*
	 * 
	 */
	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //
	/*
	 * 
	 */
	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// Find で探せばいい気もするけど・・・まぁイロイロと Unity の機能を試すという意味でこんな定義をしてみる
	/// </summary>
	public AcTitle manager;

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	public void onBegin()
	{
		manager.onSelectButtonsBegin();
	}

	public void onTurn()
	{
		manager.onSelectButtonsTurn();
	}

	public void onEnd()
	{
		manager.onSelectButtonsEnd();
	}

	//void OnButtonsOut2In()
	//{
	//	manager.OnButtonsOut2In();
	//}

	//void OnButtonIn2Out()
	//{
	//	manager.OnButtonsIn2Out();
	//}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //
}

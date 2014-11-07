﻿using UnityEngine;
using System.Collections;

// Serializable
//using System;
// 

//
//using System.Collections.Generic;
//using System.Object;
//using System.Collections;

/// <summary>
/// ランキングデータのシリアライズをするクラス
/// </summary>
[System.Serializable]
public class AcSave : Object
{
	// ========================================================================== //
	// ========================================================================== //
	/*
	 * 
	 */
	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //
	/*
	 * シリアライズ
	 * http://msdn.microsoft.com/ja-jp/library/system.serializableattribute(v=vs.90).aspx
	 * 
	 */
	/*
	 * ソート
	 * http://programmers.high-way.info/cs/list-sort.html
	 */
	/*
	 * パス
	 * http://qiita.com/bokkuri_orz/items/c37b2fd543458a189d4d
	 * http://hiiro-game.seesaa.net/article/272455420.html
	 * Application.dataPath
	 * Application.persistentDataPath
	 * Application.temporaryCachePath
	 * 
	 * スクリプトによって取得できるパス - Neareal
	 * Application.streamingAssetsPath
	 */
	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	/// <summary>
	/// サブディレクトリ名（m_vPath はここまでを保持する）
	/// </summary>
	private const string _SUBDIR = "/save";

	/// <summary>
	/// 保存するファイル名
	/// </summary>
	private const string _SAVEFILE = "/save.dat";

	/// <summary>
	/// ランキングはベストテン！
	/// </summary>
	private const int _RANKING = 10;

	// ========================================================================== //
	// ========================================================================== //


	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// シングルトン
	/// </summary>
	private static AcSave m_vInstance = null;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	/// <summary>
	/// 保存ディレクトリまでのパスを保持する
	/// </summary>
	private string m_vPath;

	/// <summary>
	/// 
	/// </summary>
	private _Data m_vSave;

	//private ArrayList m_vTimeAttackMode;
	//private ArrayList m_vChallengeMode;

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private void _ini()
	{
		m_vPath = null;
		//
		m_vSave = null;

		//m_vTimeAttackMode = new ArrayList();
		//m_vChallengeMode = new ArrayList();
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	/// <summary>
	/// コンストラクタ
	/// </summary>
	/// <param name="vPath"></param>
	public AcSave( string vPath )
	{
		_ini();
		//
		m_vPath = vPath;
		//
		m_vSave = ( _Data ) AcUtil.readObject( vPath + _SAVEFILE );
		//
		if ( m_vSave == null )
		{
			Debug.Log( "AcRanking ファイルが無いので new した" );

			m_vSave = new _Data();

			{
				/*
				 * デバッグ
				 */
				m_vSave.addTime( 99.99f );
				m_vSave.addTime( 98.0f );
				m_vSave.addTime( 97.0f );
				m_vSave.addTime( 96.0f );
				m_vSave.addTime( 95.0f );
				m_vSave.addTime( 94.0f );
				m_vSave.addTime( 93.0f );
				m_vSave.addTime( 92.0f );
				m_vSave.addTime( 91.0f );
				m_vSave.addTime( 90.0f );

				m_vSave.addDoor( 1 );
				m_vSave.addDoor( 2 );
				m_vSave.addDoor( 3 );
				m_vSave.addDoor( 4 );
				m_vSave.addDoor( 5 );
				m_vSave.addDoor( 6 );
				m_vSave.addDoor( 7 );
				m_vSave.addDoor( 8 );
				m_vSave.addDoor( 9 );
				m_vSave.addDoor( 10 );

				m_vSave.debugPrint();
			}

			m_vSave.save( vPath + _SAVEFILE );
		}
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	//	private static AcRanking Create( string vPath )
	//	{
	//		Debug.Log("AcRanking # Create()");

	//		string _path = "C:/Users/warrior/AppData/LocalLow/Manasoft/TheDoor" + _PATH;

	//		AcRanking _instance = null;
	//		//
	////		_instance = ( AcRanking ) AcUtil.readObject( Application.persistentDataPath );
	//		_instance = ( AcRanking ) AcUtil.readObject( _path );
	//		//
	//		if ( _instance == null )
	//		{
	//			Debug.Log( "AcRanking # Create() ファイルが無いので new した" );

	//			_instance = new AcRanking();

	//			_instance._addTime( 1 );
	//			_instance._addTime( 4 );
	//			_instance._addTime( 5 );
	//			_instance._addTime( 3 );
	//			_instance._addTime( 2 );

	//			_instance._addDoor( 10 );
	//			_instance._addDoor( 13 );
	//			_instance._addDoor( 12 );
	//			_instance._addDoor( 11 );
	//			_instance._addDoor( 14 );

	//		}
	//		//
	//		Debug.Log( "AcRanking # Create() 終了" );

	//		_instance._debugPrint();

	//		AcUtil.writeObject(_path, _instance);

	//		return ( _instance );
	//	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //



	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// ランキングの初期化（セーブデータが有れば読み込む）
	/// </summary>
	public static void Create()
	{
		if ( m_vInstance == null )
		{
			string _path = Application.persistentDataPath + _SUBDIR;
			//
			m_vInstance = new AcSave( _path );
		}
	}

	// ========================================================================== //
	// ========================================================================== //

	public static float[] getTimes()
	{
		ArrayList _arrayList = m_vInstance.m_vSave.m_vData_TimeAttack;

		float[] _times = new float[ _arrayList.Count ];
		//
		for ( int _count = 0; _count < _arrayList.Count; _count++ )
		{
			_times[ _count ] = ( ( _Data_TimeAttack ) _arrayList[ _count ] ).m_vTime;
		}
		//
		return ( _times );
	}

	public static int[] getDoors()
	{
		ArrayList _arrayList = m_vInstance.m_vSave.m_vData_Challenge;

		int[] _doors = new int[ _arrayList.Count ];
		//
		for ( int _count = 0; _count < _arrayList.Count; _count++ )
		{
			_doors[ _count ] = ( ( _Data_Challenge ) _arrayList[ _count ] ).m_vDoor;
		}
		//
		return ( _doors );
	}

	// ========================================================================== //
	// ========================================================================== //


	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	//private void _addTime( int vTime )
	//{
	//	m_vTimeAttackMode.Add( new _DataTimeAttackMode( vTime ) );
	//	/*
	//	 * http://programmers.high-way.info/cs/list-sort.html
	//	 */
	//	m_vTimeAttackMode.Sort( new _comparerTime() );
	//	//
	//	while ( m_vTimeAttackMode.Count > _RANKING )
	//	{
	//		Debug.Log( "Time 削除" );
	//		m_vTimeAttackMode.RemoveAt( _RANKING );
	//	}
	//	//
	//	_save();
	//}

	//private void _addDoor( int vDoor )
	//{
	//	m_vChallengeMode.Add( new _DataChallengeMode( vDoor ) );
	//	//
	//	m_vChallengeMode.Sort( new _comparerDoor() );
	//	//
	//	while ( m_vChallengeMode.Count > _RANKING )
	//	{
	//		Debug.Log( "Door 削除" );
	//		m_vChallengeMode.RemoveAt( _RANKING );
	//	}
	//	//
	//	_save();
	//}

	//private void _debugPrint()
	//{
	//	foreach ( _DataTimeAttackMode _data in m_vTimeAttackMode )
	//	{
	//		Debug.Log( "Time >> " + _data.m_vTime );
	//	}
	//	foreach ( _DataChallengeMode _data in m_vChallengeMode )
	//	{
	//		Debug.Log( "Door >> " + _data.m_vDoor );
	//	}
	//}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	//private static void _save()
	//{
	//	m_vInstance.m_vSave.save( m_vInstance.m_vPath );
	//}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	public static void addTime( float vTime )
	{
		m_vInstance.m_vSave.addTime( vTime );
		//
		m_vInstance.m_vSave.save( m_vInstance.m_vPath + _SAVEFILE );
	}

	public static void addDoor( int vDoor )
	{
		m_vInstance.m_vSave.addDoor( vDoor );
		//
		m_vInstance.m_vSave.save( m_vInstance.m_vPath + _SAVEFILE );
	}

	private static void debugPrint()
	{
		m_vInstance.m_vSave.debugPrint();
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	[System.Serializable]
	private class _Data
	{
		public ArrayList m_vData_TimeAttack;
		public ArrayList m_vData_Challenge;

		// -------------------------------------------------------------------------- //
		// -------------------------------------------------------------------------- //

		public _Data()
		{
			m_vData_TimeAttack = new ArrayList();
			m_vData_Challenge = new ArrayList();
		}

		// -------------------------------------------------------------------------- //
		// -------------------------------------------------------------------------- //

		private class _comparerTime : IComparer
		{
			// Calls CaseInsensitiveComparer.Compare with the parameters reversed.
			//int IComparer.Compare( _DataTimeAttackMode x, _DataTimeAttackMode y )
			//{
			//	return ( x.m_vTime - y.m_vTime );
			//}
			int IComparer.Compare( object vL, object vR )
			{
				_Data_TimeAttack _l = ( _Data_TimeAttack ) vL;
				_Data_TimeAttack _r = ( _Data_TimeAttack ) vR;
				//
				return ( _l.m_vTime.CompareTo( _r.m_vTime ) );
				//				return ( _l.m_vTime - _r.m_vTime );
			}
		}

		private class _comparerDoor : IComparer
		{
			// Calls CaseInsensitiveComparer.Compare with the parameters reversed.
			//int IComparer.Compare( _DataTimeAttackMode x, _DataTimeAttackMode y )
			//{
			//	return ( x.m_vTime - y.m_vTime );
			//}
			int IComparer.Compare( object vL, object vR )
			{
				_Data_Challenge _l = ( _Data_Challenge ) vL;
				_Data_Challenge _r = ( _Data_Challenge ) vR;
				//
				return ( _r.m_vDoor - _l.m_vDoor );
			}
		}

		// -------------------------------------------------------------------------- //
		// -------------------------------------------------------------------------- //

		public void save( string vPath )
		{
			AcUtil.writeObject( vPath, this );
		}

		public void addTime( float vTime )
		{
			m_vData_TimeAttack.Add( new _Data_TimeAttack( vTime ) );
			/*
			 * http://programmers.high-way.info/cs/list-sort.html
			 */
			m_vData_TimeAttack.Sort( new _comparerTime() );
			//
			while ( m_vData_TimeAttack.Count > _RANKING )
			{
				Debug.Log( "Time 削除" );
				m_vData_TimeAttack.RemoveAt( _RANKING );
			}
		}

		public void addDoor( int vDoor )
		{
			m_vData_Challenge.Add( new _Data_Challenge( vDoor ) );
			//
			m_vData_Challenge.Sort( new _comparerDoor() );
			//
			while ( m_vData_Challenge.Count > _RANKING )
			{
				Debug.Log( "Door 削除" );
				m_vData_Challenge.RemoveAt( _RANKING );
			}
		}

		public void debugPrint()
		{
			foreach ( _Data_TimeAttack _data in m_vData_TimeAttack )
			{
				Debug.Log( "Time >> " + _data.m_vTime );
			}
			foreach ( _Data_Challenge _data in m_vData_Challenge )
			{
				Debug.Log( "Door >> " + _data.m_vDoor );
			}
		}
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	[System.Serializable]
	private struct _Data_TimeAttack
	{
		public float m_vTime;

		public _Data_TimeAttack( float vTime )
		{
			m_vTime = vTime;
		}
	}

	[System.Serializable]
	private struct _Data_Challenge
	{
		public int m_vDoor;

		public _Data_Challenge( int vDoor )
		{
			m_vDoor = vDoor;
		}
	}

	// ========================================================================== //
	// ========================================================================== //
}

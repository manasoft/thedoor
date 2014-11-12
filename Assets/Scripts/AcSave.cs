using UnityEngine;
using System.Collections;

// Serializable
//using System;
// 

// List<>
using System.Collections.Generic;

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

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	/// <summary>
	/// 
	/// </summary>
	/// <param name="vString"></param>
	private void _debugLog( string vString )
	{
		AcDebug.debugLog( this.GetType().FullName + " # " + vString );
	}

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

	public int m_vRank_Time;
	public int m_vRank_Door;

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private void _ini()
	{
		m_vPath = null;
		//
		m_vSave = null;
		//
		m_vRank_Time = -1;
		m_vRank_Door = -1;
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
			//Debug.Log( "AcRanking ファイルが無いので new した" );

			m_vSave = new _Data();

			// 初期値
			float[] _timeTbl =
			{
				90.00f,
				91.00f,
				92.00f,
				93.00f,
				94.00f,
				95.00f,
				96.00f,
				97.00f,
				98.00f,
				99.00f,
			};
			//
			int[] _doorTbl =
			{
				10,
				9,
				8,
				7,
				6,
				5,
				4,
				3,
				2,
				1,
			};
			//
			foreach ( float _time in _timeTbl )
			{
				m_vSave.addTime( _time );
			}
			foreach ( int _door in _doorTbl )
			{
				m_vSave.addDoor( _door );
			}
			//
			m_vSave.save( vPath + _SAVEFILE );
			//
			//m_vSave.debugPrint();
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

	/// <summary>
	/// ランキングを配列として取得する（タイムアタックモード用の時間）
	/// </summary>
	/// <returns></returns>
	public static float[] getTimes()
	{
		//ArrayList _arrayList = m_vInstance.m_vSave.m_vData_TimeAttack;
		List<_Data_Time> _list = m_vInstance.m_vSave.m_vData_Time;

		float[] _times = new float[ _list.Count ];
		//
		for ( int _count = 0; _count < _list.Count; _count++ )
		{
			_times[ _count ] = ( ( _Data_Time ) _list[ _count ] ).m_vTime;
		}
		//
		return ( _times );
	}

	/// <summary>
	/// ランキングを配列として取得する（チャレンジモード用のドアの枚数）
	/// </summary>
	/// <returns></returns>
	public static int[] getDoors()
	{
		//ArrayList _arrayList = m_vInstance.m_vSave.m_vData_Challenge;
		List<_Data_Door> _list = m_vInstance.m_vSave.m_vData_Door;

		int[] _doors = new int[ _list.Count ];
		//
		for ( int _count = 0; _count < _list.Count; _count++ )
		{
			_doors[ _count ] = ( ( _Data_Door ) _list[ _count ] ).m_vDoor;
		}
		//
		return ( _doors );
	}

	/// <summary>
	/// ランキングの順位（0~9）
	/// </summary>
	/// <returns></returns>
	public static int getTimesRank()
	{
		//return ( m_vInstance.m_vSave.m_vRank_Time );
		return ( m_vInstance.m_vRank_Time );
	}

	/// <summary>
	/// ランキングの順位（0~9）
	/// </summary>
	/// <returns></returns>
	public static int getDoorsRank()
	{
		//return ( m_vInstance.m_vSave.m_vRank_Door );
		return ( m_vInstance.m_vRank_Door );
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
		m_vInstance.m_vRank_Time = m_vInstance.m_vSave.addTime( vTime );
		//
		m_vInstance.m_vSave.save( m_vInstance.m_vPath + _SAVEFILE );
	}

	public static void addDoor( int vDoor )
	{
		m_vInstance.m_vRank_Door = m_vInstance.m_vSave.addDoor( vDoor );
		//
		m_vInstance.m_vSave.save( m_vInstance.m_vPath + _SAVEFILE );
	}

	public static void missTime()
	{
		m_vInstance.m_vRank_Time = -1;
	}

	public static void missDoor()
	{
		m_vInstance.m_vRank_Door = -1;
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
		//public ArrayList m_vData_TimeAttack;
		//public ArrayList m_vData_Challenge;
		public List<_Data_Time> m_vData_Time;
		public List<_Data_Door> m_vData_Door;

		//public int m_vRank_Time;
		//public int m_vRank_Door;

		// -------------------------------------------------------------------------- //
		// -------------------------------------------------------------------------- //

		public _Data()
		{
			//m_vData_TimeAttack = new ArrayList();
			//m_vData_Challenge = new ArrayList();
			m_vData_Time = new List<_Data_Time>();
			m_vData_Door = new List<_Data_Door>();
			//
			//m_vRank_Time = -1;
			//m_vRank_Door = -1;
		}

		// -------------------------------------------------------------------------- //
		// -------------------------------------------------------------------------- //

		/*
		 * http://programmers.high-way.info/cs/list-sort.html
		 */

		private class _comparerTime : IComparer<_Data_Time>
		{
			// Calls CaseInsensitiveComparer.Compare with the parameters reversed.
			//int IComparer.Compare( _DataTimeAttackMode x, _DataTimeAttackMode y )
			//{
			//	return ( x.m_vTime - y.m_vTime );
			//}
			public int Compare( _Data_Time vL, _Data_Time vR )
			{
				_Data_Time _l = ( _Data_Time ) vL;
				_Data_Time _r = ( _Data_Time ) vR;
				//
				return ( _l.m_vTime.CompareTo( _r.m_vTime ) );
				//				return ( _l.m_vTime - _r.m_vTime );
			}
		}

		private class _comparerDoor : IComparer<_Data_Door>
		{
			// Calls CaseInsensitiveComparer.Compare with the parameters reversed.
			//int IComparer.Compare( _DataTimeAttackMode x, _DataTimeAttackMode y )
			//{
			//	return ( x.m_vTime - y.m_vTime );
			//}
			public int Compare( _Data_Door vL, _Data_Door vR )
			{
				_Data_Door _l = ( _Data_Door ) vL;
				_Data_Door _r = ( _Data_Door ) vR;
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

		public int addTime( float vTime )
		{
			m_vData_Time.Add( new _Data_Time( vTime ) );
			m_vData_Time.Sort( new _comparerTime() );
			//
			while ( m_vData_Time.Count > _RANKING )
			{
				m_vData_Time.RemoveAt( _RANKING );
			}
			//
			for ( int _rank = 0; _rank < m_vData_Time.Count; _rank++ )
			{
				_Data_Time _data = m_vData_Time[ _rank ];
				//
				if ( vTime == _data.m_vTime )
				{
					return ( _rank );
				}
			}
			//
			return ( -1 );
		}

		public int addDoor( int vDoor )
		{
			m_vData_Door.Add( new _Data_Door( vDoor ) );
			m_vData_Door.Sort( new _comparerDoor() );
			//
			while ( m_vData_Door.Count > _RANKING )
			{
				m_vData_Door.RemoveAt( _RANKING );
			}
			//
			for ( int _rank = 0; _rank < m_vData_Door.Count; _rank++ )
			{
				_Data_Door _data = m_vData_Door[ _rank ];
				//
				if ( vDoor == _data.m_vDoor )
				{
					return ( _rank );
				}
			}
			//
			return ( -1 );
		}

		//public void missTime()
		//{
		//	m_vRank_Time = -1;
		//}

		//public void missDoor()
		//{
		//	m_vRank_Door = -1;
		//}

		/// <summary>
		/// デバッグ用のプリント表示
		/// </summary>
		public void debugPrint()
		{
			foreach ( _Data_Time _data in m_vData_Time )
			{
				AcDebug.debugLog( "Time >> " + _data.m_vTime );
			}
			foreach ( _Data_Door _data in m_vData_Door )
			{
				AcDebug.debugLog( "Door >> " + _data.m_vDoor );
			}
		}
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// タイムアタックモード用のデータを保持するクラス（時間を保持する）
	/// </summary>
	[System.Serializable]
	private struct _Data_Time
	{
		public float m_vTime;

		public _Data_Time( float vTime )
		{
			m_vTime = vTime;
		}
	}

	/// <summary>
	/// チャレンジモード用のデータを保持するクラス（ドアの枚数を保持する）
	/// </summary>
	[System.Serializable]
	private struct _Data_Door
	{
		public int m_vDoor;

		public _Data_Door( int vDoor )
		{
			m_vDoor = vDoor;
		}
	}

	// ========================================================================== //
	// ========================================================================== //
}

using UnityEngine;
using System.Collections;

// List<>
using System.Collections.Generic;

/// <summary>
/// ScLog になる予定です
/// </summary>
public class AcLog : MonoBehaviour
{
	// ========================================================================== //
	// ========================================================================== //
	/*
	 * 
	 */
	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //
	/*
	 * Unity3D 画面上に表示出来るログ機能（レベル付き）改修版 - Qiita
	 * http://qiita.com/dk4kd/items/24dd0f4c32a170a0ec12
	 * 
	 * Unity iOSやAndroid端末向けにフリック対応をする
	 * http://www.ko-tan.com/article/377752654.html
	 * 
	 * Unity3D - Debug.Logを開発環境以外で無効化 - Qiita
	 * http://qiita.com/rodostw/items/39183e62ed2a1f52f690
	 * デバッグモード判定の仕方
	 * UnityEngine.Debug.isDebugBuild
	 * 
	 */
	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private enum _Level
	{
		Verbose,
		Debug,
		Information,
		Warning,
		Error,
	}

	// ========================================================================== //
	// ========================================================================== //

	private List<AcLog._Data> m_vList;

	private bool m_bLastPos;

	private Vector2 m_vDeltaPosition;

	private Vector2 scrollViewVector = Vector2.zero;

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	public static AcLog Create()
	{
		GameObject _object = new GameObject();
		//
		AcLog _class = ( AcLog ) _object.AddComponent( ( typeof( AcLog ) ) );
		//
		_object.name = _class.GetType().FullName;
		//
		_class._create();
		//
		return ( _class );
	}
	
	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private void _create()
	{
		m_vList = new List<_Data>();

		m_bLastPos = false;
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //


	private void _onGui()
	{
		int _item_num = m_vList.Count;
		float _item_height = 24.0f;

		Rect scrollViewRect = new Rect( 0, 0, Screen.width, Screen.height );
		Rect scrollViewAllRect = new Rect( 0, 0, Screen.width * 0.8f, _item_num * _item_height );


		float _limit_y = Mathf.Max(0, scrollViewAllRect.height - scrollViewRect.height);


		//scrollViewVector.y += m_vDeltaPosition.y;

		scrollViewVector.y = Mathf.Clamp( scrollViewVector.y + m_vDeltaPosition.y, 0, _limit_y );

		m_vDeltaPosition.y = 0;

		if(m_bLastPos )
		{
			m_bLastPos = false;

			//scrollViewVector.y = scrollViewAllRect.height - scrollViewRect.height;
			//if(scrollViewVector.y < 0)
			//{
			//	scrollViewVector.y = 0;
			//}

			scrollViewVector.y = _limit_y;

			Debug.Log( ">>>>>>>>>>>" + scrollViewVector.y.ToString() );

		}

				// スクロールビューの開始位置を作成する

		scrollViewVector = GUI.BeginScrollView( scrollViewRect, scrollViewVector, scrollViewAllRect );
//		scrollViewVector = GUILayout.BeginScrollView()

		// ボックスを作成

		//GUI.Box( new Rect( 50, 50, 200, 20 ), "Please select" );

		//foreach ( _Data _data in m_vList )
		for ( int _count = 0; _count < _item_num; _count++ )
		{
			GUIStyle _style = new GUIStyle();
			_style.fontSize = ( int ) _item_height - 1;
			_style.normal.textColor = Color.red;

			//GUILayout.Label( _data.m_vMessage, new GUIStyle() );
			GUI.Label(
				new Rect( 0, _item_height * _count, 100, _item_height ),
				new GUIContent(
					m_vList[ _count ].m_vDateTime.ToString( @"MM/dd HH:mm:ss | " ) +
					m_vList[ _count ].m_vMessage ),
				_style
			);
		}

		//d.TimeStamp.ToString("MM/dd HH:mm:ss \t ") 

		//// Button

		//GUI.Button( new Rect( 50, 80, 100, 100 ), contents[ 0 ] );

		//GUI.Button( new Rect( 50, 180, 180, 100 ), contents[ 1 ] );

		//GUI.Button( new Rect( 50, 280, 180, 100 ), contents[ 2 ] );

		//GUI.Button( new Rect( 50, 380, 180, 100 ), contents[ 3 ] );

		//GUI.Button( new Rect( 50, 480, 180, 100 ), contents[ 4 ] );



		// スクロールビューの終了位置を作成する 

		//GUILayout.EndScrollView();
		GUI.EndScrollView();

	}

	private void _update()
	{
		if ( Input.GetMouseButtonDown( 0 ) )
		{
			V( "tab_2", "おされた！" );
		}

	
		if ( Input.touchCount > 0 )
		{
		//	V( "tab_2", "タッチされたよ" );

			if ( Input.GetTouch( 0 ).phase == TouchPhase.Moved )
			{
				//V( "tab_2", "フリック" );

				m_vDeltaPosition = Input.GetTouch( 0 ).deltaPosition;

				/*
				 * 座標は右上にプラスのようです
				 * 1,0 / 1,1
				 * 0,0 / 0,1
				 */
				//V( "tab_3", "移動量 > " + m_vDeltaPosition.ToString() );
			}
		}
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //


	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		_update();
	}

	void OnGUI()
	{
		_onGui();
	}

	// ========================================================================== //
	// ========================================================================== //

	private void _add( AcLog._Level vLevel, string vTag, string vMessage )
	{
		//m_vList.Add( new _Data( vLevel, vTag, vMessage ) );
		m_vList.Insert( 0, new _Data( vLevel, vTag, vMessage ) );
		//m_bLastPos = true;
	}

	public void V( string vTag, string vMessage )
	{
		_add( AcLog._Level.Verbose, vTag, vMessage );
	}
	
	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private class _Data
	{
		public AcLog._Level m_vLevel;
		public string m_vTag;
		public string m_vMessage;
		public System.DateTime m_vDateTime;

		public _Data( AcLog._Level vLevel, string vTag, string vString )
		{
			m_vLevel = vLevel;
			m_vTag = vTag;
			m_vMessage = vString;
			m_vDateTime = System.DateTime.Now;
		}
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	public static void test()
	{
		AcLog _log = AcLog.Create();

		for ( int _count = 0; _count < 50; _count++ )
		{
			_log.V( "tab_1", "msg_" + _count.ToString() );
		}

		//_log.V( "tab_1", "msg_1" );
		//_log.V( "tab_1", "msg_2" );
		//_log.V( "tab_1", "msg_3" );
		//_log.V( "tab_1", "msg_4" );
		//_log.V( "tab_1", "msg_5" );
	}
	// ========================================================================== //
	// ========================================================================== //
}

using UnityEngine;
using System.Collections;

/// <summary>
/// 動的に呼び出すオブジェクト用のスクリプトです
/// </summary>
public class AcRanking : MonoBehaviour
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

	/// <summary>
	/// トリガー（イベント発生時のトリガーです）
	/// </summary>
	public enum Trigger
	{
		OK,
	};

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private const int _OBJECT_CAMERA = 0;
	//private const int _OBJECT_TITLE = 1;
	private const int _OBJECT_BUTTON_OK = 1;
	private const int _OBJECT_NUM = 2;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	/// <summary>
	/// 2014/12/01
	/// 表示位置を少し下げろっていってきたので・・・
	/// 本来なら不要だが OnGuI とカーソルのコルーチンで参照するので「外」に出してある
	/// しかも AcGuiTime.onGui_Background() でも参照するので public という有り様
	/// 行き当たりばったりの対応で、スパゲッティコードになる典型・・・
	/// なんか 0 のままでいい感じなんだけどちょっとだけ下げとくかな
	/// </summary>
	public const float OFFSET_Y = 5.0f;

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private static readonly string[] _objectTbl =
	{
		"Camera",
		//"Title",
		"Ok",
	};

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	/// <summary>
	/// 
	/// </summary>
	/// <param name="vObjectId"></param>
	/// <returns></returns>
	private string _getObjectName( int vObjectId )
	{
		string[] _string = _objectTbl[ vObjectId ].Split( '/' );
		//
		return ( _string[ _string.Length - 1 ] );
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	/// <summary>
	/// 
	/// </summary>
	/// <param name="vString"></param>
	private void _debugLog( string vString )
	{
		ScDebug.debugLog( this.GetType().FullName + " # " + vString );
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// 親
	/// </summary>
	private AcGameManager m_vManager;

	/// <summary>
	/// 
	/// </summary>
	private AiRankingTrigger m_vTrigger;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private GameObject[] m_vGameObject;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	/// <summary>
	/// カーソルを出すか？
	/// </summary>
	private bool m_bCursorActive;

	/// <summary>
	/// 
	/// </summary>
	private IEnumerator m_vCursorIEnumerator;

	/// <summary>
	/// カーソルの位置＋大きさ
	/// </summary>
	private Rect m_vCursorRect;

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// プレハブからインスタンスするよ
	/// </summary>
	/// <param name="vManager"></param>
	/// <param name="vTrigger"></param>
	/// <returns></returns>
	public static AcRanking Create( AcGameManager vManager, AiRankingTrigger vTrigger )
	{
		GameObject _prefab = ( GameObject ) Resources.Load( "Prefabs/Ranking" );
		//
		GameObject _object = ( GameObject ) Instantiate( _prefab, AcApp.GuiPosition, Quaternion.identity );
		//
		AcRanking _class = _object.GetComponent<AcRanking>();
		//
		_class._create( vManager, vTrigger );
		//
		return ( _class );
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private void _create( AcGameManager vManager, AiRankingTrigger vTrigger )
	{
		_debugLog( "_create" );
		//
		m_vManager = vManager;
		m_vTrigger = vTrigger;
		//
		m_bCursorActive = false;
		m_vCursorRect = new Rect();
		m_vCursorIEnumerator = null;
		//
		_setObject();
		_setRender();
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private void _setObject()
	{
		m_vGameObject = new GameObject[ _OBJECT_NUM ];
		//
		for ( int count = 0; count < _OBJECT_NUM; count++ )
		{
			m_vGameObject[ count ] = this.transform.FindChild( _objectTbl[ count ] ).gameObject;
		}
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private void _setRender()
	{
		var _tbl = new[]
		{
			//new { _object = _OBJECT_TITLE,			_image = AcApp.IMAGE_TEX_L,		_index = ( AcApp.getGameMode() == AcApp.GAMEMODE_TIMEATTACK ) ? 9 : 8, },
			new { _object = _OBJECT_BUTTON_OK,		_image = AcApp.IMAGE_TEX_S,		_index = 4, },
		};
		//
		foreach ( var _var in _tbl )
		{
			AcApp.imageRender( m_vGameObject[ _var._object ].renderer, _var._image, _var._index );
		}
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	public void setActive( bool bSw, bool bCursor )
	{
		this.gameObject.SetActive( bSw );
		//
		m_bCursorActive = false;
		//
		if ( m_vCursorIEnumerator != null )
		{
			StopCoroutine( m_vCursorIEnumerator );
			m_vCursorIEnumerator = null;
		}
		//
		if ( bSw )
		{
			_setRender();
			//
			if ( bCursor )
			{
				int _rank = -1;
				//
				switch ( AcApp.getGameMode() )
				{
					case ( AcApp.GAMEMODE_TIMEATTACK ):
						_rank = AcSave.getTimesRank();
						break;
					//
					case ( AcApp.GAMEMODE_CHALLENGE ):
						_rank = AcSave.getDoorsRank();
						break;
				}
				//
				if ( _rank >= 0 )
				{
					m_bCursorActive = true;
					//
					m_vCursorIEnumerator = _coroutine( _rank );
					StartCoroutine( m_vCursorIEnumerator );
				}
			}
		}
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// 移動処理をコルーチンでやってみるよ
	/// </summary>
	/// <param name="vRank"></param>
	/// <returns></returns>
	private IEnumerator _coroutine( int vRank )
	{
		if ( vRank >= 0 )
		{
			float _margin = 20.0f;
			float _w = 320.0f;
			float _h = 39.0f + ( _margin * 2 ); // チョット大きめにする
			float _x = ( AcApp.SCREEN_W - _w ) / 2;
			//float _y = 71.0f - ( _margin ) + ( 55.0f * vRank );
			float _y = OFFSET_Y + 71.0f - ( _margin ) + ( 55.0f * vRank );
			//
			Vector2 _dst = new Vector2( _x, _y );
			Vector2 _src = _dst;
			//
			_src.y += Screen.height / ( ( float ) Screen.height / AcApp.SCREEN_H ); // スケールを逆算して掛けておくよ！

			//
			float _time;

			// 移動
			_time = 0.0f;
			//
			while ( _time < 1.0f )
			{
				Vector2 _vector = Vector2.Lerp( _src, _dst, _time );
				//
				m_vCursorRect.Set( _vector.x, _vector.y, _w, _h );
				//
				_time += Time.deltaTime;
				//
				yield return null;
			}
			//
			m_vCursorRect.Set( _dst.x, _dst.y, _w, _h );
		}
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private void _awake()
	{
		_debugLog( "_awake" );
	}

	private void _start()
	{
		_debugLog( "_start" );
	}

	private void _update()
	{
		if ( Input.GetMouseButtonDown( 0 ) )
		{
			Ray _ray = m_vGameObject[ _OBJECT_CAMERA ].camera.ScreenPointToRay( Input.mousePosition );
			RaycastHit _hit = new RaycastHit();
			//
			if ( Physics.Raycast( _ray, out _hit ) )
			{
				GameObject _object = _hit.collider.gameObject;
				//
				if ( ( _object.name ).CompareTo( _getObjectName( _OBJECT_BUTTON_OK ) ) == ( 0 ) )
				{
					m_vTrigger.onTrigger( Trigger.OK );
				}
			}
		}

		////		if ( Application.platform == RuntimePlatform.Android && Input.GetKey( KeyCode.Escape ) )
		//if ( Input.GetKey( KeyCode.Escape ) )
		//{
		//	//
		//}
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	void Awake()
	{
		_awake();
	}

	// Use this for initialization
	void Start()
	{
		_start();
	}

	// Update is called once per frame
	void Update()
	{
		_update();
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	/*
	 * メモ
	 * Sony Tablet S 
	 *		1280 x 800
	 *		aspect 1.6		← 仮想より縦が短い（幅が広い）
	 * 
	 *	仮想の実機
	 *		800 x 480
	 *		aspect 1.6666...
	 */

	void OnGUI()
	{
		const int _best_10 = 10;
		//
		switch ( AcApp.getGameMode() )
		{
			case ( AcApp.GAMEMODE_TIMEATTACK ):
				//
				AcGuiTime.onGui_Background();
				//
				float[] _times = AcSave.getTimes();
				//
				//for ( int _count = 0; _count < _times.Length; _count++ )
				for ( int _count = 0; _count < _best_10; _count++ )
				{
					float _time = -1.0f;
					//
					if(_count < _times.Length)
					{
						_time = _times[ _count ];
					}
					//
					AcGuiTime _guiTime = new AcGuiTime(
						84.0f,
						//71.0f + ( 55.0f * _count ),
						OFFSET_Y + 71.0f + ( 55.0f * _count ),
						//_times[ _count ]
						_time
					);
					//
					_guiTime.onGui( 1.0f, true );
				}
				break;
			//
			case ( AcApp.GAMEMODE_CHALLENGE ):
				//
				AcGuiDoor.onGui_Background();
				//
				int[] _doors = AcSave.getDoors();
				//
				//for ( int _count = 0; _count < _doors.Length; _count++ )
				for ( int _count = 0; _count < _best_10; _count++ )
				{
					int _door = -1;
					//
					if ( _count < _doors.Length )
					{
						_door = _doors[ _count ];
					}
					//
					AcGuiDoor _guiDoor = new AcGuiDoor(
						84.0f + ( 26.0f * 4 ),
						//71.0f + ( 55.0f * _count ),
						OFFSET_Y + 71.0f + ( 55.0f * _count ),
						//_doors[ _count ]
						_door
					);
					//
					_guiDoor.onGui( 1.0f, true );
				}
				break;
		}
		//
		if ( m_bCursorActive )
		{
			AcGuiBase.drawCursor( m_vCursorRect );
		}
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	void OnApplicationQuit()
	{
		_debugLog( "OnApplicationQuit" );
	}

	void OnDestroy()
	{
		_debugLog( "OnDestroy" );
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// 
	/// </summary>
	public interface AiRankingTrigger
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="vTrigger"></param>
		void onTrigger( AcRanking.Trigger vTrigger );
	}

	// ========================================================================== //
	// ========================================================================== //
}

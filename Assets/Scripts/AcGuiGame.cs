using UnityEngine;
using System.Collections;

// List<T>
using System.Collections.Generic;

//public class AcGui : MonoBehaviour
/// <summary>
/// ゲーム中のタイム＆ドア＆その他を表示するクラスです
/// 
/// プレハブ無しで自前で GameObject を作って MonoBehaviour の処理を行わせる事に成功したよ！
/// </summary>
//public class AcGuiGame : Object
public class AcGuiGame : MonoBehaviour
{
	// ========================================================================== //
	// ========================================================================== //
	/*
	 *　UnityをC#で超入門してみる #4 GUIの章 - Qiita
	 * http://qiita.com/hiroyuki_hon/items/af0a52c1cb9e856f32b2
	 * 
	 * 
	 * 
	 * 
	 * 
	 * Texture _tex = ( Texture ) Instantiate( Resources.Load( "Images/Door/door_1" ) );
	 * 
	 * http://ustom.daa.jp/blog/2013/10/gui%E3%82%92%E4%BD%BF%E3%81%A3%E3%81%A6%E7%B0%A1%E5%8D%98%E3%81%ABhp%E3%83%90%E3%83%BC%E3%81%AE%E8%A1%A8%E7%A4%BA%E3%81%99%E3%82%8B%E6%96%B9%E6%B3%95unity/
	 * GUI.DrawTextureWithTexCoords(
	 * new Rect( 10.0f, 10.0f, 600.0f, 20.0f ),
	 * 	_tex,
	 * 		new Rect( 0.0f, 0.0f, 0.5f, 0.5f )
	 * 	);
	 * 	
	 * 
	 * 
	 * 画像サイズに合わせてスクリーンに表示する方法2 
	 * http://qiita.com/gc-j-lee/items/f32f9fcbce165c18e623
	 * GUI.DrawTexture(
	 * new Rect( Screen.width / 2 - 128, Screen.height / 2 - 128, _tex.width, _tex.width ),
	 *	_tex
	 * );
	 * 
	 * 画面の幅、高さ
	 * http://www40.atwiki.jp/spellbound/pages/1372.html
	 * 
	 *
	 * 
	 */
	/*
	 * 匿名構造体
	 * http://msdn.microsoft.com/ja-jp/library/bb397696.aspx
	 */

	/*
	 * MonoBehaviour を継承してもモデルにくっつけないと
	 * OnGUI() とか呼ばれないのか？
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
		TIME_END,
		DOOR_END,
		READY_321,
		READY_GO,
		RESULT_END,
	};

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// 親
	/// </summary>
	private AcPlayer m_vPlayer;

	/// <summary>
	/// 
	/// </summary>
	private AiGuiGameTrigger m_vTrigger;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private _GuiTime m_vGuiTime;
	private _GuiDoor m_vGuiDoor;
	private _GuiReady m_vGuiReady;
	private _GuiResult m_vGuiResult;

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

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// 空のオブジェクトに AddComponent() して生成するよ
	/// </summary>
	/// <returns></returns>
	public static AcGuiGame Create( AcPlayer vPlayer, AiGuiGameTrigger vTrigger )
	{
		/*
		 * Unity入門-コンポーネントの追加 - WisdomSoft
		 * http://www.wisdomsoft.jp/118.html
		 * 
		 * 各種ゲームオブジェクトをスクリプトから生成する  Lonely Mobiler
		 * http://loumo.jp/wp/archive/20140531004758/
		 */
		//AcGuiGame _guiGame = new AcGuiGame();
		//GameObject _object = new GameObject( _guiGame.GetType().FullName );

		//AcDebug.debugLog( typeof( AcGuiGame ).GetType().FullName ); // "System.MonoType"
		//
		GameObject _object = new GameObject();
		//
		//AcDebug.debugLog( "コンポーネントを add するよ" );
		//
		AcGuiGame _class = ( AcGuiGame ) _object.AddComponent( ( typeof( AcGuiGame ) ) );
		//		_object.AddComponent( typeof( AcGuiGame ) ); // この時点でコンストラクタが実行されるようです
		//
		//AcDebug.debugLog( "コンポーネントを get するよ" );
		//
		//		AcGuiGame _guiGame = _object.GetComponent<AcGuiGame>();
		//
		//AcDebug.debugLog( "オブジェクトに名前を設定するよ" );
		//
		//AcDebug.debugLog( _object.name );
		//AcDebug.debugLog( _guiGame.GetType().FullName );
		//
		_object.name = _class.GetType().FullName;
		//
		_class._create( vPlayer, vTrigger );
		//
		return ( _class );
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private void _create( AcPlayer vPlayer, AiGuiGameTrigger vTrigger )
	{
		_debugLog( "_create" );
		//
		m_vPlayer = vPlayer;
		m_vTrigger = vTrigger;
		//
		m_vGuiTime = new _GuiTime( this );
		m_vGuiDoor = new _GuiDoor( this );
		m_vGuiReady = new _GuiReady( this );
		m_vGuiResult = new _GuiResult( this );
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
	/// デフォルトコンストラクタ
	/// </summary>
	private AcGuiGame()
	{
		_debugLog( "コンストラクタ" );
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	public void OnGUI()
	{
		m_vGuiTime._onGUI();
		m_vGuiDoor._onGUI();
		m_vGuiReady._onGUI();
		m_vGuiResult._onGUI();
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// タイムの表示
	/// </summary>
	/// <param name="bSw"></param>
	public void swTimeActive( bool bSw )
	{
		m_vGuiTime._swActive( bSw );
	}

	/// <summary>
	/// ドア枚数の表示
	/// </summary>
	/// <param name="bSw"></param>
	public void swDoorActive( bool bSw )
	{
		m_vGuiDoor._swActive( bSw );
	}

	/// <summary>
	/// カウントダウンの表示
	/// </summary>
	/// <param name="bSw"></param>
	public void swReadyActive( bool bSw )
	{
		m_vGuiReady._swActive( bSw );
	}

	/// <summary>
	/// 結果の表示
	/// 成功/失敗を引数で渡します
	/// </summary>
	/// <param name="bSw"></param>
	/// <param name="bSuccess"></param>
	public void swResultActive( bool bSw, bool bSuccess )
	{
		m_vGuiResult._swActive( bSw, bSuccess );
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	/// <summary>
	/// 時間の取得
	/// </summary>
	/// <returns></returns>
	public float getTime()
	{
		return ( m_vGuiTime._getTime() );
	}

	/// <summary>
	/// 時間の開始
	/// </summary>
	public void startTime()
	{
		m_vGuiTime._startTime();
	}

	/// <summary>
	/// 時間の停止
	/// </summary>
	public void stopTime()
	{
		m_vGuiTime._stopTime();
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	/// <summary>
	/// ドア枚数の表示
	/// </summary>
	/// <returns></returns>
	public int getDoor()
	{
		return ( m_vGuiDoor._getDoor() );
	}

	/// <summary>
	/// ドア枚数の追加
	/// </summary>
	public void addDoor()
	{
		m_vGuiDoor._addDoor();
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private class _GuiTime : AcGuiTime
	{
		private AcGuiGame m_vGuiGame;
		//
		private bool m_bActive;
		private IEnumerator m_vIEnumerator;
		//
		private bool m_bStart;

		public _GuiTime( AcGuiGame vGuiGame )
			: base( 84.0f, 10.0f, 0.0f )
		{
			m_vGuiGame = vGuiGame;
			//
			m_bActive = false;
			m_vIEnumerator = null;
			//
			m_bStart = false;
		}

		public void _swActive( bool bSw )
		{
			m_bActive = bSw;
			//
			if ( m_vIEnumerator != null )
			{
				m_vGuiGame.StopCoroutine( m_vIEnumerator );
				m_vIEnumerator = null;
			}
			//
			if ( bSw )
			{
				switch ( AcApp.getGameMode() )
				{
					case ( AcApp.GAMEMODE_TIMEATTACK ):
						//
						m_vValueFloat = 0.0f;
						break;
					//
					case ( AcApp.GAMEMODE_CHALLENGE ):
						//
						m_vValueFloat = AcApp.GAMERULE_CHALLENGE_TIME;
						break;
				}
				//
				m_vIEnumerator = _coroutine();
				m_vGuiGame.StartCoroutine( m_vIEnumerator );
			}
		}

		/// <summary>
		/// コルーチン
		/// </summary>
		/// <returns></returns>
		private IEnumerator _coroutine()
		{
			while ( true )
			{
				if ( m_bStart )
				{
					switch ( AcApp.getGameMode() )
					{
						case ( AcApp.GAMEMODE_TIMEATTACK ):
							//
							m_vValueFloat += Time.deltaTime;
							break;
						//
						case ( AcApp.GAMEMODE_CHALLENGE ):
							//
							m_vValueFloat -= Time.deltaTime;
							//
							if ( m_vValueFloat <= 0.0f )
							{
								m_vValueFloat = 0.0f;
								m_vGuiGame.m_vTrigger.onTrigger( AcGuiGame.Trigger.TIME_END );
								yield break;
							}
							break;
					}
				}
				//
				yield return null;
			}
		}

		public void _startTime()
		{
			m_bStart = true;
		}

		public void _stopTime()
		{
			m_bStart = false;
		}

		public float _getTime()
		{
			return ( m_vValueFloat );
		}

		public void _onGUI()
		{
			if ( AcApp.getGameMode() == AcApp.GAMEMODE_CHALLENGE )
			{
				if ( m_bActive )
				{
					float _baseScale = AcUtil.getScreenScaleX( AcApp.SCREEN_W );
					float _sizeScale = 1.0f;
					//
					onGUI( _baseScale, _sizeScale, false );
				}
			}
		}
	}

	// ========================================================================== //
	// ========================================================================== //

	private class _GuiDoor : AcGuiDoor
	{
		private AcGuiGame m_vGuiGame;
		//
		private bool m_bActive;

		public _GuiDoor( AcGuiGame vGuiGame )
			: base( 84.0f, 10.0f, 0 )
		{
			m_vGuiGame = vGuiGame;
			//
			m_bActive = false;
		}

		public void _swActive( bool bSw )
		{
			m_bActive = bSw;
			//
			if ( bSw )
			{
				m_vValueInt = 0;
			}
		}

		public void _addDoor()
		{
			m_vValueInt++;
			//
			switch ( AcApp.getGameMode() )
			{
				case ( AcApp.GAMEMODE_TIMEATTACK ):
					//
					if ( m_vValueInt >= AcApp.GAMERULE_TIMEATTACK_DOOR )
					{
						m_vGuiGame.m_vTrigger.onTrigger( AcGuiGame.Trigger.DOOR_END );
					}
					break;
				//
				case ( AcApp.GAMEMODE_CHALLENGE ):
					break;
			}
		}

		public int _getDoor()
		{
			return ( m_vValueInt );
		}

		public void _onGUI()
		{
			if ( AcApp.getGameMode() == AcApp.GAMEMODE_TIMEATTACK )
			{
				if ( m_bActive )
				{
					float _baseScale = AcUtil.getScreenScaleX( AcApp.SCREEN_W );
					float _sizeScale = 1.0f;
					//
					onGUI( _baseScale, _sizeScale, false );
				}
			}
		}
	}

	// ========================================================================== //
	// ========================================================================== //

	private class _GuiReady : AcGuiBase
	{
		private AcGuiGame m_vGuiGame;
		//
		private bool m_bActive;
		private IEnumerator m_vIEnumerator;

		public _GuiReady( AcGuiGame vGuiGame )
			: base()
		{
			m_vGuiGame = vGuiGame;
			//
			m_bActive = false;
			m_vIEnumerator = null;
		}

		public void _swActive( bool bSw )
		{
			m_bActive = bSw;
			//
			if ( m_vIEnumerator != null )
			{
				m_vGuiGame.StopCoroutine( m_vIEnumerator );
				m_vIEnumerator = null;
			}
			//
			if ( bSw )
			{
				m_vIEnumerator = _coroutine();
				m_vGuiGame.StartCoroutine( m_vIEnumerator );
			}
		}

		/// <summary>
		/// 移動処理をコルーチンでやってみるよ
		/// </summary>
		/// <returns></returns>
		private IEnumerator _coroutine()
		{
			float _timer = 4.0f; // "4" で初期設定してすぐ減算して "3.??" になって (int) キャストするよ！
			int _old_count = ( int ) _timer;
			//
			while ( true )
			{
				_timer -= Time.deltaTime;
				//
				int _new_count = ( int ) _timer;
				//
				if ( _old_count != _new_count )
				{
					_old_count = _new_count;
					//
					if ( _new_count > 0 )
					{
						m_vGuiGame.m_vTrigger.onTrigger( AcGuiGame.Trigger.READY_321 );
					}
					else
					{
						m_vGuiGame.m_vTrigger.onTrigger( AcGuiGame.Trigger.READY_GO );
						yield break;
					}
				}
				//
				m_vValueInt = _new_count;
				//
				yield return null;
			}
		}

		public void _onGUI()
		{
			if ( m_bActive )
			{
				float _scale = AcUtil.getScreenScaleX( AcApp.SCREEN_W );

				/*
				 * サイズを決めてセンタリングしてみる
				 */
				//float _w = 200.0f;
				//float _h = 100.0f;
				//float _x = ( AcApp.SCREEN_W - _w ) / 2;
				//float _y = 50.0f;
				float _w = AcApp.SCREEN_W;
				float _h = AcApp.SCREEN_H;
				float _x = ( AcApp.SCREEN_W - _w ) / 2;
				float _y = ( AcApp.SCREEN_H - _h ) / 2;
				//
				/*
				 * ArrayList クラス
				 * http://msdn.microsoft.com/ja-jp/library/system.collections.arraylist(v=vs.110).aspx
				 */
				List<_ImageList> _data = new List<_ImageList>();
				//
				//int _index = _CHANGER_FIG_0 + ( m_vValueInt % 10 );
				int _index = _CHANGER_COUNTDOWN_0 + ( m_vValueInt % 10 );
				//
				clear();
				//
				add(
					( m_vX + _x ) * _scale,
					( m_vY + _y ) * _scale,
					_w * _scale,
					_h * _scale,
					_index
				);
				//
				draw();
			}
		}
	}

	// ========================================================================== //
	// ========================================================================== //

	private class _GuiResult : AcGuiBase
	{
		private AcGuiGame m_vGuiGame;
		//
		private bool m_bActive;
		private IEnumerator m_vIEnumerator;

		public _GuiResult( AcGuiGame vGuiGame )
			: base()
		{
			m_vGuiGame = vGuiGame;
			//
			m_bActive = false;
			m_vIEnumerator = null;
		}

		public void _swActive( bool bSw, bool bSuccess )
		{
			m_bActive = bSw;
			//
			if ( m_vIEnumerator != null )
			{
				m_vGuiGame.StopCoroutine( m_vIEnumerator );
				m_vIEnumerator = null;
			}
			//
			if ( bSw )
			{
				m_vValueInt = bSuccess ? 1 : 0;
				//
				m_vIEnumerator = _coroutine();
				m_vGuiGame.StartCoroutine( m_vIEnumerator );
			}
		}

		/// <summary>
		/// 移動処理をコルーチンでやってみるよ
		/// </summary>
		/// <returns></returns>
		private IEnumerator _coroutine()
		{
			int _frame;

			// 移動時間のフレーム数
			_frame = 60;
			//
			m_vX = AcApp.SCREEN_W / 2 + AcApp.SCREEN_W;
			m_vY = AcApp.SCREEN_H / 2;
			//
			for ( int _count = 0; _count < _frame; _count++ )
			{
				m_vX -= ( ( float ) AcApp.SCREEN_W / _frame );
				//
				yield return null;
			}

			// 停止時間のフレーム数
			_frame = 60 * 4;
			//
			for ( int _count = 0; _count < _frame; _count++ )
			{
				yield return null;
			}

			//
			//m_vX = AcApp.SCREEN_W / 2;
			//
			m_vGuiGame.m_vTrigger.onTrigger( AcGuiGame.Trigger.RESULT_END );
		}

		public void _onGUI()
		{
			if ( m_bActive )
			{
				float _scale = AcUtil.getScreenScaleX( AcApp.SCREEN_W );

				/*
				 * サイズを決めてセンタリングしてみる
				 */
				float _w = 300.0f;
				float _h = 200.0f;
				//float _x = ( AcApp.SCREEN_W - _w ) / 2;
				//float _y = 150.0f;
				float _x = m_vX - ( _w ) / 2;
				float _y = m_vY - ( _h ) / 2;

				//
				/*
				 * ArrayList クラス
				 * http://msdn.microsoft.com/ja-jp/library/system.collections.arraylist(v=vs.110).aspx
				 */
				List<_ImageList> _data = new List<_ImageList>();
				//
				clear();
				//
				int _index = _CHANGER_FAILURE;
				//
				switch ( m_vValueInt )
				{
					case ( 0 ):
						_index = _CHANGER_FAILURE;
						break;
					//
					//case ( 1 ):
					default:
						//
						switch ( AcApp.getGameMode() )
						{
							case ( AcApp.GAMEMODE_TIMEATTACK ):
								_index = _CHANGER_SUCCESS;
								break;
							//
							//case ( AcApp.GAMEMODE_CHALLENGE ):
							default:
								_index = _CHANGER_SUCCESS_CHALLENGE;
								break;
						}
						break;
				}
				//
				add(
					//( m_vX + _x ) * _scale,
					//( m_vY + _y ) * _scale,
					_x * _scale,
					_y * _scale,
					_w * _scale,
					_h * _scale,
					_index
				);
				//
				draw();
			}
		}
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// 
	/// </summary>
	public interface AiGuiGameTrigger
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="vTrigger"></param>
		void onTrigger( AcGuiGame.Trigger vTrigger );
	}

	// ========================================================================== //
	// ========================================================================== //
}

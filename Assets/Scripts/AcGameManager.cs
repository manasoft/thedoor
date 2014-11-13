using UnityEngine;
using System.Collections;

/// <summary>
/// 2014/10/30 現在で、この中で全てやりますよ
/// </summary>
public class AcGameManager : MonoBehaviour
{
	// ========================================================================== //
	// ========================================================================== //
	/*
	 * アンビエントライトの設定
	 * Unity [Edit]→[Render Setting] の "Ambient Light" を設定する
	 */
	/*
	 * プレハブ
	 * http://ws.cis.sojo-u.ac.jp/~izumi/Unity_Documentation_jp/Documentation/Manual/Prefabs.html
	 * パラメータが太字になる件
	 * オブジェクト独自に変更した場合になるっぽいです
	 * 
	 * プレハブをインスタンス化する
	 * http://qiita.com/2dgames_jp/items/8a28fd9cf625681faf87
	 * 
	 * http://docs-jp.unity3d.com/Documentation/Manual/InstantiatingPrefabs.html
	 * 
	 * なんか Resources フォルダってのは "予約された" フォルダ名のようです
	 * http://hamken100.blogspot.jp/2012/04/unity-resourcesprefab.html
	 * 
	 * オブジェクトの非表示
	 * http://hamken100.blogspot.jp/2012/05/unity-gameobject.html
	 */

	/*
	 * 
	 * 画像サイズに合わせてスクリーンに表示する方法2 
	 * http://qiita.com/gc-j-lee/items/f32f9fcbce165c18e623
	 * 
	 */

	//		GameObject prefab = ( GameObject ) Resources.Load( "Prefabs/Timer" );
	// プレハブからインスタンスを生成
	//		GameObject timer = ( GameObject ) Instantiate( prefab );

	//		timer.transform.localPosition = new Vector3( 0.0f, 0.0f, 0.0f );
	//		timer.transform.position = new Vector3( 1.0f, 1.0f, 0.0f );
	//		timer.transform.localScale = new Vector3( 0.2f, 0.1f, 1.0f );

	//		GUITexture tex = timer.GetComponent<GUITexture>();
	//		tex.pixelInset.position = new Vector2( 0.0f, 0.0f );
	//		tex.pixelInset.size = new Vector2( 10.0f, 10.0f );
	//		tex.pixelInset.Set( 0.0f, 0.0f, 10.0f, 5.0f );
	/*
	 * 解像度によってGUITextureの大きさを変更するサンプル
	 * https://gist.github.com/tsubaki/6290796
	 */
	//		tex.pixelInset = new Rect( 0.0f, 0.0f, 10.0f, 5.0f );

	//		GUITexture obj = new GUITexture();
	//		obj.pixelInset = new Rect( 0.0f, 0.0f, 10.0f, 5.0f );
	//		obj.texture = ( Texture ) Instantiate( Resources.Load( "Images/Door/door_1" ) );




	//		tex = timer.GetComponent<GUITexture>().texture;


	//GameObject prefab = ( GameObject ) Resources.Load( "Prefabs/Gui", typeof(GameObject) );
	//// プレハブからインスタンスを生成
	//GameObject objectA = ( GameObject ) Instantiate( prefab, new Vector3( 0.0f, 0.0f, 0.0f ), Quaternion.identity );

	//AcGui scr = objectA.GetComponent<AcGui>();

	//		AcGui.Create();

	//		m_vGui = AcGui.Create();

	//		m_vPlayer = AcPlayer.Create();

	/*
	 * 
	 * http://www.wisdomsoft.jp/52.html
	 */
	//	Application.targetFrameRate = 10;


#if false
		// プレハブを取得
		GameObject prefab = (GameObject)Resources.Load("Prefabs/Room");
		// プレハブからインスタンスを生成
		GameObject objectA = (GameObject)Instantiate(prefab, new Vector3(0.0f, 0.0f, 1.0f), Quaternion.identity);
		GameObject objectB = (GameObject)Instantiate(prefab, new Vector3(0.0f, 0.0f, 3.0f), Quaternion.identity);
#endif

	/*
		 * UnityをC#で超入門してみる #4 GUIの章 - Qiita
		 * http://qiita.com/hiroyuki_hon/items/af0a52c1cb9e856f32b2
		 */
	//		AcAd.onGui();

	//		m_vGui.update();


	//Texture _tex = ( Texture ) Instantiate( Resources.Load( "Images/Door/door_1" ) );

	///*
	// * http://ustom.daa.jp/blog/2013/10/gui%E3%82%92%E4%BD%BF%E3%81%A3%E3%81%A6%E7%B0%A1%E5%8D%98%E3%81%ABhp%E3%83%90%E3%83%BC%E3%81%AE%E8%A1%A8%E7%A4%BA%E3%81%99%E3%82%8B%E6%96%B9%E6%B3%95unity/
	// */
	//GUI.DrawTextureWithTexCoords(
	//	new Rect( 10.0f, 10.0f, 600.0f, 20.0f ),
	//	_tex,
	//	new Rect( 0.0f, 0.0f, 0.5f, 0.5f )
	//);

	/////*
	//// * 画像サイズに合わせてスクリーンに表示する方法2 
	//// * http://qiita.com/gc-j-lee/items/f32f9fcbce165c18e623
	//// */
	////GUI.DrawTexture(
	////	new Rect( Screen.width / 2 - 128, Screen.height / 2 - 128, _tex.width, _tex.width ),
	////	_tex
	////);

	/*
	 * カメラ
	 * http://d.hatena.ne.jp/nakamura001/20120706/1341589197
	 * 
	 * レイヤー
	 * オブジェクトのレイヤーも変えられる
	 * http://www.wisdomsoft.jp/150.html
	 * 
	 * カリングマスク
	 * http://lilligad.blogspot.jp/2012/09/unity_9.html
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


	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private AcPlayer m_vPlayer;
	//
	private AcTitle m_vTitle;
	private AcHowtoplay m_vHowtoplay;
	private AcRanking m_vRanking;
	private AcAd m_vAd;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	/// <summary>
	/// 広告が出るのは
	/// ・[ゲーム]->[ランキング]->[広告]
	/// ・[タイトル]->[ランキング]->[タイトル]
	/// で [ランキング] から [広告] へ遷移するかのフラグです
	/// </summary>
	private bool m_bAd;

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

	private void _awake()
	{
		/*
		 * _start() でやってもいいんですけど試しでココでやってみただけです
		 */

		/*
		 * 動的なオブジェクトの親子関係
		 * 親に追加するんじゃなくて、子に親を設定するらしいっす
		 * http://www.wisdomsoft.jp/114.html
		 */
		this.transform.position = AcApp.GamePosition;

		/*
		 * ランキングの読み込み
		 */
		AcSave.Create();

		//
		m_vPlayer = AcPlayer.Create( this, new _PlayerTrigger( this ) );
		//
		m_vTitle = AcTitle.Create( this, new _TitleTrigger( this ) );
		//
		m_vHowtoplay = AcHowtoplay.Create( this, new _HowtoplayTrigger( this ) );
		m_vHowtoplay.setActive( false );
		//
		m_vRanking = AcRanking.Create( this, new _RankingTrigger( this ) );
		m_vRanking.setActive( false, false );
		//
		m_vAd = AcAd.Create( this, new _AdTrigger( this ) );
		m_vAd.setActive( false );
		//
		m_bAd = false;
	}

	private void _start()
	{
	}

	private void _update()
	{
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

	void OnGUI()
	{
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	void OnApplicationQuit()
	{
		_debugLog( "AcGameManager # OnApplicationQuit" );
	}

	void OnDestroy()
	{
		_debugLog( "AcGameManager # OnDestroy" );
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// AcPlayer からのトリガー処理です
	/// </summary>
	private class _PlayerTrigger : AcPlayer.AiPlayerTrigger
	{
		private AcGameManager m_vManager;

		public _PlayerTrigger( AcGameManager vManager )
		{
			m_vManager = vManager;
		}

		public void onTrigger( AcPlayer.Trigger vTrigger )
		{
			switch ( vTrigger )
			{
				case ( AcPlayer.Trigger.FINISH ):
					/*
					 * ゲームが終了したので「Ranking」へ行くよ！
					 */
					m_vManager.m_vRanking.setActive( true, true );
					//
					m_vManager.m_bAd = true;
					break;
			}
		}
	}

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// AcTitle からのトリガー処理です
	/// </summary>
	private class _TitleTrigger : AcTitle.AiTitleTrigger
	{
		private AcGameManager m_vManager;

		public _TitleTrigger( AcGameManager vManager )
		{
			m_vManager = vManager;
		}

		public void onTrigger( AcTitle.Trigger vTrigger )
		{
			switch ( vTrigger )
			{
				case ( AcTitle.Trigger.GAME ):
					/*
					 * ゲームが選ばれたの「Title」から「Howtoplay」へ行くよ
					 */
					m_vManager.m_vTitle.setActive( false );
					m_vManager.m_vHowtoplay.setActive( true );
					break;
				//
				case ( AcTitle.Trigger.RANKING ):
					/*
					 * ランキングが選ばれたの「Title」から「Ranking」へ行くよ
					 */
					m_vManager.m_vTitle.setActive( false );
					m_vManager.m_vRanking.setActive( true, false );
					break;
			}
		}
	}

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// AcHowtoplay からのトリガー処理です
	/// </summary>
	private class _HowtoplayTrigger : AcHowtoplay.AiHowtoplayTrigger
	{
		private AcGameManager m_vManager;

		public _HowtoplayTrigger( AcGameManager vManager )
		{
			m_vManager = vManager;
		}

		public void onTrigger( AcHowtoplay.Trigger vTrigger )
		{
			switch ( vTrigger )
			{
				//
				case ( AcHowtoplay.Trigger.YES ):
					/*
					 * ゲーム開始するよ
					 */
					m_vManager.m_vHowtoplay.setActive( false );
					//
					m_vManager.m_vPlayer.requestStopAuto();
					break;
				//
				case ( AcHowtoplay.Trigger.NO ):
					/*
					 * タイトルに戻るよ
					 */
					m_vManager.m_vHowtoplay.setActive( false );
					m_vManager.m_vTitle.setActive( true );
					break;
			}
		}
	}

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// AcRanking からのトリガー処理です
	/// </summary>
	private class _RankingTrigger : AcRanking.AiRankingTrigger
	{
		private AcGameManager m_vManager;

		public _RankingTrigger( AcGameManager vManager )
		{
			m_vManager = vManager;
		}

		public void onTrigger( AcRanking.Trigger vTrigger )
		{
			switch ( vTrigger )
			{
				//
				case ( AcRanking.Trigger.OK ):
					/*
					 * 
					 */
					m_vManager.m_vRanking.setActive( false, false );
					//
					if ( m_vManager.m_bAd )
					{
						m_vManager.m_bAd = false;
						//
						m_vManager.m_vAd.setActive( true );
					}
					else
					{
						m_vManager.m_vTitle.setActive( true );
					}
					break;
			}
		}
	}

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// AcAd からのトリガー処理です
	/// </summary>
	private class _AdTrigger : AcAd.AiAdTrigger
	{
		private AcGameManager m_vManager;

		public _AdTrigger( AcGameManager vManager )
		{
			m_vManager = vManager;
		}

		public void onTrigger( AcAd.Trigger vTrigger )
		{
			switch ( vTrigger )
			{
				//
				case ( AcAd.Trigger.OK ):
					/*
					 * タイトルに戻るよ
					 */
					m_vManager.m_vAd.setActive( false );
					m_vManager.m_vTitle.setActive( true );
					break;
			}
		}
	}

	// ========================================================================== //
	// ========================================================================== //
}

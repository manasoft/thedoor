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

	//private enum _Phase
	//{
	//	_PHASE_INI,
	//	_PHASE_TITLE,
	//	_PHASE_GAME,
	//	_PHASE_RANKING,
	//}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	//private const int _PHASE_INI = 0;
	//private const int _PHASE_TITLE = 1;
	//private const int _PHASE_GAME = 2;
	//private const int _PHASE_RANKING = 3;

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// 
	/// </summary>
//	private AcSoundManager m_vSoundManager;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	//	private _Phase m_vPhase;

	private AcPlayer m_vPlayer;
	//
	private AcTitle m_vTitle;
	private AcHowtoplay m_vHowtoplay;
	private AcRanking m_vRanking;

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// 音出るよ！
	/// トラック名を返すよ
	/// </summary>
	/// <param name="vEntryName"></param>
	/// <returns></returns>
	//public string soundPlay( string vEntryName, float vFadeInTime = 0.0f )
	//{
	//	return ( m_vSoundManager.play( vEntryName, vFadeInTime ) );
	//}

	//public void soundStop( string vTrackName, float vFadeOutTime = 0.0f )
	//{
	//	m_vSoundManager.stop( vTrackName, vFadeOutTime );
	//}

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
//		m_vHowtoplay.gameObject.SetActive( false );
		m_vHowtoplay.setActive( false );
		//
		m_vRanking = AcRanking.Create( this, new _RangingTrigger( this ) );
//		m_vRanking.gameObject.SetActive( false );
		m_vRanking.setActive( false );
	}

	private void _start()
	{
		//{
		//	// 実験
		//	AcImageManager.test();
		//}

		//m_vSoundManager = new AcSoundManager();
		////
		//m_vSoundManager.add( "se_3", "sound", new string[] { "se1", "se2", "se3" }, 1.0f, 1.0f, true, null, "Sounds/Seikai02-1" );

		//m_vSoundManager.add( "se_1", "sound", new string[] { "se1", "se2", "se3" }, 1.0f, 1.0f, false, null, "Sounds/Seikai02-1" );
		//m_vSoundManager.add( "se_2", "sound", new string[] { "se1" }, 1.0f, -1.0f, false, null, "Sounds/Huseikai02-4" );


		//m_vSoundManager.add( "bgm_1", "sound", new string[] { "bgm1" }, 0.05f, 0.0f, false, "bgm_2", "Sounds/Encounter_loop" );
		//m_vSoundManager.add( "bgm_2", "sound", new string[] { "bgm1" }, 0.05f, 0.0f, true, null, "Sounds/Top_Speed" );

		//m_vSoundManager.add( "se_3", "sound", new string[] { "se1", "se2", "se3" }, 0.6f, 0.0f, false, null, "Sounds/Seikai02-1" );

		//m_vSoundManager.add( "se_cd_1", "sound", new string[] { "se1", "se2", "se3" }, 0.6f, 0.0f, false, null, "Sounds/Accent Simple07-1" );
		//m_vSoundManager.add( "se_cd_2", "sound", new string[] { "se1", "se2", "se3" }, 0.6f, 0.0f, false, null, "Sounds/Accent Simple06-1" );
	}

	private void _update()
	{
	//	m_vSoundManager.update();
		AcApp.soundUpdate();

		///*
		// * バックキー？
		// * http://search.yahoo.co.jp/search;_ylt=A7dPSCnrby5U7xwA99aJBtF7?p=unity+%E3%83%90%E3%83%83%E3%82%AF%E3%82%AD%E3%83%BC&search.x=1&fr=top_ga1&tid=top_ga1&ei=UTF-8
		// * http://unity3dplugin.blogspot.jp/2012/07/unityandroid.html
		// * http://ochachaapp.blogspot.jp/2011/09/unityandroid.html
		// * 
		// */
		//if ( Input.GetKeyDown( KeyCode.Escape ) )
		//{
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
		Debug.Log( "AcGameManager # OnApplicationQuit" );
	}

	void OnDestroy()
	{
		Debug.Log( "AcGameManager # OnDestroy" );
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// ゲーム処理からの遷移用です
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
			Debug.Log( "_PlayerTrigger が呼ばれたよ" );

			switch ( vTrigger )
			{
				//
				case ( AcPlayer.Trigger.FINISH ):
					Debug.Log( "AcPlayer.Trigger.FINISH" );
					//
					//m_vManager.m_vRanking.gameObject.SetActive( true );
					m_vManager.m_vRanking.setActive( true );
					break;
				//
				//case ( AcPlayer.Trigger.QUIT ):
				//	Debug.Log( "AcPlayer.Trigger.QUIT" );
				//	//
				//	m_vManager.m_vTitle.gameObject.SetActive( true );
				//	break;
			}
		}
	}

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// Title からの遷移用です
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
			Debug.Log( "_TitleTrigger が呼ばれたよ" );

			switch ( vTrigger )
			{
				//
				case ( AcTitle.Trigger.TIMEATTACK ):
					Debug.Log( "AcTitle.Trigger.TIMEATTACK" );
					//
					AcApp.setGameMode( AcApp.GAMEMODE_TIMEATTACK );
					//
					//m_vManager.m_vTitle.gameObject.SetActive( false );
					//m_vManager.m_vHowtoplay.gameObject.SetActive( true );
					m_vManager.m_vTitle.setActive( false );
					m_vManager.m_vHowtoplay.setActive( true );
					//
					//m_vManager.soundPlay( "se_1" );
					AcApp.soundPlay( "se_1" );
					break;
				//
				case ( AcTitle.Trigger.CHALLENGE ):
					Debug.Log( "AcTitle.Trigger.CHALLENGE" );
					//
					AcApp.setGameMode( AcApp.GAMEMODE_CHALLENGE );
					//
					//m_vManager.m_vTitle.gameObject.SetActive( false );
					//m_vManager.m_vHowtoplay.gameObject.SetActive( true );
					m_vManager.m_vTitle.setActive( false );
					m_vManager.m_vHowtoplay.setActive( true );
					break;
				//
				case ( AcTitle.Trigger.RANKING ):
					Debug.Log( "AcTitle.Trigger.RANKING" );
					//
					//m_vManager.m_vTitle.gameObject.SetActive( false );
					//m_vManager.m_vRanking.gameObject.SetActive( true );
					m_vManager.m_vTitle.setActive( false );
					m_vManager.m_vRanking.setActive( true );
					break;
			}
		}
	}

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// Howtoplay からの遷移用です
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
			Debug.Log( "_HowtoplayTrigger が呼ばれたよ" );

			switch ( vTrigger )
			{
				//
				case ( AcHowtoplay.Trigger.YES ):
					Debug.Log( "AcHowtoplay.Trigger.YES" );
					//
					//m_vManager.m_vHowtoplay.gameObject.SetActive( false );
					m_vManager.m_vHowtoplay.setActive( false );
					//
					m_vManager.m_vPlayer.requestStopAuto();
					break;
				//
				case ( AcHowtoplay.Trigger.NO ):
					Debug.Log( "AcHowtoplay.Trigger.NO" );
					//
					//m_vManager.m_vHowtoplay.gameObject.SetActive( false );
					//m_vManager.m_vTitle.gameObject.SetActive( true );
					m_vManager.m_vHowtoplay.setActive( false );
					m_vManager.m_vTitle.setActive( true );
					break;
			}
		}
	}

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// Ranking からの遷移用です
	/// </summary>
	private class _RangingTrigger : AcRanking.AiRankingTrigger
	{
		private AcGameManager m_vManager;

		public _RangingTrigger( AcGameManager vManager )
		{
			m_vManager = vManager;
		}

		public void onTrigger( AcRanking.Trigger vTrigger )
		{
			Debug.Log( "_RangingTrigger が呼ばれたよ" );

			switch ( vTrigger )
			{
				//
				case ( AcRanking.Trigger.OK ):
					Debug.Log( "AcRanking.Trigger.OK" );
					//
					//m_vManager.m_vRanking.gameObject.SetActive( false );
					//m_vManager.m_vTitle.gameObject.SetActive( true );
					m_vManager.m_vRanking.setActive( false );
					m_vManager.m_vTitle.setActive( true );
					break;
			}
		}
	}

	// ========================================================================== //
	// ========================================================================== //
}

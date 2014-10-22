using UnityEngine;
using System.Collections;

public class AcGameManager : MonoBehaviour {

	// ========================================================================== //
	// ========================================================================== //
	/*
	 * アンビエントライトの設定
	 * Unity [Edit]→[Render Setting] の "Ambient Light" を設定する
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


//	Texture tex; 

//	private AcGui m_vGui;

//	private AcPlayer m_vPlayer;

	// ========================================================================== //
	// ========================================================================== //


	// ========================================================================== //
	// ========================================================================== //
	
	private void _ini()
	{
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

		AcPlayer.Create();
		//if( m_vPlayer!= null )
		//{

		//}

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
	}
	
	// ========================================================================== //
	// ========================================================================== //


	// Use this for initialization
	void Start ()
	{
		_ini();
		//
//		AcAd.onStart( this );
	}
	
	// Update is called once per frame
	void Update ()
	{
		/*
		 * バックキー？
		 * http://search.yahoo.co.jp/search;_ylt=A7dPSCnrby5U7xwA99aJBtF7?p=unity+%E3%83%90%E3%83%83%E3%82%AF%E3%82%AD%E3%83%BC&search.x=1&fr=top_ga1&tid=top_ga1&ei=UTF-8
		 * http://unity3dplugin.blogspot.jp/2012/07/unityandroid.html
		 * http://ochachaapp.blogspot.jp/2011/09/unityandroid.html
		 * 
		 */
		if ( Input.GetKeyDown( KeyCode.Escape ) )
		{
			Debug.Log( "終了？" );

			Application.LoadLevel( "Title" );
		}
	}

	void OnGUI()
	{
		/*
		 * UnityをC#で超入門してみる #4 GUIの章 - Qiita
		 * http://qiita.com/hiroyuki_hon/items/af0a52c1cb9e856f32b2
		 */
//		AcAd.onGui();

		//		m_vGui.update();

		return;

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
	}

	void OnDestroy()
	{
		Debug.Log( "AcGameManager >> OnDestroy()" );
	}

	void OnApplicationQuit()
	{
		Debug.Log( "AcGameManager >> OnApplicationQuit()" );
	}

	// ========================================================================== //
	// ========================================================================== //
}

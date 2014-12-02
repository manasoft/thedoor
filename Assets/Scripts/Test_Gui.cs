using UnityEngine;
using System.Collections;

public class Test_Gui : MonoBehaviour
{
	// ========================================================================== //
	// ========================================================================== //
	/*
	 * 
	 */
	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //
	/*
	 * Unity入門 - WisdomSoft
	 * http://www.wisdomsoft.jp/35.html
	 * GUI が結構まとまってます
	 * 
	 * 
	 * https://gist.github.com/Buravo46/8168223
	 */
	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //


	private Vector2 scrollViewVector = Vector2.zero;
	private string innerText = "I am inside the ScrollView";


	// GUIに使うSkin

	public GUISkin skin;



	// スクロールの現在位置

//	public Vector2 scrollViewVector = Vector2.zero;

	// position : 表示位置

	public Rect scrollViewRect = new Rect( 50, 50, 220, 200 );

	// viewRect : スクロールビューの全体のRect範囲。position以上であれば、スクロールバーを操作して表示する 

	public Rect scrollViewAllRect = new Rect( 40, 40, 100, 580 );



	// GUIContent : text texture tooltip

	public GUIContent[] contents;

	// Use this for initialization




	void OnGUI_2()
	{
		contents = new GUIContent[ 5 ];

		for ( int _c=0; _c < 5; _c++ )
		{
			contents[ _c ] = new GUIContent();
			contents[ _c ].text = "a";
		}



		// Skin代入

//		GUI.skin = skin;



		// スクロールビューの開始位置を作成する

		scrollViewVector = GUI.BeginScrollView( scrollViewRect, scrollViewVector, scrollViewAllRect );



		// ボックスを作成

		GUI.Box( new Rect( 50, 50, 200, 20 ), "Please select" );



		// Button

		GUI.Button( new Rect( 50, 80, 100, 100 ), contents[ 0 ] );

		GUI.Button( new Rect( 50, 180, 180, 100 ), contents[ 1 ] );

		GUI.Button( new Rect( 50, 280, 180, 100 ), contents[ 2 ] );

		GUI.Button( new Rect( 50, 380, 180, 100 ), contents[ 3 ] );

		GUI.Button( new Rect( 50, 480, 180, 100 ), contents[ 4 ] );



		// スクロールビューの終了位置を作成する 

		GUI.EndScrollView();

	}


	private void _onGui()
	{
		OnGUI_2();

		////GUI.Sty
		//GUI.Label( new Rect( 10, 10, 50, 100 ), "MenuWindow" );

		//if ( GUI.Button( new Rect( 20, 40, 80, 20 ), "Level 1" ) )
		//{
		//	Debug.Log( "おされた！" );
		//}

		//// ScrollView を開始します。
		//scrollViewVector = GUI.BeginScrollView( new Rect( 25, 100, 120, 200 ), scrollViewVector, new Rect( 0, 0, 50, 400 ) );

		//// ScrollView 内に何かを挿入します。
		//innerText = GUI.TextArea( new Rect( 0, 0, 400, 400 ), innerText );

		//GUI.TextArea( new Rect( 0, 0, 400, 400 ), "２行目" );

		//// ScrollView を終了します。
		//GUI.EndScrollView();

	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	// Use this for initialization
	void Start()
	{
	//	AcLog.test();


		//ScGui.createButton(
		//	new Rect( 20, 20, 200, 100 ),
		//	"ボタン",
		//	() =>
		//		{
		//			Debug.Log( "ボタン押されたよーん！" );
		//		}
		//);

		//ScGui.createBox( new Rect( 10, 10, 240, 300 ), "Box" );

		ScGui.createDialog( "タイトル", "Ok", "Ng" );

		//ScGui _gui = ScGui.createButton(
		//_gui.setOnTrigger( () =>
		//	{
		//		Debug.Log( "ボタン押されたよーん！" );
		//	}
		//);
	}

	// Update is called once per frame
	void Update()
	{
		if ( Input.touchCount > 0 )
		{
			ScDebug.debugLog( "タッチされたよ" );

			if ( Input.GetTouch( 0 ).phase == TouchPhase.Moved )
			{
				ScDebug.debugLog( "フリック" );
			}
		}
	}

	void OnGUI()
	{
		//_onGui();
	}

	// ========================================================================== //
	// ========================================================================== //

}

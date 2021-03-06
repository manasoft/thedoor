using UnityEngine;
using System.Collections;

public class AcRoom : MonoBehaviour
{
	// ========================================================================== //
	// ========================================================================== //
	/*
	 * 
	 * 
	 * 四角い多次元配列
	 * http://ufcpp.net/study/csharp/st_array.html#multid
	 * あああ
	 * 
	 * 
	 */

	/*
	 * プレハブの孫が見えない件
	 * http://narudesign.com/devlog/unity-prefab-hierarchy-grandchild/
	 */

	/*
	 * 透明テクスチャの件
	 * http://www.atsuhiro-me.net/unity/dev/transparent
	 * http://am1tanaka.hatenablog.com/entry/20120131/1328015837
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

	private const int _OBJECT_WALL_ROOF = 0;
	private const int _OBJECT_WALL_FLOOR = 1;
	private const int _OBJECT_WALL_L = 2;
	private const int _OBJECT_WALL_R = 3;
	private const int _OBJECT_WALL_F = 4;
	private const int _OBJECT_WALL_NUM = 5;

	private const int _OBJECT_DOOR_L = 0;
	private const int _OBJECT_DOOR_C = 1;
	private const int _OBJECT_DOOR_R = 2;
	private const int _OBJECT_DOOR_NUM = 3;

	private const int _OBJECT_MARK_L = 0;
	private const int _OBJECT_MARK_C = 1;
	private const int _OBJECT_MARK_R = 2;
	private const int _OBJECT_MARK_NUM = 3;

	// ========================================================================== //
	// ========================================================================== //

	//private const int _ROOM_TEX_WALL_DIV_X = 8;
	//private const int _ROOM_TEX_WALL_DIV_Y = 8;
	//private const int _ROOM_TEX_WALL_FRAME = 4;

	//private const int _ROOM_TEX_DOOR_DIV_X = 8;
	//private const int _ROOM_TEX_DOOR_DIV_Y = 8;
	//private const int _ROOM_TEX_DOOR_FRAME = 4;

	// ========================================================================== //
	// ========================================================================== //

	private string[] _wallObjectTbl = new string[]
	{
		"Roof",					// _OBJECT_WALL_ROOF
		"Floor",				// _OBJECT_WALL_FLOOR
		"WallL",				// _OBJECT_WALL_L
		"WallR",				// _OBJECT_WALL_R
		"WallF",				// _OBJECT_WALL_F
	};

	private string[] _doorObjectTbl = new string[]
	{
		"Doors/DoorL",			// _OBJECT_DOOR_L
		"Doors/DoorC",			// _OBJECT_DOOR_C
		"Doors/DoorR",			// _OBJECT_DOOR_R
	};

	private string[] _markObjectTbl = new string[]
	{
		"Doors/DoorL/MarkL",	// _OBJECT_WALL_L
		"Doors/DoorC/MarkC",	// _OBJECT_WALL_C
		"Doors/DoorR/MarkR",	// _OBJECT_WALL_R
	};

	// ========================================================================== //
	// ========================================================================== //

	/*
	 * テクスチャを１枚にまとめたほうが良いかもね
	 */
	//private AcTextureChanger.Data[] _wallChangerTbl__ =
	//{
	//	//
	//	new AcTextureChanger.Data(
	//		"Images/Wall/tx_room", 6, 6, 4,
	//		// Id = 0
	//		new int[] {  0,  1,  2,  3,  4,  5,},		// _OBJECT_ROOF
	//		new int[] {  6,  7,  8,  9, 10, 11,},		// _OBJECT_FLOOR
	//		new int[] { 12, 13, 14, 15, 16, 17,},		// _OBJECT_WALL_L
	//		new int[] { 18, 19, 20, 21, 22, 23,},		// _OBJECT_WALL_R
	//		new int[] { 24, 25, 26, 27, 28, 29,},		// _OBJECT_WALL_F
	//		// Id = 1
	//		new int[] {  0,  1,  2,  3,  4,  5,},		// _OBJECT_ROOF
	//		new int[] {  6,  7,  8,  9, 10, 11,},		// _OBJECT_FLOOR
	//		new int[] { 12, 13, 14, 15, 16, 17,},		// _OBJECT_WALL_L
	//		new int[] { 18, 19, 20, 21, 22, 23,},		// _OBJECT_WALL_R
	//		new int[] { 24, 25, 26, 27, 28, 29,}		// _OBJECT_WALL_F
	//	),
	//	////
	//	//new AcTextureChanger.Data(
	//	//	"Images/Wall/wall_1", _ROOM_TEX_WALL_DIV_X, _ROOM_TEX_WALL_DIV_Y, _ROOM_TEX_WALL_FRAME,
	//	//	new int[] {  0,  1,  2,  3,  4,  5,  6,  7,},		// _ROOM_ROOF
	//	//	new int[] {  8,  9, 10, 11, 12, 13, 14, 15,},		// _ROOM_FLOOR
	//	//	new int[] { 16, 17, 18, 19, 20, 21, 22, 23,},		// _ROOM_WALL_L
	//	//	new int[] { 24, 25, 26, 27, 28, 29, 30, 31,},		// _ROOM_WALL_R
	//	//	new int[] { 32, 33, 34, 35, 36, 37, 38, 39,}		// _ROOM_WALL_F
	//	//),
	//	////
	//	//new AcTextureChanger.Data(
	//	//	"Images/Wall/wall_2", _ROOM_TEX_WALL_DIV_X, _ROOM_TEX_WALL_DIV_Y, _ROOM_TEX_WALL_FRAME,
	//	//	new int[] {  0,  1,  2,  3,  4,  5,  6,  7,},		// _ROOM_ROOF
	//	//	new int[] {  8,  9, 10, 11, 12, 13, 14, 15,},		// _ROOM_FLOOR
	//	//	new int[] { 16, 17, 18, 19, 20, 21, 22, 23,},		// _ROOM_WALL_L
	//	//	new int[] { 24, 25, 26, 27, 28, 29, 30, 31,},		// _ROOM_WALL_R
	//	//	new int[] { 32, 33, 34, 35, 36, 37, 38, 39,}		// _ROOM_WALL_F
	//	//),
	//};

	/*
	 * 
	 */
	//private AcTextureChanger.Data[] _doorChangerTbl__ =
	//{
	//	//
	//	new AcTextureChanger.Data(
	//		"Images/Door/tx_door", 3, 9, 4,
	//		new int[] {  0,  1,  2,},		// type 0
	//		new int[] {  3,  4,  5,},		// type 1
	//		new int[] {  6,  7,  8,},		// type 2
	//		new int[] {  9, 10, 11,},		// type 3
	//		new int[] { 12, 13, 14,},		// type 4
	//		new int[] { 15, 16, 17,},		// type 5
	//		new int[] { 18, 19, 20,},		// type 6
	//		new int[] { 21, 22, 23,},		// type 7
	//		new int[] { 24, 25, 26,}		// type 8
	//	),
	//	////
	//	//new AcTextureChanger.Data(
	//	//	"Images/Door/door_1", _ROOM_TEX_DOOR_DIV_X, _ROOM_TEX_DOOR_DIV_Y, _ROOM_TEX_DOOR_FRAME,
	//	//	new int[] {  0,  1,  2,  3,  4,  5,},		// _ROOM_DOOR_L
	//	//	new int[] {  8,  9, 10, 11, 12, 13,},		// _ROOM_DOOR_C
	//	//	new int[] { 16, 17, 18, 19, 20, 21,}		// _ROOM_DOOR_R
	//	//),
	//	////
	//	//new AcTextureChanger.Data(
	//	//	"Images/Door/door_2", _ROOM_TEX_DOOR_DIV_X, _ROOM_TEX_DOOR_DIV_Y, _ROOM_TEX_DOOR_FRAME,
	//	//	new int[] {  0,  1,  2,  3,  4,  5,},		// _ROOM_DOOR_L
	//	//	new int[] {  8,  9, 10, 11, 12, 13,},		// _ROOM_DOOR_C
	//	//	new int[] { 16, 17, 18, 19, 20, 21,}		// _ROOM_DOOR_R
	//	//),
	//};

	//private AcTextureChanger.Data[] _markChangerTbl__ =
	//{
	//	//
	//	new AcTextureChanger.Data(
	//		"Images/Interior/mark_1", 8, 8, 4,
	//		new int[] {  0,  1,  2,  3,},
	//		new int[] {  4,  5,  6,  7,},
	//		new int[] {  8,  9, 10, 11,},
	//		new int[] { 12, 13, 14, 15,},
	//		new int[] { 16, 17, 18, 19,},
	//		new int[] { 20, 21, 22, 23,},
	//		new int[] { 24, 25, 26, 27,},
	//		new int[] { 28, 29, 30, 31,},
	//		new int[] { 32, 33, 34, 35,},
	//		new int[] { 36, 37, 38, 39,},
	//		new int[] { 40, 41, 42, 43,},
	//		new int[] { 44, 45, 46, 47,},
	//		new int[] { 48, 49, 50, 51,},
	//		new int[] { 52, 53, 54, 55,},
	//		new int[] { 56, 57, 58, 59,},
	//		new int[] { 60, 61, 62, 63,}
	//	),
	//};

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private GameObject[] m_vWallGameObject;
	private GameObject[] m_vDoorGameObject;
	private GameObject[] m_vMarkGameObject;

	//private AcTextureChanger m_vWallChanger;
	//private AcTextureChanger m_vDoorChanger;
	//private AcTextureChanger m_vMarkChanger;

	private int m_vUnlock;

	//private int m_vWallId;
	//private int m_vDoorId;
	//private int[] m_vMarkId;

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/*
	 * コンストラクタは普通に呼ばれてるらしい
	 * 引数のあるコンストラクタとかはどうなるのかは不明です
	 */
	public AcRoom()
	{
		//		m_bIni = false;
		Debug.Log( "Room コンストラクタ" );
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/*
	 * インスタンス生成時に引数を渡したい 
	 * http://udasankoubou.blogspot.jp/2013/01/unity.html
	 * 
	 * yahooの検索用
	 * http://search.yahoo.co.jp/search;_ylt=A2RhYM9ReypUam4AkSiJBtF7?p=unity+%E3%82%A4%E3%83%B3%E3%82%B9%E3%82%BF%E3%83%B3%E3%82%B9%E6%99%82+%E3%83%91%E3%83%A9%E3%83%A1%E3%83%BC%E3%82%BF+%E6%B8%A1%E3%81%99&search.x=1&fr=top_ga1&tid=top_ga1&ei=UTF-8
	 */

	/*
	 * イベント関数の実行順 / Execution Order of Event Functions
	 * http://docs-jp.unity3d.com/Documentation/Manual/ExecutionOrder.html
	 */

	/*
	 * プレハブは１度ロードして使いまわすので別メソッドにしてみた
	 * 重くなければ Create の中でやってもいいのだけれど
	 */
	//public static AcRoom getPrefab()
	//{
	//	/*
	//	 * なんでこんなにキャスト処理が必要なのか？
	//	 */
	//	return ( ( AcRoom ) Resources.Load( "Prefabs/Room", typeof( AcRoom ) ) );
	//}

	/*
	 * 初期値を設定できるメソッドです
	 */
	//public static AcRoom Create( AcRoom vPrefab, Vector3 vPos, Quaternion vRot, int vWallId, int vDoorId )
	//{
	//	AcRoom instance = ( AcRoom ) Instantiate( vPrefab, vPos, vRot );
	//	//
	//	instance._setRoom( vWallId, vDoorId );
	//	//
	//	return ( instance );
	//}

	public static GameObject getPrefab()
	{
		return ( ( GameObject ) Resources.Load( "Prefabs/Room" ) );
	}

	/*
	 * 初期値を設定できるメソッドです
	 */
	//public static AcRoom Create( GameObject vPrefab, Vector3 vPos, Quaternion vRot, int vWallId, int vDoorId )
	//{
	//	GameObject _instance = ( GameObject ) Instantiate( vPrefab, vPos, vRot );
	//	//
	//	AcRoom _object = _instance.GetComponent<AcRoom>();

	//	_object._create( vWallId, vDoorId );
	//	//
	//	return ( _object );
	//}

	public static AcRoom Create( GameObject vPrefab )
	{
		GameObject _object = ( GameObject ) Instantiate( vPrefab, Vector3.zero, Quaternion.identity );
		//
		AcRoom _class = _object.GetComponent<AcRoom>();
		//
		_class._create();
		//
		return ( _class );
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private void _create()
	{
		m_vWallGameObject = new GameObject[ _OBJECT_WALL_NUM ];
		m_vDoorGameObject = new GameObject[ _OBJECT_DOOR_NUM ];
		m_vMarkGameObject = new GameObject[ _OBJECT_MARK_NUM ];
		//
		//m_vWallChanger = new AcTextureChanger( _wallChangerTbl[ 0 ] );
		//m_vDoorChanger = new AcTextureChanger( _doorChangerTbl[ 0 ] );
		//m_vMarkChanger = new AcTextureChanger( _markChangerTbl[ 0 ] );
		//
		//m_vWallChanger = new AcTextureChanger();
		//m_vWallChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/tx_room", 6, 6, 4, new int[] { 0, 1, 2, 3, 4, 5, } );
		//m_vWallChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/tx_room", 6, 6, 4, new int[] { 6, 7, 8, 9, 10, 11, } );
		//m_vWallChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/tx_room", 6, 6, 4, new int[] { 12, 13, 14, 15, 16, 17, } );
		//m_vWallChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/tx_room", 6, 6, 4, new int[] { 18, 19, 20, 21, 22, 23, } );
		//m_vWallChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/tx_room", 6, 6, 4, new int[] { 24, 25, 26, 27, 28, 29, } );
		////
		//m_vDoorChanger = new AcTextureChanger();
		//m_vDoorChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/tx_door", 3, 9, 4, new int[] { 0, 1, 2, } );
		//m_vDoorChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/tx_door", 3, 9, 4, new int[] { 3, 4, 5, } );
		//m_vDoorChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/tx_door", 3, 9, 4, new int[] { 6, 7, 8, } );
		//m_vDoorChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/tx_door", 3, 9, 4, new int[] { 9, 10, 11, } );
		//m_vDoorChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/tx_door", 3, 9, 4, new int[] { 12, 13, 14, } );
		//m_vDoorChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/tx_door", 3, 9, 4, new int[] { 15, 16, 17, } );
		//m_vDoorChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/tx_door", 3, 9, 4, new int[] { 18, 19, 20, } );
		//m_vDoorChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/tx_door", 3, 9, 4, new int[] { 21, 22, 23, } );
		//m_vDoorChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/tx_door", 3, 9, 4, new int[] { 24, 25, 26, } );
		////
		//m_vMarkChanger = new AcTextureChanger();
		//m_vMarkChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/mark_1", 8, 8, 4, new int[] { 4, 5, 6, 7, } );
		//m_vMarkChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/mark_1", 8, 8, 4, new int[] { 8, 9, 10, 11, } );
		//m_vMarkChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/mark_1", 8, 8, 4, new int[] { 12, 13, 14, 15, } );
		//m_vMarkChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/mark_1", 8, 8, 4, new int[] { 16, 17, 18, 19, } );
		//m_vMarkChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/mark_1", 8, 8, 4, new int[] { 20, 21, 22, 23, } );
		//m_vMarkChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/mark_1", 8, 8, 4, new int[] { 24, 25, 26, 27, } );
		//m_vMarkChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/mark_1", 8, 8, 4, new int[] { 28, 29, 30, 31, } );
		//m_vMarkChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/mark_1", 8, 8, 4, new int[] { 32, 33, 34, 35, } );
		//m_vMarkChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/mark_1", 8, 8, 4, new int[] { 36, 37, 38, 39, } );
		//m_vMarkChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/mark_1", 8, 8, 4, new int[] { 40, 41, 42, 43, } );
		//m_vMarkChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/mark_1", 8, 8, 4, new int[] { 44, 45, 46, 47, } );
		//m_vMarkChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/mark_1", 8, 8, 4, new int[] { 48, 49, 50, 51, } );
		//m_vMarkChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/mark_1", 8, 8, 4, new int[] { 52, 53, 54, 55, } );
		//m_vMarkChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/mark_1", 8, 8, 4, new int[] { 56, 57, 58, 59, } );
		//m_vMarkChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/mark_1", 8, 8, 4, new int[] { 60, 61, 62, 63, } );

		//
		for ( int count = 0; count < _OBJECT_WALL_NUM; count++ )
		{
			m_vWallGameObject[ count ] = this.transform.FindChild( _wallObjectTbl[ count ] ).gameObject;
			//
			//			m_vWallChanger.initialize( m_vWallGameObject[ count ].renderer );
		}
		//
		for ( int count = 0; count < _OBJECT_DOOR_NUM; count++ )
		{
			m_vDoorGameObject[ count ] = this.transform.FindChild( _doorObjectTbl[ count ] ).gameObject;
			//
			//			m_vDoorChanger.initialize( m_vDoorGameObject[ count ].renderer );
		}
		//
		for ( int count = 0; count < _OBJECT_MARK_NUM; count++ )
		{
			m_vMarkGameObject[ count ] = this.transform.FindChild( _markObjectTbl[ count ] ).gameObject;
			//
			//			m_vMarkChanger.initialize( m_vMarkGameObject[ count ].renderer );
		}

		/*
		 * 子のオブジェクトを検索して取得 
		 * http://bribser.co.jp/blog/unity-find-transform-child/
		 * 
		 * http://baba-s.hatenablog.com/entry/2014/08/01/101104
		 */
		/*
		 * コンソールにテクスチャが読み込めないとかエラーが出る件
		 * http://blog.be-style.jpn.com/article/53401178.html
		 * テクスチャのインスペクターから Advance で readable にするらしい
		 */

		//m_vWallRenderer = new Renderer[ _ROOM_NUM ];
		////
		//Texture texture = ( Texture ) Instantiate( Resources.Load( texName[ m_vRoomId ] ) );
		////
		//for ( int count = 0; count < _ROOM_NUM; count++ )
		//{
		//	Transform transform = this.transform.Find( objName[ count ] );
		//	Renderer renderer = transform.gameObject.renderer;
		//	//
		//	renderer.material.mainTexture = texture;
		//	//
		//	m_vWallRenderer[ count ] = renderer;
		//}

		//		Transform transform = this.transform.Find("Doors/DoorL");
		//		Transform transform = this.transform.Find(_wall[vWallId]);
		//		Renderer renderer = transform.gameObject.renderer;

		//		m_vWallRoof = renderer;
		//		renderer.material.mainTexture = (Texture)Instantiate(Resources.Load("door_r"));
		//		renderer.material.mainTexture = (Texture)Instantiate(Resources.Load( texName [ vWallId ] ) );
		//		renderer.material.SetTextureOffset("_MainTex", new Vector2(0.5f, 0.5f));
		//		renderer.material.SetTextureScale("_MainTex", new Vector2(0.1f, 0.1f));

		//		Debug.Log("開始 Rooms");
		//		m_bIni = true;

		m_vUnlock = 0;
		//m_vWallId = 0;
		//m_vDoorId = 0;
		//m_vMarkId = new int[ _OBJECT_DOOR_NUM ];
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// 部屋を作るよ
	/// 引数で何かしらの変化をさせようとしているが、現時点では何もしていない（2014/10/24）
	/// </summary>
	/// <param name="vRoomCount"></param>
	public void setRoom( int vRoomCount )
	{
		/*
		 * UnityのC#で乱数を生成する。 « GONZNOTE
		 * http://yiida.info/blog/?p=19
		 * ランダムの話
		 * int の場合は max -1 が最大になるよ！
		 */

		//const int _door_num = 9 * 4;
		//const int _mark_num = 26;

		//m_vDoorId = Random.Range( 0, _door_num );

		/*
		 * 壁
		 * 抽選は意味なしで、５枚別々の画像を表示する
		 */
		//int _wall_num = 1;
		////
		//m_vWallId = Random.Range( 0, _wall_num );
		//
		for ( int _count = 0; _count < _OBJECT_WALL_NUM; _count++ )
		{
			AcApp.imageRender( m_vWallGameObject[ _count ].renderer, AcApp.IMAGE_WALL, _count );
		}

		/*
		 * ドア
		 * 部屋が0~9 -> 0~8 * 4 を共通で使用する
		 * 部屋が10~ -> 0~8 * 4 を行い 1~3 を２回行い色が 2/1 になるようにする（意味がわかりにくいが、マークと同じ処理です）
		 */
		const int _door_w_num = 4;
		const int _door_h_num = 9;
		//
		int _door_x = Random.Range( 0, _door_h_num ) * _door_w_num;
		//
		int[] _door = new int[ _OBJECT_DOOR_NUM ];
		//
		if ( vRoomCount < 10 )
		{
			_door[ _OBJECT_DOOR_L ] =
			_door[ _OBJECT_DOOR_C ] =
			_door[ _OBJECT_DOOR_R ] = _door_x;
		}
		else
		{
			int _door_a = Random.Range( 1, _door_w_num );
			int _door_b;
			// ハズレとアタリが同じじゃダメだよ！
			while ( true )
			{
				_door_b = Random.Range( 1, _door_w_num );
				//
				if ( _door_a != _door_b )
				{
					break;
				}
			}
			//
			_door_a += _door_x;
			_door_b += _door_x;
			//
			_door[ _OBJECT_DOOR_L ] = _door_a;
			_door[ _OBJECT_DOOR_C ] = _door_a;
			_door[ _OBJECT_DOOR_R ] = _door_a;
			//
			_door[ Random.Range( 0, _OBJECT_DOOR_NUM ) ] = _door_b;
		}
		//
		for ( int _count = 0; _count < _OBJECT_DOOR_NUM; _count++ )
		{
			AcApp.imageRender( m_vDoorGameObject[ _count ].renderer, AcApp.IMAGE_DOOR, _door[ _count ] );
		}

		////
		//if ( vRoomCount < 10 )
		//{
		//	int _index = Random.Range( 0, _door_h_num ) * _door_w_num;
		//	//
		//	for ( int _count = 0; _count < _OBJECT_DOOR_NUM; _count++ )
		//	{
		//		AcApp.imageRender( m_vDoorGameObject[ _count ].renderer, AcApp.IMAGE_DOOR, _index );
		//	}
		//}
		//else
		//{
		//	for ( int _count = 0; _count < _OBJECT_DOOR_NUM; _count++ )
		//	{
		//		int _index = Random.Range( 0, _door_h_num ) * _door_w_num + Random.Range( 1, _door_w_num );
		//		//
		//		AcApp.imageRender( m_vDoorGameObject[ _count ].renderer, AcApp.IMAGE_DOOR, _index );
		//	}
		//}

		/*
		 * マーク
		 */
		const int _mark_num = 25; // 2014/11/11 元々 26 だったが 25 に変更された、画像を詰めなおしてもらう
		//
		int[] _mark = new int[ _OBJECT_MARK_NUM ];
		//
		int _mark_a = Random.Range( 0, _mark_num ); // ハズレ
		int _mark_b; // アタリ
		// ハズレとアタリが同じじゃダメだよ！
		while ( true )
		{
			_mark_b = Random.Range( 0, _mark_num );
			//
			if ( _mark_a != _mark_b )
			{
				break;
			}
		}
		//
		_mark[ _OBJECT_MARK_L ] = _mark_a;
		_mark[ _OBJECT_MARK_C ] = _mark_a;
		_mark[ _OBJECT_MARK_R ] = _mark_a;
		// アタリの位置
		m_vUnlock = Random.Range( 0, _OBJECT_MARK_NUM );
		//
		_mark[ m_vUnlock ] = _mark_b;

		for ( int _count = 0; _count < _OBJECT_MARK_NUM; _count++ )
		{
			AcApp.imageRender( m_vMarkGameObject[ _count ].renderer, AcApp.IMAGE_MARK, _mark[ _count ] );
		}

		///*
		// * ランダム
		// * http://yiida.info/blog/?p=19
		// */
		//m_vWallId = Random.Range( 0, m_vWallChanger.getDataLength() / _OBJECT_WALL_NUM );
		//m_vDoorId = Random.Range( 0, m_vDoorChanger.getDataLength() );

		//int _mark_length = m_vMarkChanger.getDataLength();
		//int _mark_a = Random.Range( 0, _mark_length ); // ハズレ
		//int _mark_b = Random.Range( 0, _mark_length ); // アタリ
		//// ハズレとアタリが同じじゃダメだよ！
		//while ( _mark_a == _mark_b )
		//{
		//	_mark_b = Random.Range( 0, _mark_length );
		//}
		////
		//m_vMarkId[ _OBJECT_DOOR_L ] = _mark_a;
		//m_vMarkId[ _OBJECT_DOOR_C ] = _mark_a;
		//m_vMarkId[ _OBJECT_DOOR_R ] = _mark_a;
		//// アタリの位置
		//m_vUnlock = Random.Range( 0, _OBJECT_DOOR_NUM );
		////
		//m_vMarkId[ m_vUnlock ] = _mark_b;
	}

	/// <summary>
	/// 鍵の掛かっていない部屋は？
	/// </summary>
	/// <returns></returns>
	public int getUnlock()
	{
		return ( m_vUnlock );
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	// Use this for initialization
	void Start()
	{
		//Debug.Log( "Room Start" );
		//		setWall(0);
		//		initialize(0);
		//
		//		_setRoom();
	}

	// Update is called once per frame
	void Update()
	{
		//int _timer = Time.frameCount;
		//
		//for ( int _count = 0; _count < _OBJECT_WALL_NUM; _count++ )
		//{
		//	// ５枚ずつ別々の画像を表示する
		//	//			m_vWallChanger.update( m_vWallGameObject[ count ].renderer, frameCount, ( m_vWallId * _OBJECT_WALL_NUM ) + count, 0, 0 );
		//	m_vWallChanger.update( m_vWallGameObject[ _count ].renderer, _timer, ( m_vWallId * _OBJECT_WALL_NUM ) + _count, 0 );
		//}
		//
		//for ( int _count = 0; _count < _OBJECT_DOOR_NUM; _count++ )
		//{
		//	// 同じ画像を開始位置を変えて表示する
		//	//			m_vDoorChanger.update( m_vDoorGameObject[ count ].renderer, frameCount, m_vDoorId, count, 0 );
		//	m_vDoorChanger.update( m_vDoorGameObject[ _count ].renderer, _timer, m_vDoorId, _count );
		//}
		////
		//for ( int _count = 0; _count < _OBJECT_MARK_NUM; _count++ )
		//{
		//	// ３毎全部違う画像を表示する
		//	//			m_vMarkChanger.update( m_vMarkGameObject[ count ].renderer, frameCount, m_vMarkId[ count ], 0, 0 );
		//	m_vMarkChanger.update( m_vMarkGameObject[ _count ].renderer, _timer, m_vMarkId[ _count ], _count );
		//}

#if false
	
		//		if (m_bIni)
		{
			/*
			 * 時刻の取得
			 * http://nirasan.hatenablog.com/entry/2014/04/12/124239
			 * コレじゃないかも、ミリ秒的なものが知りたい
			 * 
			 * フレームを取得できるようです
			 * http://www.wisdomsoft.jp/51.html
			 */
			int frameCount = Time.frameCount;
			float sizeW = 1.0f / _ROOM_TEX_DIX_X;
			float sizeH = 1.0f / _ROOM_TEX_DIX_Y;
			//
			for ( int count = 0; count < _ROOM_NUM; count++ )
			{
				int index = ( frameCount / _ROOM_TEX_FRAME ) % ( _ROOM_TEX_DIX_X * _ROOM_TEX_DIX_Y );
				int indexX = index % _ROOM_TEX_DIX_X;
				int indexY = index / _ROOM_TEX_DIX_Y;
				//
				m_vWallRenderer[ count ].material.SetTextureOffset( "_MainTex", new Vector2( sizeW * indexX, sizeH * indexY ) );
				m_vWallRenderer[ count ].material.SetTextureScale( "_MainTex", new Vector2( sizeW, sizeH ) );
			}


			int frameCount = Time.frameCount;

			int changeCount = 4; // 60フレーム = 1秒
			int divX = 4;
			int divY = 4;

			float sizeW = 1.0f / divX;
			float sizeH = 1.0f / divY;

			int index = (frameCount / changeCount) % (divX * divY);
			int indexX = index % divX;
			int indexY = index / divY;

			m_vWallRoof.material.SetTextureOffset("_MainTex", new Vector2(sizeW * indexX, sizeH * indexY));
			m_vWallRoof.material.SetTextureScale("_MainTex", new Vector2(sizeW, sizeH));
		}
#endif
	}

	// ========================================================================== //
	// ========================================================================== //
}

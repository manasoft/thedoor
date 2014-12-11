using UnityEngine;
using System.Collections;

/// <summary>
/// Nend の広告処理をこのクラスにまとめる作戦！
/// </summary>
public class AcNend : MonoBehaviour
{
	// ========================================================================== //
	// ========================================================================== //
	/*
	 * 
	 */
	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //
	/*
	 * 2014/11/21
	 * 動的に nend 広告を出そうと思ったけど、
	 * nend の使用では、各設定を Unity のインスペクタから行わないと行けないので
	 * 全てをスクリプトから行うことは出来ない事に気づいた！
	 * ↓
	 * なので多分、親子構造のモデルを作って子のオブジェクトにそれぞれ広告を入れる（それをプレハブにしても良い）
	 * って感じになると思われる
	 * でそれの親オブジェクトにこのクラスをくっつける感じに夏と思います
	 */
	/*
	 * 2014/12/10
	 * Xcode のシミュレータでは落ちるっぽいんだよね → Xcode のログ？に "EntryPointNotFoundException" が出る
	 */
	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private const int _OBJECT_BANNER = 0;
	private const int _OBJECT_ICON = 1;
	private const int _OBJECT_NUM = 2;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private static readonly string[] _objectTbl =
	{
		"Banner",
		"Icon",
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

	///// <summary>
	///// 
	///// </summary>
	///// <param name="vString"></param>
	//private void _debugLog( string vString )
	//{
	//	ScDebug.debugLog( this.GetType().FullName + " # " + vString );
	//}

	/// <summary>
	/// コッチだと static で行けそうじゃん！
	/// </summary>
	/// <param name="vString"></param>
	private static void _debugLog( string vString )
	{
		ScDebug.debugLog( typeof( AcNend ).FullName + " # " + vString );
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	//private bool m_bEnable;

	private GameObject[] m_vGameObject;

	private NendAdBanner m_vNendAdBanner;
	private NendAdIcon m_vNendAdIcon;

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// ・Xcode の iOSシミュレータで実行するとエラーになるので切り分けようと思った
	/// ・iOSシミュレータ は RuntimePlatform.IPhonePlayer らしいので切り分けられないっぽい！
	/// </summary>
	/// <returns></returns>
	private static bool _isEnable()
	{
		RuntimePlatform[] _platforms =
		{
			// 有効
			RuntimePlatform.Android,
			RuntimePlatform.IPhonePlayer,		// Xcode の "iOSシミュレータ" でもコレになっちゃう！
#if false
			// 無効
			RuntimePlatform.WindowsEditor,		// ← Windows の Unity 上のエミュレータ
#endif
			};
		//
		//Debug.Log( "プラットフォーム表示" );
		//ScDebug.debugLog( "プラットフォーム > " + Application.platform.ToString() );
		_debugLog( "プラットフォーム > " + Application.platform.ToString() );
		//Debug.Log( "終了" );

		//
		foreach ( RuntimePlatform _platform in _platforms )
		{
			if ( Application.platform == _platform )
			{
				return( true);
			}
		}
		//
		return ( false );
	}


	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	public static AcNend Create()
	{
		if ( _isEnable() )
		{
			GameObject _prefab = ( GameObject ) Resources.Load( "Prefabs/Nend" );
			//
			GameObject _object = ( GameObject ) Instantiate( _prefab, Vector3.zero, Quaternion.identity );
			//
			AcNend _class = _object.GetComponent<AcNend>();
			//
			_class._create();
			//
			return ( _class );
		}
		else
		{
			GameObject _object = new GameObject();
			//
			AcNend _class = _object.AddComponent<AcNend>();
			//
			return ( _class );
		}
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private void _create()
	{
		_debugLog( "_create" );
		//
		//_setEnable();
		//
		_setObject();
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	//private void _setEnable()
	//{
	//	RuntimePlatform[] _platforms =
	//	{
	//		RuntimePlatform.Android,
	//	};
	//	//
	//	m_bEnable = false;
	//	//
	//	foreach ( RuntimePlatform _platform in _platforms )
	//	{
	//		if ( Application.platform == _platform )
	//		{
	//			m_bEnable = true;
	//		}
	//	}
	//}

	private void _setObject()
	{
		m_vGameObject = new GameObject[ _OBJECT_NUM ];
		//
		for ( int count = 0; count < _OBJECT_NUM; count++ )
		{
			m_vGameObject[ count ] = this.transform.FindChild( _objectTbl[ count ] ).gameObject;
		}
		//
		m_vNendAdBanner = m_vGameObject[ _OBJECT_BANNER ].GetComponent<NendAdBanner>();
		m_vNendAdIcon = m_vGameObject[ _OBJECT_ICON ].GetComponent<NendAdIcon>();
		//
		m_vNendAdBanner.Show();
		//m_vNendAdIcon.Show();
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// アイコン型広告の表示切り替え
	/// 
	/// 今回のアプリで必要なのはこれだけのハズ
	/// </summary>
	/// <param name="bSw"></param>
	public void swIcon( bool bSw )
	{
		if ( _isEnable() )
		{
			if ( bSw )
			{
				m_vNendAdIcon.Show();
			}
			else
			{
				m_vNendAdIcon.Hide();
			}
		}
	}

	/// <summary>
	/// バナー型広告の表示切り替え
	/// 
	/// アイコン型広告が表示できないのでコッチで実験
	/// </summary>
	/// <param name="bSw"></param>
	public void swBanner( bool bSw )
	{
		if ( _isEnable() )
		{
			if ( bSw )
			{
				m_vNendAdBanner.Show();
			}
			else
			{
				m_vNendAdBanner.Hide();
			}
		}
	}
	
	// ========================================================================== //
	// ========================================================================== //

#if false

	public void Show()
	{
		/*
		 * バナー型広告を表示します
		 * ※Automatic DisplayをONにしている場合はシーン起動時に内部でこのメソッドを
		 * 　呼び出しているため、初回表示時の呼び出しは不要です。
		 * 　任意のタイミングでバナー型広告を表示する際や、一度非表示にしたバナー型広告を
		 * 　再表示する際などにご利用ください。
		 */
		m_vNendAdBanner.Show();
	}

	public void Hide()
	{
		/*
		 * バナー型広告を非表示にします
		 */
		m_vNendAdBanner.Hide();
	}

	public void Resume()
	{
		/*
		 * 広告ローテーションを再開します
		 */
		m_vNendAdBanner.Resume();
	}
	public void Pause()
	{
		/*
		 * 広告ローテーションを停止します
		 */
		m_vNendAdBanner.Pause();
	}
	public void Destroy()
	{
		/*
		 * バナー型広告を破棄します
		 * ※NendAdBannerを追加したGameObject自体は破棄されません。
		 * 　また、NendAdBannerを追加したGameObjectが破棄されたタイミングで、
		 * 　内部でこのメソッドの呼び出しを行っています。
		 */
		m_vNendAdBanner.Destroy();
	}

#endif

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private class _NendAdBannerCallback : NendAdBannerCallback
	{
		/// <summary>
		/// 広告のロードが完了したタイミングで呼び出されます(※iOSのみ)
		/// </summary>
		public void OnFinishLoadBanner()
		{
			//UnityEngine.Debug.Log( "+++++ OnFinishLoadBanner" );
		}
		/// <summary>
		/// 広告がクリックされたタイミングで呼び出されます
		/// </summary>
		public void OnClickBannerAd()
		{
			//UnityEngine.Debug.Log( "+++++ OnClickBannerAd" );
		}
		/// <summary>
		/// 広告の受信に成功した場合に呼び出されます
		/// </summary>
		public void OnReceiveBannerAd()
		{
			//UnityEngine.Debug.Log( "+++++ OnReceiveBannerAd" );
		}
		/// <summary>
		/// 
		/// 広告の受信に失敗した場合に呼び出されます
		///• NendErrorCode errorCode ：エラー種別 (詳細は4. NendErrorCode についてを参照)
		///• string message ：エラーメッセージ
		///
		/// </summary>
		/// <param name="errorCode"></param>
		/// <param name="message"></param>
		public void OnFailToReceiveBannerAd( NendErrorCode errorCode, string message )
		{
			//UnityEngine.Debug.Log( "+++++OnFailToReceiveBannerAd:" + errorCode + ", " + message );
		}
		/// <summary>
		/// 広告ビューが画面上に復帰した場合に呼び出されます(※Androidのみ)
		/// </summary>
		public void OnDismissScreen()
		{
			//UnityEngine.Debug.Log( "+++++ OnDismissScreen" );
		}
	}

	// ========================================================================== //
	// ========================================================================== //
}

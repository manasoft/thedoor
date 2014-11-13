using UnityEngine;
using System.Collections;

public class Test_WebView : MonoBehaviour {

	private string url = "https://www.google.co.jp";
	WebViewObject webViewObject;

	void Start()
	{
		webViewObject = ( new GameObject( "WebViewObject" ) ).AddComponent<WebViewObject>();
//		webViewObject = this.gameObject.AddComponent<WebViewObject>();
//		webViewObject = GetComponent<WebViewObject>();
		webViewObject.Init(); //初期化
		webViewObject.LoadURL( url ); //ページの読み込み
		webViewObject.SetVisibility( true ); //WebViewを表示する
		webViewObject.SetMargins( 100, 100, 100, 300 ); //下に100pxマージンを取る
	}

	// Use this for initialization
	//void Start () {
	
	//}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;

public class Test_Resource_Manager : MonoBehaviour
{

	void OnGUI()
	{
		{
			GUILayout.Label( "Textures " + Resources.FindObjectsOfTypeAll( typeof( Texture ) ).Length );
		}
	}

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
}

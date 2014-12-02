using UnityEngine;
using System.Collections;

// FileInfo
using System.IO;

public class Test_Resource_Manager : MonoBehaviour
{

	void OnGUI()
	{
		{

			Object[] _objects = Resources.FindObjectsOfTypeAll( typeof( Texture ) );
			foreach(Object _object in _objects)
			{

//				FileInfo _file = new FileInfo();

				ScDebug.debugLog( _object.name );
			}

			GUILayout.Label( "Textures " + _objects.Length );
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

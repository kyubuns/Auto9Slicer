using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

/*
OnionRingSample/Editor/Inディレクトリにpngファイルを入れると、
OnionRingSample/Editor/Outディレクトリにsliceされたpngファイルが出力されるサンプルです。
*/

namespace OnionRing
{
	public sealed class Importer : AssetPostprocessor
	{
		private static Dictionary<string, SlicedTexture> textures = new Dictionary<string, SlicedTexture>();
		
		public void OnPostprocessTexture(Texture2D texture)
		{
			if(!assetPath.Contains("OnionRingSample/Editor/In")) return;
			
			// sliceして、pngで保存
			var slicedTexture = TextureSlicer.Slice(texture);
			File.WriteAllBytes(assetPath.Replace("/In/", "/Out/"), slicedTexture.Texture.EncodeToPNG());
			
			// border設定をするときに使うので残しておく
			textures[Path.GetFileNameWithoutExtension(assetPath)] = slicedTexture;
			
			// Refreshすると、さっき保存したpngが読み込まれる
			// delay入れないと稀にEditorごと落ちるので注意。原因不明。
			EditorApplication.delayCall += () => { AssetDatabase.Refresh(); };
		}
		
		void OnPreprocessTexture()
		{
			if(!assetPath.Contains("OnionRingSample/Editor/Out")) return;
			
			var fileName = Path.GetFileNameWithoutExtension(assetPath);
			if(!textures.ContainsKey(fileName)) return;
			
			// sliceしたtextureに対して、sprite boarderを設定する
			var slicedTexture = textures[fileName];
			var importer = assetImporter as TextureImporter;
			importer.spriteBorder = slicedTexture.Boarder.ToVector4();
			
			Debug.LogFormat("{0} Sliced!", fileName);
		}
	}
}

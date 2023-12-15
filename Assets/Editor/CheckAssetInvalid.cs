using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class CheckAssetInvalid : AssetPostprocessor
{
    private void OnPostprocessModel(GameObject gameObject)
    {
        ModelImporter importer = assetImporter as ModelImporter;

        if (importer != null)
        {
            Debug.Log("Model Import");

            AssetDatabase.CreateFolder("Assets/Textures", gameObject.name);
            AssetDatabase.CreateFolder("Assets/Materials", gameObject.name);

            importer.importCameras = false;
            importer.importLights = false;
            importer.generateSecondaryUV = true;
            importer.isReadable = false;

            Debug.Log("Model Setting Complete");
        }
    }

    private void OnPostprocessTexture(Texture2D texture)
    {
        TextureImporter importer = assetImporter as TextureImporter;

        if (importer != null)
        {
            Debug.Log("Texture Import");

            importer.isReadable = false;
            importer.textureCompression = TextureImporterCompression.Uncompressed;

            if (texture.name.Contains("Normal"))
            {
                importer.textureType = TextureImporterType.NormalMap;
                Debug.Log("Change Type");
            }

            Debug.Log("Texture Setting Complete");

        }
    }
}

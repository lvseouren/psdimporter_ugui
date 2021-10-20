using System;
using System.Collections.Generic;
using System.IO;
using PDNWrapper;
using UnityEngine;
using Unity.Collections;
using System.Linq;
using UnityEditor.AssetImporters;
using UnityEditor.U2D.Animation;
using UnityEditor.U2D.Common;
using UnityEditor.U2D.Sprites;
using UnityEngine.Assertions;
using UnityEngine.Experimental.U2D.Animation;
using UnityEngine.U2D;
using UnityEngine.U2D.Animation;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEditor.U2D.PSD
{
    /// <summary>
    /// ScriptedImporter to import Photoshop files
    /// </summary>
    // Version using unity release + 5 digit padding for future upgrade. Eg 2021.2 -> 21200000
    [ScriptedImporter(20300000, "psb", AllowCaching = true)]
    [MovedFrom("UnityEditor.Experimental.AssetImporters")]
    public class PSDImporter_UGUI : PSDImporter
    {
        public override void OnImportAsset(AssetImportContext ctx)
        {
            Debug.Log("holy shit!");
            FileStream fileStream = new FileStream(ctx.assetPath, FileMode.Open, FileAccess.Read);
            Document doc = null;
            try
            {
                doc = PaintDotNet.Data.PhotoshopFileType.PsdLoad.Load(fileStream);
                GenerateResource(doc, ctx);
            }
            finally
            {
                fileStream.Close();
                if (doc != null)
                    doc.Dispose();
                EditorUtility.SetDirty(this);
            }

            GeneratePrefab();
        }

        //����prefab
        private void GeneratePrefab()
        {
            //����psdͼ����Ϣ��һһ���ɶ�Ӧ��gameobject�ڵ㣬��������sprite����
        }

        //����ͼƬ��Դ�����ͼ��
        private void GenerateResource(Document doc, AssetImportContext ctx)
        {
            var psdLayers = new List<PSDLayer>();
            ExtractLayerTask.Execute(psdLayers, doc.Layers, true);
            var oldPsdLayers = GetPSDLayers();
            //����psd��Ϣ����ȡ���е�sprite��Ϣ��һһ���ɶ�Ӧ��sprite
            foreach (var layer in psdLayers)
            {
                var image = new NativeArray<Color32>(doc.width * doc.height, Allocator.Persistent);
                try
                {
                    var textureGenerationSettings = new TextureGenerationSettings();
                    textureGenerationSettings.assetPath = ctx.assetPath;
                    if(layer.texture.Length > 0)
                        TextureGenerator.GenerateTexture(textureGenerationSettings, layer.texture);
                }
                finally
                {
                    Debug.Log("wtf");
                }

            }
        }
    }
}

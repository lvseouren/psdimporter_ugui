using System;
using System.Collections.Generic;
using System.IO;
using PhotoshopFile;
using UnityEditor.AssetImporters;
using UnityEditor.U2D.Animation;
using UnityEditor.U2D.Common;
using UnityEditor.U2D.Sprites;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEditor.U2D.PSD
{
    /// <summary>
    /// Inspector for PSDImporter
    /// </summary>
    [CustomEditor(typeof(PSDImporter_UGUI))]
    [MovedFrom("UnityEditor.Experimental.AssetImporters")]
    public class PSDImporter_UGUIEditor : PSDImporterEditor
    { }
}

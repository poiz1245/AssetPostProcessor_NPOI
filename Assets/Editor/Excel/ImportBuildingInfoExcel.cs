using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI;

public class ImportBuildingInfoExcel : AssetPostprocessor
{
    static readonly string filePath = "Assets/Editor/Data/ObjData.xlsx";
    static readonly string ObjInfoExportPath = "Assets/Resources/Data/ObjInfoData.asset";

    [MenuItem("Excel To Scriptable/Create Data")]
    static void CreateData()
    {
        MakeObjData();
    }

    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets,
        string[] movedAssets, string[] movedFromAssetPaths)
        
    {
        foreach (string s in importedAssets)
        {
            if (s == filePath)
            {
                Debug.Log("Excel data covert start.");

                MakeObjData();

                Debug.Log("Excel data covert complete.");
            }
        }
    }

    static void MakeObjData()
    {
        ObjData data = ScriptableObject.CreateInstance<ObjData>();
        AssetDatabase.CreateAsset((ScriptableObject)data, ObjInfoExportPath);
        data.hideFlags = HideFlags.NotEditable;

        data.list.Clear();

        using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
        {
            IWorkbook book = new XSSFWorkbook(stream);
            ISheet sheet = book.GetSheetAt(1);

            for (int i = 1; i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                ObjData.Attribute a = new ObjData.Attribute();

                a.productName = (string)row.GetCell(0).StringCellValue;
                a.productCategory = (string)row.GetCell(1).StringCellValue;
                a.productMaterial = (string)row.GetCell(2).StringCellValue;
                a.Floor = (string)row.GetCell(3).StringCellValue;
                a.objectSize = (string)row.GetCell(4).StringCellValue;
                a.workFlow = (int)row.GetCell(5).NumericCellValue;
                a.startDate = (string)row.GetCell(6).StringCellValue;
                a.endDate = (string)row.GetCell(7).StringCellValue;
                a.currentStatus = (string)row.GetCell(8).StringCellValue;

               data.list.Add(a);
            }

            stream.Close();
        }

        ScriptableObject obj = AssetDatabase.LoadAssetAtPath(ObjInfoExportPath, typeof(ScriptableObject)) as ScriptableObject;
        EditorUtility.SetDirty(obj);
    }
}

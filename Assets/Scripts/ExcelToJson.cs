using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Collections.Generic;
using NPOI.HPSF;
using System;

public class ExcelToJson : AssetPostprocessor
{
    static readonly string filePath = "Assets/Data/ObjData.xlsx";
    static readonly string ObjInfoExportPath = "Assets/Data/ObjInfoData.json";

    // Json ������ ����.
    [MenuItem("Excel To Json/Create Data")]
    static void CreateData()
    {
        CreateBuildingData();
    }

    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets,
       string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string s in importedAssets)
        {
            if (s == filePath)
            {
                Debug.Log("Excel data covert start.");

                CreateBuildingData();

                Debug.Log("Excel data covert complete.");
            }
        }
    }
    // �÷��̾� json ������ ���� ( json ���� ���� )
    static void CreateBuildingData()
    {
        List<ObjData.Attribute> list = new List<ObjData.Attribute>();
        ObjData attributeList = new ObjData();
        attributeList.list = list;
     
        using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
        {
            IWorkbook book = new XSSFWorkbook(stream);
            ISheet sheet = book.GetSheetAt(1);  

            for (int ix = 2; ix < sheet.LastRowNum + 1; ++ix)
            {
                //��(row)�б�
                IRow row = sheet.GetRow(ix);

                //�ø������� ���� �ӽ� ��ü ����
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

                //����Ʈ�� �߰��ϱ�
                list.Add(a);
            }

            stream.Close();

            string jsonData = JsonUtility.ToJson(attributeList, true);
            File.WriteAllText(ObjInfoExportPath, jsonData); //��ο� ���̽� ������ ����
        }
    }
}
  

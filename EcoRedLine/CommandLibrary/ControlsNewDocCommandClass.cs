// Copyright 2006 ESRI
//
// All rights reserved under the copyright laws of the United States
// and applicable international laws, treaties, and conventions.
//
// You may freely redistribute and use this sample code, with or
// without modification, provided you include the original copyright
// notice and use restrictions.
//
// See use restrictions at /arcgis/developerkit/userestrictions.

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.SystemUI;


namespace CommandLibrary
{
  /// <summary>
  /// Summary description for ControlsNewDocCommandClass.
  /// </summary>
    [Guid("D8617627-BE80-4261-BA50-1AF218B1D99F")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("CommandLibrary.ControlsNewDocCommandClass")]
  public sealed class ControlsNewDocCommandClass : BaseCommand
  {
    #region COM Registration Function(s)
    [ComRegisterFunction()]
    [ComVisible(false)]
    static void RegisterFunction(Type registerType)
    {
      // Required for ArcGIS Component Category Registrar support
      ArcGISCategoryRegistration(registerType);

      //
      // TODO: Add any COM registration code here
      //
    }

    [ComUnregisterFunction()]
    [ComVisible(false)]
    static void UnregisterFunction(Type registerType)
    {
      // Required for ArcGIS Component Category Registrar support
      ArcGISCategoryUnregistration(registerType);

      //
      // TODO: Add any COM unregistration code here
      //
    }

    #region ArcGIS Component Category Registrar generated code
    /// <summary>
    /// Required method for ArcGIS Component Category registration -
    /// Do not modify the contents of this method with the code editor.
    /// </summary>
    private static void ArcGISCategoryRegistration(Type registerType)
    {
      string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
      ControlsCommands.Register(regKey);

    }
    /// <summary>
    /// Required method for ArcGIS Component Category unregistration -
    /// Do not modify the contents of this method with the code editor.
    /// </summary>
    private static void ArcGISCategoryUnregistration(Type registerType)
    {
      string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
      ControlsCommands.Unregister(regKey);

    }

    #endregion
    #endregion

    private IHookHelper m_hookHelper;
    private IMapControl3 m_mapControl;
    public ControlsNewDocCommandClass()
    {
      base.m_category = "CommandLibrary";
      base.m_caption = "新建...";
      base.m_message = "新建工程";
      base.m_toolTip = "新建工程";
      base.m_name = "NewProject";

      try
      {
        string bitmapResourceName = GetType().Name + ".bmp";
        base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
      }
      catch (Exception ex)
      {
        System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
      }
    }

    #region Overriden Class Methods

    /// <summary>
    /// Occurs when this command is created
    /// </summary>
    /// <param name="hook">Instance of the application</param>
    public override void OnCreate(object hook)
    {
      if (hook == null)
        return;
      if (m_hookHelper == null)
        m_hookHelper = new HookHelperClass();
      m_hookHelper.Hook = hook;
      m_mapControl = hook as IMapControl3;
    }

    /// <summary>
    /// Occurs when this command is clicked
    /// </summary>
    public override void OnClick()
    {
        //ask the user whether he'd like to save th ecurrent doc
        DialogResult res = MessageBox.Show("当前文档是否需要保存?", "消息", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (res == DialogResult.Yes)
        {
            //if yes, launch the SaveAs command
            ICommand command = new ControlsSaveAsDocCommandClass();
            command.OnCreate(m_hookHelper.Hook);
            command.OnClick();
        }

        //create a new Map instance
        IMap map = new MapClass();
        map.Name = "地图";
        m_mapControl.Map = map;
    }

    #endregion
  }
}

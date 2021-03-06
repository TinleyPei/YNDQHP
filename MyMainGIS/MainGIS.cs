﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DevComponents.DotNetBar;


using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

using MyPluginEngine;
using MyMainGIS.Library;
using System.IO;

namespace MyMainGIS
{
    public partial class MainGIS : DevComponents.DotNetBar.Office2007RibbonForm
    {
        #region 公共变量
        //地图控件对象
        private ESRI.ArcGIS.Controls.IMapControlDefault _mapControl = null;
        private ESRI.ArcGIS.Controls.IPageLayoutControlDefault _pageLayoutControl = null;
        private ESRI.ArcGIS.Controls.ITOCControlDefault _tocControl = null;

        //宿主对象
        private MyPluginEngine.IApplication _App = null;
        //保存地图数据的DataSet
        private DataSet _DataSet = null;
        //插件对象集合
        //Command集合
        private Dictionary<string, MyPluginEngine.ICommand> _CommandCol = null;
        //Tool集合
        private Dictionary<string, MyPluginEngine.ITool> _ToolCol = null;
        //ToolBar集合
        private Dictionary<string, MyPluginEngine.IToolBarDef> _ToolBarCol = null;
        //Menu集合
        private Dictionary<string, MyPluginEngine.IMenuDef> _MenuItemCol = null;
        //DockableWindow集合
        private Dictionary<string, MyPluginEngine.IDockableWindowDef> _DockableWindowCol = null;
        //当前使用的Tool
        private MyPluginEngine.ITool _Tool = null;

        //同步类
        private ControlsSynchronizer m_controlsSynchronizer = null;
        //TOCControl的esriTOOControlItemMap被右键点击后弹出的快捷菜单
        private IToolbarMenu _mapMenu = null;
        private IToolbarMenu _layerMenu = null;

        #endregion

        #region 构造函数

        public MainGIS()
        {
            InitializeComponent();

            //设置图层控件的同步控件
            //this.axTOCControl1.SetBuddyControl(this.axMapControl1.Object);
            
            //初始化公共变量
            _mapControl = axMapControl1.Object as IMapControlDefault;
            _pageLayoutControl = axPageLayoutControl1.Object as IPageLayoutControlDefault;
            _tocControl = axTOCControl1.Object as ITOCControlDefault;
            _DataSet = new DataSet();
            
            //初始化主框架
            _App = new MyPluginEngine.Application();
            _App.StatusBar = this.uiStatusBar;
            _App.MapControl = _mapControl;
            _App.PageLayoutControl = _pageLayoutControl;
            _App.TOCControl = _tocControl;
            _App.MainPlatfrom = this;
            _App.Caption = this.Text;
            _App.Visible = this.Visible;
            _App.CurrentTool = null;
            _App.MainDataSet = _DataSet;

            //让MapControl和PageLatoutControl保存同步

            m_controlsSynchronizer = new ControlsSynchronizer(_mapControl, _pageLayoutControl);          

            //在同步时同时设置好与TOCControl和ToolBarControl的buddy
            m_controlsSynchronizer.AddFrameWorkControl(axTOCControl1.Object);
            m_controlsSynchronizer.AddFrameWorkControl(axToolbarControl1.Object);

            m_controlsSynchronizer.BindControls(true);

            this._App.MainCtrlsSynchronizer = m_controlsSynchronizer;

            //TOCControl的esriTOOControlItemMap被右键点击后弹出的快捷菜单
            _mapMenu = new ToolbarMenuClass();
            //通过自己的 MapMent进行添加功能，这里只有两个功能
            _mapMenu.AddItem(new MapMenu(), 1, 0, false, esriCommandStyles.esriCommandStyleTextOnly);
            _mapMenu.AddItem(new MapMenu(), 2, 1, false, esriCommandStyles.esriCommandStyleTextOnly);
            //使用 uid
            //IUID uid = new UIDClass();
            //uid.Value = "esriControlCommands.ControlsMapFullExtentCommand";
            //_mapMenu.AddItem(uid, -1, -1, true, esriCommandStyles.esriCommandStyleIconAndText);
            _mapMenu.AddItem(new ControlsMapFindCommand(), -1, 2, true, esriCommandStyles.esriCommandStyleIconAndText);
            //使用 progid
            string progid = "esriControlCommands.ControlsMapViewMenu";
            _mapMenu.AddItem(progid, -1, -1, false, esriCommandStyles.esriCommandStyleIconAndText);
            //使用内置 Command
            _mapMenu.AddItem(new ControlsAddDataCommand(), -1 , 2, true, esriCommandStyles.esriCommandStyleIconAndText);
            _mapMenu.SetHook(this._mapControl);

            
            //2015/7/17
            //TOCControl的esriTOOControlItemLayer被右键点击后弹出的快捷菜单
            _layerMenu = new ToolbarMenuClass();
            // 分别为缩放至图层、删除图层、打开属性表   符号选择器

            //符号选择器
            LayerMenu layerMenuSymbol = new LayerMenu(this._mapControl);
            layerMenuSymbol.SymbolChanged += new LayerMenu.SymbolChangedHandler(layerMenuSymbol_SymbolChanged);
            //layerMenuSymbol.SymbolChanged += new SymbolChangedHandler(layerMenuSymbol_SymbolChanged);
            _layerMenu.AddItem(layerMenuSymbol, 1, 0, false, esriCommandStyles.esriCommandStyleTextOnly);
            
            _layerMenu.AddItem(new LayerMenu(this._mapControl), 2, 0, false, esriCommandStyles.esriCommandStyleTextOnly);
            _layerMenu.AddItem(new LayerMenu(this._mapControl), 3, 0, false, esriCommandStyles.esriCommandStyleTextOnly);
            _layerMenu.AddItem(new LayerMenu(this._mapControl), 4, 0, false, esriCommandStyles.esriCommandStyleTextOnly);
            _layerMenu.SetHook(this._mapControl);
        }

        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainGIS_Load(object sender, EventArgs e)
        {
            //从插件文件夹中获得实现插件接口的对象
            PluginCollection pluginCol = PluginHandle.GetPluginsFromDll(); 
            //解析这些插件对象,获得不同类型的插件集合
            ParsePluginCollection parsePluhinCol = new ParsePluginCollection();
            parsePluhinCol.GetPluginArray(pluginCol);
            _CommandCol = parsePluhinCol.GetCommands;
            _ToolCol = parsePluhinCol.GetTools;
            _ToolBarCol = parsePluhinCol.GetToolBarDefs;
            _MenuItemCol = parsePluhinCol.GetMenuDefs;
            _DockableWindowCol = parsePluhinCol.GetDockableWindows;

            //获得Command和Tool在UI层上的Category属性，只是纯粹的分类符号
            //可以根据不同类别插件进行UI级解析，可以编写 为uicommon uitool 
            //foreach (string categoryName in parsePluhinCol.GetCommandCategorys)
            //{
            //    //对ui进行分为不同的类别
            //    //uiCommandManager.Categories.Add(new UICommandCategory(categoryName));
            //}
            //产生UI对象 
            CreateUICommandTool(_CommandCol, _ToolCol);
            CreateToolBars(_ToolBarCol);
            CreateMenus(_MenuItemCol);
            CreateDockableWindow(_DockableWindowCol);
            //保证宿主程序启动后不存在任何默认的处于使用状态的ITool对象
            _mapControl.CurrentTool = null;
            _pageLayoutControl.CurrentTool = null;

            //主界面系统名称，获取目录下的SystemName.txt[可灵活设置]
            string sUIName = "县域尺度低丘缓坡山地开发土地优化布局系统";
            
            string sTxtFileName = System.Windows.Forms.Application.StartupPath+"\\SystemName.txt";

            if (!File.Exists(sTxtFileName))
            {
                this.FindForm().Text = sUIName;
                return;
            }

            StreamReader sr = new StreamReader(sTxtFileName, Encoding.Default);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                sUIName=line.ToString();
            }
            //this.Name = sUIName;
            
            this.FindForm().Text = sUIName;
        }

        #region 插件解析与加载
        //Dictionary<string, CCmdBtn> m_DicPlugins = new Dictionary<string, CCmdBtn>();
        Dictionary<string, MyPluginEngine.IPlugin> m_DicPlugins = new Dictionary<string, MyPluginEngine.IPlugin>();
        /// <summary>
        /// 创建Command控件并添加到CommandManager中
        /// </summary>
        /// <param name="Cmds">ICommand集合</param>
        /// <param name="Tools">ITool集合</param>
        private void CreateUICommandTool(Dictionary<string, MyPluginEngine.ICommand> Cmds, Dictionary<string, MyPluginEngine.ITool> Tools)
        {
            //遍历ICommand对象集合
            foreach (KeyValuePair<string, MyPluginEngine.ICommand> cmd in Cmds)
            {
                m_DicPlugins[cmd.Value.ToString()] = cmd.Value;
            }
            foreach (KeyValuePair<string, MyPluginEngine.ITool> tool in Tools)
            {
                m_DicPlugins[tool.Value.ToString()] = tool.Value;
            }
        }

        /// <summary>
        /// 创建ToolBar的UI层对象
        /// </summary>
        /// <param name="toolBars"></param>
        private void CreateToolBars(Dictionary<string, MyPluginEngine.IToolBarDef> toolBars)
        {
                foreach (KeyValuePair<string, MyPluginEngine.IToolBarDef> toolbar in toolBars)
                {
                    MyPluginEngine.IToolBarDef nbtoolbar = toolbar.Value;
                    //产生UICommandBar对象
                    //ToolStrip UIToolbar = new ToolStrip();
                    RibbonPanel UIToolPanel = new RibbonPanel();
                    //设置UICommandBar的属性
                    //UIToolbar.CommandManager = this.uiCommandManager;
                    UIToolPanel.Name = nbtoolbar.Name;
                    //UIToolPanel.Text = nbtoolbar.Caption;
                    UIToolPanel.Tag = nbtoolbar;
                    UIToolPanel.AccessibleName = nbtoolbar.ToString();
                    UIToolPanel.Dock = DockStyle.Fill;

                	RibbonBar UIToolBar = new RibbonBar();
                	//UIToolBar.Text = nbtoolbar.Caption;

                    //将Command和Tool插入到ToolBar中
                    MyPluginEngine.ItemDef itemDef = new ItemDef();
                    for (int i = 0; i < nbtoolbar.ItemCount; i++)
                    {
                        nbtoolbar.GetItemInfo(i, itemDef);
                        MyPluginEngine.ICommand nbcmd = null;
                        //防止ICommond类或ITool类的ID与工具栏集合中项的ID无法对应上而发生异常
                        try
                        {
                    			nbcmd = m_DicPlugins[itemDef.ID] as MyPluginEngine.ICommand;
                        }
                        catch (System.Exception ex)
                        {
                            nbcmd = null;
                        }

                        if (nbcmd != null)
                        {
                            //产生一个UICommand对象
                            //ToolStripButton UICommand = new ToolStripButton();
                            ButtonItem UICommand = new ButtonItem();
                            //UICommand.sty = ;
                            //根据ICommand的信息设置UICommand的属性
                            //UICommand.ToolTipText = nbcmd.Tooltip;
                            UICommand.Text = nbcmd.Caption;
                            UICommand.Image = nbcmd.Bitmap;
                            UICommand.AccessibleName = nbcmd.ToString();
                            UICommand.Enabled = nbcmd.Enabled;
                            //UICommand的Checked与command的属性一致
                            UICommand.Checked = nbcmd.Checked;
                            //产生UICommand是调用OnCreate方法,将主框架对象传递给插件对象
                            nbcmd.OnCreate(this._App);
                            //使用委托机制处理Command的事件
                            //所有的UICommand对象Click事件均使用this.Command_Click方法处理
                            UICommand.Click += new EventHandler(UICommand_Click);
                            //将生成的UICommand添加到CommandManager中
                            //如果分组,则在该UI对象前加上一个分隔符
                            if (itemDef.Group)
                            {
                                UIToolBar = new RibbonBar();
                                //UIToolBar.Text = nbtoolbar.Caption;
                            }
                            UIToolBar.Items.Add(UICommand);
                        }

                        //获得一个ITool对象
                        MyPluginEngine.ITool nbtool = m_DicPlugins[itemDef.ID] as MyPluginEngine.ITool;
                        if (nbtool != null)
                        {
                            //产生一个ITool对象
                            //ToolStripButton UITool = new ToolStripButton();
                            ButtonItem UITool = new ButtonItem();
                            //UITool.DisplayStyle = ToolStripItemDisplayStyle.Image;
                            //根据ITool的信息设置UITool的属性
                            //UITool.ToolTipText = nbtool.Tooltip;
                            UITool.Text = nbtool.Caption;
                            UITool.Image = nbtool.Bitmap;
                            UITool.AccessibleName = nbtool.ToString();
                            //UITool.Key = nbtool.ToString();
                            UITool.Enabled = nbtool.Enabled;
                            UITool.Checked = nbtool.Checked;
                            //产生UICommand是调用OnCreate方法,将主框架对象传递给插件对象
                            nbtool.OnCreate(this._App);
                            //使用委托机制处理Command的事件
                            //所有的UICommand对象Click事件均使用this.UITool_Click方法处理
                            UITool.Click += new EventHandler(UITool_Click);
                            //将生成的UICommand添加到CommandManager中
                            if (itemDef.Group)
                            {
                                //UIToolbar.Items.Add(new ToolStripSeparator());
                                UIToolBar = new RibbonBar();
                                //UIToolBar.Text = nbtoolbar.Caption;
                                //UIToolBar.Items.Add();
                            }
                            UIToolBar.Items.Add(UITool);
                        }
                        UIToolPanel.Controls.Add(UIToolBar);
                      }
                
                      this.MainTool.Controls.Add(UIToolPanel);
            }
        }

        /// <summary>
        /// 创建UI层的菜单栏 使用ribbonControl实现菜单栏的功能
        /// </summary>
        /// <param name="Menus"></param>
        private void CreateMenus(Dictionary<string, MyPluginEngine.IMenuDef> Menus)
        {
            //遍历Menu集合中的元素
            foreach (KeyValuePair<string, MyPluginEngine.IMenuDef> menu in Menus)
            {
                MyPluginEngine.IMenuDef nbMenu = menu.Value;
                //新建菜单对象
                //ToolStripMenuItem UIMenu = new ToolStripMenuItem();
                RibbonTabItem UIMenu = new RibbonTabItem();
                //设置菜单属性
                UIMenu.Text = nbMenu.Caption;
                UIMenu.Tag = nbMenu;
                UIMenu.AccessibleName = nbMenu.ToString();

                    //添加ribbonpanel
                    RibbonPanel MenuPanel = new RibbonPanel();
                MenuPanel.Text = nbMenu.Caption;
                    MenuPanel.Dock = DockStyle.Fill;
                    MenuPanel.Font = new System.Drawing.Font("微软雅黑", 9F);
                    UIMenu.Panel = MenuPanel;

                    // 添加 ribbonbar
                    RibbonBar MenuBar = new RibbonBar();
                    //MenuBar.Text = nbMenu.Caption;//分组标题
                    //MenuBar.Dock = DockStyle.Fill;

                    //将Menu添加MainMenu的Commands中
                    //MainMenu.Items.Add(UIMenu);
                    //将Command和Tool插入到menu中
                    //遍历每一个菜单item
                    MyPluginEngine.ItemDef itemDef = new MyPluginEngine.ItemDef();
                    for (int i = 0; i < nbMenu.ItemCount; i++)
                    {
                        //寻找该菜单对象的信息,如该菜单上的Item数量,是否为Group
                        nbMenu.GetItemInfo(i, itemDef);

                        MyPluginEngine.ITool nbtool = m_DicPlugins[itemDef.ID] as MyPluginEngine.ITool;
                        if (nbtool != null)
                        {
                            //产生一个ITool对象
                            //ToolStripMenuItem UITool = new ToolStripMenuItem();
                            //
                            ButtonItem UITool = new ButtonItem();
                            //根据ITool的信息设置UITool的属性
                            //UITool.ToolTipText = nbtool.Tooltip;
                            UITool.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
                            UITool.Text = nbtool.Caption;
                            UITool.Image = nbtool.Bitmap;
                            UITool.AccessibleName = nbtool.ToString();
                            //UITool.Key = nbtool.ToString();
                            UITool.Enabled = nbtool.Enabled;
                            UITool.Checked = nbtool.Checked;
                            //产生UICommand是调用OnCreate方法,将主框架对象传递给插件对象
                            nbtool.OnCreate(this._App);
                            //使用委托机制处理Command的事件
                            //所有的UICommand对象Click事件均使用this.UITool_Click方法处理
                            UITool.Click += new EventHandler(UITool_Click);
                            //将生成的UICommand添加到CommandManager中

                            //MenuBar.Text = nbtool.Category;
                            if (itemDef.Group)
                            {
                                MenuBar = new RibbonBar();
                            MenuBar.Text = nbtool.Category;//分组标题
                                //UIMenu.DropDownItems.Add(new ToolStripSeparator());
                            }
                            MenuBar.Items.Add(UITool);
                            //UIMenu.SubItems.Add(UITool);
                        }
                        MyPluginEngine.ICommand nbcmd = m_DicPlugins[itemDef.ID] as MyPluginEngine.ICommand;
                        if (nbcmd != null)
                        {
                            //产生一个UICommand对象
                            //ToolStripMenuItem UICommand = new ToolStripMenuItem();
                            //
                            ButtonItem UICommand = new ButtonItem();
                            UICommand.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
                            //根据ICommand的信息设置UICommand的属性
                            //UICommand.ToolTipText = nbcmd.Tooltip;
                            UICommand.Text = nbcmd.Caption;
                            UICommand.Image = nbcmd.Bitmap;
                            UICommand.AccessibleName = nbcmd.ToString();
                            UICommand.Enabled = nbcmd.Enabled;
                            //UICommand的Checked与command的属性一致
                            UICommand.Checked = nbcmd.Checked;
                            //产生UICommand是调用OnCreate方法,将主框架对象传递给插件对象
                            nbcmd.OnCreate(this._App);
                            //使用委托机制处理Command的事件
                            //所有的UICommand对象Click事件均使用this.Command_Click方法处理
                            UICommand.Click += new EventHandler(UICommand_Click);
                            //将生成的UICommand添加到CommandManager中
                            //如果分组,则在该UI对象前加上一个分隔符
                            if (itemDef.Group)
                            {
                                //UIMenu.DropDownItems.Add(new ToolStripSeparator());
                                MenuBar = new RibbonBar();
                                MenuBar.Text = UICommand.Category;
                            }
                            //UIMenu.DropDownItems.Add(UICommand);
                            MenuBar.Items.Add(UICommand);
                        }
                        MenuPanel.Controls.Add(MenuBar);
                    }

                    this.MainMenu.Controls.Add(MenuPanel);
                    this.MainMenu.Items.Add(UIMenu);
                    this.MainMenu.SelectFirstVisibleRibbonTab();
            }
        }

        /// <summary>
        ///  产生Floating Panel的UI层对象(浮动窗体)
        /// </summary>
        /// <param name="dockWindows">浮动窗体集合</param>
        private void CreateDockableWindow(Dictionary<string, MyPluginEngine.IDockableWindowDef> dockWindows)
        {
            //遍历浮动窗体插件对象的集合
            foreach (KeyValuePair<string, MyPluginEngine.IDockableWindowDef> dockWindowItem in dockWindows)
            {
                //    //创建一个浮动窗体对象
                //    MyPluginEngine.IDockableWindowDef item = dockWindowItem.Value;
                //    //产生浮动窗体对象时将主框架对象传递给插件对象
                //    item.OnCreate(_App);
                //    //创建一个浮动Panel
                //    UIPanel panel = new UIPanel();
                //    panel.FloatingLocation = new System.Drawing.Point(120, 180);
                //    panel.Size = new System.Drawing.Size(188, 188);
                //    panel.Name = item.Name;
                //    panel.Text = item.Caption;
                //    panel.DockState = PanelDockState.Floating;//浮动
                //    //对象初始化
                //    ((System.ComponentModel.ISupportInitialize)(panel)).BeginInit();
                //    panel.Id = Guid.NewGuid();
                //    //临时挂起控件的布局逻辑
                //    panel.SuspendLayout();
                //    uiPanelManager.Panels.Add(panel);
                //    UIPanelInnerContainer panelContainer = new UIPanelInnerContainer();
                //    panel.InnerContainer = panelContainer;
                //    try
                //    {
                //        //插件必须保证ChildHWND正确,否则会发生异常
                //        panelContainer.Controls.Add(item.ChildHWND);
                //        panelContainer.Location = new System.Drawing.Point(1, 27);
                //        panelContainer.Name = item.Name + "Container";
                //        panelContainer.Size = new System.Drawing.Size(188, 188);
                //        panelContainer.TabIndex = 0;
                //    }
                //    catch (Exception ex)
                //    {
                //        if (AppLog.log.IsErrorEnabled)
                //        {
                //            AppLog.log.Error("浮动窗插件的子控件没有正确加载");
                //        }
                //    }
            }
        }

        #endregion

        #region 自定义事件
        void UICommand_Click(object sender, EventArgs e)
        {

                ButtonItem pTemp = sender as ButtonItem;
                string strKey = pTemp.AccessibleName;
                //当前Command被按下时,CurrentTool设置为null
                if (_App.CurrentTool != null)
                {               
                    MyPluginEngine.ITool lastTool = _ToolCol[_App.CurrentTool];
                    //先判断上一个Tool是不是工具栏中的
                    ButtonItem lastItem = GetButtonItemFromTools(lastTool.Caption);
                    if (lastItem != null)
                    {
                        lastItem.Checked = false;
                    }
                    else//若不是工具栏中的，则判断是不是菜单栏中得到Tool
                    {
                        lastItem = GetButtonItemFromMenus(lastTool.Caption);
                        if (lastItem != null)
                        {
                            lastItem.Checked = false;
                        }
                    }
                    _App.CurrentTool = null;
                }
                _App.MapControl.CurrentTool = null;
                _App.PageLayoutControl.CurrentTool = null;

                ToolStripButton UICmd = null;

                MyPluginEngine.ICommand cmd = _CommandCol[strKey];
                ////在状态栏显示插件信息
                this.statusButton1.Text = "当前操作：" + cmd.Message;
                if (null != pTemp)
                {
                    pTemp.Checked = true;
                }
                //设置Map控件的鼠标
                axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
                axPageLayoutControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
                cmd.OnClick();
                if (null != pTemp)
                {
                    pTemp.Checked = false;
            }
        }
        void UITool_Click(object sender, EventArgs e)
        {
            try
            {

                ButtonItem pTemp = sender as ButtonItem;
                string strKey = pTemp.AccessibleName;
                MyPluginEngine.ITool tool = this._ToolCol[strKey];
                //第一次按下
                if (_App.CurrentTool == null && _mapControl.CurrentTool == null && _pageLayoutControl.CurrentTool == null)
                {
                    statusButton1.Text = "当前操作：" + tool.Message;
                    if (null != pTemp)
                    {
                        pTemp.Checked = true;
                    }                   
                    axMapControl1.MousePointer = (ESRI.ArcGIS.Controls.esriControlsMousePointer)(tool.Cursor);
                    axPageLayoutControl1.MousePointer = (ESRI.ArcGIS.Controls.esriControlsMousePointer)(tool.Cursor);
					tool.OnClick();
                    _App.CurrentTool = tool.ToString();
                    //if (null != pTemp)
                    //{
                    //    pTemp.Checked = false;
                    //}
                }
                else
                {
                    if (_App.CurrentTool == strKey)
                    {
                        //如果是连续二次按下,则使这个Tool完成操作后处于关闭状态
                        if (null != pTemp)
                        {
                            pTemp.Checked = false;
                        }
                        axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
                        axPageLayoutControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
                        _App.CurrentTool = null;
                        _App.MapControl.CurrentTool = null;
                        _App.PageLayoutControl.CurrentTool = null;
                    }
                    else
                    {
                        ////按下一个Tool后没有关闭接着去按另一个Tool,则关闭前一个Tool
                        ////获得前一个Tool
                        if (pTemp != null)
                        {
                            MyPluginEngine.ITool lastTool = _ToolCol[_App.CurrentTool];
                            //先判断上一个Tool是不是工具栏中的
                            ButtonItem lastItem = GetButtonItemFromTools(lastTool.Caption);
                            if (lastItem != null)
                            {
                                lastItem.Checked = false;
                            }
                            else//若不是工具栏中的，则判断是不是菜单栏中得到Tool
                            {
                                lastItem = GetButtonItemFromMenus(lastTool.Caption);
                                if (lastItem != null)
                                {
                                    lastItem.Checked = false;
                                }
                            }
                            _App.PageLayoutControl.CurrentTool = null;
                            _App.MapControl.CurrentTool = null;
                        }
                        //设置后一个Tool的状态
                        statusButton1.Text = "当前操作：" + tool.Message;
                        if (null != pTemp)
                        {
                            pTemp.Checked = true;
                        }
                        axMapControl1.MousePointer = (ESRI.ArcGIS.Controls.esriControlsMousePointer)(tool.Cursor);
                        axPageLayoutControl1.MousePointer = (ESRI.ArcGIS.Controls.esriControlsMousePointer)(tool.Cursor);
                        tool.OnClick();
                        _App.CurrentTool = tool.ToString();
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("发生异常，原因：" + ex.Message);
                return;
            }
        }

        /// <summary>
        /// 根据名称返回Item对象
        /// </summary>
        /// <param name="tsTools"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        private ToolStripItem GetCurBtn(ToolStrip tsTools, string p)
        {
            try
            {
                if (null == tsTools)
                {
                    return null;
                }
                for (int i = 0; i < tsTools.Items.Count; i++)
                {
                    ToolStripButton item = tsTools.Items[i] as ToolStripButton;
                    if (item == null)
                    {
                        continue;
                    }
                    if (!item.AccessibleName.ToUpper().Equals(p.ToUpper()))
                        continue;
                    return item;
                }
                return null;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 根据命令或工具的Caption查找工具栏中对应的ButtonItem
        /// </summary>
        /// <param name="cmdOrToolCaption"></param>
        /// <returns></returns>
        private ButtonItem GetButtonItemFromTools(string cmdOrToolCaption)
        {
            try
            {
                Control.ControlCollection c1 = this.MainTool.Controls;
                foreach (Control item1 in c1)
                {
                    if (item1 is RibbonPanel)
                    {
                        Control.ControlCollection c2 = (item1 as RibbonPanel).Controls;
                        foreach (Control item2 in c2)
                        {
                            if (item2 is RibbonBar)
                            {
                                SubItemsCollection c3 = (item2 as RibbonBar).Items;
                                foreach (BaseItem item3 in c3)
                                {
                                    if (item3 is ButtonItem)
                                    {
                                        ButtonItem btnItem = item3 as ButtonItem;
                                        if (btnItem.Text == cmdOrToolCaption)
                                        {
                                            return btnItem;
                                        }

                                    }

                                }

                            }

                        }
                    }
                }
                return null;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 根据命令或工具的Caption查找菜单栏中对应的ButtonItem
        /// </summary>
        /// <param name="cmdOrToolCaption"></param>
        /// <returns></returns>
        private ButtonItem GetButtonItemFromMenus(string cmdOrToolCaption)
        {
            try
            {
                //获取菜单栏中MenuPanel集合
                Control.ControlCollection c1 = this.MainMenu.Controls;
                foreach (Control item1 in c1)
                {
                    if (item1 is RibbonPanel)
                    {
                        //获取当前MenuPanel中的MenuBar集合
                        Control.ControlCollection c2 = (item1 as RibbonPanel).Controls;
                        foreach (Control item2 in c2)
                        {
                            if (item2 is RibbonBar)
                            {
                                //获取当前MenuBar中的菜单项集合
                                SubItemsCollection c3 = (item2 as RibbonBar).Items;
                                foreach (BaseItem item3 in c3)
                                {
                                    if (item3 is ButtonItem)
                                    {
                                        ButtonItem btnItem = item3 as ButtonItem;
                                        if (btnItem.Text == cmdOrToolCaption)
                                        {
                                            return btnItem;
                                        }

                                    }

                                }

                            }

                        }
                    }
                }
                return null;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 图层图标符号改变时同步刷新图层树控件
        /// </summary>
        private void layerMenuSymbol_SymbolChanged()
        {
            this.axTOCControl1.Update();
        }
        #endregion

        #region 控件事件处理

        #region MapControl事件处理
        private void axMapControl_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            try
            {
                if (_App.CurrentTool != null)
                {
                    _Tool = _ToolCol[_App.CurrentTool];
                    //左键
                    if (e.button == 1)
                    {
                        _Tool.OnMouseDown(e.button, e.shift, (int)e.mapX, (int)e.mapY);
                    }
                    else if (e.button == 2)//右键
                    {
                        _Tool.OnContextMenu(e.x, e.y);
                    }
                }
                statusButton2.Text = " 当前坐标X：" + e.mapX.ToString() + "  Y：" + e.mapY.ToString();
            }
            catch (System.Exception ex)
            {
                return;
            }                    
        }

        private void axMapControl_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            try
            {
                if (_App.CurrentTool != null)
                {
                    _Tool = _ToolCol[_App.CurrentTool];
                    _Tool.OnMouseMove(e.button, e.shift, (int)e.mapX, (int)e.mapY);
                }
                statusButton2.Text = " 当前坐标X：" + e.mapX.ToString() + "  Y：" + e.mapY.ToString();
                statusButton3.Text = "比例尺：" + ((long)(_mapControl.MapScale)).ToString();
            }
            catch (System.Exception ex)
            {
                return;
            }                      
        }

        private void axMapControl_OnMouseUp(object sender, IMapControlEvents2_OnMouseUpEvent e)
        {
            try
            {
                if (_App.CurrentTool != null)
                {
                    _Tool = _ToolCol[_App.CurrentTool];
                    _Tool.OnMouseUp(e.button, e.shift, (int)e.mapX, (int)e.mapY);
                }
                statusButton2.Text = " 当前坐标X：" + e.mapX.ToString() + "  Y：" + e.mapY.ToString();
            }
            catch (System.Exception ex)
            {
                return;
            }                   
        }

        private void axMapControl_OnKeyDown(object sender, IMapControlEvents2_OnKeyDownEvent e)
        {
            try
            {
                if (_App.CurrentTool != null)
                {
                    _Tool = _ToolCol[_App.CurrentTool];
                    _Tool.OnKeyDown(e.keyCode, e.shift);
                }
            }
            catch (System.Exception ex)
            {
                return;
            }                     
        }

        private void axMapControl_OnKeyUp(object sender, IMapControlEvents2_OnKeyUpEvent e)
        {
            try
            {
                if (_App.CurrentTool != null)
                {
                    _Tool = _ToolCol[_App.CurrentTool];
                    _Tool.OnKeyUp(e.keyCode, e.shift);
                }
            }
            catch (System.Exception ex)
            {
                return;
            }           
        }

        private void axMapControl_OnDoubleClick(object sender, IMapControlEvents2_OnDoubleClickEvent e)
        {
            try
            {
                if (_App.CurrentTool != null)
                {
                    _Tool = _ToolCol[_App.CurrentTool];
                    _Tool.OnDblClick();
                }
            }
            catch (System.Exception ex)
            {
                return;
            }                     
        }

        private void axMapControl_OnViewRefreshed(object sender, IMapControlEvents2_OnViewRefreshedEvent e)
        {
            try
            {
                if (_App != null && _App.CurrentTool != null)
                {
                    _Tool = _ToolCol[_App.CurrentTool];
                    _Tool.Refresh(0);
                }
            }
            catch (System.Exception ex)
            {
                return;
            }                     
        }

        #endregion

        #region PageLayoutControl事件处理

        private void axPageLayoutControl_OnMouseDown(object sender, IPageLayoutControlEvents_OnMouseDownEvent e)
        {
            try
            {
                if (_App != null && _App.CurrentTool != null)
                {
                    _Tool = _ToolCol[_App.CurrentTool];
                    //左键
                    if (e.button == 1)
                    {
                        _Tool.OnMouseDown(e.button, e.shift, (int)e.pageX, (int)e.pageY);
                    }
                    else if (e.button == 2)//右键
                    {
                        _Tool.OnContextMenu(e.x, e.y);
                    }
                }
            }
            catch (System.Exception ex)
            {
                return;
            }                     
        }

        private void axPageLayoutControl_OnMouseMove(object sender, IPageLayoutControlEvents_OnMouseMoveEvent e)
        {
            try
            {
                if (_App.CurrentTool != null)
                {
                    _Tool = _ToolCol[_App.CurrentTool];
                    _Tool.OnMouseMove(e.button, e.shift, (int)e.pageX, (int)e.pageY);
                }
            }
            catch (System.Exception ex)
            {
                return;
            }           
        }

        private void axPageLayoutControl_OnMouseUp(object sender, IPageLayoutControlEvents_OnMouseUpEvent e)
        {
            try
            {
                if (_App.CurrentTool != null)
                {
                    _Tool = _ToolCol[_App.CurrentTool];
                    _Tool.OnMouseUp(e.button, e.shift, (int)e.pageX, (int)e.pageY);
                }
            }
            catch (System.Exception ex)
            {
                return;
            }           
        }

        private void axPageLayoutControl_OnDoubleClick(object sender, IPageLayoutControlEvents_OnDoubleClickEvent e)
        {
            try
            {
                if (_App.CurrentTool != null)
                {
                    _Tool = _ToolCol[_App.CurrentTool];
                    _Tool.OnDblClick();
                }
            }
            catch (System.Exception ex)
            {
                return;
            }           
        }

        private void axPageLayoutControl_OnKeyDown(object sender, IPageLayoutControlEvents_OnKeyDownEvent e)
        {
            try
            {
                if (_App.CurrentTool != null)
                {
                    _Tool = _ToolCol[_App.CurrentTool];
                    _Tool.OnKeyDown(e.keyCode, e.shift);
                }
            }
            catch (System.Exception ex)
            {
                return;
            }          
        }

        private void axPageLayoutControl_OnKeyUp(object sender, IPageLayoutControlEvents_OnKeyUpEvent e)
        {
            try
            {
                if (_App.CurrentTool != null)
                {
                    _Tool = _ToolCol[_App.CurrentTool];
                    _Tool.OnKeyUp(e.keyCode, e.shift);
                }
            }
            catch (System.Exception ex)
            {
                return;
            }         
        }

        private void axPageLayoutControl_OnViewRefreshed(object sender, IPageLayoutControlEvents_OnViewRefreshedEvent e)
        {
            try
            {
                if (_App != null && _App.CurrentTool != null)
                {
                    _Tool = _ToolCol[_App.CurrentTool];
                    _Tool.Refresh(0);
                }
            }
            catch (System.Exception ex)
            {
                return;
            }
        }

        #endregion

        #region 其他控件的事件处理
        // rgb 选择需要重构，这里只写了功能，需要与快捷键一起来进行包装
        // 注意，这里currentIndex 变量比较无奈，是为了记录当前选的波段。
        // 重构后要去了这个变量。
        int currentIndex = 0;
        object legend;
        ILayer currentLayer = null;
        /// <summary>
        /// 图层树控件事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void axTOCControl_OnMouseDown(object sender, ITOCControlEvents_OnMouseDownEvent e)
        {
            
            esriTOCControlItem item = esriTOCControlItem.esriTOCControlItemNone;
            IBasicMap map = null;
            ILayer layer = null;
            object other = null;
            object index = null;

            _tocControl.HitTest(e.x, e.y, ref item, ref map, ref layer, ref other, ref index);
            if (index != null)
            {
                currentIndex = (int)index;
            }
            else 
            {
                currentIndex = 0;
            }         
            
            //确保有项目被选择
            //if (item == esriTOCControlItem.esriTOCControlItemMap)
            //{
            //    _tocControl.SelectItem(map, null);
            //}
            if (item == esriTOCControlItem.esriTOCControlItemLayer && layer == currentLayer)
            {
                _tocControl.SelectItem(layer, null);
            }
            legend = other;
            currentLayer = layer;
            //选择的是Map
            if (item == esriTOCControlItem.esriTOCControlItemMap)
            {
                //将Map信息传递给propertyGrid控件
                MapInfo _mapInfo = new MapInfo(_mapControl.Map);
                //propertyGrid.SelectedObject = _mapInfo;
                //如果是右键点击,弹出菜单
                if (e.button == 2)
                {
                    _mapMenu.PopupMenu(e.x, e.y, _tocControl.hWnd);
                }
            }
            

            //选择的是 Layer
            if (item == esriTOCControlItem.esriTOCControlItemLayer)
            {
                if(e.button == 2)
                {
                    this._mapControl.CustomProperty = layer;
                    _layerMenu.PopupMenu(e.x, e.y, _tocControl.hWnd);
                }             

            }

            //Legend  选择的是遥感影像 修改波段 RGB
            if (item == esriTOCControlItem.esriTOCControlItemLegendClass && e.button == 1)
            {
                IRasterLayer pRasterLayer = layer as IRasterLayer;
                if (pRasterLayer != null && pRasterLayer.BandCount > 1) //判断是影像后排除单波段影像。
                {
                    BandSelectorMenu.Items.Clear();
                    BandSelectorMenu.Items.Add("不可见");
                    ToolStripSeparator separator = new ToolStripSeparator();
                    BandSelectorMenu.Items.Add(separator);

                    string toolItem;
                    for (int i = 0; i < pRasterLayer.BandCount; i++)
                    {
                        toolItem = "Band_" + (i + 1);
                        BandSelectorMenu.Items.Add(toolItem);
                    }
                    IRasterRGBRenderer pRGBRender = pRasterLayer.Renderer as IRasterRGBRenderer;
                    ToolStripMenuItem VisuableItem = BandSelectorMenu.Items[0] as ToolStripMenuItem;
                    ToolStripMenuItem ChangeItem;
                    if ((int)index == 0)
                    {
                        if (pRGBRender.UseRedBand == false)
                        {
                            VisuableItem.Checked = true;
                        }
                        else
                        {
                            ChangeItem = BandSelectorMenu.Items[pRGBRender.RedBandIndex + 2] as ToolStripMenuItem;
                            ChangeItem.Checked = true;
                        }
                    }
                    if ((int)index == 1)
                    {
                        if (pRGBRender.UseGreenBand == false)
                        {
                            VisuableItem.Checked = true;
                        }
                        else
                        {
                            ChangeItem = BandSelectorMenu.Items[pRGBRender.GreenBandIndex + 2] as ToolStripMenuItem;
                            ChangeItem.Checked = true;
                        }
                    }
                    if ((int)index == 2)
                    {
                        if (pRGBRender.UseBlueBand == false)
                        {
                            VisuableItem.Checked = true;
                        }
                        else
                        {
                            ChangeItem = BandSelectorMenu.Items[pRGBRender.BlueBandIndex + 2] as ToolStripMenuItem;
                            ChangeItem.Checked = true;
                        }
                    }


                    BandSelectorMenu.Show(axTOCControl1, e.x, e.y);
                }

            }
        }

        IRasterRGBRenderer pRGBRender = new RasterRGBRendererClass();
        private void BandSelectorMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string m_sBandName = e.ClickedItem.ToString();
            int index = Convert.ToInt32(currentIndex);
            //由于弹出式菜单中第一项设置为"不可见"，表征波段索引的bandIndex取值从2开始
            int bandIndex = BandSelectorMenu.Items.IndexOf(e.ClickedItem);
            //string RedLabel, GreenLabel, BlueLabel;

            //ILegendGroup pLg = legend as ILegendGroup;          //当前图层所有图例的集合
            //ILegendClass pLc = pLg.get_Class(index);          //用于管理图例，包括名称和符号
            IRasterLayer pRasterLayer = currentLayer as IRasterLayer;
            if (pRasterLayer == null) return;
            //IRasterRGBRenderer pRGBRender = pRasterLayer.Renderer as IRasterRGBRenderer;
           
            IRasterRenderer pRasterRender = pRGBRender as IRasterRenderer;
            pRasterRender.Raster = pRasterLayer as IRaster;
            pRasterRender.Update();


            //RedIndex = pRGBRender.RedBandIndex;
            //GreenIndex = pRGBRender.GreenBandIndex;
            //BlueIndex = pRGBRender.BlueBandIndex;
            //RedLabel = pLg.get_Class(0).Label;
            //GreenLabel = pLg.get_Class(1).Label;
            //BlueLabel = pLg.get_Class(2).Label;

            ToolStripMenuItem VisuableItem = BandSelectorMenu.Items[0] as ToolStripMenuItem;
            //修改各通道的显示波段
            if (index == 0)
            {
                if (m_sBandName == "不可见")
                {
                    pRGBRender.UseRedBand = false;
                    //RedLabel = "Red: NONE";
                    //设置Checked状态
                    VisuableItem.Checked = true;
                }
                else
                {
                    pRGBRender.UseRedBand = true;
                    //pLc.Label = "Red: " + m_sBandName;
                    //RedLabel = pLc.Label;
                    //IRasterRGBRenderer中波段索引从0开始，bandIndex = 2 表示第一个波段
                    pRGBRender.RedBandIndex = bandIndex - 2;
                    //RedIndex = bandIndex - 2;
                    VisuableItem.Checked = false;
                }
            }
            else if (index == 1)
            {
                if (m_sBandName == "不可见")
                {
                    //GreenLabel = "Green: NONE";
                    pRGBRender.UseGreenBand = false;
                    VisuableItem.Checked = true;
                }
                else
                {
                    pRGBRender.UseGreenBand = true;
                    //pLc.Label = "Green: " + m_sBandName;
                    //GreenLabel = pLc.Label;
                    pRGBRender.GreenBandIndex = bandIndex - 2;
                    //GreenIndex = bandIndex - 2;
                    VisuableItem.Checked = false;
                }
            }
            else if (index == 2)
            {
                if (m_sBandName == "不可见")
                {
                    //BlueLabel = "Blue: NONE";
                    pRGBRender.UseBlueBand = false;
                    VisuableItem.Checked = true;
                }
                else
                {
                    pRGBRender.UseBlueBand = true;
                    //pLc.Label = "Blue: " + m_sBandName;
                    //BlueLabel = pLc.Label;
                    pRGBRender.BlueBandIndex = bandIndex - 2;
                    //BlueIndex = bandIndex - 2;
                    VisuableItem.Checked = false;
                }
            }
            //pRGBRender.RedBandIndex = RedIndex;
            //pRGBRender.GreenBandIndex = GreenIndex;
            //pRGBRender.BlueBandIndex = BlueIndex;


            pRasterRender.Update();
            //刷新图层
            //if (m_ViewDim == VIEW_DIM.THREE)
            //{
            //    IGlobeDisplayLayers pGlobeLayer = axGlobeControl.GlobeDisplay as IGlobeDisplayLayers;
            //    pGlobeLayer.RefreshLayer(m_pSelectLayer);
            //}
            //else if (m_ViewDim == VIEW_DIM.TWO)
            //{
            //axMapControl1.Refresh();
            pRasterLayer.Renderer = pRasterRender;
            axMapControl1.Refresh();
            //}
            //pLg.get_Class(0).Label = RedLabel;          //修改Red波段显示名称，即图例名称
            //pLg.get_Class(1).Label = GreenLabel;       //修改Green波段显示名称
            //pLg.get_Class(2).Label = BlueLabel;          //修改Blue波段显示名称
            axTOCControl1.Update();
        }
    
        /// <summary>
        /// 控制两种视图的切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_controlsSynchronizer == null)
            {
                return;
            }
            if (tabControl1.SelectedTab.Name.Equals("mapTab"))
            {
                m_controlsSynchronizer.ActivateMap();
            }
            else if (tabControl1.SelectedTab.Name.Equals("pageTab"))
            {
                m_controlsSynchronizer.ActivatePageLayout();
            }
        }
        #endregion

        private void MainMenu_Click(object sender, EventArgs e)
        {

        }

        #endregion
       
    }
}

﻿using SPAD.neXt.Interfaces.Base;
using SPAD.neXt.Interfaces.DevicesConfiguration;
using SPAD.neXt.Interfaces.Events;
using SPAD.neXt.Interfaces.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SPAD.neXt.Interfaces
{
    public interface IPanelHost
    {
        IPanelDevice DeviceAttached { get; }
        IDeviceConfiguration DeviceConfiguration { get; }
        IDeviceProfile DeviceProfile { get; }
        IProfileEventProvider ProfileEventProvider { get; }

        IPanelControl PanelControl { get; }
        Guid PanelLinkID { get; }
        bool PanelHasFocus { get; }
        bool IsVirtualDevice { get; }
        string PanelName { get; }
        string DevicePanelID { get; }
        bool SupportsDefaultProfiles { get; set; }
        bool ShowDialog(string dialogName, ISPADBaseEvent evt, EventHandler configHandler = null);

        void RegisterPanelVariables(string subCategory, IReadOnlyList<string> vars);
        void UpdatePanelVariable(string name, double value);
        double GetPanelVariable(string name);

        void NavigateToDeviceSettings();
        void DevicePowerChanged(DEVICEPOWER newPowerState);

        void AddPanelButton(UserControl button, PANEL_BUTTONPOSITION position = PANEL_BUTTONPOSITION.LAST);
        UserControl AddDropDownCommand(string buttonName, string buttonTag, ICommand command,string mainbuttonLabel = null);
        Button AddPanelButton(string buttonName, string buttonTag, ICommand command, PANEL_BUTTONPOSITION position = PANEL_BUTTONPOSITION.LAST);
        void AddPanelCheckbox(string buttonName, string buttonTag, ICommand command, bool bIsChecked, PANEL_BUTTONPOSITION position = PANEL_BUTTONPOSITION.LAST);
        void SetEventContext(string eventName, Point targetPoint, IInput input);
        IEventContext GetCurrentEventContext();
        string GetConfigurationSetting(string key);
        void NavigateToThis();
        void ClipboardSetData(string data);
        void RenamePage(string newName);
    }

    public interface IPanelControl 
    {
        event EventHandler<IPanelDeviceEventArgs> EmulatedDeviceReportReceived;
        void OnEmulateDeviceReport(IPanelDeviceEventArgs e);

        void InitializePanel(IPanelHost hostControl, string panelLabel);
        void DeinitializePanel();
        void SetExtension(IExtensionPanel ctrl);
        void PanelGotFocus();
        void PanelLostFocus();

        void SetApplicationProxy(IApplication appProxy);
        void SetApplicationExtension(IExtension ext);
        void SetSubPanelID(int subPanelID);

        string GetPanelVariableSuffix();
        IReadOnlyList<string> GetPanelVariables();
        bool AllowExternalVariableChange(string varName);
        void MonitoredChanged(string varName, bool isMonitored);
        void DeviceProfileChanged();
        string GetNavigationHint();
        IInput GetAttachedInput(string switchName);

        bool PanelCanRename { get; }
        bool PanelHasSettings { get; }
        bool PanelHasPowerSettings { get; }
        bool IsCommandSupported(string commandName);
        IProfileEventProvider ProfileEventProvider { get; }

        void ApplicationReady(BooleanEventArgs e);

        bool CreateDocumentation(IPanelDocumentation docProxy);
        bool InterceptCommand(ICommand command);
        bool SavePanelImage(string filename);
    }

    public interface IPanelDocumentation
    {
        void AddPageBreak();
        void AddSection(string heading);
        void AddParagraph(string text, string style = null);
        void AddImage(string filename);
        void AddEventDocumentation(ISPADBaseEvent evt);
    }

    public interface IProfileEventProvider
    {
        PanelOptions PanelOptions {get;}
        ISPADBaseEvent FindEvent(string bound);
        void AddEvent(ISPADBaseEvent evt, bool permanent = true);
        bool IsValidEvent(string eventName);
        void RemoveAllEvents();
        bool RemoveEvent(string bound);

        IEnumerable<KeyValuePair<string, string>> GetValidEventChoices(string commandName);
        bool IsCommandSupported(string commandName);


    }
}

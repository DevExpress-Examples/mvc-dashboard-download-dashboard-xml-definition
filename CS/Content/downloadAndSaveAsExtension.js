﻿var TOOLBAR_DOWNLOAD_ICON = '<svg id="toolbar-download" viewBox="0 0 24 24"><path fill="#39A866" d="M12 2 L2 22 L22 22 Z" /></svg>';
var TOOLBAR_SAVE_AS_ICON = '<svg version="1.1" id="toolbar-save-as" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px" viewBox="0 0 24 24" style="enable-background:new 0 0 24 24;" xml:space="preserve"><style type="text/css">.dx_white{fill:#FFFFFF;}.dx_darkgray{fill:#414141;}.dx_green{fill:#39A866;}</style><path class="dx_green" d="M21,2H3C2.4,2,2,2.4,2,3v18c0,0.6,0.4,1,1,1h3v-6h12v6h3c0.6,0,1-0.4,1-1V3C22,2.4,21.6,2,21,2z"/><rect x="14" y="18" class="dx_darkgray" width="2" height="4"/><path class="dx_darkgray" d="M15,13h8c0.6,0,1-0.4,1-1V4.4c0-0.3-0.1-0.5-0.3-0.7l-2.4-2.4C21.1,1.1,20.9,1,20.6,1H15c-0.6,0-1,0.4-1,1v10C14,12.6,14.4,13,15,13z"/><polygon class="dx_white" points="16,11 22,11 22,5 20,5 20,3 16,3 "/></svg>';

function DownloadAndSaveAsDashboardExtension(dashboardControl) {
    var _this = this;
    this.name = "save-as";
    this.newName = "New Dashboard Name";
    this.toolbox = dashboardControl.findExtension("toolbox");

    DevExpress.Dashboard.ResourceManager.registerIcon(TOOLBAR_DOWNLOAD_ICON);
    DevExpress.Dashboard.ResourceManager.registerIcon(TOOLBAR_SAVE_AS_ICON);

    this._menuSaveAsItem = new DevExpress.Dashboard.Designer.DashboardMenuItem("save-as", "Save As...", 120, 0, function() { _this.showSaveAsPopup(); });
    this._menuSaveAsItem.hasSeparator = true;
    this._menuSaveAsItem.data = _this;

    this._toolbarGroup = new DevExpress.Dashboard.Designer.DashboardToolbarGroup("save", "Save", 60,
        new DevExpress.Dashboard.Designer.DashboardToolbarItem("download", function () { window.open(baseDevExpressDemoUrl + "Home/Xml/?dashboardId=" + dashboardControl.dashboardContainer().id, '_blank'); }, "toolbar-download", "Download"),
        new DevExpress.Dashboard.Designer.DashboardToolbarItem("save-as", function () { _this.showSaveAsPopup(); }, "toolbar-save-as", "Save As...")
    );

    this.showSaveAsPopup = function () {
        _this.popup.show();
        $("#textBoxContainer").dxTextBox({
            value: _this.newName,
            onValueChanged: function (e) {
                _this.newName = e.value;
            }
        });
    }
    this.hideSaveAsPopup = function () {
        dashboardControl.findExtension("toolbox").menuVisible(false);
        _this.popup.hide();
    }
    this.popup = createPopup(function () {
        dashboardControl.findExtension("create-dashboard").performCreateDashboard(_this.newName, dashboardControl.dashboard().getJSON());
        _this.hideSaveAsPopup();
    }, function () {
        _this.hideSaveAsPopup();
    });
}
DownloadAndSaveAsDashboardExtension.prototype.start = function () {
    this.toolbox.menuItems.push(this._menuSaveAsItem);
    this.toolbox.toolbarGroups.push(this._toolbarGroup);
    this.toolbox.menuItems().filter(function (item) { return item.id === "save" })[0].hasSeparator = false;
};
DownloadAndSaveAsDashboardExtension.prototype.stop = function () {
    this.toolbox.menuItems.remove(this._menuSaveAsItem);
    this.toolbox.toolbarGroups.remove(this._toolbarGroup);
};

function createPopup(saveButtonAction, cancelButtonAction) {
    return $("#popupContainer").dxPopup({
        title: "Save As...",
        width: 400,
        height: 185,
        toolbarItems: [{
            toolbar: "bottom",
            widget: "dxButton",
            location: "after",
            options: {
                text: "Save",
                onClick: saveButtonAction
            }
        }, {
            toolbar: "bottom",
            widget: "dxButton",
            location: "after",
            options: {
                text: "Cancel",
                onClick: cancelButtonAction
            }
        }]
    }).dxPopup("instance");
}
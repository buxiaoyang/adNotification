$("#btnAddUser").click(function () {
    filterClick();
    $("#dialog").dialog({
        resizable: false,
        height: 550,
        width: 700,
        modal: true,
        buttons: {
            OK: function () {
                $(this).dialog("close");
            }
        }
    });
});


$("#btnFilterUser").click(function () {
    filterClick();
});

function filterClick() {
    $.getJSON("/LDAP/GetList?filter=" + $("#filterTitle").val(), function (data) {
        if (data.result) {
            $("#userFilterList").html(getUL(data.data));
            buildLiClick($("#userFilterList"));
        }
    });
}

function buildLiClick(root)
{
    $(root).find("a").click(function (event) {
        addUser($(this));
        //stop bubble up
        if (event && event.stopPropagation) {
            event.stopPropagation();
        }
        else {
            window.event.cancelBubble = true;
        }
    });

    $(root).find("i[type='group']").click(function (event) {
        //alert($(this).attr("path"));
        var thisI = $(this);
        var thisLi = $(thisI).closest("li");
        var thisLiUL = $(thisLi).children("ul");
        if (thisI.hasClass("icon-plus"))
        {
            if (thisLiUL.length != 0)
            {
                $(thisLiUL).css("display", "");
                $(thisI).removeClass("icon-plus");
                $(thisI).addClass("icon-minus");
            }
            else
            {
                $.getJSON("/LDAP/GetListInGroup?group=" + $(thisI).attr("path"), function (data) {
                    $(thisI).removeClass("icon-plus");
                    $(thisI).addClass("icon-minus");
                    $(thisLi).append(getUL(data.data));
                    buildLiClick($(thisLi).children("ul")); 
                });
            }
        }
        else
        {
            $(thisI).removeClass("icon-minus");
            $(thisI).addClass("icon-plus");
            $(thisLiUL).css("display", "none");
        }

        //stop bubble up
        if (event && event.stopPropagation) {
            event.stopPropagation();
        }
        else {
            window.event.cancelBubble = true;
        }
    });
}

function getUL(data)
{
    var html = " <ul class='nav nav-list'>";
    for (var i = 0; i < data.length; i++) {
        var icon = "";
        if (data[i].TYPE == "user") {
            icon = "icon-user-md";
        }
        else if (data[i].TYPE == "group") {
            icon = "icon-plus";
        }
        html += '<li>';
        html += '<a href="javascript:void(0)">';
        html += '<i type="' + data[i].TYPE + '" path="' + data[i].PATH + '" account="' + data[i].ACCOUNT + '" class="icon-white ' + icon + '"></i>' + data[i].NAME + " (" + data[i].ACCOUNT + ")";
        html += '</a>';
        html += '</li>';
    }
    html += "</ul>";
    return html;
}

function addUser(thisA)
{
    var thisI = $(thisA).children("i");
    $("#btnAddUserList").find("ul").append(getUserLi(thisI.attr("type"), thisI.attr("path"), thisI.attr("account"), $(thisA).text(), true));
    buildRemoveUserFunction();
    refreshTo();
}

function getUserLi(type, path, account, text, hasRemove)
{
    var icon = "";
    if (type == "user") {
        icon = "icon-user-md";
    }
    else if (type == "group") {
        icon = "icon-group";
    }
    var html = ""
    html += '<li style="cursor:default;">';
    html += '<a type="' + type + '" path="' + path + '" account="' + account + '" href="javascript:void(0)"  style="cursor:default;">';
    html += '<i class="icon-white ' + icon + '"></i>' + text;
    if (hasRemove)
    {
        html += ' &nbsp; &nbsp;<i  style="cursor:pointer;" class="icon-white icon-remove"></i>'
    }
    html += '</a>';
    html += '</li>';
    return html;
}

function buildRemoveUserFunction()
{
    var removeI = $("#btnAddUserList").find("i.icon-remove");
    removeI.unbind("click");
    removeI.click(function () {
        $(this).closest("li").remove();
        refreshTo();
    });
}

function refreshTo()
{
    var value = "";
    $("#btnAddUserList").find("a").each(function () {
        value += "type=" + $(this).attr("type") + "%;account=" + $(this).attr("account") + "%;name=" + $.trim($(this).text().replace(" (" + $(this).attr("account") + ")", "")) + "%;path=" + $(this).attr("path") + ";#;";
    });
    $("#to").val(value);
}

function refreshFrom(value, hasRemove)
{
    var items = value.split(";#;");
    var html = "";
    for (var i = 0; i < items.length; i++) {
        var attrs = items[i].split("%;");
        if (attrs.length == 4) {
            html += getUserLi(attrs[0].substring(5), attrs[3].substring(5), attrs[1].substring(8), attrs[2].substring(5) + " (" + attrs[1].substring(8) + ")", hasRemove);
        }
    }
    $("#btnAddUserList").find("ul").append(html);
}

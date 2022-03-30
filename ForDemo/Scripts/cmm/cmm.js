function showMaintainDgvTable(target, data, column, url) {
    //bootstrap table初始化数据
    target.bootstrapTable({
        contentType: 'application/json,charset=utf-8',
        showPaginationSwitch: true,
        classes: "table table-bordered table-striped table-hover clickable",
        rowStyle: 'rowStyle',
        theadClasses: "sampletable",
        pagination: true,
        search: true,
        //fixedColumns: true,
        //height: 630,
        pageSize: 10,
        pageList: [5, 10, 20, 50, 100],
        columns: column,
        data: data,
        url: url
    });
}
function formatTableUnit(value, row, index) {
    return {
        css: {
            "white-space": "nowrap",
            "text-overflow": "ellipsis",
            "overflow": "hidden",
            "max-width": "150px"
        }
    };
}
function paramsMatter(value, row, index) {
    var span = document.createElement("span");
    span.setAttribute("title", value);
    span.innerHTML = value;
    return span.outerHTML;
}
function CallAjax(url, type, datatype, data, isasync = false) {
    return $.ajax({
        async: isasync,
        url: url,
        type: type,
        datatype: datatype,
        data: data

    });
} 
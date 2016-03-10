//url:请求地址
//submitData;提交数据（可以是form id，也可以是json字符串）
//successUrl:成功后返回的url地址，可为“”
//errorContent:错误信息
//successContent:成功信息
jQuery.extend({
    BMSubmit: function (url, submitData, successUrl, errorContent, successContent,callback) {
        var data;
        if ($.type(submitData)==="string") {
          data = $('#' + submitData).serialize();
        } else {
            data = submitData;
        }
        jQuery.ajax({
            url: url,
            data: data,
            cache: false,
            type: "POST",
            success: function (data) {
                if (data == 1) {
                    if (successContent != "" && successContent != undefined) {
                        if (successUrl !== "") {
                            layer.alert(successContent, function () {
                                location.href = successUrl;
                            });
                            
                        } else {
                            layer.alert(successContent);
                        }
                    }
                    if (callback != undefined) {
                        layer.closeAll();
                        callback();
                       
                    }
                } else {
                    layer.alert(errorContent);
                }
            }
        });
    }
})
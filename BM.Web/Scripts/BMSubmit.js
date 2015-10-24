jQuery.extend({
    BMSubmit:function(url,formId,successUrl) {
        jQuery.ajax({
            url: url,
            data: $('#' + formId).serialize(),
            cache: false,
            type:"POST",
            success: function (data) {
                if (data == 1) {
                    location.href = successUrl;
                } else {
                    layer.alert('登录失败，请确认用户名和密码！');
                }
            }
        });
    }
})
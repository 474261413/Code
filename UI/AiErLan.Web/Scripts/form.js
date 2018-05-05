/* 常用表单验证封装
 * BY bruce 2013-05-27
 */
var shapebase = new ShapeBase();
function ShapeBase() {
    //formobj = document.getElementsByName(frmname);
    /**
	 * 非空校验
	 */
    ShapeBase.notEmpty = function (val, strNote) {
        var tempValue = ShapeBase.trim(val);
        if (tempValue == "") {
              $.toast("【" + strNote + "】不能为空！");
            return false;
        }
        return true;
    }
    /**
	 * 验证数字 
	 */
    ShapeBase.isNumber = function (val, strNote, canImpty) {
        val = ShapeBase.trim(val)
        if (canImpty == 1) {
            if (val == "") {
                return true;
            }
        }
       
        //  var patrn = /^\d|([1-9][0-9]{1,})$/; 
        var patrn = /^\d+(\.\d+)?$/;
        //ar reg = new RegExp("^[0-9]*$");
        var reg = new RegExp("^[0-9]+(.[0-9]{3})?$");
        if (!patrn.test(val)) {
              $.toast("【" + strNote + "】必须由数字组成!");
            return false;
        }

        return true;
    }
    /**
	 * 验证数字
	 * @param minLen:最小长度,不校验传0;maxLen:最大长度,不校验传0
	 */
    ShapeBase.isDigit = function (val, minLen, maxLen, strNote) {
        var patrn = /^\d|([1-9][0-9]{1,})$/;
        if (!patrn.exec(val)) {
              $.toast("【" + strNote + "】必须由数字组成!");
            return false;
        }
        //长度验证
        return ShapeBase.lengthLimit(obj, minLen, maxLen, strNote);
        return true;
    }
    /**
	 * 校验登录名：只能输入5-20个以字母开头、可带数字、“_”、“.”的字串 
	 */
    ShapeBase.checkUserName = function (val) {
        var patrn = /^[a-zA-Z]{1}([a-zA-Z0-9]|[._]){4,19}$/;
        if (!patrn.exec(val)) {
              $.toast("登录名不合法！登录名只能由5-20个字母、数字、下划线组成，并以字母开头！");
            return false;
        }
        return true;
    }
    /**
	 * 校验密码：只能输入6-20个字母、数字、下划线 
	 */
    ShapeBase.checkPwd = function (val) {
        var patrn = /^(\w){6,20}$/;
        if (!patrn.exec(val)) {
              $.toast("密码格式不合法！只能输入字母、数字、下划线！");
            return false;
        }
        return true;
    }
    /**
	 * 校验密码：两次密码是否一致
	 */
    ShapeBase.checkRepeatPwd = function (val1, val2) {
        if (val1 != val2) {
              $.toast("密码不一致！请重新输入");

            return false;
        }
        return true;
    }
    /**
	 * 校验普通电话、传真号码：可以“+”开头，除数字外，可含有“-” 
	 * @param canImpty:1,可空;0,必填
	 */
    ShapeBase.checkTel = function (val, canImpty) {
        if (canImpty == 1) {
            if (val == "") {
                return true;
            }
        }
        var patrn = /^[+]{0,1}(\d){1,3}[ ]?([-]?((\d)|[ ]){1,12})+$/;
        if (!patrn.exec(val)) {
              $.toast("电话/传真号码格式不正确！");
            return false;
        }
        return true;
    }
    /**
	 * 验证手机号
	 * @param canImpty:1,可空;0,必填
	 */
    ShapeBase.checkMobile = function (val, canImpty, strNote) { 
        val = ShapeBase.trim(val)
        if (canImpty == 1) {
            if (val == "") {
                return true;
            }
        }
        if (!(/^1[\d]{10}$/g.test(val))) {
            if (strNote == null || strNote == "" || strNote == "undefined") {
                  $.toast("手机号码格式不正确");
            } else {
                  $.toast("【" + strNote + "】手机号码格式不正确");
            }

            return false;
        }
        return true;
    }
    /**
	 * 只能是汉字
	 */
    ShapeBase.isChinese = function (val, strNote) {
        var reg = /^[\u4e00-\u9fa5]+$/gi;
        if (!reg.test(val)) {
              $.toast("【" + strNote + "】必须为汉字!");
            //obj.select();
            return false;
        }
        return true;
    }
    /**
	 * 长度限制
	 * @param obj:对象,minLen:最小长度,maxLen:最大长度
	 */
    ShapeBase.lengthLimit = function (val, minLen, maxLen, strNote) {
        if (minLen > 0 && maxLen > 0) {
            if (val.length < minLen || val.length > maxLen) {
                  $.toast("【" + strNote + "】长度必须是" + minLen + "至" + maxLen + "个字符!");
            }
        }
        else if (maxLen > 0) {
            if (val.length > maxLen) {
                  $.toast("【" + strNote + "】不能超过" + maxLen + "个字符！");
                //obj.select();
                return false;
            }
        }
        else if (minLen > 0) {
            if (val.length < minLen) {
                  $.toast("【" + strNote + "】不能少于" + minLen + "个字符！");
                //obj.select();
                return false;
            }
        }
        return true;
    }
    /**
	 * 验证邮箱格式
	 * @param canImpty:1,可空;0,必填
	 */
    ShapeBase.isEmail = function (val, canImpty) {
        if (canImpty == 1) {
            if (val == "") {
                return true;
            }
        }
        if (val.search(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/) != -1) {
            return true;
        }
        else {
              $.toast("邮箱格式不正确!");
            //obj.select();
            return false;
        }
    }
    /**  
	 * 身份证15位编码规则：dddddd yymmdd xx p   
	 * dddddd：地区码   
	 * yymmdd: 出生年月日   
	 * xx: 顺序类编码，无法确定   
	 * p: 性别，奇数为男，偶数为女  
	 * <p />  
	 * 身份证18位编码规则：dddddd yyyymmdd xxx y   
	 * dddddd：地区码   
	 * yyyymmdd: 出生年月日   
	 * xxx:顺序类编码，无法确定，奇数为男，偶数为女   
	 * y: 校验码，该位数值可通过前17位计算获得  
	 * <p />  
	 * 18位号码加权因子为(从右到左) Wi = [ 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2,1 ]  
	 * 验证位 Y = [ 1, 0, 10, 9, 8, 7, 6, 5, 4, 3, 2 ]   
	 * 校验位计算公式：Y_P = mod( ∑(Ai×Wi),11 )   
	 * i为身份证号码从右往左数的 2...18 位; Y_P为脚丫校验码所在校验码数组位置  
	 *   
	 * @param canImpty:1,可空;0,必填
	 */
    ShapeBase.checkIdCard = function (val, canImpty) {
        idCard = ShapeBase.trim(val.replace(/ /g, ""));
        if (canImpty == 1) {
            if (val == "") {
                return true;
            }
        }
        if (idCard.length == 15) {
            return ShapeBase.isValidityBrithBy15IdCard(idCard);
        }
        else if (idCard.length == 18) {
            var a_idCard = idCard.split("");// 得到身份证数组   
            if (ShapeBase.isValidityBrithBy18IdCard(idCard) && ShapeBase.isTrueValidateCodeBy18IdCard(a_idCard)) {
                return true;
            }
            else {
                return false;
            }
        }
        else {
              $.toast("身份证号长度必须是15位或18位！");
            return false;
        }
    }
    /**  
	 * 判断身份证号码为18位时最后的验证位是否正确  
	 * @param a_idCard 身份证号码数组  
	 * @return  
	 */
    ShapeBase.isTrueValidateCodeBy18IdCard = function (a_idCard) {
        var sum = 0; // 声明加权求和变量   
        var Wi = [7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2, 1];// 加权因子   
        var ValideCode = [1, 0, 10, 9, 8, 7, 6, 5, 4, 3, 2];// 身份证验证位值.10代表X   
        if (a_idCard[17].toLowerCase() == 'x') {
            a_idCard[17] = 10;// 将最后位为x的验证码替换为10方便后续操作   
        }
        for (var i = 0; i < 17; i++) {
            sum += Wi[i] * a_idCard[i];// 加权求和   
        }
        valCodePosition = sum % 11;// 得到验证码所位置   
        if (a_idCard[17] == ValideCode[valCodePosition]) {
            return true;
        }
        else {
              $.toast("身份证号码格式不正确！请确认");
            return false;
        }
    }
    /**  
     * 验证18位数身份证号码中的生日是否是有效生日  
     * @param idCard 18位书身份证字符串  
     * @return  
     */
    ShapeBase.isValidityBrithBy18IdCard = function (idCard18) {
        var year = idCard18.substring(6, 10);
        var month = idCard18.substring(10, 12);
        var day = idCard18.substring(12, 14);
        var temp_date = new Date(year, parseFloat(month) - 1, parseFloat(day));
        // 这里用getFullYear()获取年份，避免千年虫问题   
        if (temp_date.getFullYear() != parseFloat(year) || temp_date.getMonth() != parseFloat(month) - 1 || temp_date.getDate() != parseFloat(day)) {
              $.toast("身份证号格式不正确！出生日期有误!");
            return false;
        }
        else {
            return true;
        }
    }
    /**  
	 * 验证15位数身份证号码中的生日是否是有效生日  
	 * @param idCard15 15位书身份证字符串  
	 * @return  
	 */
    ShapeBase.isValidityBrithBy15IdCard = function (idCard15) {
        var year = idCard15.substring(6, 8);
        var month = idCard15.substring(8, 10);
        var day = idCard15.substring(10, 12);
        var temp_date = new Date(year, parseFloat(month) - 1, parseFloat(day));
        // 对于老身份证中的你年龄则不需考虑千年虫问题而使用getYear()方法   
        if (temp_date.getYear() != parseFloat(year) || temp_date.getMonth() != parseFloat(month) - 1 || temp_date.getDate() != parseFloat(day)) {
              $.toast("身份证号格式不正确！出生日期有误!");
            return false;
        }
        else {
            return true;
        }
    }
    /**  
	 * 通过身份证判断是男是女  
	 * @param idCard 15/18位身份证号码   
	 * @return 'female'-女、'male'-男  
	 */
    ShapeBase.getSexByIdCard = function (idCard) {
        idCard = ShapeBase.trim(idCard.replace(/ /g, ""));// 对身份证号码做处理。包括字符间有空格。   
        if (idCard.length == 15) {
            if (idCard.substring(14, 15) % 2 == 0) {
                return 'female';
            }
            else {
                return 'male';
            }
        }
        else if (idCard.length == 18) {
            if (idCard.substring(14, 17) % 2 == 0) {
                return 'female';
            }
            else {
                return 'male';
            }
        }
        else {
            return null;
        }
    }
    /**  
	 * 通过身份证获取出生日期
	 * @param idCard 15/18位身份证号码   
	 * @return tempStr:出生日期 
	 */
    ShapeBase.getBirthdayByIdCard = function (idCard) {
        var tmpStr = "";
        var idDate = "";
        var tmpInt = 0;
        var strReturn = "";

        idCard = ShapeBase.trim(idCard);
        if (idCard.length == 15) {
            tmpStr = idCard.substring(6, 12);
            tmpStr = "19" + tmpStr;
            tmpStr = tmpStr.substring(0, 4) + "-" + tmpStr.substring(4, 6) + "-" + tmpStr.substring(6);
        }
        else {
            tmpStr = idCard.substring(6, 14);
            tmpStr = tmpStr.substring(0, 4) + "-" + tmpStr.substring(4, 6) + "-" + tmpStr.substring(6);
        }
        return tmpStr;
    }
    /**
	 * 日期验证
	 * @param :obj,日期对象,值格式:YYYY-mm-dd
	 */
    ShapeBase.checkDate = function (val) {
        var strDate = val;
        var result = strDate.match(/((^((1[8-9]\d{2})|([2-9]\d{3}))(-)(10|12|0?[13578])(-)(3[01]|[12][0-9]|0?[1-9])$)|(^((1[8-9]\d{2})|([2-9]\d{3}))(-)(11|0?[469])(-)(30|[12][0-9]|0?[1-9])$)|(^((1[8-9]\d{2})|([2-9]\d{3}))(-)(0?2)(-)(2[0-8]|1[0-9]|0?[1-9])$)|(^([2468][048]00)(-)(0?2)(-)(29)$)|(^([3579][26]00)(-)(0?2)(-)(29)$)|(^([1][89][0][48])(-)(0?2)(-)(29)$)|(^([2-9][0-9][0][48])(-)(0?2)(-)(29)$)|(^([1][89][2468][048])(-)(0?2)(-)(29)$)|(^([2-9][0-9][2468][048])(-)(0?2)(-)(29)$)|(^([1][89][13579][26])(-)(0?2)(-)(29)$)|(^([2-9][0-9][13579][26])(-)(0?2)(-)(29)$))/);
        if (result == null) {
              $.toast("请输入正确的日期格式！,例如：2009-01-01。\r\n 或从时间列表中选择日期。");
            return false;
        }
        return true;
    }
    /**
	 * 月份验证
	 * @param :obj,月份对象,格式:YYYY-mm
	 */
    ShapeBase.checkMon = function (val) {
        var strDate = val;
        var result = strDate.match(/^(19|20)\d{2}-((0[1-9])|(1[0-2]))$/);
        if (result == null) {
              $.toast("请输入正确的日期格式！如：2013-05");
            return false;
        }
        return true;
    }
    /**
	 * 字符串处理
	 * 去掉首尾空格
	 */
    ShapeBase.trim = function (str) {
        if (str == undefined) {
            return "";
        }
        if (str!=null) {
             return str.replace(/(^\s*)|(\s*$)/g, "");
        }
     
    }
    /**
	 * 重置表单
	 * @para formseq:表单序号
	 */
    ShapeBase.resetForm = function (formSeq) {
        var obj = null;
        for (var i = 0; i <= document.forms[0].elements.length - 1; i++) {
            obj = frm1.elements[i];
            if (obj.tagName == "INPUT" && obj.type == "text") {
                obj.setAttribute("value", "");
            }
            if (obj.tagName == "INPUT" && obj.type == "checkbox") {
                obj.setAttribute("checked", false);
            }
            if (obj.tagName == "SELECT") {
                obj.options[0].selected = true;
            }
        }
        return false;
    }
}
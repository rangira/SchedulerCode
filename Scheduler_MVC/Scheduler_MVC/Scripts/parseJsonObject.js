/*var JsonObject = [
    { "Disabled": false, "Group": null, "Selected": false, "Text": "09:00 AM", "Value": null },
    { "Disabled": false, "Group": null, "Selected": false, "Text": "10:30 AM", "Value": null },
    { "Disabled": false, "Group": null, "Selected": false, "Text": "12:30 PM", "Value": null },
    { "Disabled": false, "Group": null, "Selected": false, "Text": "02:00 PM", "Value": null },
    { "Disabled": false, "Group": null, "Selected": false, "Text": "03:30 PM", "Value": null },
    { "Disabled": false, "Group": null, "Selected": false, "Text": "05:00 PM", "Value": null },
    { "Disabled": false, "Group": null, "Selected": false, "Text": "06:30 PM", "Value": null }
];*/
parseObject = function (obj, key) {
    //console.log(obj);
    //console.log(key);
   
    this.obj = obj;
    this.obj = JSON.parse(this.obj);
    //console.log(this.obj);
    this.key = key;
    this.lst = [];
};
parseObject.prototype = {
    iterateEach: function (obj, len) {
        for (var i = 0; i < len; i++) {
            //console.log("obj[i] in iterateEch" + obj[i]);
            this.parseJson(JSON.stringify(obj[i]));
        }
    },
    parse: function () {
        /*console.log("in parse");
        console.log("this.obj" + this.obj);
        console.log(this.obj.length);
        console.log(this.key);*/
        this.iterateEach(this.obj, this.obj.length);
        return this.lst.toString();

    },

    listOnKey: function (val) {
        /*console.log("flow inside function");
        console.log("the lst is " + this.lst);*/
        this.lst.push(val);
        
        //console.log("the lst is "+this.lst.toString());

    },

    valueOfKey: function (obj) {
        /*console.log("this.key in valueOfkey" + this.key);
        console.log("obj[this.key]" + obj[this.key]);
        console.log("check after");*/
        this.listOnKey(obj[this.key]);
    },
    parseJson: function (tuple) {
        var obj = tuple;
        //console.log("obj in parseJson" + obj);
        this.valueOfKey(JSON.parse(obj));
    }
};
//var T = new parseObject(JsonObject, "Text");

//console.log("The list is " + T.parse());


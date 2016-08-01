function ToJavaScriptDate(value) {
    var pattern = /Date\(([^)]+)\)/;
    var results = pattern.exec(value);
    var dt = new Date(parseFloat(results[1]));
    console.log(dt);
    console.log(dt.getMonth());
    console.log((dt.getMonth() + 1) + "/" + dt.getDate() + "/" + dt.getFullYear());
}

ToJavaScriptDate("/Date(1465396200000)/");
ToJavaScriptDate("/Date(1465850944159)/");
moment("Mon Jun 27 2016 00:00:00 GMT-0400", "YYYY-MM-DD HH:mm Z");
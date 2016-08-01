function am_pm_to_hours(time) {
    console.log(time);
    var hours = Number(time.match(/^(\d+)/)[1]);
    var minutes = Number(time.match(/:(\d+)/)[1]);
    var AMPM = time.match(/\s(.*)$/)[1];
    if (AMPM == "pm" && hours < 12) hours = hours + 12;
    if (AMPM == "am" && hours == 12) hours = hours - 12;
    var sHours = hours.toString();
    var sMinutes = minutes.toString();
    if (hours < 10) sHours = "0" + sHours;
    if (minutes < 10) sMinutes = "0" + sMinutes;
    return (sHours + ':' + sMinutes);
}

function hours_am_pm(time) {
    var hours = time[0] + time[1];
    var min = time[2] + time[3];
    if (hours < 12) {
        return hours + ':' + min + ' AM';
    } else {
        hours = hours - 12;
        hours = (hours.length < 10) ? '0' + hours : hours;
        return hours + ':' + min + ' PM';
    }
}

console.log(am_pm_to_hours("12:00 pm"));

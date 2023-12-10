
setInterval(tellTime, 1000);

function tellTime() {
    const timeBody = document.getElementById('time');

    const date = new Date();
    let hours = date.getHours();
    let fixHours = hours % 12;
    let minutes = date.getMinutes();
    let seconds = date.getSeconds();
    if (hours <= 12) {
        var meridiem = ("am");
    }
    else {
        var meridiem = ("pm");
    }

    output = (fixHours + " : " + minutes + " : " + seconds + " " + meridiem);
    timeBody.innerHTML = (output);
}


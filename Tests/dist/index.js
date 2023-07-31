
// window.addEventListener("DOMContentLoaded", () => {
//     console.log(window.photino);
// });


function $updateCount(message) {
    document.querySelector("#lbl_count").innerText = `${message.count}`;
}

function countUp() {
    window.photino.Counter.CountUp([], $updateCount);
}

function countDown() {
    var count = parseInt(document.querySelector("#lbl_count").innerText);
    if (count > 0) {
        window.photino.Counter.CountDown([], $updateCount);
    }
}

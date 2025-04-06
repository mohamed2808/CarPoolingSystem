document.addEventListener("DOMContentLoaded", function () {
    let bookButtons = document.querySelectorAll(".bookRideBtn");

    bookButtons.forEach(button => {
        button.addEventListener("click", function () {
            document.getElementById("rideId").value = this.getAttribute("data-id");
            document.getElementById("rideOrigin").value = this.getAttribute("data-origin");
            document.getElementById("rideDestination").value = this.getAttribute("data-destination");
            document.getElementById("rideDateTime").value = this.getAttribute("data-datetime");
            document.getElementById("rideSeats").value = this.getAttribute("data-seats");
        });
    });
});
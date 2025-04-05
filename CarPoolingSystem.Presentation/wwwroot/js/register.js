document.getElementById("roleSelect").addEventListener("change", function () {
    var vehicleDetails = document.getElementById("vehicleDetails");
    if (this.value === "Driver") {
        vehicleDetails.style.display = "block";
    } else {
        vehicleDetails.style.display = "none";
    }
});
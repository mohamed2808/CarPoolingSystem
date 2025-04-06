document.addEventListener("DOMContentLoaded", function () {
    const roleSelect = document.getElementById("roleSelect");
    const vehicleDetails = document.getElementById("vehicleDetails");

    function toggleVehicleDetails() {
        if (roleSelect.value === "Driver") {
            vehicleDetails.style.display = "block";
        } else {
            vehicleDetails.style.display = "none";
        }
    }

   
    toggleVehicleDetails();

    
    roleSelect.addEventListener("change", toggleVehicleDetails);
});

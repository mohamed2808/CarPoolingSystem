document.addEventListener("DOMContentLoaded", function () {

    // View Booking
    document.querySelectorAll(".viewBookingBtn").forEach(button => {
        button.addEventListener("click", () => {
            document.getElementById("bookingOrigin").innerText = button.dataset.origin;
            document.getElementById("bookingDestination").innerText = button.dataset.destination;
            document.getElementById("bookingDateTime").innerText = button.dataset.datetime;
            document.getElementById("bookingSeats").innerText = button.dataset.seats;
            document.getElementById("bookingTotalPrice").innerText = button.dataset.price;
        });
    });

    // Delete Booking
    document.querySelectorAll(".deleteBookingBtn").forEach(button => {
        button.addEventListener("click", () => {
            document.getElementById("deleteBookingOrigin").innerText = button.dataset.origin;
            document.getElementById("deleteBookingDestination").innerText = button.dataset.destination;
            document.getElementById("deleteBookingId").value = button.dataset.id;
        });
    });

    // Edit Booking
    document.querySelectorAll(".editBookingBtn").forEach(button => {
        button.addEventListener("click", () => {
            document.getElementById("editBookingId").value = button.dataset.id;
            document.getElementById("editRideId").value = button.dataset.rideid;
            document.getElementById("editDriverName").value = button.dataset.driver;
            document.getElementById("editOrigin").value = button.dataset.origin;
            document.getElementById("editDestination").value = button.dataset.destination;
            document.getElementById("editDateTime").value = button.dataset.datetime;
            document.getElementById("editPrice").value = button.dataset.price;
            document.getElementById("editSeatsBooked").value = button.dataset.seats;
        });
    });

    // Blur effect for all modals
    const modals = ["bookingDetailsModal", "editBookingModal", "deleteBookingModal"];

    modals.forEach(modalId => {
        const modal = document.getElementById(modalId);

        if (modal) {
            modal.addEventListener("show.bs.modal", () => {
                const blur = document.createElement("div");
                blur.className = "modal-backdrop modal-backdrop-blur";
                document.body.appendChild(blur);
            });

            modal.addEventListener("hidden.bs.modal", () => {
                const blur = document.querySelector(".modal-backdrop-blur");
                if (blur) blur.remove();
            });
        }
    });

});

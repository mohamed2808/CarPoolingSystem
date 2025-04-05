
        document.addEventListener("DOMContentLoaded", function () {

            document.querySelectorAll(".viewBookingBtn").forEach(button => {
                button.addEventListener("click", function () {
                    document.getElementById("bookingOrigin").innerText = this.dataset.origin;
                    document.getElementById("bookingDestination").innerText = this.dataset.destination;
                    document.getElementById("bookingDateTime").innerText = this.dataset.datetime;
                    document.getElementById("bookingSeats").innerText = this.dataset.seats;
                    document.getElementById("bookingTotalPrice").innerText = this.dataset.price;
                });
            });

            
            document.querySelectorAll(".deleteBookingBtn").forEach(button => {
            button.addEventListener("click", function () {
                document.getElementById("deleteBookingOrigin").innerText = this.dataset.origin;
                document.getElementById("deleteBookingDestination").innerText = this.dataset.destination;
                document.getElementById("deleteBookingId").value = this.dataset.id;
            });
            });

           
            document.querySelectorAll(".editBookingBtn").forEach(button => {
            button.addEventListener("click", function () {
                document.getElementById("editBookingId").value = this.dataset.id;
                document.getElementById("editRideId").value = this.dataset.rideid;
                document.getElementById("editDriverName").value = this.dataset.driver;
                document.getElementById("editOrigin").value = this.dataset.origin;
                document.getElementById("editDestination").value = this.dataset.destination;
                document.getElementById("editDateTime").value = this.dataset.datetime;
                document.getElementById("editPrice").value = this.dataset.price;
                document.getElementById("editSeatsBooked").value = this.dataset.seats;
            });
            });
        });

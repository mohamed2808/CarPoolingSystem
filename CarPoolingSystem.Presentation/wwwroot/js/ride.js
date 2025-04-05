    document.addEventListener("DOMContentLoaded", function () {

        document.querySelectorAll(".bookRideBtn").forEach(button => {
            button.addEventListener("click", function () {
                document.getElementById("rideId").value = this.dataset.id;
                document.getElementById("rideOrigin").value = this.dataset.origin;
                document.getElementById("rideDestination").value = this.dataset.destination;
                document.getElementById("rideDateTime").value = this.dataset.datetime;
                document.getElementById("rideSeats").value = this.dataset.seats;
            });
        });

          
            document.querySelectorAll(".viewDetailsBtn").forEach(button => {
        button.addEventListener("click", function () {
            document.getElementById("detailOrigin").innerText = this.dataset.origin;
            document.getElementById("detailDestination").innerText = this.dataset.destination;
            document.getElementById("detailDateTime").innerText = this.dataset.datetime;
            document.getElementById("detailSeats").innerText = this.dataset.seats;
            document.getElementById("detailPrice").innerText = this.dataset.price;

            const editBtn = document.getElementById("editRideBtn");
            editBtn.onclick = function () {

                document.getElementById("editRideId").value = button.dataset.id;
                document.getElementById("editRideOrigin").value = button.dataset.origin;
                document.getElementById("editRideDestination").value = button.dataset.destination;
                document.getElementById("editRideDateTime").value = button.dataset.datetime;
                document.getElementById("editRideSeats").value = button.dataset.seats;
                document.getElementById("editRidePrice").value = button.dataset.price;


                const detailsModal = bootstrap.Modal.getInstance(document.getElementById("detailsModal"));
                detailsModal.hide();

                const editModal = new bootstrap.Modal(document.getElementById("editRideModal"));
                editModal.show();
            };
        });
            });

           document.querySelectorAll(".deleteRideBtn").forEach(button => {
        button.addEventListener("click", function () {

            document.getElementById("deleteRideOrigin").innerText = this.dataset.origin;
            document.getElementById("deleteRideDestination").innerText = this.dataset.destination;


            document.getElementById("deleteRideId").value = this.dataset.id;
        });
            });

           
            document.querySelectorAll(".editRideBtn").forEach(button => {
        button.addEventListener("click", function () {
            document.getElementById("editRideId").value = this.dataset.id;
            document.getElementById("editRideOrigin").value = this.dataset.origin;
            document.getElementById("editRideDestination").value = this.dataset.destination;
            document.getElementById("editRideDateTime").value = this.dataset.datetime;
            document.getElementById("editRideSeats").value = this.dataset.seats;
            document.getElementById("editRidePrice").value = this.dataset.price;
        });
            });
    });

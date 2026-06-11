// -----------------------------
// BED DATA (Mock backend data)
// -----------------------------
let beds = [
    { bedNumber: 1, isOccupied: false, bloodPressure: "120/80", respiratoryRate: 16 },
    { bedNumber: 2, isOccupied: true, bloodPressure: "160/100", respiratoryRate: 28 },
    { bedNumber: 3, isOccupied: false, bloodPressure: "130/85", respiratoryRate: 18 },
    { bedNumber: 4, isOccupied: true, bloodPressure: "170/110", respiratoryRate: 30 },
    { bedNumber: 5, isOccupied: false, bloodPressure: "125/82", respiratoryRate: 17 },
    { bedNumber: 6, isOccupied: false, bloodPressure: "118/76", respiratoryRate: 15 },
    { bedNumber: 7, isOccupied: true, bloodPressure: "155/95", respiratoryRate: 26 },
    { bedNumber: 8, isOccupied: false, bloodPressure: "122/80", respiratoryRate: 16 },
    { bedNumber: 9, isOccupied: true, bloodPressure: "180/120", respiratoryRate: 32 },
    { bedNumber: 10, isOccupied: false, bloodPressure: "124/78", respiratoryRate: 18 },
    { bedNumber: 11, isOccupied: true, bloodPressure: "145/90", respiratoryRate: 22 },
    { bedNumber: 12, isOccupied: false, bloodPressure: "120/80", respiratoryRate: 16 }
];


// -----------------------------
// FUNCTION: Render beds on screen
// -----------------------------
function isCriticalBed(bloodPressure, respiratoryRate) {

    let systolicBP = parseInt(bloodPressure.split("/")[0]);

    return systolicBP > 150 || respiratoryRate > 25;
}
function renderBeds() {

    let container = document.getElementById("bedContainer");
    let occupiedCount = 0;

    // Clear existing beds
    container.innerHTML = "";

    // Loop through all beds
    for (let i = 0; i < beds.length; i++) {

        let bed = beds[i];

        // Create a div for each bed
        let bedDiv = document.createElement("div");

        // Assign common bed class
        bedDiv.classList.add("bed");

        // Condition to decide color
        if (bed.isOccupied) {

            occupiedCount++;

            bedDiv.classList.add("occupied");
            bedDiv.innerText = `Bed ${bed.bedNumber}\nOccupied`;

            // Disable clicking on occupied beds
            bedDiv.style.cursor = "not-allowed";

        } else {

            bedDiv.classList.add("available");
            bedDiv.innerText = `Bed ${bed.bedNumber}\nAvailable`;

            // Only available beds can be clicked
            bedDiv.onclick = function () {
                bed.isOccupied = true;
                renderBeds();
            };
        }

        // Add bed to container
        container.appendChild(bedDiv);
    }

    // Show summary
    document.getElementById("bedSummary").innerText =
        `Occupied Beds: ${occupiedCount} / ${beds.length}`;
}


// -----------------------------
// INITIAL LOAD
// -----------------------------
renderBeds();
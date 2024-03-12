"use strict";
for (const hyperlink of document.getElementsByTagName("a")) { 
    hyperlink.onclick = zoekBrouwer;
}
async function zoekBrouwer() {
    const response = await fetch(`brouwerss/${this.dataset.id}`); 
    if (response.ok) {
        const brouwer = await response.json();
        document.getElementById("postcode").innerText = brouwer.postcode;
        document.getElementById("gemeente").innerText = brouwer.gemeente;
    }
    else if (response.status === 404) {
        alert("Brouwer niet gevonden.");
    } else {
        alert("Technisch probleem, contacteer de helpdesk.");
    }
}

document.addEventListener("DOMContentLoaded", function () {
    const ddl = document.getElementById("LineaTematicaId");
    const nuevoTemaDiv = document.querySelector('.temaOtro');
    const spanOtro = document.querySelector('.otroRequerido');
    const inputOtro = document.getElementById("LineaTematicaOtro");
    nuevoTemaDiv.disabled = true;
    spanOtro.style.display = "none";

    ddl.addEventListener("change", function () {
        const opciones = ddl.options;
        const ultimaOpcion = opciones[opciones.length - 1];
        const seleccionada = ddl.options[ddl.selectedIndex];

        // Si se selecciona la última opción
        if (seleccionada.value === ultimaOpcion.value && seleccionada.value !== "") {
            nuevoTemaDiv.disabled = false;
            spanOtro.style.display = "block";
            inputOtro.classList.add("otroRequeridoInput");
        } else {
            nuevoTemaDiv.disabled = true;
            inputOtro.value = "";
            spanOtro.style.display = "none";
            inputOtro.classList.remove("otroRequeridoInput");
        }

    });
    inputOtro.addEventListener('input', function () {
        
        if (this.value.length > 0) {
            // Si tiene valor, quitar la clase
            this.classList.remove("otroRequeridoInput");
            spanOtro.style.display = "none";
        }
    });
});

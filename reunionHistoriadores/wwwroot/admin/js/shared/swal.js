$(function () {
    ValidaForma();
});

function ValidaForma() {
    var settings = $("form").data("validator").settings;
    settings.ignore = ":hidden";

    settings.submitHandler =
        function (form, event) {
            event.preventDefault();

            Swal.fire({ title: $(":submit").data('swaltitle'), text: $(":submit").data('swaltext'), icon: "info", showCancelButton: true, confirmButtonText: "Sí", cancelButtonText: "Cancelar" }).then((result) => {
                if (result.isConfirmed) {
                    // Deshabilita el botón de enviar
                    $("#btnEnviar").attr({ disabled: "disabled" }).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Enviando...');
                    // Envía los datos
                    form.submit();
                }
            });
        }
}
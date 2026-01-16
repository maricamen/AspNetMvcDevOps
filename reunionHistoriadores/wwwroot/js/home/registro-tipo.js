var objtr = { elementobase: Object.keys(new Object()) },
    objTemas = { urln: "", seleccionados: new Array() };
$(function () {
    ValidaForma();
    ControlesDinamicos('.panel-autores');
    ObtenerTemas();
    $('.cargando').fadeIn();

});

function ValidaForma() {
    var settings = $("form").data("validator").settings;
    settings.ignore = ":hidden";

    settings.submitHandler =
        function (form, event) {
            event.preventDefault();
            Swal.fire(
                {
                    title: $(":submit").data('swaltitle'),
                    text: $(":submit").data('swaltext'),
                    icon: "info",
                    showCancelButton: true,
                    confirmButtonText: "Sí",
                    cancelButtonText: "Cancelar"
                }).then((result) => {
                    if (result.isConfirmed) {
                        // Remueve los autores que no se usan
                        $('.panel-autor').filter(':hidden').remove();
                        // Deshabilita el botón de enviar
                        $("#btnEnviar").attr({ disabled: "disabled" }).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Enviando...');
                        // Envía los datos
                        form.submit();
                    }
                });
        }
    return true;
}

function ObtenerTemas() {
    objTemas.urln = $("#tematicas").val();
    console.log(objTemas.urln);

    $.ajax({
        url: objTemas.urln,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            console.log(result);
            objTemas.seleccionados = new Array();
            $.each(result, function (index, value) {
                objtr.elementobase = Object.keys(new Object());
                objtr.elementobase = $('<div>').append($('<input id="' + value.nombre + value.id + '" class="form-check-input" type="checkbox" value="' + value.nombre + '"/>' +
                    '<label class="form-check-label" for="' + value.nombre + value.id + '">' + value.nombre + ' </label>'));
                $('#tematica').append(objtr.elementobase);
            });

            $('#Temas').val(objTemas.seleccionados.join(','));

            SeleccionTematica();
        }
    });
}

function SeleccionTematica() {
    $('#tematica input[type=checkbox]').on('change', function () {
        objTemas.seleccionadas = new Array();
        $('#tematica input[type=checkbox]:checked').each(function () {
            objTemas.seleccionadas.push($(this).val());
        });
        $('#Temas').val(objTemas.seleccionadas.join(','));
    });
}
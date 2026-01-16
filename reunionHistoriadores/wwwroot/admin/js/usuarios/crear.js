$(function () {
    ValidaForma();
    if (jQuery().easyAutocomplete)
        Autocompleta();
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

function Autocompleta() {
    var options = {

        url: function (phrase) {
            return $("#UrlBuscaUsuario").val();
        },

        //getValue: function (element) {
        //    return element.UserName;
        //},
        //getValue: "UserName",
        getValue: "email",

        ajaxSettings: {
            dataType: "json",
            method: "POST",
            data: {
                dataType: "json"
            }
        },

        preparePostData: function (data) {
            $('.easy-autocomplete').append('<i class="bi bi-hourglass-bottom fa-easy text-primary"></i>');
            data.phrase = $("#lista").val();
            return data;
        },

        template: {
            type: "description",
            fields: {
                description: "nombrecompleto"
            }
        },

        minCharNumber: 3,

        list: {
            onChooseEvent: function () {

                var value = $("#lista").getSelectedItemData().nombrecompleto;
                $("#Nombrecompleto").val(value).focus();

                var value2 = $("#lista").getSelectedItemData().email;
                $("#Email").val(value2);

                $("form").data("validator").element('#lista');
            },
            onLoadEvent: function () {
                $('.easy-autocomplete').find('.fa-easy').remove();
            }
        },

        requestDelay: 300
    };

    $("#lista").easyAutocomplete(options);
}


// Generate a password string
function randString(id) {
    var dataSet = $(id).attr('data-character-set').split(',');
    var size = $(id).attr('data-size');
    var possible = '';
    var conjunto;
    var text = '';

    if ($.inArray('a-z', dataSet) >= 0) {
        conjunto = 'abcdefghijklmnopqrstuvwxyz';
        text += conjunto.charAt(Math.floor(Math.random() * conjunto.length));
        size--;
        possible += conjunto;
    }
    if ($.inArray('A-Z', dataSet) >= 0) {
        conjunto = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ';
        text += conjunto.charAt(Math.floor(Math.random() * conjunto.length));
        size--;
        possible += conjunto;
    }
    if ($.inArray('0-9', dataSet) >= 0) {
        conjunto = '0123456789';
        text += conjunto.charAt(Math.floor(Math.random() * conjunto.length));
        size--;
        possible += conjunto;
    }
    if ($.inArray('#', dataSet) >= 0) {
        //possible += '![]{}()%&*$#^<>~@|';
        conjunto = '!@#$%^&*)(';
        text += conjunto.charAt(Math.floor(Math.random() * conjunto.length));
        size--;
        possible += conjunto;
    }

    for (var i = 0; i < size; i++) {
        text += possible.charAt(Math.floor(Math.random() * possible.length));
    }
    return text;
}

// Create a new password
$(".getNewPass").click(function () {
    var field = $(this).closest('div').find('input[rel="gp"]');
    field.val(randString(field));
});

// Auto Select Pass On Focus
$('input[rel="gp"]').on("click", function () {
    $(this).select();
});
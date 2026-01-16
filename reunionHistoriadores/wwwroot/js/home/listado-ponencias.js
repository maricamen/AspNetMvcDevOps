$(function () {
    function filtrar() {
        var filtro = $("#filtroRelacion").val();
        var buscar = $("#buscar").val();

        $.ajax({
            url: '@Url.Action("Filtrar", "Ponencias")',
            type: 'GET',
            data: { filtroRelacion: filtro, buscar: buscar },
            success: function (result) {
                $("#tabla-container").html(result);
            }
        });
    }

    $("#btnFiltrar").on("click", filtrar);
    $("#filtroRelacion").on("change", filtrar);
    $("#buscar").on("keyup", function (e) {
        if (e.keyCode === 13) filtrar(); // presiona Enter para buscar
    });
});
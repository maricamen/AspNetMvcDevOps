function ControlesDinamicos(panel_selector) {
    var panel = $(panel_selector);
    var MAX_OPTIONS = panel.find('.max').text();
    var MIN_OPTIONS = 1;
    
    $(document).on('click', panel_selector + ' .btn-add', function (e) {
        e.preventDefault();

        // Add item
        panel.find('.panel-autor:hidden:first').find('input, textarea').val('');
        panel.find('.panel-autor:hidden:first').removeClass('d-none');

        if (panel.find('.panel-autor:visible').length >= MAX_OPTIONS)
            panel.find('.btn-add').attr('disabled', 'disabled');

        if (panel.find('.panel-autor:visible').length > MIN_OPTIONS)
            panel.find('.btn-remove').removeAttr('disabled');

    }).on('click', panel_selector + ' .btn-remove', function (e) {
        e.preventDefault();

        // Remove item
        panel.find('.panel-autor:visible:last').addClass('d-none');

        if (panel.find('.panel-autor:visible').length <= MIN_OPTIONS)
            panel.find('.btn-remove').attr('disabled', 'disabled');

        if (panel.find('.panel-autor:visible').length < MAX_OPTIONS)
            panel.find('.btn-add').removeAttr('disabled');
    });
    // Agrega el primer elemento
    panel.find('.btn-add').click();
    panel.find('.btn-remove').attr('disabled', 'disabled');
}
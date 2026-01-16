/**
 * Validaciones personalizadas
 */
(function ($) {
    $.validator.unobtrusive.adapters.addBool("checkboxrequired", "required");

    // requiredifchecked
    $.validator.addMethod("requiredifchecked", function (value, element, params) {
        var prefix = '';
        if (n = element.name.indexOf('.') > 0) {
            prefix = element.name.substr(0, n + 1);
        }

        var targetElement = $('[name="' + prefix + params.otherproperty + '"]');

        if (targetElement.is(':checked'))
            return true;
        return true;
    });
    $.validator.unobtrusive.adapters.add("requiredifchecked", ['otherproperty'],
        function (options) {
            options.rules['requiredifchecked'] = {
                otherproperty: options.params.otherproperty
            };
            options.messages['requiredifchecked'] = options.message;

            /* 
            * OPCIONAL: Lo siguiente es para ocultar/mostrar los dependientes automaticamente
            */
            var prefix = '';
            if (n = options.element.name.indexOf('.') > 0) {
                prefix = options.element.name.substr(0, n + 1);
            }
            var targetElement = $('[name="' + prefix + options.params.otherproperty + '"]');
            $(targetElement).on("change", function (e) {
                OcultaMuestraIfChecked(options, $(this));
            });

            $(document).ready(function () {
                OcultaMuestraIfChecked(options);
            });
            /* 
            * OPCIONAL: Lo siguiente es para ocultar/mostrar los dependientes automaticamente
            */
        }
    );
    function OcultaMuestraIfChecked(options, targetElement) {
        var prefix = '';
        if (n = options.element.name.indexOf('.') > 0) {
            prefix = options.element.name.substr(0, n + 1);
        }

        if (targetElement == null)
            targetElement = $('[name="' + prefix + options.params.otherproperty + '"]');        

        if (targetElement.is(':checked')) {
            $('[name="' + options.element.name + '"]').closest('div').show();
        } else {
            $('[name="' + options.element.name + '"]').closest('div').hide();
            $('[name="' + options.element.name + '"]').closest('div').find('select, input').val('');
        }
    }
    // requiredifchecked


    // requiredifvalor
    $.validator.addMethod("requiredifvalor", function (value, element, params) {
        var prefix = '';
        if (n = element.name.indexOf('.') > 0) {
            prefix = element.name.substr(0, n + 1);
        }

        var targetElement = $('[name="' + prefix + params.otherproperty + '"]');
        var propertyValue = null;

        if (targetElement.is('select'))
            propertyValue = targetElement.val();
        else
            propertyValue = $('[name="' + prefix + params.otherproperty + '"]:checked').val();

        var targetValue = params.targetvalue;
        if (propertyValue == targetValue) {
            if (value == null || value == '')
                return false;
        }
        return true;
    });
    $.validator.unobtrusive.adapters.add("requiredifvalor", ['otherproperty', 'targetvalue'],
        function (options) {
            options.rules['requiredifvalor'] = {
                otherproperty: options.params.otherproperty,
                targetvalue: options.params.targetvalue
            };
            options.messages['requiredifvalor'] = options.message;

            /* 
            * OPCIONAL: Lo siguiente es para ocultar/mostrar los dependientes automaticamente
            */
            var prefix = '';
            if (n = options.element.name.indexOf('.') > 0) {
                prefix = options.element.name.substr(0, n + 1);
            }
            var targetElement = $('[name="' + prefix + options.params.otherproperty + '"]');
            $(targetElement).on("change", function (e) {
                OcultaMuestraIfValor(options, $(this));
            });

            $(document).ready(function () {
                OcultaMuestraIfValor(options);
            });
            /* 
            * OPCIONAL: Lo siguiente es para ocultar/mostrar los dependientes automaticamente
            */
        }
    );
    function OcultaMuestraIfValor(options, targetElement) {
        var prefix = '';
        if (n = options.element.name.indexOf('.') > 0) {
            prefix = options.element.name.substr(0, n + 1);
        }

        if (targetElement == null)
            targetElement = $('[name="' + prefix + options.params.otherproperty + '"]');

        var propertyValue = null;
        if (targetElement.is(':checkbox') || targetElement.is(':radio'))
            propertyValue = $('[name="' + prefix + options.params.otherproperty + '"]:checked').val();
        else
            propertyValue = $(targetElement).val();

        var targetValue = options.params.targetvalue;

        if (propertyValue == targetValue) {
            $('[name="' + options.element.name + '"]').closest('div').show();
        } else {
            $('[name="' + options.element.name + '"]').closest('div').hide();
            $('[name="' + options.element.name + '"]').closest('div').find('select, input').val('');
        }
    }
    // requiredifvalor


    // requiredifvalores
    $.validator.addMethod("requiredifvalores", function (value, element, params) {
        var prefix = '';
        if (n = element.name.indexOf('.') > 0) {
            prefix = element.name.substr(0, n + 1);
        }

        var targetElement = $('[name="' + prefix + params.otherproperty + '"]');
        var propertyValue = null;

        if (targetElement.is(':checkbox') || targetElement.is(':radio'))
            propertyValue = $('[name="' + prefix + options.params.otherproperty + '"]:checked').val();
        else
            propertyValue = $(targetElement).val();

        var listoftargetvalues = params.listoftargetvalues.split(",");
        for (i = 0; i < listoftargetvalues.length; i++) {
            if (listoftargetvalues[i] == propertyValue)
                if (value == null || value == '')
                    return false;
        }
        return true;
    });
    $.validator.unobtrusive.adapters.add("requiredifvalores", ['otherproperty', 'listoftargetvalues'],
        function (options) {
            options.rules['requiredifvalores'] = {
                otherproperty: options.params.otherproperty,
                listoftargetvalues: options.params.listoftargetvalues,
            };
            options.messages['requiredifvalores'] = options.message;

            /* 
            * OPCIONAL: Lo siguiente es para ocultar/mostrar los dependientes automaticamente
            */
            var prefix = '';
            if (n = options.element.name.indexOf('.') > 0) {
                prefix = options.element.name.substr(0, n + 1);
            }
            var targetElement = $('[name="' + prefix + options.params.otherproperty + '"]');
            $(targetElement).on("change", function (e) {
                OcultaMuestraIfValores(options, $(this));
            });

            $(document).ready(function () {
                OcultaMuestraIfValores(options);
            });
            /* 
            * OPCIONAL: Lo siguiente es para ocultar/mostrar los dependientes automaticamente
            */
        }
    );
    function OcultaMuestraIfValores(options, targetElement) {
        var prefix = '';
        if (n = options.element.name.indexOf('.') > 0) {
            prefix = options.element.name.substr(0, n + 1);
        }

        if (targetElement == null)
            targetElement = $('[name="' + prefix + options.params.otherproperty + '"]');

        var propertyValue = null;
        if (targetElement.is(':checkbox') || targetElement.is(':radio'))
            propertyValue = $('[name="' + prefix + options.params.otherproperty + '"]:checked').val();
        else
            propertyValue = $(targetElement).val();

        var listoftargetvalues = options.params.listoftargetvalues.split(",");
        for (i = 0; i < listoftargetvalues.length; i++) {
            if (listoftargetvalues[i] == propertyValue) {
                $('[name="' + options.element.name + '"]').closest('div').show();
                return;
            }
        }
        // No lo encontró
        $('[name="' + options.element.name + '"]').closest('div').hide();
        $('[name="' + options.element.name + '"]').closest('div').find('select, input').val('');
    }
    // requiredifvalores


    // muestrabloqueifvalores
    $.validator.addMethod("muestrabloqueifvalores", function (value, element, params) {
        // Siempre va a ser válido porque solo queremos mostrar/ocultar un div
        return true;
    });
    $.validator.unobtrusive.adapters.add("muestrabloqueifvalores", ['bloqueid', 'listoftargetvalues'],
        function (options) {
            options.rules['muestrabloqueifvalores'] = {
                bloqueid: options.params.bloqueid,
                listoftargetvalues: options.params.listoftargetvalues,
            };
            options.messages['muestrabloqueifvalores'] = options.message;

            $(options.element).on("change", function (e) {
                OcultaMuestraBloqueId(options);
            });

            $(document).ready(function () {
                OcultaMuestraBloqueId(options);
            });
        }
    );
    function OcultaMuestraBloqueId(options) {
        var targetElement = $(options.element);

        var propertyValue = null;
        if (targetElement.is(':checkbox') || targetElement.is(':radio'))
            propertyValue = $('[name="' + prefix + options.params.bloqueid + '"]:checked').val();
        else
            propertyValue = $(targetElement).val();

        var listoftargetvalues = options.params.listoftargetvalues.split(",");
        for (i = 0; i < listoftargetvalues.length; i++) {
            if (listoftargetvalues[i] == propertyValue) {
                $('#' + options.params.bloqueid).show();
                return;
            }
        }
        // No lo encontró
        $('#' + options.params.bloqueid).hide();
        $('#' + options.params.bloqueid).find('input:checkbox').prop("checked", false);
    }
    // muestrabloqueifvalores


    // requiredifgroupempty
    $.validator.addMethod("requiredifgroupempty", function (value, element, params) {
        var prefix = '';
        if (n = element.name.indexOf('.') > 0) {
            prefix = element.name.substr(0, n + 1);
        }

        var countChecked = 0;
        var properties = params.listofproperties.split(',');
        $.each(properties, function (i, otherproperty) {
            if (element.name != (prefix + otherproperty)) {
                //Removes validation from input-fields
                var targetElement = $('[name="' + prefix + otherproperty + '"]:checkbox');
                targetElement.addClass('valid').removeClass('input-validation-error')
                targetElement = $('*[data-valmsg-for="' + prefix + otherproperty + '"]');
                targetElement.addClass('field-validation-valid').removeClass('field-validation-error').text('');
            }
            // Revisa si esta checado
            var targetElement = $('[name="' + prefix + otherproperty + '"]:checked');
            if (targetElement.length > 0)
                countChecked++;
        });

        // Si no ha sido checado ningun otro checkbox
        if (countChecked == 0) {
            return false;
        }
        return true;
    });
    $.validator.unobtrusive.adapters.add("requiredifgroupempty", ['listofproperties'],
        function (options) {
            options.rules['requiredifgroupempty'] = {
                listofproperties: options.params.listofproperties
            };
            options.messages['requiredifgroupempty'] = options.message;
        }
    );
    // requiredifgroupempty

    // requiredcountwords
    $.validator.addMethod("requiredcountwords", function (value, element, params) {
        //if (value == null || value == '')
        //    return false;

        return CuentaPalabrasText(element);
    });
    $.validator.unobtrusive.adapters.add("requiredcountwords", ['minvalue', 'maxvalue', 'archivopalabrasescritas', 'archivopalabrasrestantes', 'archivominimo1', 'archivominimo2', 'archivoexceder1', 'archivoexceder2'],
        function (options) {
            options.rules['requiredcountwords'] = {
                minvalue: options.params.minvalue,
                maxvalue: options.params.maxvalue,
                archivopalabrasescritas: options.params.archivopalabrasescritas,
                archivopalabrasrestantes: options.params.archivopalabrasrestantes,
                archivominimo1: options.params.archivominimo1,
                archivominimo2: options.params.archivominimo2,
                archivoexceder1: options.params.archivoexceder1,
                archivoexceder2: options.params.archivoexceder2
            };
            options.messages['requiredcountwords'] = options.message;

            // Agregamos la leyenda del contador de palabras al inicio
            var element = options.element;

            //if (n = element.name.indexOf('.') > 0) {
            //    var prefix = element.name.substr(0, n);
            //    var nombre = element.name.substr(n + 1);
            //    $(element).attr('id', nombre + prefix);
            //}
            $(element).parent().append('<div class="form-text">' + options.params.archivominimo1 + ' <span id="wordmin-' + element.id + '">' + options.params.minvalue + '</span> ' + options.params.archivominimo2 + '.<br />' + options.params.archivoexceder1 + ' <span id="wordlimit-' + element.id + '">' + options.params.maxvalue + '</span> ' + options.params.archivoexceder2 + '.<br />' + options.params.archivopalabrasescritas + ' <span id="words-' + element.id + '">0</span> ' + options.params.archivopalabrasrestantes + ' <span id="word-count-' + element.id + '">' + options.params.maxvalue + '</span></div>');

            $(element).on("keyup", function (e) {
                CuentaPalabrasText(element);
            });
            CuentaPalabrasText(element);
        }
    );
    function CuentaPalabrasText(obj) {
        var spanw = document.getElementById('words-' + obj.id);
        var span = document.getElementById('word-count-' + obj.id);
        var spanmin = document.getElementById('wordmin-' + obj.id);
        var spanlimit = document.getElementById('wordlimit-' + obj.id);
        var wordmin = parseInt(spanmin.innerText);
        var wordlimit = parseInt(spanlimit.innerText);
        var text = $(obj).val();
        var words = (text.match(/\S+/gi) != null) ? text.match(/\S+/gi).length : 0
        var wordcount = wordlimit - words;
        spanw.innerText = words;

        if (wordcount < 0)
            span.innerText = 0;
        else
            span.innerText = wordcount;

        if (words < wordmin) {
            return false;
        }

        if (wordcount < 0 || words == 0) {
            return false;
        }
        return true;
    }
    // requiredcountwords

    // requiredcountchars
    $.validator.addMethod("requiredcountchars", function (value, element, params) {
        //if (value == null || value == '')
        //    return false;

        return CuentaLetrasText(element);
    });
    $.validator.unobtrusive.adapters.add("requiredcountchars", ['minvalue', 'maxvalue', 'archivocaracteresescritas', 'archivocaracteresrestantes', 'archivominimo1', 'archivominimo2', 'archivoexceder1', 'archivoexceder2'],
        function (options) {
            options.rules['requiredcountchars'] = {
                minvalue: options.params.minvalue,
                maxvalue: options.params.maxvalue,
                archivocaracteresescritas: options.params.archivocaracteresescritas,
                archivocaracteresrestantes: options.params.archivocaracteresrestantes,
                archivominimo1: options.params.archivominimo1,
                archivominimo2: options.params.archivominimo2,
                archivoexceder1: options.params.archivoexceder1,
                archivoexceder2: options.params.archivoexceder2
            };
            options.messages['requiredcountchars'] = options.message;

            // Agregamos la leyenda del contador de caracteres al inicio
            var element = options.element;

            //if (n = element.name.indexOf('.') > 0) {
            //    var prefix = element.name.substr(0, n);
            //    var nombre = element.name.substr(n + 1);
            //    $(element).attr('id', nombre + prefix);
            //}
            $(element).parent().append('<div class="form-text">' + options.params.archivominimo1 + ' <span id="wordmin-' + element.id + '">' + options.params.minvalue + '</span> ' + options.params.archivominimo2 + '.<br />' + options.params.archivoexceder1 + ' <span id="wordlimit-' + element.id + '">' + options.params.maxvalue + '</span> ' + options.params.archivoexceder2 + '.<br />' + options.params.archivocaracteresescritas + ' <span id="words-' + element.id + '">0</span> ' + options.params.archivocaracteresrestantes + ' <span id="word-count-' + element.id + '">' + options.params.maxvalue + '</span></div>');

            $(element).on("keyup", function (e) {
                CuentaLetrasText(element);
            });
            CuentaLetrasText(element);
        }
    );
    function CuentaLetrasText(obj) {
        var spanw = document.getElementById('words-' + obj.id);
        var span = document.getElementById('word-count-' + obj.id);
        var spanmin = document.getElementById('wordmin-' + obj.id);
        var spanlimit = document.getElementById('wordlimit-' + obj.id);
        var letrasmin = parseInt(spanmin.innerText);
        var letraslimit = spanlimit.innerHTML;
        // Correccion de caracteres en navegadores
        var text = $(obj).val().replace(/\n/g, "\r\n");
        var letras = text.length;
        var letrascount = letraslimit - letras;
        spanw.innerHTML = letras;

        if (letrascount < 0)
            span.innerHTML = 0;
        else
            span.innerHTML = letrascount;

        if (letras < letrasmin) {
            return false;
        }
        
        if (letrascount < 0 || letras == 0) {
            return false;
        }
        return true;
    }
    // requiredcountchars

    // custompasswordvalidator
    $.validator.addMethod("custompasswordvalidator", function (value, element, params) {
        if (value == null)
            return false;

        value = value.replace(/\s+/g, '');  // quita los espacios
        $(element).val(value);

        if (params.allowemptystring && value == '') {
            return true;
        }

        if (value.length < params.minlength)
            return false;

        // lowercase        
        var patt = new RegExp("^(?=.*[a-z])");        
        if (params.requirelowercase) {
            if (!patt.test(value))
                return false;
        }

        // uppercase
        if (params.requireuppercase) {
            patt = new RegExp("^(?=.*[A-Z])");
            if (!patt.test(value))
                return false;
        }

        // requiredigit
        if (params.requiredigit) {
            patt = new RegExp("^(?=.*[0-9])");
            if (!patt.test(value))
                return false;
        }

        // requirenonletterordigit
        if (params.requirenonletterordigit) {
            patt = new RegExp("^(?=.*[!@#$%^&*)(])");
            if (!patt.test(value))
                return false;
        }

        return true;
    });
    $.validator.unobtrusive.adapters.add("custompasswordvalidator", ['allowemptystring', 'minlength', 'requiredigit', 'requirelowercase', 'requireuppercase', 'requirenonletterordigit'],
        function (options) {
            options.rules['custompasswordvalidator'] = {
                allowemptystring: options.params.allowemptystring,
                minlength: options.params.minlength,
                requiredigit: options.params.requiredigit,
                requirelowercase: options.params.requirelowercase,
                requireuppercase: options.params.requireuppercase,
                requirenonletterordigit: options.params.requirenonletterordigit,
            };
            options.messages['custompasswordvalidator'] = options.message;          
        }
    );
    // custompasswordvalidator

    // maxfilesize
    $.validator.addMethod("maxfilesize", function (value, element, params) {
        if (element.files) {
            if (element.files[0].size > params.maxvalue)
                return false;
        }

        return true;
    });
    $.validator.unobtrusive.adapters.add("maxfilesize", ['maxvalue', 'pesomaximo'],
        function (options) {
            options.rules['maxfilesize'] = {
                maxvalue: options.params.maxvalue,
                pesomaximo: options.params.pesomaximo,
            };
            options.messages['maxfilesize'] = options.message;

            // Agregamos la leyenda del contador de caracteres al inicio
            var element = options.element;

            $(element).parent().append('<div class="form-text">' + options.params.pesomaximo + '</div>');
        }
    );
    // maxfilesize

    // allowedextensions
    $.validator.addMethod("allowedextensions", function (value, element, params) {
        var fileExtensions = params.extensions.split(',');
        
        if (element.files) {
            if ($.inArray($(element).val().split('.').pop().toLowerCase(), fileExtensions) == -1)
                return false;
        }

        return true;
    });
    $.validator.unobtrusive.adapters.add("allowedextensions", ['extensions', 'accept', 'extensionespermitidas'],
        function (options) {
            options.rules['allowedextensions'] = {
                extensions: options.params.extensions,
                accept: options.params.accept,
                extensionespermitidas: options.params.extensionespermitidas,
            };
            options.messages['allowedextensions'] = options.message;

            // Agregamos la leyenda del contador de caracteres al inicio
            var element = options.element;

            $(element).parent().append('<div class="form-text">' + options.params.extensionespermitidas + '</div>');
            console.log($(element).attr('class'));
            $(element).attr('accept', options.params.accept);
        }
    );
    // allowedextensions

}(jQuery));
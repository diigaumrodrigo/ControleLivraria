var aviso = (function ($) {

    var tarja = null;
    var tarjaAnimSpeed = 250;
    var mensagemAnimSpeed = 150;
    var comMouse = 'com-mouse';
    var isVisivel = 'is-visivel';
    var deveEsconder = 'deve-esconder';

    function escondeMensagens() {
        var containers = tarja.children('div:not(.ativo)');
        containers
            .children()
            .fadeOut(mensagemAnimSpeed, function () {
                containers.animate(
                    { height: '0px' },
                    tarjaAnimSpeed,
                    function () {
                        containers.remove();
                        tarja.esconde();
                    });
            });
    }

    function iniciaTemporizador(containerMensagem) {
        setTimeout(function () {
            containerMensagem.removeClass('ativo');
            if (!tarja.data(comMouse))
                escondeMensagens();
        }, 5000);
    }

    function adicionaMensagem(containerMensagem) {
        var mensagens = containerMensagem
            .children()
            .hide()
            .end()
            .prependTo(tarja)
            .children();

        var totalHeight = 0;
        var childcount = mensagens.length;

        for (var i = 0; i < childcount; i++)
            totalHeight += mensagens.eq(i).height();

        containerMensagem.animate(
            { height: totalHeight + 'px' },
            tarjaAnimSpeed,
            function () {
                mensagens
                    .fadeIn(mensagemAnimSpeed)
                    .promise()
                    .then(function () {
                        iniciaTemporizador(containerMensagem);
                    });
            });
    }

    function getTarja() {
        if (!tarja) {
            tarja = $('.aviso');

            if (tarja.length == 0)
                tarja = $('<div/>')
                    .addClass('aviso')
                    .hide()
                    .appendTo($('body'));

            tarja
                .data(isVisivel, false)
                .data(deveEsconder, false)
                .data(comMouse, false)
                .off('click')
                .click(function () {
                    tarja
                        .children('.ativo')
                        .removeClass('ativo')
                        .end()
                        .data(comMouse, false)
                        .esconde();
                })
                .off('mouseenter')
                .mouseenter(function () {
                    tarja.data(comMouse, true);
                })
                .off('mouseleave')
                .mouseleave(function () {
                    tarja.data(comMouse, false);
                    if (tarja.data(deveEsconder)) {
                        tarja.esconde();
                    } else {
                        escondeMensagens();
                    }
                });

            tarja.exibe = function (callback) {
                if (tarja.data(isVisivel)) {
                    callback();
                } else {
                    tarja
                        .data(isVisivel, true)
                        .fadeIn(tarjaAnimSpeed, callback);
                }
            };

            tarja.esconde = function () {
                if (tarja.data(comMouse)) {
                    tarja.data(deveEsconder, true);
                } else if (tarja.children('.ativo').length == 0) {
                    tarja
                        .data(deveEsconder, false)
                        .data(isVisivel, false)
                        .fadeOut(tarjaAnimSpeed, function () {
                            tarja.empty();
                        });
                }
            };
        }

        return tarja;
    }

    function exibeMensagem(mensagem) {
        getTarja().exibe(function () {
            adicionaMensagem($('<div/>').addClass('ativo').append('<p>' + mensagem + '</p>'));
        });
    }

    function exibeMensagemUnico(mensagem) {
        getTarja().exibe(function () {
            adicionaMensagem($('<div/>').addClass('ativo').append('<p>' + mensagem + '</p>'));
        });
    }

    function exibeMensagens(lista) {
        getTarja().exibe(function () {
            var p = lista.map(function (mensagem) {
                return '<p>' + mensagem + '</p>';
            });

            adicionaMensagem($('<div/>').addClass('ativo').append(p));
        });
    }

    return {
        exibe: exibeMensagem,
        exibeMensagens: exibeMensagens,
        exibeMensagemUnico: exibeMensagemUnico
    };
})(jQuery);
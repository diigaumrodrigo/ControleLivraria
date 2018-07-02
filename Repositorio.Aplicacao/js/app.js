$(document).ready(function () {    

    function configuraAjax() {
        $.ajaxSetup({
            async: true,
            cache: false,
            dataType: "json",
            contentType: 'application/json; charset=UTF-8'
        });

        $.ajaxPrefilter(function (options) {
            var headers = options.headers || {};
            options.headers = headers;
        });
    }

    configuraAjax();

    $("#btnRegerarBaseDados").on('click', function (e) {
        e.preventDefault();
        $.get("livros/inserirBaseLivros")
            .done(function (retorno) {
                aviso.exibeMensagemUnico(retorno);
            }).fail(function (r) {
                aviso.exibeMensagens(r.responseJSON);
            }).always(function () {     
                $("#tabela_livros").DataTable().ajax.reload();
                util.esconderLoading();
            });
    });

    $("#btnAdicionarLivro").click(function () {
        $.when(util.carregarModal("ModalCurso.html", ".modal_livro")())
            .then(function () {
                manutencaoLivro.getSelect();
            });
    });
    $(".bg_modal, .fechar_modal").click(util.escondeModal);

});

$.ajaxSetup({
    async: true,
    cache: false,
    dataType: 'json',
    global: true,
    beforeSend: function (jqXHR, settings) {
        if ($(".loading").is(':hidden'))
            $(".loading").fadeIn();
    },
    complete: function () {
        if ($(".loading").is(':visible'))
            $(".loading").fadeOut();
    }
});
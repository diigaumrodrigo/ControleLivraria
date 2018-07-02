var util = (function ($) {

    function configuraDatatables() {
        $.extend(true, $.fn.dataTable.defaults, {
            "autoWidth": true,
            "lengthMenu": [
                [5, 10, 15, 25, 50, 100, -1],
                [5, 10, 15, 25, 50, 100, "Todos"]
            ],
            scrollX: true,
            order: [
                [0, "desc"]
            ],
            "iDisplayLength": 5,
            select: {
                style: 'api',
                info: false
            },
            language: {
                "sEmptyTable": "Nenhum registro encontrado",
                "sInfo": "Mostrando de _START_ até _END_ de _TOTAL_ registros",
                "sInfoEmpty": "Mostrando 0 até 0 de 0 registros",
                "sInfoFiltered": "(Filtrados de _MAX_ registros)",
                "sInfoPostFix": "",
                "sInfoThousands": ".",
                "sLengthMenu": "_MENU_ resultados por página",
                "sLoadingRecords": "Carregando...",
                "sProcessing": "Processando...",
                "sZeroRecords": "Nenhum registro encontrado",
                "sSearch": "Pesquisar",
                "oPaginate": {
                    "sNext": "Próximo",
                    "sPrevious": "Anterior",
                    "sFirst": "Primeiro",
                    "sLast": "Último"
                },
                "oAria": {
                    "sSortAscending": ": Ordenar colunas de forma ascendente",
                    "sSortDescending": ": Ordenar colunas de forma descendente"
                },
                "select": {
                    "rows": {
                        "_": "%d  linhas selecionadas",
                        "0": "Clique na linha para seleciona-la",
                        "1": "1 linha selecionada"
                    }
                }
            }
        });

        $.fn.dataTable.ext.errMode = 'none';
    }

    function exibemodal(div) {
        $(".bg_modal, " + div).fadeIn(300);
        $(".atalho_contato, nav, header, section, iframe, footer").addClass("blur");
        $(".dropdown_curso").slideUp();
        $("body").css("overflow-y", "hidden");
    }

    function escondeModal() {
        $(".bg_modal, .modal_livro").fadeOut(300);
        $(".atalho_contato, nav, header, section, iframe, footer").removeClass("blur");

        if (!$(".sobre_nos_div").is(':visible'))
            $("body").css("overflow-y", "auto");
    }

    function carregarModal(url, div, click, hide) {
        return function () {
            var q = $.Deferred();

            $.ajax({
                url: url,
                method: 'GET',
                contentType: "text/html",
                dataType: "html"
            }).done(function (r) {

                $(div).html(r);

                exibemodal(div);

            }).fail(function () {
                exibirAviso("Erro inesperado");
            }).always(q.resolve);;

            return q.promise();
        }
    }

    function renderData(data, type) {
        if (data != null)
            return moment(data, moment.ISO_8601).format('DD/MM/YYYY HH:mm:ss');
        else
            return null
    }

    function getOptionsSelect(opcoes) {
        var q = $.Deferred();

        var opt = $.extend({}, {
            url: '',
            data: null,
            elemento: $({}),
            codigo: 'id',
            alt: 'alt',
            nome: 'nome',
            itemPadrao: '',
            itemVazio: ''
        }, opcoes);

        if (opt.elemento) {

            $.get(opt.url, opt.data)
                .done(function (itens) {
                    opt.elemento.empty();
                    var option = $('<option/>');
                    var options = (itens || []).map(function (item) {
                        return option
                            .clone()
                            .attr('value', getValueOf(item, opt.codigo))
                            .attr('alt', getValueOf(item, opt.alt))
                            .text(getValueOf(item, opt.nome));
                    });

                    if (options.length == 0 && opt.itemVazio !== '')
                        opt.elemento.append(option.clone().attr('value', 0).text(opt.itemVazio));
                    else if (opt.itemPadrao !== '')
                        opt.elemento.append(option.clone());

                    opt.elemento.append(option.clone()).append(options);

                })
                .always(q.resolve);
        } else {
            q.resolve();
        }

        return q.promise();
    }

    function getValueOf(item, opcao) {
        if (typeof opcao == 'function') {
            return opcao(item);
        } else {
            return item[opcao];
        }
    }

    function exibirLoading() {
        $(".loading").fadeIn();
    }

    function esconderLoading() {
        $(".loading").fadeOut();
    }

    return {
        carregarModal: carregarModal,
        exibemodal: exibemodal,
        escondeModal: escondeModal,
        configuraDatatables: configuraDatatables,
        renderData: renderData,
        exibirLoading: exibirLoading,
        esconderLoading: esconderLoading,
        getOptionsSelect: getOptionsSelect,
    }

})(jQuery);
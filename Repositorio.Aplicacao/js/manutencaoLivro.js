$(document).ready(function () {

    $("#btnAdicionarLivro").on('click', function (e) {
        e.preventDefault();

        var dados = manutencaoLivro.getDados();

        $.post("livros/adicionar", JSON.stringify(dados))
            .done(function (retorno) {
                aviso.exibeMensagemUnico(retorno);
                util.escondeModal();
            }).fail(function (r) {
                aviso.exibeMensagens(r.responseJSON);
            }).always(function () {
                $("#tabela_livros").DataTable().ajax.reload();
                util.esconderLoading();
            });
    })
});

var manutencaoLivro = (function ($) {

    function getDados() {
        return {
            Titulo: $("#tituloLivro").val(),
            Autor: $("#autorLivro").val(),
            Categoria: $("#categoriaLivro").val(),
            Ano: $("#anoLivro").val()
        };
    }

    function getSelect() {
        return util.getOptionsSelect({
            url: "categorias",
            elemento: $("#categoriaLivro"),
            codigo: "IdCategoria",
            nome: "Descricao"
        })
    }

    function dadosEditar(data) {

        $("#tituloLivro").val(data.Titulo);
        $("#autorLivro").val(data.Autor);
        $("#anoLivro").val(data.Ano);

        $.when(getSelect()).then(function () {
            $("#categoriaLivro option[value='" + data.IdCategoria + "']").prop('selected', true);
        });
    }

    return {
        getSelect: getSelect,
        dadosEditar: dadosEditar,
        getDados: getDados
    }
})(jQuery);
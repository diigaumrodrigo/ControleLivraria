$(document).ready(function () {
    util.configuraDatatables();
    tabelaLivros();

    function tabelaLivros() {
        var tabela_cursos = $("#tabela_livros").DataTable({
            order: [[0, 'asc']],
            ajax: {
                url: "livros",
                dataSrc: ""
            },
            columns: [
                {
                    title: "Título",
                    data: "Titulo"
                },
                {
                    title: "Autor",
                    data: "Autor"
                },
                {
                    title: "Categoria",
                    data: "Categoria"
                },
                {
                    title: "Ano de Publicação",
                    data: "Ano"
                },
                {
                    title: "Data do Cadastro",
                    data: "DataCadastro",
                    render: util.renderData
                },
                {
                    title: "Editar",
                    render: function (data, type) {
                        if (type == "display")
                            return '<img class="editar_livro img_dt_tables img_click" src="images/editar.svg" alt="Editar" />'
                        else
                            return null;
                    }
                },
                {
                    title: "Remover",
                    render: function (data, type) {
                        if (type == "display")
                            return '<img class="remover_livro img_dt_tables img_click" src="images/remover.svg" alt="Remover" />'
                        else
                            return null;
                    }
                }
            ],
            createdRow: function (row, data) {
                $(".remover_livro", row).click(function () {
                    $.post('livros/deletar/' + data.IdLivro)
                        .done(function (retorno) {
                            aviso.exibeMensagemUnico(retorno);
                        }).fail(function (r) {
                            aviso.exibeMensagens(r.responseJSON);
                        }).always(function () {
                            $("#tabela_livros").DataTable().ajax.reload();
                            util.esconderLoading();
                        });
                });

                $(".editar_livro", row).click(function () {
                    var modal = util.carregarModal("ModalCurso.html", ".modal_livro");
                    $.when(
                        modal(),
                    ).then(function () {
                        $(".modal_livro_int h3").html("Editar Livro");
                        $("#btnAdicionarLivro").remove();
                        
                        manutencaoLivro.dadosEditar(data);

                        $("form").append('<button type="submit" class="btn btn-primary" id="btnEditarLivro">Editar</button>');

                        $("#btnEditarLivro").click(function (e) {
                            e.preventDefault();
                            var dados = manutencaoLivro.getDados();
                            dados["IdLivro"] = data.IdLivro;

                            $.post("livros/editar", JSON.stringify(dados))
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
                });

            }
        });
    }
});
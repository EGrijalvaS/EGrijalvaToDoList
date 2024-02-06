$(document).ready(function () {
    renderTareas();
});

function renderTareas() {
    $("table_container").empty();

    var settings = {
        type: "GET",
        url: "",
        contentType: "application/json; charset=uft-8",
    };

    $.ajax(settings).done(function (result) {
        var theadTemplate = `
        <table class="table tabla-hover" id="tableTareas">
        <thead>
        <tr>
        <th> Editar </th>
        <th> Titulo </th>
        <th> Descripción </th>
        <th> Fecha Inicio </th>
        <th> Fecha Caducidad </th>
        <th> Estatus </th>
        <th> Eliminar </th>
        </tr>
        </thead>
        <tbody>
        `;

        $("#table_container").append(theadTemplate);

        $.each(result.objects, function (i, tarea) {

            var trowTemplate =
                '<tr>'
                + '<td class="text-center"> <button class="btn btn-info" onclick="GetById(' + tarea.idTarea + ')"><span class="bi bi-pencil-square"></span></button></td>'
                + "<td class='text-center'>" + tarea.idTarea + "</td>"
                + "<td class='text-center'>" + tarea.Titulo + "</td>"
                + "<td class='text-center'>" + tarea.Descripcion + "</td>"
                + "<td class='text-center'>" + tarea.FechaInicio + "</td>"
                + "<td class='text-center'>" + tarea.FechaCaducidad + "</td>"
                + "<td class='text-center'>" + tarea.Estatus + "</td>"
                + '<td class="text-center"><button class="btn btn-danger" onclick="Delete"(' + tarea.idTarea + ')"><span class="bi bi-trash-fill"></span></button></td>'

                + "<tr>";

            $("tableTareas tbody").append(trowTemplate);
        });

        var tBodyEndTemplate = `
        </tbody>
        </table>
        `;

        $("table_container").append(tBodyEndTemplate);
    }).fail(function (xhr, status, error) {
        alert('Error en la Actualizacion.' + error);
    });
}

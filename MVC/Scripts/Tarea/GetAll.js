$(document).ready(function () {
    GetAllTarea();
    GetAllStatus();
});

function GetAllTarea() {
    $.ajax({
        type: 'GET',
        url: ,

        success: function (result) {
            $('#tblTarea tbody').empty();
            $.each(result.objects, function (i, empleado) {
                var filas =
                    '<tr>'
                    + '<td class="text-center">'
                    + '<a class="btn btn-warning bi bi-gear" href="#" onclick="GetById(' + tarea.idTarea + ')">'
                    + '</a>'
                    // Contenido de la Tabla
                    + '</td>'
                    + "<td id='idTarea' class='text-center'>" + tarea.idTarea + "</td>"
                    + "<td class='text-center'>" + tarea.Titulo + "</td>"
                    + "<td class='text-center'>" + tarea.Descripcion + "</td>"
                    + "<td class='text-center'>" + tarea.FechaInicio + "</td>"
                    + "<td class='text-center'>" + tarea.FechaCaducidad + "</td>"
                    + "<td class='text-center'>" + tarea.IdStatus + "</td>"

                    + '<td class="text-center"><button class="btn btn-danger bi bi-persson-dash" onclick="Eliminar(' + tarea.idTarea + ')"><span class="glyphicon-trash" style="color:#FFFFFF></span></button></td>'
                    + "</tr>";

                $("#tblTarea tbody").append(filas);

            });
        },

        error: function (result) {
            alert('Error al hacer la Consulta.' + result.responseJSON.ErrorMessage);
        }
    });

    function AddTarea() {
        var tareaJSON = {
            "idTarea": 0,
            "Titulo": $('txtNombre').val(),
            "Descripcion": $('txtDescripcion').val(),
            "FechaInicio": $('datePicker').val(),
            "FechaInicio": $('datePicker').val(),
            "estatus": {
                "idStatus": $('#ddlEstatus').val(),
                "Descripcion": "string",
                "ListStatus": ["string"]
            },
        }

        $.ajax({
            type: 'POST',
            URL: '',
            dataType: '',
            data: tareaJSON,

            success: function (result) {

            }
        })
    }
}

function Guardar() {
    var tarea = {
        "idTarea": Number($('#ddlIdTarea').val()),
        "Titulo": $('#txtTitulo').val(),
        "Descripcion": $('#txtDescripcion').val(),

        "idStatus": Number($('#ddlIdStatus').val()),
    }
    if (tarea.idTarea == 0) {
        AddTarea();
    }
    else {
        UpdateTarea();
    }
}


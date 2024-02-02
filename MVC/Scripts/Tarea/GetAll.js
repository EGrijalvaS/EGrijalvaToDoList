$(document).ready(function () {
    renderTareas();
});

function renderTareas() {
	$('#tablecontainer').empty();

	var settings = {
		type: 'GET',
		url: '',
		contentType: "application/json; charset=uft-8",
	};

	$.ajax(settings).done(function (result) {
		var theadTemplate = `
        <table class="table table-hover" id="tableTareas">
        <thead>
        <tr>
        <th class="text-white"> Editar</th>
        <th class="text-white"> Titulo</th>
        <th class="text-white"> Descripcion</th>
        <th class="text-white"> Status</th>
        <th class="text-white"> Eliminar</th>
        </tr>
        </thead>
        <tbody>
    `;
		$("table_conteiner").append(theadTemplate);
		$.each(result.objects, function (i, tarea) {
			var trowTemplate =
				'<tr>'
				+ '<th class="text-center"><button class="btn btn-info" onclick="GetById(' + tarea.idTarea + ')"></button></th>'
				+ "<th class='text-center'>" + tarea.Titulo + "</th>"
				+ "<th class='text-center'>" + tarea.Descripcion + "</th>"
				+ "<th class='text-center'>" + tarea.IdStatus + "</th>"
				+ '<th class="text-center"><button class="btn btn-info" onclick="GetById(' + tarea.idTarea + ')"></button></th>'

				+ "</tr>";

			$("#tableTareas tbody").append(trowTemplate);
		});

		var tBodyEndTemplate = `
		</tbody>
		</table>
		`;
			$("#table_Container").append(tBodyEndTemplate);
		}).fail(function (xhr, status, error) {
			alert('Error en la Consulta.' + error);
		});
	}

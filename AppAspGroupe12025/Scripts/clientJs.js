//Load Data in Table when documents is ready 
$(document).ready(function () {
    loadData();
});
//Load Data function 
function loadData() {
    $.ajax({
        url: "/Client/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.IdUtilisateur + '</td>';
                html += '<td>' + item.CNIClient + '</td>';
                html += '<td>' + item.NomUtilisateur + '</td>';
                html += '<td>' + item.PrenomUtilisateur + '</td>';
                html += '<td>' + item.TelUtilisateur + '</td>';
                html += '<td>' + item.EmailUtilisateur + '</td>';
                html += '<td><a href="#" onclick="return getbyID(' + item.CNIClient +
                    ')">Edit</a> | <a href="#" onclick="Delele(' + item.CNIClient + ')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
//Add Data Function 
function Add() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var empObj = {
        ID: $('#ID').val(),
        CNIClient: $('#CNIClient').val(),
        NomUtilisateur: $('#NomUtilisateur').val(),
        PrenomUtilisateur: $('#PrenomUtilisateur').val(),
        TelUtilisateur: $('#TelUtilisateur').val(),
        EmailUtilisateur: $('#EmailUtilisateur').val()
    };
    $.ajax({
        url: "/Client/Add",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
//Function for getting the Data Based upon Employee ID 
function getbyID(EmpID) {
    $('#CNIClient').css('border-color', 'lightgrey');
    $('#NomUtilisateur').css('border-color', 'lightgrey');
    $('#PrenomUtilisateur').css('border-color', 'lightgrey');
    $('#TelUtilisateur').css('border-color', 'lightgrey');
    $('#EmailUtilisateur').css('border-color', 'lightgrey');
    $.ajax({
        url: "/Client/getbyID/" + EmpID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) { 
            $('#ID').val(result.ID);
            $('#CNIClient').val(result.CNIClient);
            $('#NomUtilisateur').val(result.NomUtilisateur);
            $('#PrenomUtilisateur').val(result.PrenomUtilisateur);
            $('#TelUtilisateur').val(result.TelUtilisateur);
            $('#EmailUtilisateur').val(result.EmailUtilisateur);
            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}
//function for updating employee's record 
function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var empObj = {
        ID: $('#ID').val(),
        CNIClient: $('#CNIClient').val(),
        NomUtilisateur: $('#NomUtilisateur').val(),
        PrenomUtilisateur: $('#PrenomUtilisateur').val(),
        TelUtilisateur: $('#TelUtilisateur').val(),
        EmailUtilisateur: $('#EmailUtilisateur').val()
    };
    $.ajax({
        url: "/Client/Update",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
            $('#ID').val(""),
            $('#CNIClient').val(""),
            $('#NomUtilisateur').val(""),
            $('#PrenomUtilisateur').val(""),
            $('#TelUtilisateur').val(""),
            $('#EmailUtilisateur').val("")
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
//function for deleting employee's record 
function Delele(ID) {
    var ans = confirm("Etes-vous sur de vouloir supprimer?");
    if (ans) {
        $.ajax({
            url: "/Client/Delete/" + ID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadData();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}
//Function for clearing the textboxes 
function clearTextBox() {
    $('#CNIClient').val("");
    $('#NomUtilisateur').val("");
    $('#PrenomUtilisateur').val("");
    $('#TelUtilisateur').val("");
    $('#EmailUtilisateur').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#CNIClient').css('border-color', 'lightgrey');
    $('#NomUtilisateur').css('border-color', 'lightgrey');
    $('#PrenomUtilisateur').css('border-color', 'lightgrey');
    $('#TelUtilisateur').css('border-color', 'lightgrey');
    $('#EmailUtilisateur').css('border-color', 'lightgrey');
    $('#myModal').modal('show');
}

function Close() {
    $('#myModal').modal('hide');
}
//Valdidation using jquery 
function validate() {
    var isValid = true;
    if ($('#CNIClient').val().trim() == "") {
        $('#CNIClient').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#CNIClient').css('border-color', 'lightgrey');
    }
    if ($('#NomUtilisateur').val().trim() == "") {
        $('#NomUtilisateur').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#NomUtilisateur').css('border-color', 'lightgrey');
    }
    if ($('#PrenomUtilisateur').val().trim() == "") {
        $('#PrenomUtilisateur').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#PrenomUtilisateur').css('border-color', 'lightgrey');
    }
    if ($('#TelUtilisateur').val().trim() == "") {
        $('#TelUtilisateur').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#TelUtilisateur').css('border-color', 'lightgrey');
    }
    if ($('#EmailUtilisateur').val().trim() == "") {
        $('#EmailUtilisateur').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#EmailUtilisateur').css('border-color', 'lightgrey');
    }
    return isValid;
} 

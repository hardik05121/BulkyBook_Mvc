$(document).ready(function () {
    $('#country').attr('disable', true);
    $('#state').attr('disable', true);
    $('#city').attr('disable', true);
    LoadCountries();

    $('#country').change(function () {
        var countryId = $(this).val();
        if (countryId > 0) {
            LoadStates(countryId);
        }
        else {
            alert("Select country");
            $('#state').empty();
            $('#city').empty();
            $('#state').attr('disable', true);
            $('#city').attr('disable', true);
            $('#state').append('<option>--Select state--</option>');
            $('#city').append('<option>--Select city--</option>');
        }

    });
    $('#state').change(function () {
        var stateId = $(this).val();
        if (stateId > 0) {
            LoadCities(stateId);
        }
        else {
            alert("Select state");
            $('#city').empty();
            $('#city').attr('disable', true);
            $('#city').append('<option>--Select city--</option>');
        }

    });

});
function LoadCountries() {
    $('$country').empty();

    $.ajax({
        url: '/admin/upsert/getCountries',
        success: function (response) {
            if (response != null && response != undefine && response.length > 0) {
                $('#country').attr('disable', flase);
                $('#country').append('<option>--Select country--</option>');
                $('#state').append('<option>--Select state--</option>');
                $('#city').append('<option>--Select city--</option>');
                $.each(response, function (i, data) {
                    $('#country').append('<otion value=' + data.id + '>' data.name + '</option>');

                });
            }
            else {
                $('#country').attr('disable', true);
                $('#state').attr('disable', true);
                $('#city').attr('disable', true);
                $('#country').append('<option>-- country not available--</option>');
                $('#state').append('<option>-- state not available--</option>');
                $('#city').append('<option>-- city not available--</option>');
            }

        },
        error: function (error) {
            alert(error);
        }
    });
}

function LoadStates(countryId) {
    $('$state').empty();
    $('$city').empty();
    $('#city').attr('disable', true);


    $.ajax({
        url: '/admin/upsert/getStates?Id=' + countryId,
        success: function (response) {
            if (response != null && response != undefine && response.length > 0) {
                $('#state').attr('disable', flase);
                $('#state').append('<option>--Select state--</option>');
                $('#city').append('<option>--Select city--</option>');
                $.each(response, function (i, data) {
                    $('#state').append('<otion value=' + data.id + '>' data.name + '</option>');

                });
            }
            else {
                $('#state').attr('disable', true);
                $('#city').attr('disable', true);
                $('#state').append('<option>-- state not available--</option>');
                $('#city').append('<option>-- city not available--</option>');
            }

        },
        error: function (error) {
            alert(error);
        }
    });
}

function LoadCities(stateId) {
    $('$city').empty();
    $('#city').attr('disable', true);


    $.ajax({
        url: '/admin/upsert/getCities?Id=' + stateId,
        success: function (response) {
            if (response != null && response != undefine && response.length > 0) {
                $('#city').attr('disable', flase);
                $('#city').append('<option>--Select city--</option>');
                $.each(response, function (i, data) {
                    $('#city').append('<otion value=' + data.id + '>' data.name + '</option>');

                });
            }
            else {
                $('#city').attr('disable', true);
                $('#city').append('<option>-- city not available--</option>');
            }

        },
        error: function (error) {
            alert(error);
        }
    });
}
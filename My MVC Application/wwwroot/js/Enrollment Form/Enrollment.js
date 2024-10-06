//GET STUDENT INFO
document.getElementById('studentId').addEventListener('keypress', function (e) {
    if (e.key === 'Enter') {
        e.preventDefault(); // Prevent the default behavior (form submission)
        searchStudent();
    }
});

function searchStudent() {

    var studentId = $('#studentId').val();

    // AJAX call to the server
    $.ajax({
        url: '/EnrollmentHeaderFile/GetStudentInfo?studentId=' + studentId,
        type: 'GET',
        success: function (data) {
            if (data.error) {

                alert("Student ID not exist")

                // Clear other fields
                $('#name').val('');
                $('#course').val('');
                $('#year').val('');
            }
            else {
                // Display student information in the specified fields
                $('#name').val(data.name);
                $('#course').val(data.course);
                $('#year').val(data.year);


                // Clear the error message
                $('#studentInfo').html('');
            }
        },
        error: function (xhr, status, error) {
            console.log("Error: " + error);
            console.log("Status: " + status);
            console.log("XHR: ", xhr);

            // Handle error if the AJAX request fails
            alert('Error retrieving student information');
        }
    });
}

//GET SUBJECTS
document.getElementById('edpId').addEventListener('keypress', function (e) {
    if (e.key === 'Enter') {
        e.preventDefault(); // Prevent the default behavior (form submission)
        searchsubject();
    }
});

var totalUnits = 0;
var addedEdpCodes = [];

function searchsubject() {
    var subjectId = $('#edpId').val();

    // If the current totalUnits is already at the limit, prevent further actions
    if (totalUnits >= 24) {
        alert("Total units limit reached. You cannot add more subjects.");
        return;
    }

    // Check if the EDP code is already added
    if (addedEdpCodes.includes(subjectId)) {
        alert("EDP code already added. Duplicate EDP codes are not allowed.");
        return;
    }

    var newRow = $('<tr>');
    newRow.append($('<td>').text(subjectId));

    // AJAX call to the server
    $.ajax({
        url: '/EnrollmentHeaderFile/GetSubjects?subjectSchedId=' + subjectId,
        type: 'GET',
        success: function (data) {
            if (data.error) {
                alert("EDP not exist")
                // Clear other fields
                $('#edpCode').val('');
            } else {
                // Check if adding this subject would exceed the unit limit
                if (totalUnits + parseInt(data.units) <= 24) {
                    // Add the subject information to the table
                    var newRow = $('<tr>');
                    newRow.append($('<td>').text(data.edpId));
                    newRow.append($('<td>').text(data.subJcode));
                    newRow.append($('<td>').text(data.startTime));
                    newRow.append($('<td>').text(data.endTime));
                    newRow.append($('<td>').text(data.days));
                    newRow.append($('<td>').text(data.room));
                    newRow.append($('<td>').text(data.units));

                    $('#edpTableBody').append(newRow);

                    // Update total units
                    totalUnits += parseInt(data.units);
                    $('#totalUnits').val(totalUnits);

                    // Add the EDP code to the list of added codes
                    addedEdpCodes.push(subjectId);
                } else {
                    alert("Adding this subject would exceed the unit limit.");
                }
            }
        },

        error: function (xhr, status, error) {
            console.log("Error: " + error);
            console.log("Status: " + status);
            console.log("XHR: ", xhr);

            // Handle error if the AJAX request fails
            alert('Error retrieving student information');
        }
    });
}


//SAVE
document.getElementById('saveEnrollment').addEventListener('click', function () {
    saveEnrollment();
});

function saveEnrollment() {
    // Extract values from the form
    var studentId = $('#studentId').val();
    var edpId = $('#edpId').val();

    // Validate input (add your validation logic here)
    if (!studentId || !edpId) {
        alert('Please fill out all the required fields.');
        return;
    }

    // Create an object with enrollment data
    var enrollmentData = {
        ENRHSTUDID: studentId,
        ENRHSTUDDATEENROLL: new Date(),  // Set the enrollment date
        ENRHSTUDSCHLYR: '2023-2024',  // Set the school year as needed
        ENRHSTUDENCODER: 'YourEncoderName',  // Set the encoder name as needed
        EnrollmentDetails: [
            {
                ENRDSTUDEDPCODE: edpId,
                ENRDSTUDSUBJCODE: 'YourSubjectCode',  // Replace with your data source
                ENRDSTUDSTATUS: 'Active',
                ENRDSTUDUNITS: 0  // Set initial units value
            }
            // Add more entries for additional subjects if needed
        ]
    };

    $.ajax({
        type: 'POST',
        url: '/EnrollmentHeaderFile/AddEnrollment',
        data: JSON.stringify(enrollmentData),
        contentType: 'application/json;charset=utf-8',
        success: function (data) {
            if (data.success) {
                alert('Enrollment details saved successfully.');
                // Clear the form or perform any other actions upon success
            } else {
                alert('Enrollment details saving failed. ' + data.message);
            }
        },
        error: function (xhr, status, error) {
            console.error('Error: ' + error);
            console.error('Status: ' + status);
            console.error('XHR: ', xhr);

            // Handle error if the AJAX request fails
            alert('Error occurred while saving enrollment details');
        }
    });
}

//Reset
$(document).ready(function () {
    // Handle the click event for the reset button
    $('#reset').on('click', function () {
        // Clear the content of the table body
        $('#edpTableBody').empty();

        // Reset total units to 0
        totalUnits = 0;
        $('#totalUnits').val(totalUnits);

        // Clear the added EDP codes list
        addedEdpCodes = [];
    });
});
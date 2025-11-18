$(document).ready(function () {

    // Handle CreateSubject form submission
    $("#frmSubject").submit(function (e) {
        e.preventDefault(); // prevent normal submit

        var formData = $(this).serialize();

        $.ajax({
            url: createSubjectUrl,
            type: 'POST',
            data: formData,
            success: function () {
                // Clear the form
                $("#frmSubject")[0].reset();

                // Reload the subject list
                $("#subjectListContainer").load(subjectListUrl);
            },
            error: function () {
                alert("Failed to save subject.");
            }
        });
    });

    // Initial load of subject list (optional if Index loads with Model)
    $("#subjectListContainer").load(subjectListUrl);
});

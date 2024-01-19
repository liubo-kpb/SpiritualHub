let modulesCounter = 0;
let newModules = 0;

function courseEdit(modulesCount) {
    modulesCounter = modulesCount;

    setAddModuleEventListener();

    for (let i = 0; i < modulesCounter; ++i) {
        setDeleteModuleEventListener(i);
    }
}

function setAddModuleEventListener() {
    $(`#add-module`).on("click", function (e) {
        e.preventDefault();

        const moduleIndex = modulesCounter++;



        $("#modules-list").append(`<div id="module-${moduleIndex}" class="d-flex align-items-start bg-light mb-2">
                                        <input id="id-${moduleIndex}" type="hidden" value="new"/>
                                        <select id="order-select-${moduleIndex}" class="w-25 form-control" name="Modules[${moduleIndex}].Number">
                                        </select>
                                         <div class="form-group d-flex flex-column w-75 mx-2">
                                            <input id="module-${moduleIndex}-name" name="Modules[${moduleIndex}].Name" class="form-control text-center" placeholder="Enter Module Name"
                                                data-val-required="The Name field is required." data-val-length-max="80" data-val-length-min="5" data-val-length="The field Name must be a string with a minimum length of 5 and a maximum length of 80." data-val="true"
                                                value="New Module ${++newModules}"/>
                                            <span class="small text-danger text-center field-validation-valid" data-valmsg-for="Modules[${moduleIndex}].Name" data-valmsg-replace="true"></span>
                                        </div>
                                        <input id="delete-btn-${moduleIndex}" class="btn btn-danger" type="button" title="Delete Module" value="X" />
                                        <input id="is-new-${moduleIndex}" name="Modules[${moduleIndex}].IsNew" type="hidden" value="true">
                                   </div>`
        );

        updateSelectsValues();

        setDeleteModuleEventListener(moduleIndex);

        $("form").removeData("validator");
        $("form").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse("form");
    });
}

function updateSelectsValues() {
    for (let i = 0; i < modulesCounter; i++) {
        const selectElement = $(`#order-select-${i}`);
        let startValue = modulesCounter;

        if (i + 1 == modulesCounter) {
            startValue = 1;
            var value = modulesCounter
        }

        addOptions(selectElement, startValue, value);
    }
}

function addOptions(selectElement, startValue, value) {
    for (let i = startValue; i <= modulesCounter; i++) {
        selectElement.append(`<option value="${i}" class="text-center">Module ${i}</option>`);
    }

    if (value) {
        selectElement.val(value);
    }
}

function setDeleteModuleEventListener(moduleIndex) {
    $(`#delete-btn-${moduleIndex}`).on("click", function (e) {
        e.preventDefault();

        if ($(`#is-new-${moduleIndex}`).prop(`value`) === "true") {
            modulesCounter--;
            const deletedElementValue = $(`#order-select-${moduleIndex}`).find(":selected").val();
            updateSelectValues(deletedElementValue);

            return;
        }

        updateDeletedStatusValues();

        function updateSelectValues(deletedElementValue) {
            let isAfterDeletedElement = false;
            for (let i = 0; i <= modulesCounter; i++) {
                if (i == moduleIndex) {
                    $(`#module-${i++}`).remove();
                    isAfterDeletedElement = true;
                }

                const selectElement = $(`#order-select-${i}`);
                let selectValue = selectElement.find(":selected").val();
                if (selectValue > deletedElementValue) {
                    selectValue--;
                }

                selectElement.empty();
                addOptions(selectElement, 1, selectValue);

                if (isAfterDeletedElement) {
                    $(`#delete-btn-${i}`).off();

                    changeElementIndexes(i);

                    setDeleteModuleEventListener(i - 1);
                }
            }
        }

        function changeElementIndexes(i) {
            $(`#module-${i}`).prop("id", `module-${i - 1}`);

            $(`#order-select-${i}`).prop("name", `Modules[${i - 1}].Number`);
            $(`#order-select-${i}`).prop("id", `order-select-${i - 1}`);

            $(`#module-${i}-name`).prop("name", `Modules[${i - 1}].Name`);
            $(`#module-${i}-name`).prop("id", `order-select-${i - 1}`);

            $(`#delete-btn-${i}`).prop("id", `delete-btn-${i - 1}`);

            $(`#is-deleted-${i}`).prop("name", `Modules[${i - 1}].IsDeleted`);
            $(`#is-deleted-${i}`).prop("id", `is-deleted-${i - 1}`);

            $(`#is-new-${i}`).prop("name", `Modules[${i - 1}].IsNew`);
            $(`#is-new-${i}`).prop("id", `is-new-${i - 1}`);
        }

        function updateDeletedStatusValues() {
            const deleteButton = $(`#delete-btn-${moduleIndex}`);
            const isDeletedElement = $(`#is-deleted-${moduleIndex}`);
            const selectElement = $(`#order-select-${moduleIndex}`);
            const moduleName = $(`#module-${moduleIndex}-name`);

            if (isDeletedElement.val() === "false") {
                isDeletedElement.val(true);

                selectElement.prop("title", "Module marked for deletion");

                moduleName.prop("readonly", true);
                moduleName.prop("title", "Module marked for deletion");

                deleteButton.val("⎌");
                deleteButton.removeClass("btn-danger");
                deleteButton.addClass("btn-primary");
                deleteButton.prop("title", "Cancel Deletion");
            }
            else {
                isDeletedElement.val(false);

                moduleName.prop("readonly", false);
                moduleName.prop("title", "Module will not be deleted");

                selectElement.prop("title", "Module will not be deleted");

                deleteButton.val("X");
                deleteButton.removeClass("btn-primary");
                deleteButton.addClass("btn-danger");
                deleteButton.prop("title", "Delete Module");
            }
        }
    });
}
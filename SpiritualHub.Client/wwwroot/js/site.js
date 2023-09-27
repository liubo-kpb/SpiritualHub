const webAPIDomain = `https://localhost:7177/api/`;
function statistics() {
    $('#statistics_btn').on('click', function (e) {
        e.preventDefault();
        e.stopPropagation();

        // hasClass('d-none') -> Statistics are hidden
        if ($('#statistics_box').hasClass('d-none')) {
            $.get(`${webAPIDomain}statistics`, function (data) {
                $('#total_authors').text(data.model.totalActiveAuthors + " Active Authors");
                $('#total_events').text(data.model.totalEvents + " Events");
                $('#total_courses').text(data.model.totalCourses + " Courses");
                $('#total_books').text(data.model.totalBooks + " Books");
                $('#total_blog-posts').text(data.model.totalBlogPosts + " Posts");
                $('#total_users').text(data.model.totalUsers + " Users");

                $('#statistics_box').removeClass('d-none');

                $('#statistics_btn').text('Hide Statistics');
                $('#statistics_btn').removeClass('btn-primary');
                $('#statistics_btn').addClass('btn-danger');
            });
        } else {
            $('#statistics_box').addClass('d-none');

            $('#statistics_btn').text('Show Statistics');
            $('#statistics_btn').removeClass('btn-danger');
            $('#statistics_btn').addClass('btn-primary');
        }
    });
}

async function authorRessources(id) {
    let type = "";

    $("#event-tab").on("click", async function (e) {
        e.preventDefault();
        e.stopPropagation();

        type = `events`;
        document.getElementById(type).innerHTML = "";
        addLoader(type);

        const response = await getResources(id, type);
        if (!response) {
            noResourcesMessage(type);
        }
        else if (response.hasError) {
            displayErrors(response, type);
        }
        else {
            response.data.forEach((event) => {
                $(`#${type}`).append(`<div class="d-flex flex-row justify-content-between">
                                          <p class="w-25"><b><a href="/Event/Details/${event.id}">${event.title}</a></b></p>
                                          <p>Start: ${formatDate(event.startDateTime)}</p>
                                          <p>End: ${formatDate(event.endDateTime)}</p>
                                          <p>Price: <b>$${event.price}<b></p>
                                     </div>`);
            });
        }
        removeLoader(type);
    });

    $("#book-tab").on("click", async function (e) {
        e.preventDefault();
        e.stopPropagation();

        type = `books`;
        document.getElementById(type).innerHTML = "";
        addLoader(type);

        const response = await getResources(id, type);
        if (!response) {
            noResourcesMessage(type);
        }
        else if (response.hasError) {
            displayErrors(response, type);
        }
        else {
            response.data.forEach((book) => {
                $(`#${type}`).append(`<div class="d-flex flex-row justify-content-between">
                                          <p class="w-50"><b><a href="/Book/Details/${book.id}">${book.title}</a></b></p>
                                          <p class="w-25">Price: <b>$${book.price}</b></p>
                                          <p>${book.shortDescription}</p>
                                     </div>`);
            });
        }
        removeLoader(type);
    });

    $("#course-tab").on("click", async function (e) {
        e.preventDefault();
        e.stopPropagation();

        type = `courses`;
        document.getElementById(type).innerHTML = "";
        addLoader(type);

        const response = await getResources(id, type);
        if (!response) {
            noResourcesMessage(type);
        }
        else if (response.hasError) {
            displayErrors(response, type);
        }
        else {
            response.data.forEach((course) => {
                $(`#${type}`).append(`<div class="d-flex flex-row justify-content-between">
                                          <p class="w-50"><b><a href="/Course/Details/${course.id}">${course.name}</a></b></p>
                                          <p class="w-25">Price: <b>$${course.price}</b></p>
                                          <p>${course.shortDescription}</p>
                                     </div>`);
            });
        }
        removeLoader(type);
    });

    $("#sub-tab").on("click", async function (e) {
        e.preventDefault();
        e.stopPropagation();

        type = `subscriptions`;
        document.getElementById(type).innerHTML = "";
        addLoader(type);

        const response = await getResources(id, type);
        if (!response) {
            noResourcesMessage(type);
        }
        else if (response.hasError) {
            displayErrors(response, type);
        }
        else {
            response.data.forEach((subscription) => {
                $(`#${type}`).append(`<div class="d-flex flex-row justify-content-between">
                                          <p><b>${subscription.subscriptionType}:</b> $${subscription.price.toFixed(2)}</p>
                                     </div>`);
            });
            $(`#${type}`).append(`<form class="input-group-sm" action="/Author/Subscribe/${id}" method="get">
                                        <input class="btn btn-primary mb-1" type="submit" value="Choose Subscription" />
                                </form>`);
        }
        removeLoader(type);
    });
}

function displayErrors(response, type) {
    response.errorMessages.forEach((error) => {
        $(`#${type}`).append(`<p>${error}</p>`);
    });
}

function noResourcesMessage(type) {
    document.getElementById(type).innerHTML = `<p></p><p><b>Author has no ${type} at this time.</b></p>`;
}

function addLoader(type) {
    $(`#${type}`).addClass(`spinner-border`);
}

function removeLoader(type) {
    $(`#${type}`).removeClass(`spinner-border`);
}

async function getResources(id, type) {
    const response = new Promise(async function (resolve, reject) {
        $.ajax({
            url: `${webAPIDomain}author/${id}/${type}`,
            method: 'GET',
            success: function (data) {
                resolve(data);
            },
            error: async function (xhr, status, error) {
                if (!xhr.ok && xhr.responseJSON.hasError) {
                    const data = {
                        hasError: xhr.responseJSON.hasError,
                        errorMessages: xhr.responseJSON.errorMessages
                    }
                    resolve(data);
                }

                reject({
                    statusCode: xhr.status,
                    statusText: xhr.statusText,
                    errorMessage: error
                });
            }
        });
    });
    return response;
}

function formatDate(inputDate) {
    const date = new Date(inputDate);

    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0'); // Month is 0-based
    const day = String(date.getDate()).padStart(2, '0');
    const hours = String(date.getHours()).padStart(2, '0');
    const minutes = String(date.getMinutes()).padStart(2, '0');

    const formattedDateTime = `${year}-${month}-${day} ${hours}:${minutes}`;

    return formattedDateTime;
}
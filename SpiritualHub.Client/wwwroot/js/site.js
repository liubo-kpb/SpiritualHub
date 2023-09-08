function statistics() {
    $('#statistics_btn').on('click', function (e) {
        e.preventDefault();
        e.stopPropagation();

        // hasClass('d-none') -> Statistics are hidden
        if ($('#statistics_box').hasClass('d-none')) {
            $.get('https://localhost:7177/api/statistics', function (data) {
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
        const response = await getRessources(id, type);
        if (response && response.hasError) {
            response.errorMessage.forEach((error) => {
                $(`#${type}`).append(`<p>${error}</p>`);
            });
        }
        else if (!response) {


            switch (type) {
                case "events": {
                        document.getElementById(type).innerHTML = `<p></p><p>Author has no upcoming events at this time.</p>`;
                    break;
                }
                case "courses": {

                    break;
                }
                case "books": {

                    break;
                }
                case "posts": {

                    break;
                }
                case "subscriptions": {

                    break;
                }
            }
        }
        else {
            document.getElementById(type).innerHTML = "";

            switch (type) {
                case "events": {
                    response.data.forEach((event) => {
                        $(`#${type}`).append(`<p></p><div class="d-flex flex-row justify-content-around">
                                                    <p class="w-25"><b><a href="/Event/Details/${event.id}">${event.title}</a></b></p>
                                                    <p>Start: ${formatDate(event.startDateTime)}</p>
                                                    <p>End: ${formatDate(event.endDateTime)}</p>
                                                    <p>Price: <b>$${event.price}<b></p>
                                               </div>`);
                    });
                    break;
                }
                case "courses": {

                    break;
                }
                case "books": {

                    break;
                }
                case "posts": {

                    break;
                }
                case "subscriptions": {

                    break;
                }
            }
        }
    });
}

const webAPIDomain = `https://localhost:7177/api/`;
async function getRessources(id, type) {
    const response = new Promise(function (resolve, reject) {
        $.ajax({
            url: `${webAPIDomain}author/${id}/${type}`,
            method: 'GET',
            success: function (data) {
                resolve(data);
            },
            error: function (xhr, status, error) {
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
    const seconds = String(date.getSeconds()).padStart(2, '0');

    const formattedDateTime = `${year}-${month}-${day} ${hours}:${minutes}:${seconds}`;

    return formattedDateTime;
}
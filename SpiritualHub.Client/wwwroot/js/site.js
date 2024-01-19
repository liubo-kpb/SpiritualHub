const webAPIDomain = `https://localhost:7177/api/`;
function statistics() {
    $('#statistics_btn').on('click', function (e) {
        e.preventDefault();
        e.stopPropagation();

        // hasClass('d-none') -> Statistics are hidden
        if ($('#statistics_box').hasClass('d-none')) {
            const statisticsBtn = $('#statistics_btn');
            $.get(`${webAPIDomain}statistics`, function (data) {
                $('#total_authors').text(data.model.totalActiveAuthors + " Active Authors");
                $('#total_events').text(data.model.totalEvents + " Events");
                $('#total_courses').text(data.model.totalCourses + " Courses");
                $('#total_books').text(data.model.totalBooks + " Books");
                $('#total_blog-posts').text(data.model.totalBlogPosts + " Posts");
                $('#total_users').text(data.model.totalUsers + " Users");

                $('#statistics_box').removeClass('d-none');

                statisticsBtn.text('Hide Statistics');
                statisticsBtn.removeClass('btn-primary');
                statisticsBtn.addClass('btn-danger');
            });
        } else {
            $('#statistics_box').addClass('d-none');

            statisticsBtn.text('Show Statistics');
            statisticsBtn.removeClass('btn-danger');
            statisticsBtn.addClass('btn-primary');
        }
    });
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
function statistics() {
    $('#statistics_btn').on('click', function (e) {
        e.preventDefault();
        e.stopPropagation();

        // hasClass('d-none') -> Statistics are hidden
        if ($('#statistics_box').hasClass('d-none')) {
            $.get('https://localhost:7177/api/statistics', function (data) {
                $('#total_authors').text(data.totalActiveAuthors + " Active Authors");
                $('#total_events').text(data.totalEvents + " Events");
                $('#total_courses').text(data.totalCourses + " Courses");
                $('#total_books').text(data.totalBooks + " Books");
                $('#total_blog-posts').text(data.totalBlogPosts + " Posts");
                $('#total_users').text(data.totalUsers + " Users");

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

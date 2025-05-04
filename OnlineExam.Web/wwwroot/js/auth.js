// Authentication functions
$(document).ready(function () {
    // Check if user is logged in
    checkAuth();

    // Login form submission
    $('#loginForm').submit(function (e) {
        e.preventDefault();
        const username = $('#username').val();
        const password = $('#password').val();

        // In a real app, this would be an API call
        const user = db.users.find(u => u.username === username && u.password === password);

        if (user) {
            // Store user in session
            sessionStorage.setItem('currentUser', JSON.stringify(user));

            // Redirect based on role
            if (user.role === 'admin') {
                window.location.href = 'admin/dashboard.html';
            } else {
                window.location.href = 'user/dashboard.html';
            }
        } else {
            showAlert('Invalid username or password', 'danger');
        }
    });

    // Logout button
    $('#logoutBtn').click(function () {
        sessionStorage.removeItem('currentUser');
        window.location.href = '../login.html';
    });
});

function checkAuth() {
    const currentUser = JSON.parse(sessionStorage.getItem('currentUser'));
    const isAuthPage = window.location.pathname.includes('login.html') ||
        window.location.pathname.includes('register.html');

    // if (!currentUser && !isAuthPage) {
    //     window.location.href = '../login.html';
    // } else if (currentUser) {
    //     // Redirect admin from user pages and vice versa
    //     const isAdminPage = window.location.pathname.includes('admin/');

    //     if (currentUser.role === 'admin' && !isAdminPage && !isAuthPage) {
    //         window.location.href = 'admin/dashboard.html';
    //     } else if (currentUser.role === 'user' && isAdminPage) {
    //         window.location.href = '../user/dashboard.html';
    //     }

    //     // Show user info in navbar
    //     $('.user-name').text(currentUser.username);
    // }
}

function showAlert(message, type) {
    const alert = `<div class="alert alert-${type} alert-dismissible fade show" role="alert">
        ${message}
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
    </div>`;

    $('#alerts-container').html(alert);
}
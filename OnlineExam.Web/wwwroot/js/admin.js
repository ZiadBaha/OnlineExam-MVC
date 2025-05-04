// Admin-specific functions
$(document).ready(function () {
    // Load data based on current page
    const path = window.location.pathname;

    if (path.includes('dashboard.html')) {
        loadDashboardStats();
    } else if (path.includes('quizzes/list.html')) {
        loadQuizzes();
    } else if (path.includes('users/list.html')) {
        loadUsers();
    } else if (path.includes('reports/grades.html')) {
        loadGradeReports();
    } else if (path.includes('quizzes/create.html')) {
        initQuizForm();
    } else if (path.includes('quizzes/edit.html')) {
        const quizId = new URLSearchParams(window.location.search).get('id');
        loadQuizForEdit(quizId);
    } else if (path.includes('users/create.html')) {
        initUserForm();
    } else if (path.includes('users/view.html')) {
        const userId = new URLSearchParams(window.location.search).get('id');
        loadUserDetails(userId);
    }
});

function loadDashboardStats() {
    $('#totalQuizzes').text(db.quizzes.length);
    $('#totalUsers').text(db.users.filter(u => u.role === 'user').length);
    $('#totalAttempts').text(db.results.length);

    // Show recent attempts
    const recentAttempts = db.results.slice(0, 5).map(result => {
        const user = db.users.find(u => u.id === result.userId);
        const quiz = db.quizzes.find(q => q.id === result.quizId);
        return `
            <tr>
                <td>${user.username}</td>
                <td>${quiz.title}</td>
                <td>${result.score}%</td>
                <td>${new Date(result.completedAt).toLocaleDateString()}</td>
            </tr>
        `;
    }).join('');

    $('#recentAttempts').html(recentAttempts);
}

function loadQuizzes() {
    const quizzes = db.quizzes.map(quiz => {
        return `
            <tr>
                <td>${quiz.title}</td>
                <td>${quiz.questions.length}</td>
                <td>${new Date(quiz.createdAt).toLocaleDateString()}</td>
                <td>
                    <a href="edit.html?id=${quiz.id}" class="btn btn-sm btn-primary me-1">
                        <i class="fas fa-edit"></i>
                    </a>
                    <button class="btn btn-sm btn-danger delete-quiz" data-id="${quiz.id}">
                        <i class="fas fa-trash"></i>
                    </button>
                </td>
            </tr>
        `;
    }).join('');

    $('#quizzesTableBody').html(quizzes);

    $('.delete-quiz').click(function () {
        const quizId = $(this).data('id');
        $('#quizToDelete').val(quizId);
        $('#deleteQuizModal').modal('show');
    });

    // Search functionality
    $('#quizSearch').on('keyup', function () {
        const value = $(this).val().toLowerCase();
        $('#quizzesTableBody tr').filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
        });
    });
}

function deleteQuiz(quizId) {
    db.quizzes = db.quizzes.filter(q => q.id !== parseInt(quizId));
    showAlert('Quiz deleted successfully', 'success');
    loadQuizzes();
}

function loadUsers() {
    const users = db.users.filter(u => u.role === 'user').map(user => {
        const attempts = db.results.filter(r => r.userId === user.id).length;
        return `
            <tr>
                <td>${user.username}</td>
                <td>${user.email}</td>
                <td>${new Date(user.createdAt).toLocaleDateString()}</td>
                <td>${attempts}</td>
                <td>
                    <a href="view.html?id=${user.id}" class="btn btn-sm btn-info me-1">
                        <i class="fas fa-eye"></i>
                    </a>
                    <button class="btn btn-sm btn-danger delete-user" data-id="${user.id}">
                        <i class="fas fa-trash"></i>
                    </button>
                </td>
            </tr>
        `;
    }).join('');

    $('#usersTableBody').html(users);

    $('.delete-user').click(function () {
        const userId = $(this).data('id');
        $('#userToDelete').val(userId);
        $('#deleteUserModal').modal('show');
    });

    $('#userSearch').on('keyup', function () {
        const value = $(this).val().toLowerCase();
        $('#usersTableBody tr').filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
        });
    });
}

function deleteUser(userId) {
    db.users = db.users.filter(u => u.id !== parseInt(userId));
    showAlert('User deleted successfully', 'success');
    loadUsers();
}

function loadGradeReports() {
    const reports = db.results.map(result => {
        const user = db.users.find(u => u.id === result.userId);
        const quiz = db.quizzes.find(q => q.id === result.quizId);
        return `
            <tr>
                <td>${user.username}</td>
                <td>${quiz.title}</td>
                <td>${result.score}%</td>
                <td>${new Date(result.completedAt).toLocaleDateString()}</td>
                <td>
                    <button class="btn btn-sm btn-primary view-details" data-id="${result.id}">
                        <i class="fas fa-info-circle"></i> Details
                    </button>
                </td>
            </tr>
        `;
    }).join('');

    $('#reportsTableBody').html(reports);

    $('.view-details').click(function () {
        const resultId = $(this).data('id');
        viewResultDetails(resultId);
    });
}

function viewResultDetails(resultId) {
    const result = db.results.find(r => r.id === parseInt(resultId));
    const user = db.users.find(u => u.id === result.userId);
    const quiz = db.quizzes.find(q => q.id === result.quizId);

    let detailsHtml = `
        <h5>${user.username}'s Results for ${quiz.title}</h5>
        <p>Score: ${result.score}%</p>
        <p>Completed: ${new Date(result.completedAt).toLocaleString()}</p>
        <hr>
        <h6>Question Details:</h6>
        <ul class="list-group">
    `;

    result.answers.forEach(answer => {
        const question = quiz.questions.find(q => q.id === answer.questionId);
        const selectedOption = question.options.find(o => o.id === answer.answerId);
        const isCorrect = answer.correct ? 'text-success' : 'text-danger';
        const icon = answer.correct ? 'fa-check' : 'fa-times';

        detailsHtml += `
            <li class="list-group-item">
                <div class="d-flex justify-content-between">
                    <div>
                        <p class="mb-1"><strong>${question.text}</strong></p>
                        <p class="mb-1">Your answer: <span class="${isCorrect}">
                            <i class="fas ${icon} me-1"></i>${selectedOption.text}
                        </span></p>
                    </div>
                    ${answer.correct ? '' : `
                        <div>
                            <small class="text-muted">Correct answer: ${question.options.find(o => o.correct).text}</small>
                        </div>
                    `}
                </div>
            </li>
        `;
    });

    detailsHtml += '</ul>';

    $('#resultDetailsBody').html(detailsHtml);
    $('#resultDetailsModal').modal('show');
}

function initQuizForm() {
    // Add question button
    $('#addQuestionBtn').click(function () {
        const questionCount = $('.question-container').length + 1;
        const questionHtml = `
            <div class="question-container card mb-4">
                <div class="card-header d-flex justify-content-between align-items-center">
                    <h6 class="mb-0">Question ${questionCount}</h6>
                    <button type="button" class="btn btn-sm btn-danger remove-question">
                        <i class="fas fa-times"></i>
                    </button>
                </div>
                <div class="card-body">
                    <div class="mb-3">
                        <label class="form-label">Question Text</label>
                        <input type="text" class="form-control question-text" required>
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Question Type</label>
                        <select class="form-select question-type">
                            <option value="multiple-choice">Multiple Choice</option>
                            <option value="true-false">True/False</option>
                        </select>
                    </div>
                    <div class="options-container">
                        <div class="option-item mb-2">
                            <div class="input-group">
                                <div class="input-group-text">
                                    <input class="form-check-input correct-answer" type="radio" name="correct-answer-${questionCount}" required>
                                </div>
                                <input type="text" class="form-control option-text" placeholder="Option text" required>
                                <button class="btn btn-outline-danger remove-option" type="button">
                                    <i class="fas fa-times"></i>
                                </button>
                            </div>
                        </div>
                        <div class="option-item mb-2">
                            <div class="input-group">
                                <div class="input-group-text">
                                    <input class="form-check-input correct-answer" type="radio" name="correct-answer-${questionCount}" required>
                                </div>
                                <input type="text" class="form-control option-text" placeholder="Option text" required>
                                <button class="btn btn-outline-danger remove-option" type="button">
                                    <i class="fas fa-times"></i>
                                </button>
                            </div>
                        </div>
                    </div>
                    <button type="button" class="btn btn-sm btn-outline-primary add-option">
                        <i class="fas fa-plus me-1"></i>Add Option
                    </button>
                </div>
            </div>
        `;

        $('#questionsContainer').append(questionHtml);
        initQuestionEvents();
    });

    // Form submission
    $('#quizForm').submit(function (e) {
        e.preventDefault();

        const title = $('#quizTitle').val();
        const description = $('#quizDescription').val();
        const questions = [];

        $('.question-container').each(function () {
            const questionText = $(this).find('.question-text').val();
            const questionType = $(this).find('.question-type').val();
            const options = [];

            $(this).find('.option-item').each(function () {
                const optionText = $(this).find('.option-text').val();
                const isCorrect = $(this).find('.correct-answer').is(':checked');

                options.push({
                    id: options.length + 1,
                    text: optionText,
                    correct: isCorrect
                });
            });

            questions.push({
                id: questions.length + 1,
                text: questionText,
                type: questionType,
                options: options
            });
        });
        const difficulty = $('#quizDifficulty').val();

        // In a real app, this would be an API call
        const newQuiz = {
            id: db.quizzes.length + 1,
            title: title,
            description: description,
            difficulty: parseInt(difficulty),
            questions: questions,
            createdBy: JSON.parse(sessionStorage.getItem('currentUser')).id,
            createdAt: new Date()
        };

        db.quizzes.push(newQuiz);
        showAlert('Quiz created successfully', 'success');
        setTimeout(() => {
            window.location.href = 'list.html';
        }, 1500);
    });

    initQuestionEvents();
}

function initQuestionEvents() {
    // Remove question
    $('.remove-question').off('click').on('click', function () {
        $(this).closest('.question-container').remove();
        updateQuestionNumbers();
    });

    // Add option
    $('.add-option').off('click').on('click', function () {
        const optionsContainer = $(this).siblings('.options-container');
        const currentOptionsCount = optionsContainer.find('.option-item').length;

        if (currentOptionsCount >= 4) {
            alert("Maximum of 4 options allowed.");
            return;
        }

        const questionIndex = $('.question-container').index($(this).closest('.question-container')) + 1;

        const optionHtml = `
            <div class="option-item mb-2">
                <div class="input-group">
                    <div class="input-group-text">
                        <input class="form-check-input correct-answer" type="radio" name="correct-answer-${questionIndex}" required>
                    </div>
                    <input type="text" class="form-control option-text" placeholder="Option text" required>
                    <button class="btn btn-outline-danger remove-option" type="button">
                        <i class="fas fa-times"></i>
                    </button>
                </div>
            </div>
        `;
        optionsContainer.append(optionHtml);
        initQuestionEvents(); // Rebind events
    });

    // Remove option
    $('.remove-option').off('click').on('click', function () {
        $(this).closest('.option-item').remove();
    });
}

function updateQuestionNumbers() {
    $('.question-container').each(function (index) {
        $(this).find('h6').text(`Question ${index + 1}`);
    });
}

// Updates question numbers after removal
function updateQuestionNumbers() {
    $('.question-container').each(function (index) {
        $(this).find('h6').text(`Question ${index + 1}`);
    });
}


function updateQuestionNumbers() {
    $('.question-container').each(function (index) {
        $(this).find('.card-header h6').text(`Question ${index + 1}`);
        $(this).find('.correct-answer').attr('name', `correct-answer-${index + 1}`);
    });
}

function loadQuizForEdit(quizId) {
    const quiz = db.quizzes.find(q => q.id === parseInt(quizId));

    if (!quiz) {
        showAlert('Quiz not found', 'danger');
        setTimeout(() => {
            window.location.href = 'list.html';
        }, 1500);
        return;
    }

    $('#quizTitle').val(quiz.title);
    $('#quizDescription').val(quiz.description);

    // Add questions
    quiz.questions.forEach((question, qIndex) => {
        const questionHtml = `
            <div class="question-container card mb-4">
                <div class="card-header d-flex justify-content-between align-items-center">
                    <h6 class="mb-0">Question ${qIndex + 1}</h6>
                    <button type="button" class="btn btn-sm btn-danger remove-question">
                        <i class="fas fa-times"></i>
                    </button>
                </div>
                <div class="card-body">
                    <div class="mb-3">
                        <label class="form-label">Question Text</label>
                        <input type="text" class="form-control question-text" value="${question.text}" required>
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Question Type</label>
                        <select class="form-select question-type">
                            <option value="multiple-choice" ${question.type === 'multiple-choice' ? 'selected' : ''}>Multiple Choice</option>
                            <option value="true-false" ${question.type === 'true-false' ? 'selected' : ''}>True/False</option>
                        </select>
                    </div>
                    <div class="options-container">
                        ${question.options.map((option, oIndex) => `
                            <div class="option-item mb-2">
                                <div class="input-group">
                                    <div class="input-group-text">
                                        <input class="form-check-input correct-answer" type="radio" 
                                               name="correct-answer-${qIndex + 1}" 
                                               ${option.correct ? 'checked' : ''} required>
                                    </div>
                                    <input type="text" class="form-control option-text" 
                                           value="${option.text}" 
                                           ${question.type === 'true-false' ? 'readonly' : ''} required>
                                    ${question.type === 'multiple-choice' ? `
                                        <button class="btn btn-outline-danger remove-option" type="button">
                                            <i class="fas fa-times"></i>
                                        </button>
                                    ` : ''}
                                </div>
                            </div>
                        `).join('')}
                    </div>
                    ${question.type === 'multiple-choice' ? `
                        <button type="button" class="btn btn-sm btn-outline-primary add-option">
                            <i class="fas fa-plus me-1"></i>Add Option
                        </button>
                    ` : ''}
                </div>
            </div>
        `;

        $('#questionsContainer').append(questionHtml);
    });

    initQuestionEvents();

    // Form submission for edit
    $('#quizForm').submit(function (e) {
        e.preventDefault();

        const title = $('#quizTitle').val();
        const description = $('#quizDescription').val();
        const questions = [];

        $('.question-container').each(function () {
            const questionText = $(this).find('.question-text').val();
            const questionType = $(this).find('.question-type').val();
            const options = [];

            $(this).find('.option-item').each(function () {
                const optionText = $(this).find('.option-text').val();
                const isCorrect = $(this).find('.correct-answer').is(':checked');

                options.push({
                    id: options.length + 1,
                    text: optionText,
                    correct: isCorrect
                });
            });

            questions.push({
                id: questions.length + 1,
                text: questionText,
                type: questionType,
                options: options
            });
        });

        // Update quiz in database
        const quizIndex = db.quizzes.findIndex(q => q.id === parseInt(quizId));
        if (quizIndex !== -1) {
            db.quizzes[quizIndex] = {
                ...db.quizzes[quizIndex],
                title: title,
                description: description,
                questions: questions
            };

            showAlert('Quiz updated successfully', 'success');
            setTimeout(() => {
                window.location.href = 'list.html';
            }, 1500);
        }
    });
}

function initUserForm() {
    $('#userForm').submit(function (e) {
        e.preventDefault();

        const username = $('#username').val();
        const email = $('#email').val();
        const password = $('#password').val();
        const role = $('#role').val();

        // In a real app, this would be an API call
        const newUser = {
            id: db.users.length + 1,
            username: username,
            email: email,
            password: password,
            role: role,
            createdAt: new Date()
        };

        db.users.push(newUser);
        showAlert('User created successfully', 'success');
        setTimeout(() => {
            window.location.href = 'list.html';
        }, 1500);
    });
}

function loadUserDetails(userId) {
    const user = db.users.find(u => u.id === parseInt(userId));
    const attempts = db.results.filter(r => r.userId === parseInt(userId));

    if (!user) {
        showAlert('User not found', 'danger');
        setTimeout(() => {
            window.location.href = 'list.html';
        }, 1500);
        return;
    }

    $('#userUsername').text(user.username);
    $('#userEmail').text(user.email);
    $('#userRole').text(user.role.charAt(0).toUpperCase() + user.role.slice(1));
    $('#userCreated').text(new Date(user.createdAt).toLocaleString());

    // User attempts
    if (attempts.length > 0) {
        const attemptsHtml = attempts.map(attempt => {
            const quiz = db.quizzes.find(q => q.id === attempt.quizId);
            return `
                <tr>
                    <td>${quiz.title}</td>
                    <td>${attempt.score}%</td>
                    <td>${new Date(attempt.completedAt).toLocaleString()}</td>
                    <td>
                        <button class="btn btn-sm btn-primary view-attempt" data-id="${attempt.id}">
                            <i class="fas fa-eye"></i> View
                        </button>
                    </td>
                </tr>
            `;
        }).join('');

        $('#userAttemptsBody').html(attemptsHtml);

        $('.view-attempt').click(function () {
            const attemptId = $(this).data('id');
            viewResultDetails(attemptId);
        });
    } else {
        $('#userAttemptsBody').html('<tr><td colspan="4" class="text-center">No quiz attempts found</td></tr>');
    }
}